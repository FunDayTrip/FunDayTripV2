using homepage.Models;
using Newtonsoft.Json;
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
        DB_FunDayTripEntities db = new DB_FunDayTripEntities();
        public JsonResult ShowAD()
        {
            List<CAdJson> adLsit = new List<CAdJson>();
            
            var q = from photo in db.tAds
                    where photo.fStatus_Ad == "通過"
                    select photo;

            foreach(var item in q)
            {
                CAdJson ad = new CAdJson
                {
                    fId_Ad = item.fId_Ad,
                    fId_Role = item.fId_Role,
                    fId_Admin_Role = item.fId_Admin_Role,
                    fContent_Ad = item.fContent_Ad,
                    fId_Location = item.fId_Location,
                    fPhoto_Ad = item.fPhoto_Ad,
                    fSolution_Ad = item.fSolution_Ad,
                    fStatus_Ad = item.fStatus_Ad
                };

                adLsit.Add(ad);
            }

            return Json(adLsit,JsonRequestBehavior.AllowGet);
        }

        
       
        public ActionResult newCommercialData()
        {
            return View();
        }
        //儲存廣告
        [HttpPost]
        public ActionResult newCommercialData(CAd data)
        {
           
            //座標
            decimal Lng = Convert.ToDecimal( Session["sessionLng"]);
            decimal Lat = Convert.ToDecimal(Session["sessionLat"]);

            tCoordinate coordinateAd = new tCoordinate();
            coordinateAd.fX_Coordinate = Lng;
            coordinateAd.fY_Coordinate = Lat;
            db.tCoordinates.Add(coordinateAd);
            db.SaveChanges();

            int lastestCoordinateFid = (from i in db.tCoordinates
                                        orderby i.fId_Coordinate descending
                                        select i.fId_Coordinate).FirstOrDefault();


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
                //地點
                tLocation locationAD = new tLocation();
                locationAD.fId_Role = Convert.ToInt32(Session[CDictionary.SK_ActiveRoleId]);
                locationAD.fId_Icon = 3;
                locationAD.fId_ShareAuth = 3;
                locationAD.fId_Coordinate = lastestCoordinateFid;                
                locationAD.fType_Location = "point";
                locationAD.fTime_Location = DateTime.Now;
                locationAD.fName_Location = Convert.ToString(Session[CDictionary.SK_ActiveRoleName]); //商家名稱                
                locationAD.fDescript_Location = data.fContent_Ad;
                locationAD.fDelete_Location = 0;                
                db.tLocations.Add(locationAD);
                db.SaveChanges();

                //照片

                string photoName = Guid.NewGuid().ToString();
                photoName += Path.GetExtension(data.image.FileName);
                data.image.SaveAs(Server.MapPath("~/Content/" + photoName));
                data.fPhoto_Ad = "../Content/" + photoName;

                string lastestLocationFid = (from l in db.tLocations
                                             orderby l.ID descending
                                             select l.fId_Location).FirstOrDefault();
                tPhoto photoAd = new tPhoto();
                photoAd.fId_Location = lastestLocationFid;
                photoAd.fId_Role = Convert.ToInt32(Session[CDictionary.SK_ActiveRoleId]);
                photoAd.fTime_Photo = DateTime.Now;
                photoAd.fPath_Photo = data.fPhoto_Ad;
                db.tPhotoes.Add(photoAd);
                db.SaveChanges();

                //廣告

                tAd tad = new tAd();
                tad.fId_Role = Convert.ToInt32(Session[CDictionary.SK_ActiveRoleId]);             

                tad.fId_Admin_Role = 7;//有問題
                tad.fPhoto_Ad = data.fPhoto_Ad;
                tad.fId_Location = lastestLocationFid;
                tad.fContent_Ad = data.fContent_Ad;
                tad.fSolution_Ad = data.fSolution_Ad;
                tad.fStatus_Ad = "審核中";

                db.tAds.Add(tad);
                db.SaveChanges();

                Session["ADok"] = "ok";

                return RedirectToAction("Index", "Home");
            }
        }


        public string StashAdCoordinate(double lng , double lat) {

            Session["sessionLng"] = lng;
            Session["sessionLat"] = lat;
            return "stash done"+lat+lng;

        }
     

        
    }
}