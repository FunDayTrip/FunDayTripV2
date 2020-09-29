using homepage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace homepage.Controllers
{
    public class PointController : Controller
    {
        DB_FunDayTripEntities dB_FunDayTrip = new DB_FunDayTripEntities();
        // GET: Point
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult get(int role_id)
        {
            List<CPoint> point_list = new List<CPoint>();

            var q = (from p in dB_FunDayTrip.tPoints
                     where p.fId_Role == role_id
                     orderby p.fTime_Point descending
                     select p).ToList();

            foreach(var tpo in q)
            {
                CPoint cpo = new CPoint
                {
                    fId_Point = tpo.fId_Point,
                    fId_Role = tpo.fId_Role,
                    fPoint_Point = tpo.fPoint_Point,
                    fType_Point = tpo.fType_Point,
                    fTime_Point = tpo.fTime_Point,
                    fTimeReadable_Point = Convert.ToString(tpo.fTime_Point)
                };

                point_list.Add(cpo);
            }
            return Json(point_list,JsonRequestBehavior.AllowGet);

        }
    }
}