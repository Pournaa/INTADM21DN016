using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlineexam.Models;

namespace onlineexam.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authorize(Student ld)
        {
            using (OESEntities1 db = new OESEntities1())
            {
                if (ld.UserName == "admin" && ld.User_Password == "adm123")
                {
                    return RedirectToAction("Admin", "AdminDashboard");
                }
                else
                {
                    var userdetail = db.Students.Where(x => x.UserName == ld.UserName && x.User_Password == ld.User_Password).FirstOrDefault();
                    if (userdetail == null)
                    {
                        ld.LoginErrorMsg = "invalid username or password";
                        return View("Index", ld);
                    }
                    else
                    {
                        Session["Id"] = ld.UserName;
                        return RedirectToAction("userdashboard", "userdashboard");
                    }
                }
               

            }

        }

    }
}

