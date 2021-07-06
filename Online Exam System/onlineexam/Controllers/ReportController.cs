using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace onlineexam.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult report()
        {
            String constring = "Data Source=DESKTOP-0SOGDSH\\SQLEXPRESS; Initial Catalog=OES; Integrated Security=true";
            SqlConnection sqlcon = new SqlConnection(constring);
            String pname = "GetStudentReport"; 
            sqlcon.Open();
            SqlCommand com = new SqlCommand(pname, sqlcon);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = com.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            return View(dt);

            
        }
    }
}