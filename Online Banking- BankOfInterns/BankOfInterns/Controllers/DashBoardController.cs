using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankOfIntern.Models;

namespace BankOfIntern.Controllers
{
   
    public class DashBoardController : Controller
    {
        
        // GET: DashBoard
        BOIFinalDBEntities1 db = new BOIFinalDBEntities1();

        public ActionResult Menu()
        {
            return View();
        }
        public ActionResult BranchDetails(decimal id)
        {
            var s = db.GetBranchDetails(id).SingleOrDefault();
            return View(s);
        }
        public ActionResult AcSummary(decimal id)
        {
            var s = db.GetAcSummary(id).ToList();
            return View(s);
        }

        public ActionResult AcStatement()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AcStatement(decimal id,DateTime? from,DateTime? to)
        {
            var s = db.GetAcStatement(id, from, to).ToList();
            return View(s);
        }


    }
}