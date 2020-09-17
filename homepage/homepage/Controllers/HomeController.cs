
using homepage.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace homepage.Controllers
{
    public class HomeController : Controller
    {

      
       
   
        // GET: Home
        public ActionResult Index()
        {
 
            return View();
        }
        [HttpPost]
        public ActionResult Index(CAddLocationViewModel locate)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO tTransaction (fDate) VALUES ('"+locate.txtLocationName+"')";
            cmd.ExecuteNonQuery();

            con.Close();

            return View();
        }

       
    }
}