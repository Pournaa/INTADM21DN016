using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectBusss.Models;

namespace ProjectBusss.Controllers
{
    
    public class SchedulesController : Controller
    {
        private ProjectBusContext db = new ProjectBusContext();

        // GET: Schedules
        public ActionResult Index()
        {
            var schedule = db.Schedule.Include(s => s.Bus);
            return View(schedule.ToList());
        }

        // GET: Schedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedule.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewBag.BusId = new SelectList(db.Bus, "BusId", "BusName");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "scheduleId,BusId,source,sourceId,dest,destId,scheduleDate,depatureTime,arrivalDate,arrivalTime,status,Ac_SlepperSeats,NonAc_SleeperSeats,AcSemi_Sleeperseats,NonAcSemi_Sleeperseats,costAc_Sleeper,costNonAc_Sleeper,costAcSemi_Sleeper,costNonAcSemi_Sleeper")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedule.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusId = new SelectList(db.Bus, "BusId", "BusName", schedule.BusId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedule.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusId = new SelectList(db.Bus, "BusId", "BusName", schedule.BusId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "scheduleId,BusId,source,sourceId,dest,destId,scheduleDate,depatureTime,arrivalDate,arrivalTime,status,Ac_SlepperSeats,NonAc_SleeperSeats,AcSemi_Sleeperseats,NonAcSemi_Sleeperseats,costAc_Sleeper,costNonAc_Sleeper,costAcSemi_Sleeper,costNonAcSemi_Sleeper")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusId = new SelectList(db.Bus, "BusId", "BusName", schedule.BusId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedule.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedule.Find(id);
            db.Schedule.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("Index");
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
