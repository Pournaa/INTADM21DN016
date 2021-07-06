using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F1.Models;

namespace F1.Controllers
{
    public class TrackerController : Controller
    {
        // GET: Loan

        D1Entities db = new D1Entities();
        // GET: tracker33
        [HttpGet]
        public ActionResult LoanTracker()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(MVCregUser model)
        {

            D1Entities db = new D1Entities();

            var loandetail = db.MVCregUsers.Where(x => x.F_name == model.F_name && x.MobileNo == model.MobileNo).FirstOrDefault();
           
           
                var s = db.loantrackeredit(model.ID,model.MobileNo).FirstOrDefault();

            if (loandetail != null)
            {
                var z = s.Age;
                var y = s.Gross_income / 12;
                var t = s.Tensure;
                var l = s.Loan_amount;
                var a = l / (t * 12);


          


                if (z >= 21 && z <= 70)
                {
                    if ((y >= 25000 && y <= 75000) && (l < 9000000))
                    {
                        return RedirectToAction("Congratulation", "Home");
                    }
                    else if ((y >= 75000 && y <= 100000) && (l < 13500000))
                    {
                        return RedirectToAction("Congratulation", "Home");
                    }
                    else if ((y >= 100000 && y <= 500000) && (l < 13500000))
                    {
                        return RedirectToAction("Congratulation", "Home");
                    }
                    else if (y > 500000)
                    {
                        return RedirectToAction("Congratulation", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Rejection", "Home");
                    }
                }

                else
                {
                    return RedirectToAction("Rejection", "Home");
                }

            }
            else
            {
                return RedirectToAction("Registration","UserRegistration");
            }


        }
    }
}