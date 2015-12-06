using CL.DataAccess.Repository;
using CL.Framework.DataAccess.Interfaces;
using CL.Framework.DataAccess.Repository;
using CL.Transverse.Model.System;

namespace CL.DataAccess.BusinessLogic
{
    public class UserRepository :  BaseRepository<S_User>, IUserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
