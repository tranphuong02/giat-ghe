using CL.DataAccess.Repository;
using CL.Framework.DataAccess.Interfaces;
using CL.Framework.DataAccess.Repository;
using CL.Transverse.Model.Post;

namespace CL.DataAccess.BusinessLogic
{
    public class CategoryRepository :  BaseRepository<P_Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
