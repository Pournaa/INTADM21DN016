using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FarmerApp.Models;


namespace FarmerApp.Controllers
{
    [HandleError]
    public class FarmerController : Controller
    {
        FarmersEntities db = new FarmersEntities();
        // GET: Farmer

        /// <summary>
        /// Farmer Index Page:- Here he can perform various operations like Place Sell request,
        /// View Sold history, view marketplace, claim insurance, view his profile. 
        /// </summary>
        /// <returns></returns>
        #region Farmer Index Page
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// Here Farmer can raise a Sell Request for his crop.
        /// </summary>
        /// <returns></returns>
        #region Add Sell Request
        [HttpGet]
        public ActionResult AddSellRequest()
        {
            //return RedirectToAction("Login");
            return View();
        }
       
        [HttpPost]
        public ActionResult AddSellRequest(SellRequest sr, HttpPostedFileBase ImageUpload)
        {
            try
            {
                int fid = (int)Session["fid"];
                string myfilename = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                //if (extension != "jpg" || extension != "jpeg")
                //{
                //    return View();
                //}
                myfilename = myfilename + extension;
                sr.Soil_PH = "~/FarmerImages/" + myfilename;
                myfilename = Path.Combine(Server.MapPath("~/FarmerImages/"), myfilename);
                ImageUpload.SaveAs(myfilename);
                sr.Farmer_ID = fid;
                db.SellRequests.Add(sr);
                db.SaveChanges();
            }

            catch (Exception Msg)
            {
                Response.Write("Oopss!!Something went wrong!!");
            }
            return RedirectToAction("Index");
        }
        #endregion

        /// <summary>
        /// It shows farmer personal Details.
        /// </summary>
        /// <returns></returns>
        #region Farmer Details
        public ActionResult FarDetails()
        {
            //return View(db.FarmerRegs.ToList());
            int fid = (int)Session["fid"];
            var y = (from i in db.FarmerRegs where i.Farmer_ID == fid select i).ToList();
            return View(y);
        }
        #endregion

        /// <summary>
        /// Here farmer can edit his profile.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       
        #region Farmer Edit Profile
        [HttpGet]
        public ActionResult FarEdit(int? id)
        {
              if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FarmerReg t = db.FarmerRegs.Find(id);
            return View(t);    
        }
     
      
        [HttpPost]
        public ActionResult FarEdit(FarmerReg t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("FarDetails");
        }
        #endregion


        /// <summary>
        /// Farmer can see all the sold crops. 
        /// </summary>
        /// <returns></returns>
        #region Farmer view Sold History
        public ActionResult ViewSoldhistory()
        {

            int fid = (int)Session["fid"];
            //var x = (from ad in db.Adminapprovals
            //         join sr in db.SellRequests on ad.Crop_ID equals sr.Crop_ID
            //         join b in db.Biddings on ad.Crop_ID equals b.Crop_ID
            //         join fr in db.FarmerRegs on ad.Farmer_ID equals fr.Farmer_ID
            //         select new
            //         {
            //             b.Current_Bid

            //         }).ToList();
            var x = (from sh in db.Soldhistories
                     join sr in db.SellRequests on sh.Crop_ID equals sr.Crop_ID
                     where fid == sr.Farmer_ID
                     select sh
                             ).ToList();
            return View(x);
        }
        #endregion
        /// <summary>
        /// Farmer can watch the current bid amount for his crop.
        /// </summary>
        /// <returns></returns>
        #region Farmer View MarketPlace
        public ActionResult ViewMarketplace()
        {
            int fid = (int)Session["fid"];
            var y = (from b in db.Biddings
                     join s in db.SellRequests
                    on b.Crop_ID equals s.Crop_ID
                     where s.Farmer_ID == fid
                     select b).ToList();

            return View(y);

            //return View(db.Biddings.ToList());
        }
    }
    #endregion
}