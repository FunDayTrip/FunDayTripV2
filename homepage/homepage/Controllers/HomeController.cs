
using homepage.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
using System.Web.Helpers;
using homepage.Models;

namespace homepage.Controllers
{
    public class HomeController : Controller
    {
        DB_FunDayTripEntities dbFundaytrip = new DB_FunDayTripEntities();
        CLoginViewModel loginMember = new CLoginViewModel();



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

        //public string login(string email, string pwd)
        //{
        //    string loginMsg = "";

        //    if (email == "0000" && pwd == "0000" )
        //    {
        //        loginMsg = "登入成功";
        //    }            
        //    else
        //    {
        //        loginMsg = "登入失敗";
        //    }

        //    return loginMsg;
        //}
        [HttpPost]
        public ActionResult login(string email, string pwd)
        {
            var q = from m in dbFundaytrip.tMembers
                    where m.fEmail_Member == email
                    select m;

            //CLoginViewModel loginMember = new CLoginViewModel();

            if (!q.Any())
            {
                loginMember.loginMessage = "無此帳號";
            }
            else if (q.FirstOrDefault().fPassword_Member == pwd)
            {
                loginMember.loginMessage = "歡迎回來! " + q.FirstOrDefault().fNickName_Member;
                loginMember.fId_Member = q.FirstOrDefault().fId_Member;
                loginMember.fNickName_Member = q.FirstOrDefault().fNickName_Member;
                loginMember.fEmail_Member = q.FirstOrDefault().fEmail_Member;
                loginMember.fPassword_Member = q.FirstOrDefault().fPassword_Member;


                //讀取角色
                List<CRole> rolesList = new CRolesFactory().readRoles(loginMember.fId_Member);
                loginMember.fActiveRoleId_Member = rolesList.FirstOrDefault(a => a.fId_Master_Role == loginMember.fId_Member).fId_Role;
                loginMember.fActiveRoleName_Member = rolesList.FirstOrDefault(a => a.fId_Master_Role == loginMember.fId_Member).fNickName_Role;
                //foreach (var rr in rolesList)
                //{
                //    rolesList.Add(rr);
                //}

                //讀取通知
                List<tNote> notesList = new CNotesFactory().readNotes(loginMember.fActiveRoleId_Member);
                loginMember.fNotesCount_Member = notesList.Count;

                //放入Session
                Session[CDictionary.SK_MemberId] = loginMember.fId_Member;
                Session[CDictionary.SK_MemberLogin] = loginMember;
                Session[CDictionary.SK_ActiveRoleId] = loginMember.fActiveRoleId_Member;
                
            }
            else if (q.FirstOrDefault().fPassword_Member != pwd)
            {
                loginMember.loginMessage = "密碼錯誤";
            }
            else
            {
                loginMember.loginMessage = "登入失敗";
            }
            return Json(loginMember);
        }
        [HttpPost]
        public string logout()
        {
            CLoginViewModel logoutMember = new CLoginViewModel();
            logoutMember.loginMessage = "登出成功";
            Session[CDictionary.SK_MemberId] = CDictionary.SK_anonymous;
            Session[CDictionary.SK_MemberLogin] = logoutMember;

            return logoutMember.loginMessage;
        }
        [HttpPost]
        public JsonResult readNotes(int role_id)
        {
            var q = from n in dbFundaytrip.tNotes
                    where n.fId_Role == role_id
                    select n;

            List<CNotes> note = new List<CNotes>();
            foreach(var n in q.ToList())
            {
                CNotes nc = new CNotes();
                nc.fMessage_Note = n.fMessage_Note;
                note.Add(nc);
            }

            return Json(note);
        }
        [HttpPost]
        public JsonResult readRoles(string member_id)
        {
            List<CRole> roles = new CRolesFactory().readRoles(member_id);          
            
            
            return Json(roles);            
        }
        [HttpPost]
        public string changeRole(int role_id)
        {
            CRole rol = new CRolesFactory().getRole(role_id);
            
            Session[CDictionary.SK_ActiveRoleId] = rol.fId_Role;
            Session[CDictionary.SK_ActiveRoleName] = rol.fNickName_Role;
            return rol.fNickName_Role;
        }

    }
}