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

    }
}