using Lara.Service;
using Lara.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lara.Controllers
{
    public class UserController : Controller
    {
        public UserService userService;
        public UserController()
        {
            userService = new UserService();
        }

        // GET: User
        public ActionResult Index()
        {      
            return View();
        }
            
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            bool isLogged = userService.Login(loginModel);

            Session["UserName"] = loginModel.UserName.ToString();
            return Json(isLogged);
        }
    }
}