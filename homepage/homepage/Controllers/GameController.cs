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
        public JsonResult getGames()
        {
            var games = new CGameFactory().getNavAll();
            //games.FirstOrDefault().fItems_GameNav[0].fX_Coordinate;
            //games.FirstOrDefault().fGroup_GameNav.fId_GameGroup
            return Json(games,JsonRequestBehavior.AllowGet);
        }

    }
}