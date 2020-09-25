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
        //編輯會員資料 0925 by 郭松明/王詠平
        public ActionResult edit()
        {
            //傳入Session 中的member ID
            string memeber_id = Convert.ToString(Session[CDictionary.SK_MemberId]);
            if(memeber_id == CDictionary.SK_anonymous)
            {
                //如果是未登入，則返回首頁
                return RedirectToAction("Index","Home");
            }
            else
            {
                tMember data = dbFundaytrip.tMembers.FirstOrDefault(m => m.fId_Member == memeber_id);
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult edit(CMember change)
        {
            tMember oldData = dbFundaytrip.tMembers.FirstOrDefault(m => m.fId_Member == change.fId_Member);
            if (oldData == null)
            {
                return RedirectToAction("edit");
            }
            oldData.fNickName_Member = change.fNickName_Member;
            oldData.fAdd_Member = change.fAdd_Member;
            oldData.fPhone_Member = change.fPhone_Member;
            oldData.fCountry_Member = change.fCountry_Member;
            oldData.fFirstName_Member = change.fFirstName_Member;
            oldData.fLastName_Member = change.fLastName_Member;

            //說明:圖片有更改時才執行 
            if (change.image == null)
            {
                dbFundaytrip.SaveChanges();
                return RedirectToAction("Edit");
            }
            else
            {
                string photoName = Guid.NewGuid().ToString();
                photoName += Path.GetExtension(change.image.FileName);
                change.image.SaveAs(Server.MapPath("~/Content/" + photoName));
                change.fPhoto_Member = "../Content/" + photoName;
                oldData.fPhoto_Member = change.fPhoto_Member;
                dbFundaytrip.SaveChanges();
                return RedirectToAction("Edit");
            }
        }

    }
}