using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
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
            var dataTableResult = DataTablesResult.Create(entities, dataTableParam);
            var dataTableData = dataTableResult.Data as DataTablesData;
            if (dataTableData != null)
            {
                var entitesResults = dataTableData.aaData as List<P_Category>;
                var cc = entitesResults.MapTo<P_Category, CategoryVM>();
                var b = cc;
            }


            return DataTablesResult.Create<P_Category, CategoryVM>(entities, dataTableParam);
        }
    }
}