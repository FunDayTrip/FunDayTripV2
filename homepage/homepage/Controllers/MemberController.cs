using homepage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace homepage.Controllers
{
    public class MemberController : Controller
    {
        DB_FunDayTripEntities dbFundaytrip = new DB_FunDayTripEntities();


        // 說明:註冊起始頁面
        public ActionResult signUp()
        {
            return View();
        }

        //說明:註冊接收Form回傳控制器
        [HttpPost]
        public ActionResult signUp(CMember data)
        {
            //說明:資料模型驗證必加判斷式
            if (ModelState.IsValid)
            {
                //說明:使用者有上傳圖片才進入判斷
                if (data.image != null)
                {
                    string photoName = Guid.NewGuid().ToString();
                    photoName += Path.GetExtension(data.image.FileName);
                    data.image.SaveAs(Server.MapPath("~/Content/" + photoName));
                    data.fPhoto_Member = "../Content/" + photoName;
                }

                //說明:由於傳回資料不是用tMember型別而是自創的CMember型別，欄位不對稱，
                //所以view傳回端資料data需一個一個代入資料庫tMember。

                //仍需確認email是否重複

                DB_FunDayTripEntities db = new DB_FunDayTripEntities();
                tMember tmember = new tMember();
                tmember.fNickName_Member = data.fNickName_Member;
                tmember.fEmail_Member = data.fEmail_Member;
                tmember.fPassword_Member = data.fPassword_Member;
                tmember.fFirstName_Member = data.fFirstName_Member;
                tmember.fLastName_Member = data.fLastName_Member;
                tmember.fPhone_Member = data.fPhone_Member;
                tmember.fAdd_Member = data.fAdd_Member;
                tmember.fBirthday_Member = data.fBirthday_Member;
                tmember.fCountry_Member = data.fCountry_Member;
                tmember.fGender_Member = data.fGender_Member;
                tmember.fPhoto_Member = data.fPhoto_Member;
                tmember.fId_FunctionAuth = data.fId_FunctionAuth;
                db.tMembers.Add(tmember);
                db.SaveChanges();

                //說明:跑Model裡的CRole類別裡的方法，用暱稱欄位新增tRole資料表
                //CRole getNewRole = new CRole();
                //string newRoleName = data.fNickName_Member;
                //getNewRole.getNewRoleMethod(newRoleName);
                CRolesFactory roleFactory = new CRolesFactory();
                roleFactory.createRole(tmember);


                //說明:跑Model裡的CPoint類別裡的方法，用Role暱稱欄位新增tPoint資料表

                //CPoint getFirstPoint = new CPoint();
                //string newPointName = data.fNickName_Member;
                //int point1 = getFirstPoint.firstRegister(newPointName);


                //說明:把值代入ViewBag傳到mainPage控制器，為了顯示註冊成功alert
                Session["ok"] = "ok";

                //說明:轉跳到mainPage控制器
                return RedirectToAction("Index","Home");
            }

            //說明:說果資料模型驗證沒過重新導向會員註冊頁面
            return View();
        }
        public ActionResult edit()
        {
            return View();
        }
    }
}