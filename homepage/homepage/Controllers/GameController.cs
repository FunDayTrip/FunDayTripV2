using homepage.Models;
using homepage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
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
        public JsonResult get()
        {
            var games = new CGameFactory().getNavAll();
            return Json(games, JsonRequestBehavior.AllowGet);           

        }

        public ActionResult post(int group_id, int role_id)
        {
            CGameFactory factory = new CGameFactory();
            CGameNavigationViewModel status = factory.getStatus(group_id,role_id);
           
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult put(int group_id, int role_id, int finish)
        {
            CGameFactory factory = new CGameFactory();
            bool isFinish = factory.updateGameRecord(role_id, group_id, finish);
            //var record = from q in db.tGameRecords
            //             where q.fId_GameGroup == group_id && q.fId_Role == role_id
            //             select q;


            //if (factory.getGamesById(group_id).Count > finish)
            //{
            //    record.FirstOrDefault().fOrder_Game = finish;
            //}
            //else
            //{
            //    record.FirstOrDefault().fOrder_Game = 0;
            //}

            //db.SaveChanges();

            CGameNavigationViewModel status = factory.getStatus(group_id, role_id);
            if (isFinish)
            {
                status.fIsPass_GameNav = 1;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult checkQA(int role_id, int group_id, int game_id,int answer)
        {
            CGameFactory factory = new CGameFactory();
            CGame qa = factory.get1GameById(game_id);
            CCheckQAGameViewModel result = new CCheckQAGameViewModel();

            if (qa != null)
            {
                
                if(answer == qa.cGameQA.fAnswer_GameQA)//正確
                {
                    result.fMessage_CheckQA = "答對了!";
                    result.fResult_CheckQA = 1;
                    result.fCorrect_CheckQA = true;
                    int finish = qa.fOrder_Game;

                }
                else //錯誤
                {
                    result.fMessage_CheckQA = "答錯了!";
                    result.fResult_CheckQA = 0;
                    result.fCorrect_CheckQA = false;

                }

            }
            else
            {
                result.fMessage_CheckQA = "這個問題不存在!";
                result.fResult_CheckQA = 0;
                result.fCorrect_CheckQA = false;

            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }


    }
}