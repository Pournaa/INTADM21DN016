using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmerApp.Models;
using System.Net;
using System.Net.Mail;
using System.Web.Security;

namespace FarmerApp.Controllers
{
    [HandleError]
    public class FarmerhomeController : Controller
    {
        // GET: Farmerhome
        FarmersEntities db = new FarmersEntities();
        /// <summary>
        /// This is the home page of the app, it contains Farmer, Bidder and Admin Login.
        /// </summary>
        /// <returns></returns>
        #region Farmerhome Index 
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// About Page:- It Contains the information about the Scheme for Farmer Application.
        /// </summary>
        /// <returns></returns>
        #region About Page
        public ActionResult Fabout()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        #endregion


        /// <summary>
        /// Contact Page:- It contains the contact related information.
        /// </summary>
        /// <returns></returns>
        #region Contact Page
        public ActionResult Fcontact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion

        /// <summary>
        /// Admin Login Page
        /// </summary>
        /// <returns></returns>
        #region Admin Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]


        public ActionResult Login(FLogin l)
        {
            var un = l.Username;
            var pw = l.Password;
            var admin = db.Admins.ToList();
            var res = (from i in db.Admins
                       where i.Username == un &&
                       i.Password == pw
                       select i).SingleOrDefault();
            
            if (res != null)
            {
                Session["UserId"] = true;
                Session["aid"] = res.Admin_ID;
                return RedirectToAction("Index","Admin");
            }
            else
            {
                Session["UserId"] = false;
                return View();
            }
        }
        #endregion

        /// <summary>
        /// Farmer Login Page
        /// </summary>
        /// <returns></returns>
        #region Farmer Login
        [HttpGet]
        public ActionResult FarLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FarLogin(FarmerReg l)
        {
            var un = l.Email;
            var pw = l.Password;
            var FarmerRegs = db.FarmerRegs.ToList();
            FarmerReg res = (from i in db.FarmerRegs
                       where i.Email == un &&
                       i.Password == pw
                       select i).SingleOrDefault();

            if (res != null)
            {
                Session["UserId"] = true;
                Session["fid"] = res.Farmer_ID;
                return RedirectToAction("Index", "Farmer");
            }
            
            else
            {
                if (!IsEmailExistF(un))
                {
                    ViewBag.m = "Enter Correct Mail id ";
                }
                else if (!IsPasswordExistF(pw))
                {
                    ViewBag.m1 = "Enter Correct Password ";
                }
                else
                {
                    Session["UserId"] = false;
                    ViewBag.m2 = "Enter Correct Mail id and Password";
                    return RedirectToAction("FarLogin", "Farmerhome");
                }
                return View();
                
            }
            
        }
        public bool IsEmailExistF(string Email)
        {
            var result = (from i in db.FarmerRegs
                          where i.Email == Email
                          select i).ToList();
            if (result.Count != 0)
                return true;
            else
                return false;
        }
        public bool IsPasswordExistF(string Password)
        {
            var result = (from i in db.FarmerRegs
                          where i.Password == Password
                          select i).ToList();
            if (result.Count != 0)
                return true;
            else
                return false;
        }
        #endregion


        /// <summary>
        /// Bidder login Page
        /// </summary>
        /// <returns></returns>
        #region Bidder Login
        [HttpGet]
        public ActionResult BidLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BidLogin(BidderReg l)
        {
            var un = l.Email;
            var pw = l.Password;
            var BidderRegs = db.BidderRegs.ToList();
            var res = (from i in db.BidderRegs
                       where i.Email == un &&
                       i.Password == pw
                       select i).SingleOrDefault();
            
            if (res != null)
            {
                Session["UserId"] = true;
                Session["bid"] = res.Bidder_ID;
                return RedirectToAction("BidWelcome", "Bidder");
            }
            
            else
            {
                if (!IsEmailExist(un))
                {
                    ViewBag.i = "Enter Correct Mail id ";
                }
                else if (!IsPasswordExist(pw))
                {
                    ViewBag.k = "Enter Correct Password ";
                }
                else
                {
                    Session["UserId"] = false;
                    ViewBag.l = "Enter Correct Mail id and Password";
                    return RedirectToAction("BidLogin", "Farmerhome");
                }
                return View();

            }
        }
        public bool IsEmailExist(string Email)
        {
            var result = (from i in db.BidderRegs
                          where i.Email == Email
                          select i).ToList();
            if (result.Count != 0)
                return true;
            else
                return false;
        }
        public bool IsPasswordExist(string Password)
        {
            var result = (from i in db.BidderRegs
                          where i.Password == Password
                          select i).ToList();
            if (result.Count != 0)
                return true;
            else
                return false;
        }
        #endregion

        /// <summary>
        /// Farmer Registration Page
        /// </summary>
        /// <returns></returns>
        #region Farmer Register
        [HttpGet]
        public ActionResult FarmerRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FarmerRegister(FarmerReg reg, HttpPostedFileBase ImageUpload)
        {
            string myfilename = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
            string extension = Path.GetExtension(ImageUpload.FileName);
           
            myfilename = myfilename + extension;
            reg.Certificate = "~/Images/" + myfilename;
            myfilename = Path.Combine(Server.MapPath("~/Images/"), myfilename);
            ImageUpload.SaveAs(myfilename);

            //reg.Password = Security.HashSHA1(reg.Password);


            db.FarmerRegs.Add(reg);
            db.SaveChanges();

            string to = reg.Email; //To address    
            string from = "farmer.schema@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Hello from Farmer Schema, enjoy selling!!" ;
            message.Subject = "Bienvenido!";
            message.Body = mailbody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            
            return RedirectToAction("FarLogin", "Farmerhome");

        }
        #endregion


        /// <summary>
        /// Bidder Registration Page
        /// </summary>
        /// <returns></returns>
        #region Bidder Registration
        [HttpGet]
        public ActionResult BidderReg()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult BidderReg(BidderReg b,
            HttpPostedFileBase ImageUpload)
        {

            string myfilename = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
            string extension = Path.GetExtension(ImageUpload.FileName);

            myfilename = myfilename + extension;
            b.Trader_license = "~/Images/" + myfilename;
            myfilename = Path.Combine(Server.MapPath("~/Images/"), myfilename);
            ImageUpload.SaveAs(myfilename);

            //reg.Password = Security.HashSHA1(reg.Password);


            db.BidderRegs.Add(b);
            db.SaveChanges();

            string to = b.Email; //To address    
            string from = "farmer.schema@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Hello from Farmer Schema, enjoy selling!!";
            message.Subject = "Bienvenido!";
            message.Body = mailbody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            return RedirectToAction("BidLogin", "Farmerhome");
        }
        //    string myfilename = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
        //    string extension = Path.GetExtension(ImageUpload.FileName);

        //    myfilename = myfilename + extension;
        //    b.Trader_license = "~/Images/" + myfilename;
        //    myfilename = Path.Combine(Server.MapPath("~/Images/"), myfilename);
        //    ImageUpload.SaveAs(myfilename);

        //    //reg.Password = Security.HashSHA1(reg.Password);


        //    db.BidderRegs.Add(b);
        //    db.SaveChanges();

        //    string to = b.Email; //To address    
        //    string from = "farmer.schema@gmail.com"; //From address    
        //    MailMessage message = new MailMessage(from, to);

        //    string mailbody = "Hello from Farmer Schema, enjoy selling!!";
        //    message.Subject = "Bienvenido!";
        //    message.Body = mailbody;
        //    message.BodyEncoding = System.Text.Encoding.UTF8;
        //    message.IsBodyHtml = true;
        //    SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
        //    System.Net.NetworkCredential basicCredential1 = new
        //    System.Net.NetworkCredential("farmer.schema@gmail.com", "Farmer1234");
        //    client.EnableSsl = true;
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = basicCredential1;
        //    try
        //    {
        //        client.Send(message);
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return RedirectToAction("BidLogin", "Farmerhome");

        //}



        #endregion


        /// <summary>
        /// Farmer forgot password page, Farmer will get the password via Email.
        /// </summary>
        /// <returns></returns>
        #region Farmer Forget Password
        [HttpGet]
        public ActionResult ForgotPasswordF()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPasswordF(FarmerReg l)
        {
            var un = l.Email;
            var num = l.Contact_no;
            var FarmerRegs = db.FarmerRegs.ToList();
            var res = (from i in db.FarmerRegs
                       where i.Email == un &&
                       i.Contact_no == num
                       select i.Password).SingleOrDefault();

            var res1 = (from i in db.FarmerRegs
                        where i.Email == un
                        select i.Password).SingleOrDefault();
            var res2 = (from i in db.FarmerRegs
                        where i.Contact_no == num
                        select i.Password).SingleOrDefault();
            if (res1 == null && res2 == null)
            {
                ViewBag.m2 = "Invalid email and mobilenumber";
            }
            else if (res1 == null)
            {
                ViewBag.m = "Invalid email";
            }
            else if (res2 == null)
            {
                ViewBag.m1 = "Invalid mobilenumber";
            }

            else
               
            {
                ViewBag.i = "Your Password is sent to your Mail";
                string to = l.Email; //To address    
                string from = "farmer.schema@gmail.com"; //From address    
                MailMessage message = new MailMessage(from, to);

                string mailbody = "Your Email Id And Passwords is :-" + " <br />" + "Email id " + l.Email + " <br />" + "Your Password " + res;
                message.Subject = "Welcome";
                message.Body = mailbody;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("farmer.schema@gmail.com", "Farmer1234");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                try
                {
                    client.Send(message);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                ViewBag.i = "Your Password is sent to your Mail";
                return RedirectToAction("FarLogin", "Farmerhome");

            }
           
            return View();
        }
        #endregion

        /// <summary>
        /// Bidder forgot password, Bidder will get the password via Email.
        /// </summary>
        /// <returns></returns>
        #region Bidder Forget Password
        [HttpGet]
        public ActionResult ForgotPasswordB()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPasswordB(BidderReg l)
        {
            var un = l.Email;
            var num = l.Contact_no;
            var BidderReg = db.BidderRegs.ToList();
            var res = (from i in db.BidderRegs
                       where i.Email == un &&
                       i.Contact_no == num
                       select i.Password).SingleOrDefault();

            var res1 = (from i in db.BidderRegs
                        where i.Email == un
                        select i.Password).SingleOrDefault();
            var res2 = (from i in db.BidderRegs
                        where i.Contact_no == num

                        select i.Password).SingleOrDefault();
            if (res1 == null && res2 == null)
            {
                ViewBag.m2 = "Invalid email and mobilenumber";
            }
            else if (res1 == null)
            {
                ViewBag.m = "invalid email";
            }
            else if (res2 == null)
            {
                ViewBag.m1 = "invalid mobilenumber";
            }
            else

            {

                string to = l.Email; //To address    
                string from = "farmer.schema@gmail.com"; //From address    
                MailMessage message = new MailMessage(from, to);

                string mailbody = "Your Email Id And Passwords is :-" + " <br />" + "Email id " + l.Email + " <br />" + "Your Password " + res;
                message.Subject = "Welcome";
                message.Body = mailbody;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("farmer.schema@gmail.com", "Farmer1234");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                try
                {
                    client.Send(message);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("BidLogin", "Farmerhome");

            }
            return View();
        }
        #endregion

        /// <summary>
        /// Log out, used for clearing session.
        /// </summary>
        /// <returns></returns>
       #region Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            if (Session["UserId"] == null)
            {

                return RedirectToAction("Index", "Farmerhome");
            }
            else
            {
                return View();
            }
        }
        #endregion

    }
}

