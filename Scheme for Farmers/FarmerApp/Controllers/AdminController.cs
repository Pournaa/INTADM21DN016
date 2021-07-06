using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FarmerApp.Models;

namespace FarmerApp.Controllers
{[HandleError]
    public class AdminController : Controller
    {
        FarmersEntities db = new FarmersEntities();
        // GET: Admin
        #region Admin Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// It shows all the Farmer's Details to admin that are registered in the application, admin can Remove the
        /// farmer account from this page.
        /// </summary>
        /// <param name="Sorting_Order"></param>
        /// <returns></returns>
        /// 
        #region FarDetails
        public ActionResult FarDetails(string Sorting_Order)
        {
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";

            var far = from f in db.FarmerRegs select f;
            switch (Sorting_Order)
            {
                case "Name_Description":
                    far = far.OrderByDescending(f => f.Name);
                    break;

                default:
                    far = far.OrderBy(f => f.Name);
                    break;
            }
            return View(far.ToList());
        }
        #endregion

        /// <summary>
        /// Used to remove the Farmer account from the application.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Farmer Delete operation
        public ActionResult FarDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            FarmerReg t = db.FarmerRegs.Find(id);
            return View(t);
        }

        [HttpPost]
        [ActionName("FarDelete")]
      
        public ActionResult FarDeleteConfirm(int? id)
        {

            try
            {
                FarmerReg t = db.FarmerRegs.Find(id);
                db.FarmerRegs.Remove(t);
                db.SaveChanges();
            }
            catch (Exception Msg)
            {
                Response.Write("Error");
            }
            return RedirectToAction("FarDetails");
        }
       #endregion

        /// <summary>
        /// It shows all the Bidder's Details to admin that are registered in the application, admin can Remove the
        /// bidder account from this page.
        /// </summary>
        /// <param name="Bidder details"></param>
        /// <returns></returns>
        #region Bidder Details
        public ActionResult BidDetails(string Sorting_Order)
            {
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";

            var bid = from b in db.BidderRegs select b;
            switch (Sorting_Order)
            {
                case "Name_Description":
                    bid = bid.OrderByDescending(b => b.Name);
                    break;

                default:
                    bid = bid.OrderBy(b => b.Name);
                    break;
            }
            return View(bid.ToList());
        }
        #endregion

        /// <summary>
        /// Used to remove the Bidder account from the application.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         #region Bidder Delete
        public ActionResult BidDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BidderReg t = db.BidderRegs.Find(id);
            return View(t);
        }
        
        [HttpPost]
        [ActionName("BidDelete")]
        public ActionResult BidDeleteConfirm(int id)
        {
            BidderReg t = db.BidderRegs.Find(id);
            db.BidderRegs.Remove(t);
            db.SaveChanges();
            return RedirectToAction("BidDetails");
        }
        #endregion

        /// <summary>
        /// Admin can View all the Bidding made on a crop here.
        /// </summary>
        /// <param name="View Bidding"></param>
        /// <returns></returns>
        #region  ViewBidding
        public ActionResult ViewBidding(string Sorting_Order)
        {
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";

            var bid = from b in db.Biddings orderby b.Crop_ID select b;
            switch (Sorting_Order)
            {
                case "Name_Description":
                    bid = bid.OrderByDescending(b => b.Current_Bid);
                    break;

                default:
                    bid = bid.OrderBy(b => b.Current_Bid);
                    break;
            }
            return View(bid.ToList());
        }
        #endregion

        /// <summary>
        /// Used t 
        /// </summary>
        /// <param name="Approve bid"></param>
        /// <returns></returns>
        #region  ApproveBidding
        [HttpGet]
        public ActionResult ApproveBidding(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result =(from a in db.Adminapprovals join b in db.Biddings on
                            a.Bidder_ID equals b.Bidder_ID
                             where a.Crop_ID == b.Crop_ID && a.Bidder_ID == b.Bidder_ID select a).Count();
           
            if (result == 0)
            {
                var x = (from i in db.Biddings
                             join j in db.SellRequests on i.Crop_ID equals j.Crop_ID
                             where i.BiddingID == id
                             select new { i.Crop_ID, i.Bidder_ID, j.Farmer_ID, j.Crop_name, j.Quantity, j.Baseprice, i.Current_Bid }).SingleOrDefault();
                    Adminapproval ad = new Adminapproval();
                    ad.Bidder_ID = x.Bidder_ID;
                    ad.Crop_ID = x.Crop_ID;
                    ad.Farmer_ID = (int)x.Farmer_ID;
                    ad.Status = "yes";

                    Soldhistory sh = new Soldhistory();
                   
                    sh.Date = DateTime.Now;
                

                    sh.Crop_ID = x.Crop_ID;
                    sh.Crop_name = x.Crop_name;
                    sh.Quantity = x.Quantity;
                    sh.Baseprice = (double)x.Baseprice;
                    sh.Soldprice = x.Current_Bid;
                    sh.Totalprice = sh.Soldprice - (x.Current_Bid * 2) / 100;


                    db.Adminapprovals.Add(ad);
                    db.SaveChanges();
                    db.Soldhistories.Add(sh);
                    db.SaveChanges();
                    return View("Index");

            }
            else
            {
                ViewBag.i = "You cannot Approve for the same crop";
                return RedirectToAction("Index");
            }
        }
        #endregion


        /// <summary>
        /// Reject Bidding
        /// </summary>
        /// <param name="Reject Bidding"></param>
        /// <returns></returns>
        #region RejectBidding
        public ActionResult RejectBidding(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bidding t = db.Biddings.Find(id);
            return View(t);
        }
       

        [HttpPost]
        [ActionName("RejectBidding")]

        public ActionResult RejectBiddingConfirm(int id)
        {
            Bidding t = db.Biddings.Find(id);
            db.Biddings.Remove(t);
            db.SaveChanges();
            return RedirectToAction("ViewBidding");
        }
        #endregion

        /// <summary>
        /// View Sold History
        /// </summary>
        /// <returns></returns>
        #region ViewSoldhistory
        public ActionResult ViewSoldhistory()
        {
            return View(db.Soldhistories.ToList());

        }
        #endregion
    }

}