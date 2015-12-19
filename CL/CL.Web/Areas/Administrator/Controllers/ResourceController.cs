using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CL.DataAccess.Repository;
using CL.Framework.Logger;
using CL.Transverse;
using CL.Transverse.Model.Post;
using CL.Transverse.ViewVM;

namespace CL.Web.Areas.Administrator.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceController(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetList()
        {
            var resource = _resourceRepository.GetList();
            return Json(new { resource }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            var result = _resourceRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add()
        {
            try
            {
                var fileName = "";
                foreach (string item in Request.Files)
                {
                    var file = Request.Files[item];

                    //Save file content goes here
                    if (file == null || file.ContentLength <= 0) continue;

                    var fileNameSaved = file.FileName;
                    fileName = file.FileName;
                    var originalDirectory = new DirectoryInfo(Constants.ResourcePath.Resource);
                    var isExists = Directory.Exists(originalDirectory.FullName);

                    // if folder not exist, create it
                    if (!isExists)
                    {
                        Directory.CreateDirectory(originalDirectory.FullName);
                    }

                    fileNameSaved = string.Format("{0}-{1}", DateTime.Now.ToString(Constants.DateFormat.FullDateTime), fileNameSaved);
                    var pathToSaveInDatabase = Path.Combine(ConfigurationManager.AppSettings["ResourceDirectory"], fileNameSaved);
                    var path = Path.Combine(originalDirectory.FullName, fileNameSaved);
                    var baseVM = _resourceRepository.Add(file.FileName, pathToSaveInDatabase);

                    if (baseVM.IsSuccess)
                    {
                        file.SaveAs(path);
                    }
                }
                return Json(
                    new
                    {
                        IsSuccess = true,
                        Message = string.Format(Constants.Message.AddSuccessFormat, Constants.Page.Resource.ToLower(), fileName),
                    }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
                return Json(
                    new BaseVM
                    {
                        IsSuccess = false,
                        Message = ex.Message
                    }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}