using System;
using CL.DataAccess.Repository;
using CL.Framework.DataAccess.Interfaces;
using CL.Framework.DataAccess.Repository;
using CL.Transverse.Enums;
using CL.Transverse.ErrorHandling;
using CL.Transverse.Model.Post;
using CL.Transverse.ViewVM;
using CL.Transverse.Resources;

namespace CL.DataAccess.BusinessLogic
{
    public class CategoryRepository :  BaseRepository<P_Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public BaseVM Active(int id)
        {
            try
            {
                var category = GetById(id);
                var isPublish = category.IsPublish;
                category.IsPublish = !isPublish;
                Update(category);
                SaveChanges();
                return new BaseVM
                {
                    Code = (int)ResponseCode.Success,
                    Message = string.Format("{0} {1}", Message.ActiveSuccessFormat, "")
                };
            }
            catch (Exception ex)
            {
                return new BaseVM
                {
                    Code = (int)ResponseCode.InternalError,
                    Data = new CLBusinessException(ex.Message, CLErrorType.Error),
                    Message = Message.ErrorOccur
                };
            }
        }
    }
}
