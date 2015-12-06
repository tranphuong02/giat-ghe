using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CL.Framework.DataAccess.Interfaces;
using EntityFramework.Extensions;

namespace CL.Framework.DataAccess.Repository
{
    /// <summary>
    /// Repository contain all of base method to work with db, very easy to inheritance, very easy to upgrade or change method
    /// one time for all project
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        #region Fields

        // ReSharper disable once StaticFieldInGenericType
        private static readonly bool _timeTracked;

        protected readonly IDbContext DbContext;
        protected IDbSet<TEntity> _entity;

        #endregion Fields

        #region Ctors

        static BaseRepository()
        {
            _timeTracked = typeof(ICreateUpdateTrackedEntity).IsAssignableFrom(typeof(TEntity));
        }

        public BaseRepository(IDbContext dbContext)
        {
            DbContext = dbContext;
        }

        #endregion Ctors

        #region Properties

        private IDbSet<TEntity> Entity
        {
            get
            {
                if (_entity != null)
                {
                    return _entity;
                }
                _entity = DbContext.Set<TEntity>();
                return _entity;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Get IQueryable list of all entities from DbSet
        /// </summary>
        public IQueryable<TEntity> Table
        {
            get { return Entity; }
        }

        /// <summary>
        ///  Get an entity by ID
        /// </summary>
        /// <param name="id">ID of entity</param>
        public virtual TEntity GetById(int id)
        {
            return Table.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        ///  Get an entity by ID
        /// </summary>
        /// <param name="id">ID of entity</param>
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await Table.FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Get all entities from a DbSet
        /// </summary>
        public virtual IQueryable<TEntity> GetAll(bool inCludePublished = true, bool includeDeleted = false, params string[] includes)
        {
            return Get(null, null, inCludePublished,includeDeleted,  includes);
        }

        /// <summary>
        /// Get ordered IEnumerable list of entities from a DbSet by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="inCludeUnPublished"></param>
        /// <param name="includeDeleted"></param>
        /// <param name="includeProperties"></param>
        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool inCludeUnPublished = true, bool includeDeleted = false,
            params string[] includeProperties)
        {
            var query = Table;

            query = inCludeUnPublished ? query.Where(x => x.IsPublish || !x.IsPublish) : query.Where(x => x.IsPublish);
            query = includeDeleted ? query.Where(x => x.IsDelete || !x.IsDelete) : query.Where(x => !x.IsDelete);

            if (!includeDeleted)
            {
                query = query.Where(x => x.IsDelete == false );
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query) : query;
        }

        /// <summary>
        /// Insert a new entity to DbSet
        /// </summary>
        /// <param name="entity">entity to insert</param>
        public virtual void Insert(TEntity entity)
        {
            if (_timeTracked)
            {
                SetDefaultFieldsWhenCreate(entity as ICreateUpdateTrackedEntity);
            }
            entity.UpdatedDate = DateTime.Now;
            Entity.Add(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> listEntity)
        {

            foreach (var entity in listEntity)
            {
                if (_timeTracked)
                {
                    SetDefaultFieldsWhenCreate(entity as ICreateUpdateTrackedEntity);
                }

                entity.UpdatedDate = DateTime.Now;
                Entity.Add(entity);
            }
        }

        /// <summary>
        /// Update an existing entity in DbSet
        /// </summary>
        /// <param name="entity">entity to update</param>
        public virtual void Update(TEntity entity)
        {
            if (_timeTracked)
            {
                SetDefaultFieldsWhenUpdate(entity as ICreateUpdateTrackedEntity);
            }
            entity.UpdatedDate = DateTime.Now;
            Entity.Attach(entity);
            DbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        /// <summary>
        /// Delete an entity in DbSet by set Deleted to true or remove from DbSet
        /// </summary>
        /// <param name="entity">entity to delete</param>
        /// <param name="isRemoveFromDb"></param>
        public virtual void Delete(TEntity entity, bool isRemoveFromDb = false)
        {
            if (isRemoveFromDb)
            {
                try
                {
                    Entity.Remove(entity);
                }
                catch (Exception exception)
                {
                    ResetDeleteState(entity);
                    throw exception.InnerException ?? exception;
                }
            }
            else
            {
                entity.IsDelete = true;
                Update(entity);
            }
        }

        /// <summary>
        /// Delete an entity in DbSet by set Deleted to true or remove from DbSet
        /// </summary>
        /// <param name="id">id of entity to delete</param>
        /// <param name="isRemoveFromDb"></param>
        public virtual TEntity Delete(int id, bool isRemoveFromDb = false)
        {
            var entity = GetById(id);
            Delete(entity, isRemoveFromDb);
            SaveChanges();
            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(int id, bool isRemoveFromDb = false)
        {
            var entity = await GetByIdAsync(id);
            Delete(entity, isRemoveFromDb);
            await SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool isRemoveFromDb = false)
        {
            if (isRemoveFromDb)
            {
                return await Table.Where(predicate).DeleteAsync();
            }
            else
            {
                return await Table.Where(predicate).UpdateAsync(e => new TEntity() { IsDelete = true });
            }
        }

        /// <summary>
        /// Delete an list of entities in DbSet by set Delete to true or remove from DbSet
        /// </summary>
        /// <param name="entities">list of entities to delete</param>
        /// <param name="isRemoveFromDb"></param>
        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool isRemoveFromDb = false)
        {
            if (isRemoveFromDb)
            {
                var enumerable = entities as TEntity[] ?? entities.ToArray();
                try
                {
                    ((DbSet<TEntity>)Entity).RemoveRange(enumerable);
                }
                catch (Exception exception)
                {
                    foreach (var entity in enumerable)
                    {
                        ResetDeleteState(entity);
                    }
                    throw exception.InnerException ?? exception;
                }
            }
            else
            {
                foreach (var entity in entities)
                {
                    Delete(entity);
                }
            }
        }

        /// <summary>
        /// Save all changes made in this DbContext to the underlying database
        /// </summary>
        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// Save all changes made in this DbContext to the underlying database
        /// </summary>
        public virtual async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Count the number of records in table
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? Table.Count() : Table.Count(filter);
        }

        /// <summary>
        /// Get IEnumerable list of entities in DbSet by raw sql query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        public virtual DbSqlQuery<TEntity> GetWithRawSql(string query, object[] parameters)
        {
            return ((DbSet<TEntity>)Entity).SqlQuery(query, parameters);
        }

        /// <summary>
        /// Execute the given DDL/DML command against the database
        /// </summary>
        /// <param name="query"></param>
        public virtual void ExecuteSQLCommand(string query)
        {
            DbContext.ExecuteSqlCommand(query);
        }

        /// <summary>
        /// Reload the entity from database
        /// </summary>
        /// <param name="entityToReload"></param>
        public virtual void ResetDeleteState(TEntity entityToReload)
        {
            DbContext.Entry(entityToReload).Reload();
        }

        #endregion Methods

        #region Helpers

        protected virtual void SetDefaultFieldsWhenCreate(ICreateUpdateTrackedEntity entity)
        {
            var nowUtc = DateTime.UtcNow;
            entity.CreatedOnUtc = nowUtc;
            entity.UpdatedOnUtc = nowUtc;
        }

        protected virtual void SetDefaultFieldsWhenUpdate(ICreateUpdateTrackedEntity entity)
        {
            entity.UpdatedOnUtc = DateTime.UtcNow;
        }

        protected void SetProperty(TEntity entityToSet, string propertyName, object value)
        {
            var targetProperty = entityToSet.GetType().GetProperty(propertyName);
            if (targetProperty != null)
            {
                targetProperty.SetValue(entityToSet, value, null);
            }
        }

        protected object GetProperty(TEntity entityToSet, string propertyName)
        {
            var targetProperty = entityToSet.GetType().GetProperty(propertyName);
            return targetProperty != null ? targetProperty.GetValue(entityToSet) : null;
        }

        #endregion Helpers
    }
}
