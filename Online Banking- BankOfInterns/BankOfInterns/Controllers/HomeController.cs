using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BankOfIntern.Models;

namespace BankOfIntern.Controllers
{
    
    public class HomeController : Controller
    {
        BOIFinalDBEntities1 db = new BOIFinalDBEntities1();
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult AccOpen(int CusId = 0)
        //{
        //    NewAcc accopen = new NewAcc();
        //    ViewBag.gender = new SelectList(db.NewAccs, "Cust_id", "gender");
        //    ViewBag.branchname = new SelectList(db.NewAccs, "Cust_id", "branchname");
        //    return View(accopen);
        //}
        //[HttpPost]
        //public ActionResult AccOpen(NewAcc accopen, HttpPostedFileBase panpdf)
        //{
        //    using (BOIFinalDBEntities db = new BOIFinalDBEntities())
        //    {
        //        db.NewAccs.Add(accopen);
        //        db.SaveChanges();

        //    }
        //    ModelState.Clear();
        //    return View("AccOpen", new NewAcc());
        //}
        //[HttpPost]
        //public ActionResult AccSuccess()
        //{
        //   return View();
        //}

        //        [HttpPost]
        //        public ActionResult Mailing(MailingDet_Result model)
        //        {
        //            BOIFinalDBEntities db = new BOIFinalDBEntities();
        //            var sa = db.MailingDet().FirstOrDefault();
        //            try
        //            {
        //                MailMessage mail = new MailMessage();
        //                mail.To.Add(sa.Email);
        //                mail.From = new MailAddress("bankofinterns@gmail.com");
        //                mail.Subject = "Your BANK OF INTERNS account details ";
        //                string userMessage = "";

        //                userMessage = userMessage + "<br/><b>Account Number: </b> " + model.AccNo;
        //                userMessage = userMessage + "<br/><b>User Id: </b> " + model.UserId;
        //                userMessage = userMessage + "<br/><b>Passsword: </b>" + model.Pwd;
        //                userMessage = userMessage + "<br/><b>NOTE: </b>Reset your password after logging in first time using the given UserID and password <br /> The UserID remains the same after resetting password.";

        //                string Body = "Dear Customer, <br/><br/>Login details for your account are as follows:<br/></br> " + userMessage + "<br/><br/>Thanks";
        //                mail.Body = Body;
        //                mail.IsBodyHtml = true;

        //                SmtpClient smtp = new SmtpClient
        //                {
        //                    Host = "smtp.gmail.com", //SMTP Server Address of gmail
        //                    Port = 587,
        //                    Credentials = new System.Net.NetworkCredential("bankofinterns@gmail.com", "ctsprojectmates"),
        //                    // Smtp Email ID and Password For authentication
        //                    EnableSsl = true
        //                };
        //                smtp.Send(mail);
        //                return View();
        //            }
        //            catch
        //            {
        //                return View("Error");

        //            }
        //        }
    }
}