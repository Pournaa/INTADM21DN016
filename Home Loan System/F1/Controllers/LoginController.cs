using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F1.Models;
using System.Web.Security;

namespace F1.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Verify(User_login model)

        {
            D1Entities cbe = new D1Entities();
            var s = cbe.LoginCheck(model.User_Name, model.Password);

            var item = s.FirstOrDefault();
            if (item == 1)
            {
                //return View("AdminView");
                return RedirectToAction("Index", "Admin");
            }
            else if (item == 2)
            {
                return RedirectToAction("Index","Home");
                //return View("UserView");
            }
            else
            {
                model.LoginErrorMsg = "Invalid Username or Password";
                return View("Login", model);
            }
        }
     }
}