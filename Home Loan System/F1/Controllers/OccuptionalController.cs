using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F1.Models;

namespace F1.Controllers
{
    public class OccuptionalController : Controller
    {
        D1Entities db = new D1Entities();
        // GET: occupational
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(occupation_table o)
        {
            db.occupation_table.Add(o);
            //db.Regs.Add(r);
            db.SaveChanges();
            return View();

        }
          }
}
