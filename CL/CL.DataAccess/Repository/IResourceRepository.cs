using System.Collections.Generic;
using CL.Framework.Autofac.Interfaces;
using CL.Framework.DataAccess.Interfaces;
using CL.Transverse.Model.Post;
using CL.Transverse.ViewVM;

namespace CL.DataAccess.Repository
{
    public interface IResourceRepository : IBaseRepository<P_Resource>, IDependency
    {
        List<P_Resource> GetList();

        BaseVM DeleteResource(int id);

        BaseVM Add(string fileName, string pathToSaveInDatabase);
    }
}
