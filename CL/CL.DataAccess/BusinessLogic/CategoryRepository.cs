using System;
using CL.DataAccess.Repository;
using CL.Framework.DataAccess.Interfaces;
using CL.Framework.DataAccess.Repository;
using CL.Framework.Logger;
using CL.Framework.Mapper;
using CL.Transverse;
using CL.Transverse.Enums;
using CL.Transverse.ErrorHandling;
using CL.Transverse.Model.Post;
using CL.Transverse.ViewVM;
using CL.ViewModel.Category;

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
                var isSaved = SaveChanges() > 0;
                return new BaseVM
                {
                    IsSuccess = isSaved,
                    Message = !isPublish
                        ? string.Format(isSaved ? Constants.Message.ActiveSuccessFormat : Constants.Message.ActiveFailFormat, Constants.Page.Category.ToLower(), category.Title)
                        : string.Format(isSaved ? Constants.Message.DeActiveSuccessFormat : Constants.Message.DeActiveFailFormat, Constants.Page.Category.ToLower(), category.Title)
                };
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
                return new BaseVM
                {
                    IsSuccess = false,
                    Data = new CLBusinessException(ex.Message, CLErrorType.Error),
                    Message = Constants.Message.ErrorOccur
                };
            }
        }

        public BaseVM DeleteCategory(int id)
        {
            try
            {
                var category = Delete(id);

                return new BaseVM
                {
                    IsSuccess = true,
                    Message = string.Format(Constants.Message.DeleteSuccessFormat, Constants.Page.Category.ToLower(), category.Title) 
                };
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
                return new BaseVM
                {
                    IsSuccess = false,
                    Data = new CLBusinessException(ex.Message, CLErrorType.Error),
                    Message = Constants.Message.ErrorOccur
                };
            }
        }

        public BaseVM Add(CategoryAddVM viewModel)
        {
            try
            {
                var category = viewModel.MapTo<CategoryAddVM, P_Category>();
                category.IsPublish = true;
                category.CreatedDate = category.UpdatedDate = DateTime.Now;
                Insert(category);
                var isSaved = SaveChanges() > 0;

                return new BaseVM
                {
                    IsSuccess = isSaved,
                    Message = string.Format(isSaved ? Constants.Message.AddSuccessFormat : Constants.Message.AddFailFormat, Constants.Page.Category.ToLower(), category.Title)
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

        public BaseVM Edit(P_Category category, CategoryEditVM viewModel)
        {
            try
            {
                category.Title = viewModel.Title;
                category.Url = viewModel.Url;
                category.Keyword = viewModel.Keyword;
                category.Description = viewModel.Description;
                category.ParentId = viewModel.ParentId;
                category.UpdatedDate = DateTime.Now;

                Update(category);

                var isSaved = SaveChanges() > 0;
                return new BaseVM
                {
                    IsSuccess = isSaved,
                    Message = string.Format(isSaved ? Constants.Message.EditSuccessFormat : Constants.Message.EditFailFormat, Constants.Page.Category.ToLower(), category.Title)
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
