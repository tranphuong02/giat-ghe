using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CL.DataAccess.Repository;
using CL.Framework.Mapper;
using CL.Transverse;
using CL.Transverse.Datatables;
using CL.Transverse.Enums;
using CL.Transverse.Extensions;
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
            var result = _categoryRepository.Active(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = _categoryRepository.DeleteCategory(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new CategoryAddVM();
            InitialAddVM(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(CategoryAddVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData.SetStatusMessage(Constants.Message.Oops, StatusMessageType.Danger);
                InitialAddVM(viewModel);
                return View(viewModel);
            }

            var result = _categoryRepository.Add(viewModel);
            if (result.IsSuccess)
            {
                TempData.SetStatusMessage(result.Message);
                return RedirectToAction("Index");
            }

            TempData.SetStatusMessage(result.Message, StatusMessageType.Danger);
            InitialAddVM(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null || category.IsDelete)
            {
                return HttpNotFound();
            }
            var viewModel = category.MapTo<P_Category, CategoryEditVM>();
            InitialEditVM(viewModel, id);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryEditVM viewModel, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData.SetStatusMessage(Constants.Message.Oops, StatusMessageType.Danger);
                InitialEditVM(viewModel, id);
                return View(viewModel);
            }

            var category = _categoryRepository.GetById(id);
            if (category == null || category.IsDelete)
            {
                return HttpNotFound();
            }

            var result = _categoryRepository.Edit(category, viewModel);
            if (result.IsSuccess)
            {
                TempData.SetStatusMessage(result.Message);
                return RedirectToAction("Index");
            }

            TempData.SetStatusMessage(result.Message, StatusMessageType.Danger);
            InitialEditVM(viewModel, id);
            return View(viewModel);
        }

        #region Helper Methods

        private void InitialAddVM(CategoryAddVM viewModel)
        {
            viewModel.Categories = _categoryRepository.Get(x => !x.IsDelete && x.IsPublish).OrderBy(x => x.Title).ToList();
        }

        private void InitialEditVM(CategoryEditVM viewModel, int id)
        {
            viewModel.Categories = _categoryRepository.Get(x => !x.IsDelete && x.IsPublish && x.Id != id).OrderBy(x => x.Title).ToList();
        }

        #endregion
    }
}