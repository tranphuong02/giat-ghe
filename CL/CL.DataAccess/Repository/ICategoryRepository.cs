using CL.Framework.Autofac.Interfaces;
using CL.Framework.DataAccess.Interfaces;
using CL.Transverse.Model.Post;
using CL.Transverse.ViewVM;
using CL.ViewModel.Category;

namespace CL.DataAccess.Repository
{
    public interface ICategoryRepository : IBaseRepository<P_Category>, IDependency
    {
        BaseVM Active(int id);

        BaseVM DeleteCategory(int id);
        BaseVM Add(CategoryAddVM viewModel);
        BaseVM Edit(P_Category category, CategoryEditVM viewModel);
    }
}
