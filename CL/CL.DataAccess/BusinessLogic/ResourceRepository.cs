using System;
using System.Collections.Generic;
using System.Linq;
using CL.DataAccess.Repository;
using CL.Framework.DataAccess.Interfaces;
using CL.Framework.DataAccess.Repository;
using CL.Framework.Logger;
using CL.Transverse;
using CL.Transverse.Helpers;
using CL.Transverse.Model.Post;
using CL.Transverse.ViewVM;

namespace CL.DataAccess.BusinessLogic
{
    public class ResourceRepository : BaseRepository<P_Resource>, IResourceRepository
    {
        public ResourceRepository(IDbContext dbContext) 
            : base(dbContext)
        {
        }

        public List<P_Resource> GetList()
        {
            try
            {
                return Get(x => !x.IsDelete, x => x.OrderBy(o => o.CreatedDate)).ToList();
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
                return new List<P_Resource>();
            }
        }

        public BaseVM DeleteResource(int id)
        {
            try
            {
                var resource = Delete(id);

                return new BaseVM
                {
                    IsSuccess = true,
                    Message = string.Format(Constants.Message.DeleteSuccessFormat, Constants.Page.Resource.ToLower(), resource.Name)
                };
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
                return new BaseVM
                {
                    IsSuccess = false,
                    Data = ex.Message,
                    Message = Constants.Message.ErrorOccur
                };
            }
        }

        public BaseVM Add(string fileName, string pathToSaveInDatabase)
        {
            try
            {
                var resource = new P_Resource
                {
                    Name = fileName,
                    Alt = fileName,
                    Url = pathToSaveInDatabase,
                    Type = BackendHelpers.GetResourceTypeFromFileName(fileName),
                    IsPublish = true,
                    IsDelete = false,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                Insert(resource);

                var isSaved = SaveChanges() > 0;

                return new BaseVM
                {
                    IsSuccess = isSaved,
                    Message = string.Format(isSaved ? Constants.Message.AddSuccessFormat : Constants.Message.AddFailFormat, Constants.Page.Resource.ToLower(), resource.Name)
                };
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);

                return new BaseVM
                {
                    IsSuccess = false,
                    Data = ex.Message,
                    Message = Constants.Message.ErrorOccur
                };
            }
        }
    }
}
