using System.Collections.Generic;
using System.Web.Mvc;
using CL.DataAccess.Repository;
using CL.Framework.Mapper;
using CL.Transverse.Datatables;
using CL.Transverse.Model.Post;
using CL.ViewModel.Category;

namespace CL.Web.Areas.Administrator.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GetCategories(DataTablesParam dataTableParam)
        {
            var entities = _categoryRepository.GetAll();
            var dataTableResults = DataTablesResult.Create(entities, dataTableParam);
            var dataTableData = dataTableResults.Data as DataTablesData;
            if (dataTableData == null) return dataTableResults;
            var entitesResults = dataTableData.aaData as List<P_Category>;
            dataTableData.aaData = entitesResults.MapTo<P_Category, CategoryVM>(x => x.Parent, y => y.MapFrom(x => x.Parent != null ? x.Parent.Title : ""));
            return dataTableResults;
        }

        [HttpPost]
        public ActionResult Active(int id)
        {
            
        }
    }
}