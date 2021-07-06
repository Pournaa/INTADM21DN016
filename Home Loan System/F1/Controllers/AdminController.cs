using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using F1.Models;

namespace F1.Controllers
{
    public class AdminController : Controller
    {
        private D1Entities db = new D1Entities();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.MVCregUsers.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MVCregUser mVCregUser = db.MVCregUsers.Find(id);
            if (mVCregUser == null)
            {
                return HttpNotFound();
            }
            return View(mVCregUser);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,F_name,L_name,MobileNo,Email,Dob,Address1,Address2,City,State,Country,Pincode")] MVCregUser mVCregUser)
        {
            if (ModelState.IsValid)
            {
                db.MVCregUsers.Add(mVCregUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mVCregUser);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MVCregUser mVCregUser = db.MVCregUsers.Find(id);
            if (mVCregUser == null)
            {
                return HttpNotFound();
            }
            return View(mVCregUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,F_name,L_name,MobileNo,Email,Dob,Address1,Address2,City,State,Country,Pincode")] MVCregUser mVCregUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mVCregUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mVCregUser);
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
