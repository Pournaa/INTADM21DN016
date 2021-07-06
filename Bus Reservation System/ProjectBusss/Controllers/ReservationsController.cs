using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectBusss.Models;

namespace ProjectBusss.Controllers
{
    [Authorize]

    public class ReservationsController : Controller
    {
        private ProjectBusContext db = new ProjectBusContext();

        // GET: Reservation
        public ActionResult Index()
        {
            var ticket = db.Ticket.Include(t => t.Bus).Include(t => t.Payment).Include(t => t.Schedule);
            return View(ticket.ToList());
        }

        // GET: Reservation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Reservations/Book
        public ActionResult Book(int sid, int fid, DateTime doj, string name, string Tclass)
        {

            Ticket model = new Ticket()
            {
                BusId = fid,
                scheduleId = sid,
                dateOfJourney = doj.Date,
                travelType = Tclass
                //UserId = User.Identity.Name
            };
            ViewBag.BusName = name;

            return View(model);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteBooking([Bind(Include = "paymentMode,totalAmount,bankDetails")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payment.Add(payment);
                db.SaveChanges();
            }

            Ticket ticket = TempData["Ticket"] as Ticket;
            ticket.UserId = User.Identity.Name;
            ticket.paymentId = payment.paymentId;

            db.Ticket.Add(ticket);
            db.SaveChanges();

            Schedule schedule = db.Schedule.Find(ticket.scheduleId);
            if (ticket.travelType.Contains("AC_Sleeper"))
            {
                schedule.AC_SleeperSeats--;
            }
            else if (ticket.travelType.Contains("NonAC_Sleeper"))
            {
                schedule.NonAC_SleeperSeats--;
            }
            else if (ticket.travelType.Contains("ACSemi_Sleeper"))
            {
                schedule.ACSemi_SleeperSeats--;
            }
            else
            {
                schedule.NonAC_SleeperSeats--;
            }
            db.Entry(schedule).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment([Bind(Include = "BusId,scheduleId,dateOfJourney,passengerName,gender,phoneNumber,address,emergencyContact,travelType")] Ticket ticket)
        {
            TempData["Ticket"] = ticket;
            ticket.seatNo = db.Ticket.Include(t => t.ticketId).Where(t => t.travelType == ticket.travelType).Count() + 1;
            Payment payment = new Payment()
            {
                totalAmount = getcostofticket(ticket.scheduleId, ticket.travelType)
            };

            return View(payment);
        }


        private double getcostofticket(int scheduleId, string travelType)
        {
            double cost = 0.00;
            System.Diagnostics.Debug.WriteLine(travelType);

            Schedule s = new Schedule();
            string sub = "cost" + travelType;
            System.Diagnostics.Debug.WriteLine(sub);

            var query = "SELECT " + sub + " FROM Schedule where scheduleId=" + scheduleId;
            string cString = ConfigurationManager.ConnectionStrings["ProjectBusReContext"].ConnectionString;
            //string cString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog = aspnet-ProjectBus-20210617112227; Integrated Security = True";
            using (SqlConnection c = new SqlConnection(cString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            cost = rdr.GetDouble(rdr.GetOrdinal("cost" + travelType));
                        }
                    }
                }
            }
            return (cost);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusId = new SelectList(db.Bus, "BusId", "BusName", ticket.BusId);
            ViewBag.paymentId = new SelectList(db.Payment, "paymentId", "paymentMode", ticket.paymentId);
            ViewBag.scheduleId = new SelectList(db.Schedule, "scheduleId", "source", ticket.scheduleId);
            return View(ticket);
        }

        public ActionResult CancelTicket(int id)
        {

            Ticket ticket = db.Ticket.Find(id);
            //ViewBag.cancelexception = "true";
            //if (ticket.dateOfJourney.CompareTo(DateTime.Today) > 0)
            //{
            //    ViewBag.cancelexception = "false";
            //}
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        [HttpPost, ActionName("CancelTicket")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            db.Ticket.Remove(ticket);
            db.SaveChanges();
            Schedule schedule = db.Schedule.Find(ticket.scheduleId);
            if (ticket.travelType.Contains("AC_Sleeper"))
            {
                schedule.AC_SleeperSeats--;
            }
            else if (ticket.travelType.Contains("NonAC_Sleeper"))
            {
                schedule.NonAC_SleeperSeats--;
            }
            else if (ticket.travelType.Contains("ACSemi_Sleeper"))
            {
                schedule.ACSemi_SleeperSeats--;
            }
            else
            {
                schedule.NonAC_SleeperSeats--;
            }
            db.Entry(schedule).State = EntityState.Modified;
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
