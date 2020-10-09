using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;

namespace homepage.Models
{
    public class CGameFactory
    {
        DB_FunDayTripEntities db = new DB_FunDayTripEntities();
        public List<CGameGroup> getAll()
        {
            var q = from g in db.tGameGroups
                    select g;

            List<CGameGroup> g_group = new List<CGameGroup>();
            foreach(var item in q.ToList())
            {
                CGameGroup game = new CGameGroup();
                game.fId_GameGroup = item.fId_GameGroup;
                game.fName_GameGroup = item.fName_GameGroup;
                game.fDescription_GameGroup = item.fDescript_GameGroup;
                game.fId_Route = item.fId_Route;
                game.fPhoto_GameGroup = item.fPhoto_GameGroup;
                g_group.Add(game);
            }

            return g_group;
        }

        public List<CGameNavigationViewModel> getNavAll()
        {
            List<CGameNavigationViewModel> nav_list = new List<CGameNavigationViewModel>();
            foreach(var item in getAll())
            {
                CGameNavigationViewModel nav = new CGameNavigationViewModel();
                nav.fGroup_GameNav = item;
                nav.fItems_GameNav = getGamesById(item.fId_GameGroup);
                nav_list.Add(nav);
            }
            return nav_list;
        }

        public CGameGroup getGroupById(int group_id)
        {
            CGameGroup group = getAll().FirstOrDefault(item => item.fId_GameGroup == group_id);
            return group;
        }
        public CGameNavigationViewModel getNavById(int group_id)
        {
            CGameNavigationViewModel nav = new CGameNavigationViewModel();
            nav.fGroup_GameNav = getGroupById(group_id);
            nav.fItems_GameNav = getGamesById(group_id);
            nav.fPath_GameNav = (from p in db.tRoutes
                                 where p.fId_Route == nav.fGroup_GameNav.fId_Route
                                 select p.fPath_Route).FirstOrDefault();
            return nav;
        }
        public List<CGame> getGamesById(int group_id)
        {
            var q = from g in db.tGames
                    where g.fId_GameGroup == group_id
                    orderby g.fOrder_Game ascending
                    select g;

            List<CGame> game_list = new List<CGame>();
            foreach(var item in q.ToList())
            {
                CGame game = new CGame();
                game.fId_Game = item.fId_Game;
                game.fId_Coordinate = item.fId_Coordinate;

                var co = from c in db.tCoordinates
                        where c.fId_Coordinate == item.fId_Coordinate
                        select c;

                game.fX_Coordinate = co.FirstOrDefault().fX_Coordinate;
                game.fY_Coordinate = co.FirstOrDefault().fY_Coordinate;
                game.fId_GameGroup = item.fId_GameGroup;
                game.fName_Game = item.fName_Game;
                game.fType_Game = item.fType_Game;

                switch (game.fType_Game) 
                {
                    case 1:
                        game.cGamesteps = getSteps(game.fId_Game);
                        break;
                    case 2:
                        game.cGameQA = getQA(game.fId_Game);
                        break;                
                }
                game.fAR_Game = item.fAR_Game;
                game.fSource_Game = item.fSource_Game;
                game.fOrder_Game = item.fOrder_Game;                

                game_list.Add(game);
            }
            return game_list;
        }
        public CGameRecord getGameRecord(int group_id, int role_id)
        {
            var q = from r in db.tGameRecords
                    where r.fId_Role == role_id && r.fId_GameGroup == group_id
                    select r;

            if (q.Any())
            {
                var record_table =  q.FirstOrDefault();
                CGameRecord record = new CGameRecord
                {
                    fId_GameGroup = (int)record_table.fId_GameGroup,
                    fId_Role = record_table.fId_Role,
                    fTime_GameRecord = (DateTime)record_table.fTime_GameRecord,
                    fTime_Readable_GameRecord = record_table.fTime_GameRecord.ToString(),
                    fFinished_GameRecord = record_table.fFinished_GameRecord,
                    fOrder_Game = record_table.fOrder_Game
                };


                return record;
            }
            else
            {
                return createGameRecord(role_id, group_id);
            }

        }
        public CGameRecord createGameRecord(int role_id, int group_id)
        {
            var q = from r in db.tGameRecords
                    where r.fId_Role == role_id && r.fId_GameGroup == group_id
                    select r;

            tGameRecord record = new tGameRecord
            {
                fId_Role = role_id,
                fId_GameGroup = group_id,
                fOrder_Game = 0,
                fTime_GameRecord = DateTime.Now,
                fFinished_GameRecord = 0,
            };

            CGameRecord recordforinfo = new CGameRecord
            {
                fId_Role = record.fId_Role,
                fId_GameGroup = record.fId_GameGroup,
                fOrder_Game = record.fOrder_Game,
                fTime_GameRecord = (DateTime)record.fTime_GameRecord,
                fTime_Readable_GameRecord = record.fTime_GameRecord.ToString(),
                fFinished_GameRecord = 0,
            };

            db.tGameRecords.Add(record);
            db.SaveChanges();
            return recordforinfo;
        }

        public bool updateGameRecord(int role_id, int group_id, int finish)
        {
            var q = from r in db.tGameRecords
                    where r.fId_Role == role_id & r.fId_GameGroup == group_id
                    select r;

            var q_game = getGamesById(group_id);
            
            bool isFinish = false;
            if (q.Any())
            {
                if(q_game.Count > finish)
                {
                    q.FirstOrDefault().fOrder_Game = finish;
                }
                else //完成遊戲
                {
                    q.FirstOrDefault().fOrder_Game = 0;
                    q.FirstOrDefault().fFinished_GameRecord += 1;
                    isFinish = true;
                }    

            }
            db.SaveChanges();
            return isFinish;
        }

        public CGameSteps getSteps(int game_id)
        {
            var q = (from g in db.tGameSteps
                     where g.fId_Game == game_id
                     select g).FirstOrDefault();
            CGameSteps steps = new CGameSteps
            {
                fId_Game = q.fId_Game,
                fId_GameStep = q.fId_GameStep,
                fTitle_GameStep = q.fTitle_GameStep,
                fContent_GameStep = q.fContent_GameStep,
                fReward_GameStep = q.fReward_GameStep                
            };
            return steps;
        }

        public CGameQA getQA(int game_id)
        {
            var q = (from g in db.tGameQAs
                     where g.fId_Game == game_id
                     select g).FirstOrDefault();
            CGameQA qa = new CGameQA
            {
                fId_Game = q.fId_Game,
                fId_GameQA = q.fId_GameQA,
                fQuestion_GameQA = q.fQuestion_GameQA,
                fAnswer_GameQA = q.fAnswer_GameQA,
                fOption_1_GameQA = q.fOption_1_GameQA,
                fOption_2_GameQA = q.fOption_2_GameQA,
                fOption_3_GameQA = q.fOption_3_GameQA,
                fOption_4_GameQA = q.fOption_4_GameQA,
                fReward_GameQA = q.fReward_GameQA
            };
            return qa;
        }

        public CGameNavigationViewModel getStatus(int group_id, int role_id)
        {
            CGameFactory factory = new CGameFactory();
            CGameRecord record = factory.getGameRecord(group_id, role_id);
            CGameNavigationViewModel status = factory.getNavById(group_id);

            status.fPlaying_GameNav = (int)record.fId_GameGroup;
            status.fStatus_GameNav = (int)record.fOrder_Game;
            status.fItems_GameNav = new CGameFactory().getGamesById(group_id);

            return status;
        }

    }
}