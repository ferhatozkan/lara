using Lara.Service;
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
        public ActionResult Index()
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
            return Json(formList);
        }

    }
}