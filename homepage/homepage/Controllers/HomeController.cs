
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
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            string sql = "SELECT fDate FROM tTransaction WHERE fId = 2005";
            cmd.CommandText = sql;

            SqlDataReader reader = cmd.ExecuteReader();
            CAddLocationViewModel locationViewModel = new CAddLocationViewModel();
            while (reader.Read())
            {

                ViewBag.kk = reader["fDate"].ToString();
            }

            con.Close();
            return View();
        }
        [HttpPost]
        public ActionResult Index(CAddLocationViewModel locate)
        {
            string photoname = Guid.NewGuid().ToString(); //重新命名一個不會重複的照片ID進資料庫
            photoname += Path.GetExtension(locate.image.FileName);//取得副檔名
            locate.image.SaveAs(Server.MapPath("~/Content/" + photoname)); //根目錄:~(不行),要用..回上一層
            locate.fImage = "../Content/" + photoname;


            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO tTransaction (fDate) VALUES ('" + locate.fImage + "')";
            cmd.ExecuteNonQuery();

            con.Close();

            return View();
        }


    }
}