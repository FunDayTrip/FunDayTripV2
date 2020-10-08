using homepage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;


namespace homepage.Controllers
{
    public partial class GameController : Controller
    {
        DB_FunDayTripEntities db = new DB_FunDayTripEntities();
        // GET: Game
        public ActionResult Navigation()
        {
            List<CGameNavigationViewModel> game_list = new CGameFactory().getNavAll();
            return View(game_list);
        }
        public ActionResult AR()
        {
            return View();
        }
        public JsonResult get(int group_id)
        {
            if (group_id == 0)
            {
                var games = new CGameFactory().getNavAll();
                return Json(games, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var games = new CGameFactory().getNavById(group_id);
                return Json(games, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult post(int group_id, int role_id)
        {
            CGameFactory factory = new CGameFactory();
            CGameRecord record = factory.getGameRecord(group_id,role_id);
            CGameNavigationViewModel status = factory.getNavById(group_id);

            status.fPlaying_GameNav = (int)record.fId_GameGroup;
            status.fStatus_GameNav = (int)record.fOrder_Game;
            status.fItems_GameNav = new CGameFactory().getGamesById(group_id);

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult put(int group_id, int role_id, int finish)
        //{
        //    CGameFactory factory = new CGameFactory();

        //    var record = from q in db.tGameRecords
        //                 where q.fId_GameGroup == group_id && q.fId_Role == role_id
        //                 select q;
        //    record.FirstOrDefault().fOrder_Game = finish;

        //}
    }
}