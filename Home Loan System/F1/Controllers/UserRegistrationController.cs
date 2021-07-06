using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F1.Models;

namespace F1.Controllers
{
    public class UserRegistrationController : Controller
    {
        D1Entities db = new D1Entities();
        // GET: User
       
        public ActionResult Registration()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Registration(MVCregUser r)
        {
            db.MVCregUsers.Add(r);
            //db.Regs.Add(r);
            
            db.SaveChanges();
           
            return View();
        }

    }
}
