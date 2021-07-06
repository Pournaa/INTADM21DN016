using System;
using System.Collections.Generic;
//using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlightReservationProject.Models;
using System.Configuration;
using System.Data.SqlClient;
using Rotativa;

namespace FlightReservationProject.Controllers
{
    [HandleError]
    [Authorize]
    public class ReservationsController : Controller
    {
        private FlightReservationContext db = new FlightReservationContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var ticket = db.Ticket.Include(t => t.Flight).Include(t => t.Payment).Include(t => t.Schedule).Where(t => t.UserId == User.Identity.Name);
            return View(ticket.ToList());
        }
        
      

        //GET: Reservations/Details/5
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
        public ActionResult IndexById(int id)
        {
            var emp = db.Ticket.Where(e => e.ticketId == id).First();
            return View(emp);
        }
        public ActionResult PrintInvoice(int id)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();

            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }

            return new ActionAsPdf("IndexById", new { id = id })
            {
                FileName = "TicketInvoice.pdf",
                Cookies = cookieCollection
            };
          
            
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Reservations/Book
        public ActionResult Book(int sid, int fid, DateTime doj, string name, string Tclass)
        {

            Ticket model = new Ticket()
            {
                flightId = fid,
                scheduleId = sid,
                dateOfJourney = doj.Date,
                travelclass = Tclass
                //UserId = User.Identity.Name
            };
            ViewBag.flightname = name;

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
            if (ticket.travelclass.Contains("First"))
            {
                schedule.FCseats--;
            }
            else if (ticket.travelclass.Contains("Business"))
            {
                schedule.BCseats--;
            }
            else
            {
                schedule.ECseats--;
            }
            db.Entry(schedule).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment([Bind(Include = "flightId,scheduleId,dateOfJourney,passengerName,gender,phoneNumber,address,emergencyContact,travelclass")] Ticket ticket)
        {
            TempData["Ticket"] = ticket;
            ticket.seatNo = db.Ticket.Include(t => t.ticketId).Where(t => t.travelclass == ticket.travelclass).Count() + 1;
            Payment payment = new Payment()
            {
                totalAmount = getcostofticket(ticket.scheduleId, ticket.travelclass)
            };

            return View(payment);
        }

        private double getcostofticket(int scheduleId, string travelclass)
        {
            double cost = 0.00;
            
            System.Diagnostics.Debug.WriteLine(travelclass);

            Schedule s = new Schedule();
            string sub = "cost" + travelclass;
            System.Diagnostics.Debug.WriteLine(sub);
            var query = "SELECT " + sub + " from Schedule where scheduleId=" + scheduleId;

            string cString = ConfigurationManager.ConnectionStrings["FlightReservationProjectContext"].ConnectionString;
            using (SqlConnection c = new SqlConnection(cString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT " + sub + " from Schedule where scheduleId=" + scheduleId, c))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            cost = rdr.GetDouble(rdr.GetOrdinal("cost" + travelclass));
                        }
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(cost);
            return (cost);
        }

        // GET: Reservations/Edit/5
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
            ViewBag.flightId = new SelectList(db.Flight, "flightId", "flightName", ticket.flightId);
            ViewBag.paymentId = new SelectList(db.Payment, "paymentId", "paymentMode", ticket.paymentId);
            ViewBag.scheduleId = new SelectList(db.Schedule, "scheduleId", "source", ticket.scheduleId);
            return View(ticket);
        }
        // GET: Reservations/Delete/5
        public ActionResult CancelTicket(int id)
        {

            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("CancelTicket")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            db.Ticket.Remove(ticket);
            db.SaveChanges();
            Schedule schedule = db.Schedule.Find(ticket.scheduleId);
            if (ticket.travelclass.Contains("First"))
            {
                schedule.FCseats++;
            }
            else if (ticket.travelclass.Contains("Business"))
            {
                schedule.BCseats++;
            }
            else
            {
                schedule.ECseats++;
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
