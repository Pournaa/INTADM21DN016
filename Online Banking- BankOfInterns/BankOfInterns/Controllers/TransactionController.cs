
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BankOfIntern.Models;

namespace BankOfIntern.Controllers
{

    public class TransactionController : Controller
    {
        // GET: Transaction
        BOIFinalDBEntities1 db = new BOIFinalDBEntities1();
        List<Beneficiary> Beneficiary = new List<Beneficiary>();
        List<TransTable> TransTable = new List<TransTable>();
        public ActionResult Benifi(int id = 0)
        {
            Beneficiary b = new Beneficiary();
            ViewBag.BeneficiaryType = new SelectList(db.Beneficiaries, "BeneficiaryType");
            return View(b);

        }
        [HttpPost]
        public ActionResult Benifi(Beneficiary b)
        {
            using (BOIFinalDBEntities1 db = new BOIFinalDBEntities1())
            {
                if (db.Beneficiaries.Any(x => x.BenAccNo == b.BenAccNo))
                {
                    ViewBag.DuplicatMessage = "Beneficiary already Exists";
                    return View("Benifi", b);
                }
                else
                {
                    db.Beneficiaries.Add(b);
                    db.SaveChanges();

                }

            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Beneficiary Added";
            return View("Benifi", new Beneficiary());

        }
        public ActionResult Trans(int i = 0)
        {
            TransTable t = new TransTable();
            return View(t);
        }
        [HttpPost]
        public ActionResult Trans(TransTable t)
        {
            using (BOIFinalDBEntities1 sb = new BOIFinalDBEntities1())
            {
                sb.TransTables.Add(t);
                sb.SaveChanges();
            }
            return View("EnterAccNo");
        }
        [HttpPost]
        public ActionResult EnterAccNo(EnterAccNo model)
        {
            BOIFinalDBEntities1 bce = new BOIFinalDBEntities1();
            var id = bce.GetEmail2(model.AccNo);
            var item1 = id.FirstOrDefault();
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(item1);
                mail.From = new MailAddress("bankofinterns@gmail.com");
                mail.Subject = "OTP for NEFT Payment";
                string userMessage = "";

                Random r = new Random();
                string OTP = r.Next(1000, 9999).ToString();

                userMessage = userMessage + "<br/><b>OTP:</b> " + OTP;

                string Body = "Dear Customer, Your SECRET One Time Password (OTP) for Bank of Interns Transaction is:<br/>" + userMessage + "<br/><br/>Do not share it with anyone.";
                mail.Body = Body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", //SMTP Server Address of gmail
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("bankofinterns@gmail.com", "ctsprojectmates"),
                    // Smtp Email ID and Password For authentication
                    EnableSsl = true
                };
                smtp.Send(mail);

                Session["OTP"] = OTP;

                return RedirectToAction("verify");
            }
            catch
            {
                model.AccNoErrorMsg = "Incorrect Account Number";
                return View("EnterAccNo", model);
            }
        }
        public ActionResult verify(EnterOtp1 model, TransTable t)
        {

            if (Session["OTP"].ToString() == model.OTP)
            {

                return RedirectToAction("TransDetail");
            }
            else
            {
                
                return View("verify", model);
            }

        }

        public ActionResult TransDetail()
        {

            var s = db.TransDetails().FirstOrDefault();
            return View(s);
        }

    }
}
