using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FarmerApp.Models;

namespace FarmerApp.Controllers
{
    [HandleError]
    public class BidderController : Controller
    {
        // GET: Bidder
        FarmersEntities db = new FarmersEntities();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///  Bidder Details displays the details of bidder
        /// </summary>
        /// <return

        #region Bidder Details
        public ActionResult BidDetails()
        {
            int bid = (int)Session["bid"];
            var x = (from i in db.BidderRegs where i.Bidder_ID == bid select i).ToList();

            return View(x); 
            //return View (x);
        }
        #endregion

        /// <summary>
        /// Bidder can edit his profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Bidder Edit
        [HttpGet]
      

        public ActionResult BidEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Regions = db.BidderRegs.ToList();
            BidderReg t = db.BidderRegs.Find(id);
            return View(t);
        }

        [HttpPost]

        public ActionResult BidEdit(BidderReg t)
        {
            try
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception Msg)
            {
                Response.Write("Oopss!!Something went wrong!!");
            }
            return RedirectToAction("BidWelcome");
        }
        #endregion


        /// <summary>
        /// Bidder Welcome page allows bidder to bid the crops
        /// </summary>
        /// <returns></returns>
        #region Bidder Welcome
        [HttpGet]
        public ActionResult BidWelcome()
        {
            return View(db.SellRequests.ToList());
        }

        #endregion


        /// <summary>
        /// Bid page where it allows bidder to enter his bid amount.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       #region Entering bid amount
        [HttpGet]

        public ActionResult Bid(int id)
        {

                int bid = (int)Session["bid"];
                Bidding t = new Bidding();
                t.Crop_ID = id;
                var result = (from s in db.Biddings
                              where s.Bidder_ID == bid && s.Crop_ID == t.Crop_ID
                              select s).Count();

                if (result == 0)
                {
                    return View(t);

                }
                else
                {
                TempData["i"] = "You can't bid for the same crop again.";
                    return RedirectToAction("Alertmessage");
                }
            
 
        }
      
        [HttpPost]
        public ActionResult Bid(Bidding t)
        {
            int bid = (int)Session["bid"];
            var x = (from i in db.SellRequests where i.Crop_ID == t.Crop_ID select i).SingleOrDefault();
            t.Baseprice = (double)x.Baseprice;
            t.Crop_type = x.Crop_type;
            t.Bidder_ID = bid;
            db.Biddings.Add(t);
            db.SaveChanges();
            return RedirectToAction("BidWelcome");
        }
        public ActionResult Alertmessage()
        {
            return View();
        }
        #endregion


    }
}