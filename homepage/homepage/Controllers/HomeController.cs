
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
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            //con.Open();

            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //string sql = "SELECT fDate FROM tTransaction WHERE fId = 2005";
            //cmd.CommandText = sql;

            //SqlDataReader reader = cmd.ExecuteReader();
            //CAddLocationViewModel locationViewModel = new CAddLocationViewModel();
            //while (reader.Read())
            //{

            //    ViewBag.kk = reader["fDate"].ToString();
            //}

            //con.Close();
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
                List<CRole> rolesList = new CRolesFactory().getRoleList(loginMember.fId_Member);
                loginMember.fActiveRoleId_Member = rolesList.FirstOrDefault(a => a.fId_Master_Role == loginMember.fId_Member).fId_Role;
                loginMember.fActiveRoleName_Member = rolesList.FirstOrDefault(a => a.fId_Master_Role == loginMember.fId_Member).fNickName_Role;
                //foreach (var rr in rolesList)
                //{
                //    rolesList.Add(rr);
                //}

                //讀取通知
                List<tNote> notesList = new CNotesFactory().readNotes(loginMember.fActiveRoleId_Member);
                loginMember.fNotesCount_Member = notesList.Count(x => x.fIsRead_Note == 0);
                //讀取點數
                var member_point = new CPointFactory().getPoint(loginMember.fActiveRoleId_Member);
                loginMember.fPointTotal_Member = Convert.ToInt32(member_point.Sum(s => s.fPoint_Point));
                
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
            foreach (var n in q.ToList())
            {
                CNotes nc = new CNotes();
                nc.fMessage_Note = n.fMessage_Note;
                note.Add(nc);
            }

            return Json(note.OrderByDescending(n => n.fTime_Note));
        }
        [HttpPost]
        public JsonResult readRoles(string member_id)
        {
            List<CRole> roles = new CRolesFactory().getRoleList(member_id);


            return Json(roles);
        }

        [HttpPost]
        public JsonResult changeRole(int role_id)
        {
            CRole rol = new CRolesFactory().getRole(role_id);

            Session[CDictionary.SK_ActiveRoleId] = rol.fId_Role;
            Session[CDictionary.SK_ActiveRoleName] = rol.fNickName_Role;
            return Json(rol);
        }
        public JsonResult showFollow(int role_id)
        {
            var q = from p in dbFundaytrip.tFollows
                    where p.fId_Self_Role == role_id
                    select p;


            List<CFollowlistViewModel> f = new List<CFollowlistViewModel>();
            foreach (var x in q.ToList())
            {
                CFollowlistViewModel FV = new CFollowlistViewModel();
                FV.follow_Self_Name = x.tRole1.fNickName_Role;
                FV.follow_Self_ID = x.tRole1.fId_Role;
                f.Add(FV);
            }
            return Json(f);
        }
        public JsonResult showFan(int role_id)
        {
            var q = from p in dbFundaytrip.tFollows
                    where p.fId_Target_Role == role_id
                    select p;
            

            List<CFanslistViewModel> f = new List<CFanslistViewModel>();
            foreach (var x in q.ToList())
            {
                CFanslistViewModel FN = new CFanslistViewModel();
                FN.follow_Target_Name = x.tRole.fNickName_Role;
                FN.follow_Target_ID = x.tRole.fId_Role;
                f.Add(FN);
            }
            return Json(f);
        }
        public void pushMessage(int fId_To_Role, int fId_From_Role, string message)
        {
           DB_FunDayTripEntities db =new DB_FunDayTripEntities();
            tMessage CM = new tMessage();



            CM.fId_To_Role = fId_To_Role;
            CM.fId_From_Role = fId_From_Role;
            CM.fMessage_Message = message;
            CM.fTime_Message = DateTime.Now;

            db.tMessages.Add(CM);

            db.SaveChanges();
        }
        public JsonResult showHistoryMessage(int from_role_id,int to_role_id)
        {
            var q = from p in dbFundaytrip.tMessages
                    where p.fId_From_Role == from_role_id && p.fId_To_Role == to_role_id
                    select p;
           
            List<CHistoryMessage> f = new List<CHistoryMessage>();            
            foreach (var x in q.ToList())
            {
                CHistoryMessage CH = new CHistoryMessage();
                CH.fMessage_From = x.fMessage_Message;
                CH.fMessage_Time = Convert.ToString(x.fTime_Message);
                CH.fId = x.fId_Message;
                f.Add(CH);
            }
            var q1 = from z in dbFundaytrip.tMessages
                     where z.fId_To_Role == from_role_id && z.fId_From_Role == to_role_id
                     select z;
            foreach (var y in q1.ToList())
            {
                CHistoryMessage CH = new CHistoryMessage();
                CH.fMessage_To = y.fMessage_Message;
                CH.fMessage_Time = Convert.ToString(y.fTime_Message);
                CH.fId = y.fId_Message;
                f.Add(CH);
            }
            var res = f.OrderBy(x => x.fId);

            return Json(res);
        }
       
    }
      
}