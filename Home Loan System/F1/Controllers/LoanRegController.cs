using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F1.Models;

namespace F1.Controllers
{
    public class LoanRegController : Controller
    {
        D1Entities db = new D1Entities();
        // GET: LoanReg
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(User_login us)
        {

            db.User_login.Add(us);
            db.SaveChanges();
            return View();
        }
    }
}
