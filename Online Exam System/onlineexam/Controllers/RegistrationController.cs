using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using onlineexam.Controllers;
using onlineexam.Models;

namespace onlineexam.Controllers
{
    public class RegistrationController : Controller
    {
        OESEntities1 db = new OESEntities1();
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(Student ld)
        {
            db.Students.Add(ld);
            db.SaveChanges();
            return RedirectToAction("RegistrationSuccessful");
        }
        [HttpGet]
        public ActionResult RegistrationSuccessful()
        {
            return View();
        }
        [HttpPost]
        [ActionName("RegistrationSuccessful")]
        public ActionResult RegistrationSuccessfulPost()
        {
            return RedirectToAction("Index1","Home");
        }
        


        // GET: Registration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registration/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Registration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Registration/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Registration/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
