using homepage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace homepage.Controllers
{
    public class AdvertisementController : Controller
    {
        public ActionResult newCommercialData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult newCommercialData(CAd data)
        {
            DB_FunDayTripEntities db = new DB_FunDayTripEntities();

            if (data.image == null)
            {
                Session["noPhoto"] = "noPhoto";
                return View();
            }

            else if (data.fSolution_Ad == "x") 
            {
                Session["x"] = "x";
                return View();
            }

            else
            {
                if (Session[CDictionary.SK_ActiveRoleId].ToString() == "")
                {
                    return RedirectToAction("index");
                }

                string photoName = Guid.NewGuid().ToString();
                photoName += Path.GetExtension(data.image.FileName);
                data.image.SaveAs(Server.MapPath("~/Content/" + photoName));
                data.fPhoto_Ad = "../Content/" + photoName;

                tAd tad = new tAd();
                tad.fId_Role = Convert.ToInt32(Session[CDictionary.SK_ActiveRoleId]);
                tad.fId_Admin_Role = 7;
                tad.fPhoto_Ad = data.fPhoto_Ad;
                tad.fId_Location = "l1";
                tad.fContent_Ad = data.fContent_Ad;
                tad.fSolution_Ad = data.fSolution_Ad;
                tad.fStatus_Ad = "審核中";

                db.tAds.Add(tad);
                db.SaveChanges();

                Session["ADok"] = "ok";

                return View();
            }
        }
    }
}