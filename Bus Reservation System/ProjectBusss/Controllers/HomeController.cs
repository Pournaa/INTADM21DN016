using ProjectBusss.Models;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBusss.Controllers
{
    public class HomeController : Controller
    {
        private ProjectBusContext db = new ProjectBusContext();
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to BookMyTrip";

            var srclist = new List<Schedule>();
            var destlist = new List<Schedule>();
            string cString = ConfigurationManager.ConnectionStrings["ProjectBusReContext"].ConnectionString;
            //string cString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog = aspnet-FlightReservationSystem-20151101020301; Integrated Security = True";
            using (SqlConnection c = new SqlConnection(cString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT source FROM Schedule", c))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            srclist.Add(new Schedule
                            {
                                source = rdr.GetString(0)
                            });
                        }
                    }
                }
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT dest FROM Schedule", c))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            destlist.Add(new Schedule
                            {
                                dest = rdr.GetString(0)
                            });
                        }
                    }
                }
            }

            ViewBag.source = new SelectList(srclist, "source", "source");
            ViewBag.dest = new SelectList(destlist, "dest", "dest");

            //ViewBag.source = new SelectList(db.Schedule, "source","source");
            //ViewBag.dest = new SelectList(db.Schedule, "dest", "dest");
            //ViewBag.date = new DateTime();
            ViewBag.date = new DateTime();
            return View();
        }
        [HttpPost]
        public ActionResult SearchResults(string source, string dest, DateTime dateOfJourney)
        {
            ViewBag.Source = source;
            ViewBag.Dest = dest;
            ViewBag.ScheduleMessage = "";
            if (DateTime.Compare(dateOfJourney, DateTime.Today) > 0)
            {
                var data = from s in db.Schedule
                           where s.source == source && s.dest == dest && s.scheduleDate == dateOfJourney
                           select s;
                if (data.ToList().Count() == 0)
                {
                    ViewBag.ScheduleMessage = "No Buses on the entered date, below are the Buses from other days";
                    data = from s in db.Schedule
                           where s.source == source && s.dest == dest && DateTime.Compare(s.scheduleDate, DateTime.Today) > 0
                           select s;
                }
                return View(data.ToList());
            }
            else
            {

                //var data = from s in db.Schedule
                //           where s.source == source && s.dest == dest && s.scheduleDate.CompareTo(DateTime.Today) >= 0 && TimeSpan.Compare(s.depatureTime,DateTime.Now.TimeOfDay) > 0
                //           select s;
                var data = from s in db.Schedule
                           where s.source == source && s.dest == dest && DateTime.Compare(s.scheduleDate, DateTime.Today) >= 0
                           select s;
                if (dateOfJourney.CompareTo(DateTime.Today) == 0)
                    ViewBag.ScheduleMessage = "Cannot book Buses for today for requested source and destination. Buses from requested source to destination are listed below";
                else
                    ViewBag.ScheduleMessage = "Entered past date, Buses from requested source to destination are listed below";
                return View(data.ToList());

            }

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}