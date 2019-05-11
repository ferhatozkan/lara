using Lara.Service;
using Lara.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lara.Controllers
{
    public class FormController : Controller
    {
       
        public FormService formService;
        public FormController()
        {
            formService = new FormService();
        }

        // GET: Form
        public ActionResult Index(int? formId)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            return View();
        }

        [HttpGet]
        public ActionResult GetFormList()
        {
            var formList = formService.GetFormList();
            return Json(formList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateForm(FormModel formModel)
        {
            formModel.CreatedBy = Convert.ToInt32(Session["UserId"]);
            bool isSuccess = formService.CreateForm(formModel);
            return Json(isSuccess);
        }

        public ActionResult Forms(int formId)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            FormModel formModel = formService.GetForm(formId);


            return View(formModel);
        }
    }
}