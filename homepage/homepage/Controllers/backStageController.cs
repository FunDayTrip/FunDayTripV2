using homepage.Models;
using homepage.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace homepage.Controllers
{
    public class backStageController : Controller
    {
        //帶ad_id通過回傳
        public ActionResult pass(string id)
        {
            new CAdFactory().getPass(id);
            Session["pass"] = "pass";
            return RedirectToAction("showApplyAD");
        }
        //顯示詳細廣告
        public ActionResult showAdDetail(string id)
        {
            CAdDetailVM item = (new CAdFactory()).getAdDetail(id);
            return View(item);
        }
        //待審核廣告列表
        public ActionResult showApplyAD()
        {
            List<tAd> applyAD;
            applyAD = (new CAdFactory()).getApplyAD();

            if (applyAD.Count == 0)
            {
                Session["nothing"] = "nothing";
                return RedirectToAction("indexBack");
            }
            else
            {
                return View(applyAD);
            }
        }
//------------------------------------------------------------------------------------
        //說明:檢舉會員回到搜尋會員
        public ActionResult backToAllMembers()
        {
            Session["searchResult"] = null;
            Session["ok"] = null;

            return RedirectToAction("showAllMembers");
        }

        //說明:秀被點擊的檢舉會員
        public ActionResult findReportedMember(string rId)
        {
            if (string.IsNullOrEmpty(rId))
            {
                return RedirectToAction("reportedList");
            }

            List<tMember> searchResult = (new CReportFactory()).getReportedMember(rId);
            Session["searchResult"] = searchResult;
            return RedirectToAction("showAllMembers");
        }

        //說明:秀被檢舉名單
        public ActionResult reportedList()
        {
            List<tReport> reportedMember = (new CReportFactory()).getAllReport();
            if (reportedMember.Count != 0)
            {
                return View(reportedMember);
            }
            return RedirectToAction("showAllMembers");
        }

        //審核控制器
        public ActionResult disappearData(string id)
        {
            string result = "";
            result = (new CReportFactory().getDisappear(id));
            Session["disappered"] = result;
            return RedirectToAction("reportedList");
        }

        //說明:停權控制器
        public ActionResult suspendAccountController(string id)
        {
            string result = "";
            result = (new CMemberFactory().suspendMember(id));
            Session["ok"] = result;
            return RedirectToAction("showAllMembers");
        }

        //說明:搜尋會員主畫面
        public ActionResult showAllMembers()
        {
            string key = Request.Form["keyword"];
            List<tMember> Members = null;

            if (string.IsNullOrEmpty(key))
            {
                Members = (new CMemberFactory()).getAllMember();
                if (Session["ok"] != null)
                {
                    ViewBag.suspensionOK = Session["ok"];
                }
                else if (Session["searchResult"] != null)
                {
                    return View(Session["searchResult"]);
                }
            }
            else
            {
                List<tMember> result = null;
                result = (new CMemberFactory()).getByKeyword(key);
                if (result.Count != 0)
                {
                    Members = result;
                }
                else
                {
                    ViewBag.nothing = "查無資訊";
                }
            }
            return View(Members);
        }

        public ActionResult indexBack()
        {
            return View();
        }
    }
}