using System.Linq;

namespace CL.Framework.DataAccess.Interfaces
{
    public interface ISingleUpdateHierarchyBaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IHierarchyEntity, new()
    {
        /// <summary>
        /// Get possible parents
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetPossibleParents(TEntity entity);

        /// <summary>
        /// Get all child items of parent
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetHierarcies(TEntity entity);
    }
}
