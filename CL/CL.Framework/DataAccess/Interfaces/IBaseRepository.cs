using CL.Framework.Autofac.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CL.Framework.DataAccess.Interfaces
{
    public interface IBaseRepository<TEntity> : IDependency where TEntity : class, IBaseEntity, new()
    {
        #region Get

        /// <summary>
        /// Get IQueryable list of all entities from DbSet
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        ///  Get an entity by ID
        /// </summary>
        /// <param name="id">ID of entity</param>
        TEntity GetById(int id);

        /// <summary>
        ///  Get an entity by ID
        /// </summary>
        /// <param name="id">ID of entity</param>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Get all entities from a DbSet
        /// </summary>
        IQueryable<TEntity> GetAll(bool inCludeUnPublished = true, bool includeDeleted = false, params string[] includes);

        /// <summary>
        /// Get ordered IEnumerable list of entities from a DbSet by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="inCludePublished"></param>
        /// <param name="includeDeleted"></param>
        /// <param name="includeProperties"></param>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool inCludePublished = true, bool includeDeleted = false,
            params string[] includeProperties);

        #endregion Get

        #region Insert

        /// <summary>
        /// Insert a new entity to DbSet
        /// </summary>
        /// <param name="entity">entity to insert</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Insert a list entity to DbSet
        /// </summary>
        /// <param name="listEntity">list entity to insert</param>
        void InsertRange(IEnumerable<TEntity> listEntity);

        #endregion Insert

        #region Update

        /// <summary>
        /// Update an existing entity in DbSet
        /// </summary>
        /// <param name="entity">entity to update</param>
        void Update(TEntity entity);

        #endregion Update

        #region Delete

        /// <summary>
        /// Delete an entity in DbSet by set Deleted to true or remove from DbSet
        /// </summary>
        /// <param name="entity">entity to delete</param>
        /// <param name="isRemoveFromDb"></param>
        void Delete(TEntity entity, bool isRemoveFromDb = false);

        /// <summary>
        /// Delete an entity in DbSet by set Deleted to true or remove from DbSet
        /// </summary>
        /// <param name="id">id of entity to delete</param>
        /// <param name="isRemoveFromDb"></param>
        TEntity Delete(int id, bool isRemoveFromDb = false);

        /// <summary>
        /// Delete an entity in DbSet by set Deleted to true or remove from DbSet
        /// </summary>
        /// <param name="id">id of entity to delete</param>
        /// <param name="isRemoveFromDb"></param>
        Task<TEntity> DeleteAsync(int id, bool isRemoveFromDb = false);

        /// <summary>
        /// Batch Delete based on condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="isRemoveFromDb"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool isRemoveFromDb = false);

        /// <summary>
        /// Delete an list of entities in DbSet by set Delete to true or remove from DbSet
        /// </summary>
        /// <param name="entities">list of entities to delete</param>
        /// <param name="isRemoveFromDb"></param>
        void DeleteRange(IEnumerable<TEntity> entities, bool isRemoveFromDb = false);

        #endregion Delete

        #region Save

        /// <summary>
        /// Save all changes made in this DbContext to the underlying database
        /// </summary>
        int SaveChanges();

        Task<int> SaveChangesAsync();

        #endregion

        #region Count

        int Count(Expression<Func<TEntity, bool>> filter = null);

        #endregion

        #region Utils

        /// <summary>
        /// Get IEnumerable list of entities in DbSet by raw sql query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        DbSqlQuery<TEntity> GetWithRawSql(string query, object[] parameters);

        /// <summary>
        /// Execute the given DDL/DML command against the database
        /// </summary>
        /// <param name="query"></param>
        void ExecuteSQLCommand(string query);

        /// <summary>
        /// Reload the entity from database
        /// </summary>
        /// <param name="entityToReload"></param>
        void ResetDeleteState(TEntity entityToReload);

        #endregion
    }
}
