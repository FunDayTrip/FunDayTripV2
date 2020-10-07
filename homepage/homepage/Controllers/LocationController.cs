using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace homepage.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            return View();
        }


        DB_FunDayTripEntities dbFundaytrip = new DB_FunDayTripEntities();
        public string DeleteThisLoc(string LocationID)
        {


            tLocation ALocation = (from l in dbFundaytrip.tLocations
                                   where l.fId_Location == LocationID
                                   select l).FirstOrDefault();
            ALocation.fDelete_Location = 1;
            dbFundaytrip.SaveChanges();
            return "delete sucess";
        }
    }
}