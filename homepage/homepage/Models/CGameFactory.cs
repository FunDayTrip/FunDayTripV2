using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                game.fOrder_Game = item.fOrder_Game;
                game_list.Add(game);
            }
            return game_list;
        }

    }
}