using CL.Framework.Autofac.Interfaces;
using CL.Framework.DataAccess.Interfaces;
using CL.Transverse.Model.Post;
using CL.Transverse.ViewVM;

namespace CL.DataAccess.Repository
{
    public interface ICategoryRepository : IBaseRepository<P_Category>, IDependency
    {
        BaseVM Active(int id);
    }
}
