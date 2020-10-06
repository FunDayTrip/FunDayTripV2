using homepage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace homepage.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic
        public ActionResult ShowHeatMap()
        {
            return View();
        }

        DB_FunDayTripEntities dbFundaytrip = new DB_FunDayTripEntities();

        //ajax傳回所有地點
        public JsonResult getAllLocations()
        {
            List<tLocation> location = (from l in dbFundaytrip.tLocations.AsEnumerable()
                                        where l.fDelete_Location == 0
                                        join s2 in dbFundaytrip.tPhotoes on l.fId_Location equals s2.fId_Location
                                        select l).ToList();

            List<CLocation> clocations = new List<CLocation>();

            foreach (var item in location)
            {
                CLocation clocation = new CLocation(item);
                clocations.Add(clocation);
            }
            return Json(clocations);
        }
    }
}