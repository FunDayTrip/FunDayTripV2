
using homepage.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
using System.Web.Helpers;
using homepage.Models;
using System.Text;

namespace homepage.Controllers
{
    public class HomeController : Controller
    {
        DB_FunDayTripEntities dbFundaytrip = new DB_FunDayTripEntities();
        CLoginViewModel loginMember = new CLoginViewModel();
        string key = "**********";

        [HttpPost]
        public JsonResult myRoutesshow(int data)
        {
            List<tRoute> route = (from l in dbFundaytrip.tRoutes.Where(entity => entity.fId_Role == data).AsEnumerable()
                                  select l).ToList();

            List<CRoute> croutes = new List<CRoute>();

            foreach (var item in route)
            {
                CRoute croute = new CRoute(item);
                croutes.Add(croute);
            }
            return Json(croutes);

        }

        [HttpPost]
        public JsonResult myLocationsshow(int data)
        {
            List<tLocation> location = (from l in dbFundaytrip.tLocations.Where(entity => entity.fId_Role == data).AsEnumerable()
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

        [HttpPost]
        public JsonResult myRount(int data)
        {
            List<tRoute> route = (from l in dbFundaytrip.tRoutes.Where(entity => entity.fId_Role == data).AsEnumerable()
                                  select l).ToList();
            List<CRoute> croutes = new List<CRoute>();
            foreach (var item in route)
            {
                CRoute croute = new CRoute(item);
                croutes.Add(croute);
            }
            return Json(croutes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult myLocation(int data)
        {
            List<tLocation> location = (from l in dbFundaytrip.tLocations.Where(entity => entity.fId_Role == data).AsEnumerable()
                                        select l).ToList();
            List<CLocation> clocations = new List<CLocation>();

            foreach (var item in location)
            {
                CLocation clocation = new CLocation(item);
                clocations.Add(clocation);
            }
            return Json(clocations, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAllRoutesshow(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                List<tRoute> route = (from l in dbFundaytrip.tRoutes.AsEnumerable()
                                      select l).ToList();

                List<CRoute> croutes = new List<CRoute>();

                foreach (var item in route)
                {
                    CRoute croute = new CRoute(item);
                    croutes.Add(croute);
                }
                return Json(croutes);
            }
            else
            {
                List<tRoute> route = (from l in dbFundaytrip.tRoutes.Where(entity => entity.fName_Route.Contains(data)).AsEnumerable()
                                      select l).ToList();

                List<CRoute> croutes = new List<CRoute>();

                foreach (var item in route)
                {
                    CRoute croute = new CRoute(item);
                    croutes.Add(croute);
                }
                return Json(croutes);
            }
        }

        [HttpPost]
        public JsonResult getAllLocationsshow(string data)
        {
            if (string.IsNullOrEmpty(data))
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
            else
            {
                List<tLocation> location = (from l in dbFundaytrip.tLocations.Where(entity => entity.fName_Location.Contains(data)).AsEnumerable()
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
        [HttpPost]
        public JsonResult SearchLocation(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                List<tLocation> location = (from l in dbFundaytrip.tLocations.AsEnumerable()
                                            select l).ToList();
                List<CLocation> clocations = new List<CLocation>();

                foreach (var item in location)
                {
                    CLocation clocation = new CLocation(item);
                    clocations.Add(clocation);
                }
                return Json(clocations, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<tLocation> location = (from l in dbFundaytrip.tLocations.Where(entity => entity.fName_Location.Contains(data)).AsEnumerable()
                                            select l).ToList();
                List<CLocation> clocations = new List<CLocation>();
                foreach (var item in location)
                {
                    CLocation clocation = new CLocation(item);
                    clocations.Add(clocation);
                }

                mapItem.LocationList = clocations;
                return Json(clocations, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult SearchRount(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                List<tRoute> route = (from l in dbFundaytrip.tRoutes.AsEnumerable()
                                      select l).ToList();
                List<CRoute> croutes = new List<CRoute>();
                foreach (var item in route)
                {
                    CRoute croute = new CRoute(item);
                    croutes.Add(croute);
                }
                return Json(croutes, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<tRoute> route = (from l in dbFundaytrip.tRoutes.Where(entity => entity.fName_Route.Contains(data)).AsEnumerable()
                                      select l).ToList();
                List<CRoute> croutes = new List<CRoute>();
                foreach (var item in route)
                {
                    CRoute croute = new CRoute(item);
                    croutes.Add(croute);
                }
                return Json(croutes, JsonRequestBehavior.AllowGet);
            }

        }

        CMapItem mapItem = new CMapItem();
        // GET: Home _verna_0930
        public ActionResult Index(string trancId)
        {
            List<tLocation> location = (from s in dbFundaytrip.tLocations.Where(entity => entity.fName_Location.Contains(key)).AsEnumerable()
                                        where s.fId_Coordinate == s.tCoordinate.fId_Coordinate && s.fDelete_Location == 0
                                        join s2 in dbFundaytrip.tPhotoes on s.fId_Location equals s2.fId_Location
                                        select s).ToList();

            List<CLocation> clocations = new List<CLocation>();

            foreach (var item in location)
            {
                CLocation clocation = new CLocation(item);
                clocations.Add(clocation);
            }

            List<tRoute> route = (from l in dbFundaytrip.tRoutes.Where(entity => entity.fName_Route.Contains(key)).AsEnumerable()
                                  select l).ToList();

            List<CRoute> croutes = new List<CRoute>();

            foreach (var item in route)
            {
                CRoute croute = new CRoute(item);
                croutes.Add(croute);
            }

            mapItem.LocationList = clocations;
            mapItem.RouteList = croutes;

            return View(mapItem);
        }

        [HttpPost]
        public ActionResult googlelogin(string email, string nickname, string firstname, string lastname, string photo)
        {
            var q = (from m in dbFundaytrip.tMembers
                     where m.fEmail_Member == email
                     select m);

            if (!q.Any())
            {
                CMember user = new CMember
                {
                    fEmail_Member = email,
                    fNickName_Member = nickname,
                    fFirstName_Member = firstname,
                    fLastName_Member = lastname,
                    fPhoto_Member = photo
                };
                MemberController member = new MemberController();
                member.signUpFromGoogle(user);
            }
            else
            {
                loginMember.loginMessage = "歡迎回來! " + q.FirstOrDefault().fNickName_Member;
                loginMember.fId_Member = q.FirstOrDefault().fId_Member;
                loginMember.fNickName_Member = q.FirstOrDefault().fNickName_Member;
                loginMember.fEmail_Member = q.FirstOrDefault().fEmail_Member;
                loginMember.fPassword_Member = q.FirstOrDefault().fPassword_Member;
                //讀取角色
                loginMember.fId_FuntionAuth_Member = q.FirstOrDefault().fId_FunctionAuth;

                List<CRole> rolesList = new CRolesFactory().getRoleList(loginMember.fId_Member);
                loginMember.fActiveRoleId_Member = rolesList.FirstOrDefault(a => a.fId_Master_Role == loginMember.fId_Member).fId_Role;
                loginMember.fActiveRoleName_Member = rolesList.FirstOrDefault(a => a.fId_Master_Role == loginMember.fId_Member).fNickName_Role;

                List<tNote> notesList = new CNotesFactory().readNotes(loginMember.fActiveRoleId_Member);
                loginMember.fNotesCount_Member = notesList.Count(x => x.fIsRead_Note == 0);
                //讀取點數
                var member_point = new CPointFactory().getPoint(loginMember.fActiveRoleId_Member);
                loginMember.fPointTotal_Member = Convert.ToInt32(member_point.Sum(s => s.fPoint_Point));

                //放入Session
                Session[CDictionary.SK_MemberId] = loginMember.fId_Member;
                Session[CDictionary.SK_MemberLogin] = loginMember;
                Session[CDictionary.SK_ActiveRoleId] = loginMember.fActiveRoleId_Member;
                Session[CDictionary.SK_ActiveRoleName] = loginMember.fActiveRoleName_Member;
                Session[CDictionary.SK_AunctionAuth] = 1;
            }
            return Json(loginMember);
        }


        [HttpPost]
        public ActionResult login(string email, string pwd)
        {
            var q = from m in dbFundaytrip.tMembers
                    where m.fEmail_Member == email
                    select m;

            //CLoginViewModel loginMember = new CLoginViewModel();

            if (!q.Any())
            {
                loginMember.loginMessage = "無此帳號";
            }
            else if (q.FirstOrDefault().fPassword_Member == pwd)
            {
                loginMember.loginMessage = "歡迎回來! " + q.FirstOrDefault().fNickName_Member;
                loginMember.fId_Member = q.FirstOrDefault().fId_Member;
                loginMember.fNickName_Member = q.FirstOrDefault().fNickName_Member;
                loginMember.fEmail_Member = q.FirstOrDefault().fEmail_Member;
                loginMember.fPassword_Member = q.FirstOrDefault().fPassword_Member;
                //讀取會員權限 by 郭松明
                loginMember.fId_FuntionAuth_Member = q.FirstOrDefault().fId_FunctionAuth;

                //讀取角色
                List<CRole> rolesList = new CRolesFactory().getRoleList(loginMember.fId_Member);
                loginMember.fActiveRoleId_Member = rolesList.FirstOrDefault(a => a.fId_Master_Role == loginMember.fId_Member).fId_Role;
                loginMember.fActiveRoleName_Member = rolesList.FirstOrDefault(a => a.fId_Master_Role == loginMember.fId_Member).fNickName_Role;
                //foreach (var rr in rolesList)
                //{
                //    rolesList.Add(rr);
                //}

                //讀取通知
                List<tNote> notesList = new CNotesFactory().readNotes(loginMember.fActiveRoleId_Member);
                loginMember.fNotesCount_Member = notesList.Count(x => x.fIsRead_Note == 0);
                //讀取點數
                var member_point = new CPointFactory().getPoint(loginMember.fActiveRoleId_Member);
                loginMember.fPointTotal_Member = Convert.ToInt32(member_point.Sum(s => s.fPoint_Point));
                
                //放入Session
                Session[CDictionary.SK_MemberId] = loginMember.fId_Member;
                Session[CDictionary.SK_MemberLogin] = loginMember;
                Session[CDictionary.SK_ActiveRoleId] = loginMember.fActiveRoleId_Member;
                Session[CDictionary.SK_ActiveRoleName] = loginMember.fActiveRoleName_Member;
                //新增會員權限by郭松明
                Session[CDictionary.SK_AunctionAuth] = loginMember.fId_FuntionAuth_Member;

            }
            else if (q.FirstOrDefault().fPassword_Member != pwd)
            {
                loginMember.loginMessage = "密碼錯誤";
            }
            else
            {
                loginMember.loginMessage = "登入失敗";
            }
            return Json(loginMember);
        }
        [HttpPost]
        public string logout()
        {
            CLoginViewModel logoutMember = new CLoginViewModel();
            logoutMember.loginMessage = "登出成功";
            Session[CDictionary.SK_MemberId] = CDictionary.SK_anonymous;
            Session[CDictionary.SK_MemberLogin] = logoutMember;

            return logoutMember.loginMessage;
        }
        [HttpPost]
        public JsonResult readNotes(int role_id)
        {
            var q = from n in dbFundaytrip.tNotes
                    where n.fId_Role == role_id
                    select n;

            List<CNotes> note = new List<CNotes>();
            foreach (var n in q.ToList())
            {
                CNotes nc = new CNotes();
                nc.fMessage_Note = n.fMessage_Note;
                note.Add(nc);
            }

            return Json(note.OrderByDescending(n => n.fTime_Note));
        }
        [HttpPost]
        public JsonResult readRoles(string member_id)
        {
            List<CRole> roles = new CRolesFactory().getRoleList(member_id);


            return Json(roles);
        }

        [HttpPost]
        public JsonResult changeRole(int role_id)
        {
            CRole rol = new CRolesFactory().getRole(role_id);

            Session[CDictionary.SK_ActiveRoleId] = rol.fId_Role;
            Session[CDictionary.SK_ActiveRoleName] = rol.fNickName_Role;
            return Json(rol);
        }
        public JsonResult showFollow(int role_id)
        {
            var q = from p in dbFundaytrip.tFollows
                    where p.fId_Self_Role == role_id
                    select p;


            List<CFollowlistViewModel> f = new List<CFollowlistViewModel>();
            foreach (var x in q.ToList())
            {
                CFollowlistViewModel FV = new CFollowlistViewModel();
                FV.follow_Self_Name = x.tRole1.fNickName_Role;
                FV.follow_Self_ID = x.tRole1.fId_Role;
                f.Add(FV);
            }
            return Json(f);
        }
        public JsonResult showFan(int role_id)
        {
            var q = from p in dbFundaytrip.tFollows
                    where p.fId_Target_Role == role_id
                    select p;
            

            List<CFanslistViewModel> f = new List<CFanslistViewModel>();
            foreach (var x in q.ToList())
            {
                CFanslistViewModel FN = new CFanslistViewModel();
                FN.follow_Target_Name = x.tRole.fNickName_Role;
                FN.follow_Target_ID = x.tRole.fId_Role;
                f.Add(FN);
            }
            return Json(f);
        }
        public void pushMessage(int fId_To_Role, int fId_From_Role, string message)
        {
           DB_FunDayTripEntities db =new DB_FunDayTripEntities();
            tMessage CM = new tMessage();



            CM.fId_To_Role = fId_To_Role;
            CM.fId_From_Role = fId_From_Role;
            CM.fMessage_Message = message;
            CM.fTime_Message = DateTime.Now;

            db.tMessages.Add(CM);

            db.SaveChanges();
        }
        public JsonResult showHistoryMessage(int from_role_id,int to_role_id)
        {
            var q = from p in dbFundaytrip.tMessages
                    where p.fId_From_Role == from_role_id && p.fId_To_Role == to_role_id
                    select p;
           
            List<CHistoryMessage> f = new List<CHistoryMessage>();            
            foreach (var x in q.ToList())
            {
                CHistoryMessage CH = new CHistoryMessage();
                CH.fMessage_From = x.fMessage_Message;
                CH.fMessage_Time = Convert.ToString(x.fTime_Message);
                CH.fId = x.fId_Message;
                f.Add(CH);
            }
            var q1 = from z in dbFundaytrip.tMessages
                     where z.fId_To_Role == from_role_id && z.fId_From_Role == to_role_id
                     select z;
            foreach (var y in q1.ToList())
            {
                CHistoryMessage CH = new CHistoryMessage();
                CH.fMessage_To = y.fMessage_Message;
                CH.fMessage_Time = Convert.ToString(y.fTime_Message);
                CH.fId = y.fId_Message;
                f.Add(CH);
            }
            var res = f.OrderBy(x => x.fId);

            return Json(res);
        }
        ///////////////////////////////////

        //ajax傳回所有地點_verna_0930
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


        //載入所有路線//verna_0930 kevin 10/06
        public JsonResult getAllRoutes()
        {
            List<tRoute> route = (from l in dbFundaytrip.tRoutes.AsEnumerable()
                                  where l.fDelete_Route==0
                                  select l).ToList();

            List<CRoute> croutes = new List<CRoute>();

            foreach (var item in route)
            {
                CRoute croute = new CRoute(item);
                croutes.Add(croute);
            }
            return Json(croutes);
        }
        //顯示點擊該地點的資料_verna_0930
        [HttpPost]
        public string CallBackThisLocationInfo(string locationID)
        {
            var LocDetail = from s in dbFundaytrip.tLocations
                            where s.fId_Location == locationID //地點
                            join s2 in dbFundaytrip.tPhotoes
                            on s.fId_Location equals s2.fId_Location
                            select new
                            {

                                s.tCoordinate.fName_Coordinate,
                                s.tCoordinate.fX_Coordinate,
                                s.tCoordinate.fY_Coordinate,
                                s.fId_Location,
                                s.fId_ShareAuth,
                                s.fDelete_Location,
                                s.fAdd_Location,
                                s.fDescript_Location,
                                s.fName_Location,
                                s.fTime_Location,
                                s2.fPath_Photo,
                                s2.fTime_Photo,
                                s2.fTitle_Photo,
                                s2.fDescript_Photo,

                            };
            string InfoJson = JsonConvert.SerializeObject(LocDetail);


            return InfoJson;
        }
      
        //新增地點
        public string createAlocation(tCoordinate createCoordinate, CCreateLocation loadPostPhoto, tLocation createlocation, tPhoto createphoto)
        {
            string alert = null;

            string postLocId = createlocation.fId_Location;


            //檢查是否有上傳照片
            string photoname = Guid.NewGuid().ToString(); //重新命名一個不會重複的照片ID進資料庫
            if (loadPostPhoto.PostImage == null)
            {
                alert = "photoIsnotUploaded";
                return alert;
            };


            dbFundaytrip.tCoordinates.Add(createCoordinate);
            dbFundaytrip.SaveChanges();

            int lastestCoordinateFid = (from i in dbFundaytrip.tCoordinates
                                        orderby i.fId_Coordinate descending
                                        select i.fId_Coordinate).FirstOrDefault();


            int sa = createlocation.fId_ShareAuth;
            //tLocation location = new tLocation();
            //createlocation.fId_Role 
            createlocation.fId_Coordinate = lastestCoordinateFid;
            createlocation.fId_ShareAuth = createlocation.fId_ShareAuth;
            createlocation.fId_Icon = createlocation.fId_ShareAuth;
            createlocation.fType_Location = "point";
            createlocation.fTime_Location = DateTime.Now;
            createlocation.fDelete_Location = createlocation.fDelete_Location;
            dbFundaytrip.tLocations.Add(createlocation);
            dbFundaytrip.SaveChanges();


            string lastestLocationFid = (from l in dbFundaytrip.tLocations
                                         orderby l.ID descending
                                         select l.fId_Location).FirstOrDefault();


            createphoto.fId_Location = lastestLocationFid;
            createphoto.fId_Role = 3;
            //照片路徑
            photoname += Path.GetExtension(loadPostPhoto.PostImage.FileName);//取得副檔名
            loadPostPhoto.PostImage.SaveAs(Server.MapPath("../Content/" + photoname)); //根目錄:~(不行),要用..回上一層
            createphoto.fPath_Photo = "../Content/" + photoname;
            //end
            createphoto.fTime_Photo = DateTime.Now;
            dbFundaytrip.tPhotoes.Add(createphoto);
            dbFundaytrip.SaveChanges();

            return alert;

        }


        //修改地點
        public string EditLocationInfo(tCoordinate createCoordinate, CCreateLocation loadPostPhoto, tLocation createlocation, tPhoto createphoto)
        {
            string locId = createlocation.fId_Location;


            string alert = null;


            tLocation ALocation = (from l in dbFundaytrip.tLocations
                                   where l.fId_Location == locId
                                   select l).FirstOrDefault();

            //ALocation.fId_Role = 25;
            ALocation.fId_ShareAuth = createlocation.fId_ShareAuth;
            ALocation.fId_Icon = createlocation.fId_ShareAuth;
            ALocation.fName_Location = createlocation.fName_Location;
            ALocation.fAdd_Location = createlocation.fAdd_Location;
            ALocation.fDescript_Location = createlocation.fDescript_Location;
            ALocation.fDelete_Location = createlocation.fDelete_Location;


            //存座標,地點,照片
            //座標

            ALocation.tCoordinate.fName_Coordinate = createCoordinate.fName_Coordinate;
            //ALocation.tCoordinate.fX_Coordinate = createCoordinate.fX_Coordinate;
            //ALocation.tCoordinate.fY_Coordinate = createCoordinate.fY_Coordinate;

            tPhoto Aphoto = (from p in dbFundaytrip.tPhotoes
                             where p.fId_Location == locId
                             select p).FirstOrDefault();
            Aphoto.fTitle_Photo = createphoto.fTitle_Photo;
            Aphoto.fDescript_Photo = createphoto.fDescript_Photo;

            //檢查是否有修改照片  
            string photoname = Guid.NewGuid().ToString(); //重新命名一個不會重複的照片ID進資料庫
            if (loadPostPhoto.PostImage != null)
            {
                photoname += Path.GetExtension(loadPostPhoto.PostImage.FileName);//取得副檔名
                loadPostPhoto.PostImage.SaveAs(Server.MapPath("../Content/" + photoname)); //根目錄:~(不行),要用..回上一層
                Aphoto.fPath_Photo = "../Content/" + photoname;

            };

            dbFundaytrip.SaveChanges();


            return alert;
        }

        //===== 獲得地點 by kevin 10/06 =====//
        public JsonResult GetLocation(string LocationID)
        {
            List<tLocation> location = (from l in dbFundaytrip.tLocations.AsEnumerable()
                                        where l.fId_Location == LocationID
                                        select l).ToList();

            List<CLocation> clocations = new List<CLocation>();

            foreach (var item in location)
            {
                CLocation clocation = new CLocation(item);
                clocations.Add(clocation);
            }
            return Json(clocations);
        }

        //===== 獲得路線 by kevin 10/06 =====//
        public JsonResult GetRoute(string RouteID)
        {
            tRoute Route = (from s in dbFundaytrip.tRoutes
                            where s.fId_Route == RouteID
                            select s).FirstOrDefault();
            CRoute cRoute = new CRoute(Route);

            return Json(cRoute);
        }

        //===== 獲得路線中的地點 by kevin 10/06 =====//
        public JsonResult RouteGetPoint(string RouteID)
        {
            List<tLR_Relation> RelationList = (from s in dbFundaytrip.tLR_Relation
                                               where s.fId_Route == RouteID
                                               select s).ToList();

            List<CLocation> cLocations = new List<CLocation>();

            foreach (var item in RelationList)
            {
                tLocation location = (from s in dbFundaytrip.tLocations
                                      where s.fId_Location == item.fId_Location
                                      select s).FirstOrDefault();
                CLocation cspot = new CLocation(location);
                cLocations.Add(cspot);
            }
            return Json(cLocations);
        }

        //===== 獲得路線中的地點照片 by kevin 10/06 =====//
        public JsonResult GetLocationPhoto(string LocationID)
        {
            List<tPhoto> photo = (from p in dbFundaytrip.tPhotoes
                                  where p.fId_Location == LocationID
                                  select p).ToList();

            List<CPhoto> photos = new List<CPhoto>();

            foreach (var item in photo)
            {
                CPhoto cPhoto = new CPhoto(item);
                photos.Add(cPhoto);
            }

            return Json(photos);
        }
        
        //===== 判斷點擊地是否有已知坐標 by kevin 10/06 =====//
        public JsonResult SearchXY(decimal x, decimal y)
        {
            CCoordinate cxy;
            decimal offset = 0.000225M;
            tCoordinate xy = (from c in dbFundaytrip.tCoordinates
                              where c.fX_Coordinate < x + offset && c.fX_Coordinate > x - offset
                                 && c.fY_Coordinate < y + offset && c.fY_Coordinate > y - offset
                              select c).FirstOrDefault();
            if (xy == null)
            {
                cxy = new CCoordinate();
            }
            else
            {
                cxy = new CCoordinate(xy);
            }

            return Json(cxy);
        }

        //===== 新增路線 by kevin 10/06 =====//
        public string createAroute(CCreateRouteAjax Coordinate, tRoute createroute, tLR_Relation createLRrelation)
        {
            string alert = null;

            //檢查是否有上傳照片           
            if (Coordinate.PostImages[0] == null)
            {
                alert = "photoIsnotUploaded";
                return alert;
            };

            //新增坐標
            tCoordinate createCoordinate = new tCoordinate();
            for (int i = 0; i < Coordinate.fX_Coordinate.Length; i++)
            {
                if (Coordinate.fId_Coordinate[i] != 0)
                {
                    createCoordinate.fId_Coordinate = Coordinate.fId_Coordinate[i];
                }
                else
                {
                    createCoordinate.fX_Coordinate = Coordinate.fX_Coordinate[i];
                    createCoordinate.fY_Coordinate = Coordinate.fY_Coordinate[i];
                    createCoordinate.fName_Coordinate = Coordinate.fName_Coordinate[i];
                    dbFundaytrip.tCoordinates.Add(createCoordinate);
                    dbFundaytrip.SaveChanges();
                }
            }

            //新增地點
            tLocation createlocation = new tLocation();
            tPhoto createphoto = new tPhoto();
            List<int> coordFid = new List<int>();
            for (int i = 0; i < Coordinate.fX_Coordinate.Length; i++)
            {
                decimal x = Convert.ToDecimal(Coordinate.fX_Coordinate[i]);
                decimal y = Convert.ToDecimal(Coordinate.fY_Coordinate[i]);

                decimal offset = 0.0000225M;
                coordFid.Add((from c in dbFundaytrip.tCoordinates.AsEnumerable()
                              where c.fX_Coordinate < x + offset && c.fX_Coordinate > x - offset
                             && c.fY_Coordinate < y + offset && c.fY_Coordinate > y - offset
                              select c.fId_Coordinate).FirstOrDefault());
                if (coordFid.Count == 0)
                    return "新增地點失敗";

                createlocation.fAdd_Location = Coordinate.fAdd_Location[i];
                createlocation.fDescript_Location = Coordinate.fDescript_Location[i];
                createlocation.fName_Location = Coordinate.fName_Location[i];

                createlocation.fId_Role = Coordinate.fId_Role;
                createlocation.fId_Coordinate = coordFid[i];
                createlocation.fId_ShareAuth = Coordinate.fId_ShareAuth[0];
                createlocation.fId_Icon = Coordinate.fId_ShareAuth[0];
                createlocation.fType_Location = "point";
                createlocation.fTime_Location = DateTime.Now;

                if (Coordinate.fDelete_Route != null)
                {
                    createlocation.fDelete_Location = (int)Coordinate.fDelete_Route;
                }
                
                dbFundaytrip.tLocations.Add(createlocation);
                dbFundaytrip.SaveChanges();
            }


            //新增照片
            int tempi = 0;
            foreach (int item in coordFid)
            {
                List<string> lastestLocationFid = (from l in dbFundaytrip.tLocations
                                                   where l.fId_Coordinate == item
                                                   select l.fId_Location).ToList();

                string photoname = Guid.NewGuid().ToString(); //重新命名一個不會重複的照片ID進資料庫
                createphoto.fId_Location = lastestLocationFid[0];
                createphoto.fId_Role = 3;
                //照片路徑
                photoname += Path.GetExtension(Coordinate.PostImages[tempi].FileName);//取得副檔名
                Coordinate.PostImages[tempi].SaveAs(Server.MapPath("../Content/" + photoname)); //根目錄:~(不行),要用..回上一層
                createphoto.fPath_Photo = "../Content/" + photoname;
                createphoto.fTitle_Photo = Coordinate.fTitle_Photo[tempi];
                createphoto.fDescript_Photo = Coordinate.fDescript_Photo[tempi];
                createphoto.fTime_Photo = DateTime.Now;
                dbFundaytrip.tPhotoes.Add(createphoto);
                dbFundaytrip.SaveChanges();
                tempi++;
            }

            //新增路線
            createroute.fId_Role = Coordinate.fId_Role;
            createroute.fId_Icon = Coordinate.fId_ShareAuth[0];            
            createroute.fId_ShareAuth = Coordinate.fId_ShareAuth[0];
            createroute.fTime_Route = DateTime.Now;
            createroute.fType_Route = "line";
            createroute.fDescript_Route = Coordinate.fDescript_Route;
            createroute.fName_Route = Coordinate.fName_Route;
            createroute.fPath_Route = Coordinate.fPath_Route;

            if (Coordinate.fDelete_Route != null)
            {
                createroute.fDelete_Route = (int)Coordinate.fDelete_Route;
            }

            dbFundaytrip.tRoutes.Add(createroute);
            dbFundaytrip.SaveChanges();

            //新增地點路線關聯
            foreach (int item in coordFid)
            {
                int i = 0;
                List<string> lastestLocationFid = (from l in dbFundaytrip.tLocations
                                                   where l.fId_Coordinate == item
                                                   select l.fId_Location).ToList();

                string lastestRouteFid = (from t in dbFundaytrip.tRoutes
                                          orderby t.ID descending
                                          select t.fId_Route).FirstOrDefault();

                createLRrelation.fId_Location = lastestLocationFid[i];
                createLRrelation.fId_Route = lastestRouteFid;
                dbFundaytrip.tLR_Relation.Add(createLRrelation);
                dbFundaytrip.SaveChanges();
                i++;
            }
            return "checkfordbCoordinate";
        }

        //===== 編輯路線 by kevin 10/06=====//
        public string EditRouteInfo(CCreateRouteAjax Coordinate, tRoute createroute, tLR_Relation createLRrelation)
        {
            //修改坐標
            tCoordinate createCoordinate = new tCoordinate();

            for (int i = 0; i < Coordinate.fX_Coordinate.Length; i++)
            {
                var tempId = Coordinate.fId_Coordinate[i];
                var c = (from cs in dbFundaytrip.tCoordinates
                         where cs.fId_Coordinate == tempId
                         select cs).FirstOrDefault();

                if (Coordinate.fId_Coordinate[i] != 0)
                {
                    c.fId_Coordinate = Coordinate.fId_Coordinate[i];
                    c.fX_Coordinate = Coordinate.fX_Coordinate[i];
                    c.fY_Coordinate = Coordinate.fY_Coordinate[i];
                    c.fName_Coordinate = Coordinate.fName_Coordinate[i];
                    dbFundaytrip.SaveChanges();
                }
            }

            //修改地點
            List<int> tempcoordFid = new List<int>();

            for (int i = 0; i < Coordinate.fX_Coordinate.Length; i++)
            {
                decimal x = Convert.ToDecimal(Coordinate.fX_Coordinate[i]);
                decimal y = Convert.ToDecimal(Coordinate.fY_Coordinate[i]);

                decimal offset = 0.0000225M;
                createCoordinate = (from c in dbFundaytrip.tCoordinates
                                    where c.fX_Coordinate < x + offset && c.fX_Coordinate > x - offset
                                    && c.fY_Coordinate < y + offset && c.fY_Coordinate > y - offset
                                    select c).FirstOrDefault();

                var templid = createCoordinate.fId_Coordinate;
                tempcoordFid.Add(templid);

                tLocation editlocation = (from t in dbFundaytrip.tLocations
                                          where t.fId_Coordinate == templid
                                          select t).FirstOrDefault();

                editlocation.fAdd_Location = Coordinate.fAdd_Location[i];                                
                editlocation.fId_ShareAuth = createroute.fId_ShareAuth;
                editlocation.fId_Icon = createroute.fId_ShareAuth;
                editlocation.fDescript_Location = Coordinate.fDescript_Location[i];
                editlocation.fName_Location = Coordinate.fName_Location[i];
                editlocation.fTime_Location = DateTime.Now;
                
                if (Coordinate.fDelete_Route != null)
                {
                    editlocation.fDelete_Location = (int)Coordinate.fDelete_Route;
                }

                dbFundaytrip.SaveChanges();
            }

            //修改照片
            int temppi = 0;
            foreach (int item in tempcoordFid)
            {
                List<string> lastestLocationFid = (from l in dbFundaytrip.tLocations
                                                   where l.fId_Coordinate == item
                                                   select l.fId_Location).ToList();

                var templid = lastestLocationFid[0];

                tPhoto editphoto = (from p in dbFundaytrip.tPhotoes
                                    where p.fId_Location == templid
                                    select p).FirstOrDefault();

                string photoname = Guid.NewGuid().ToString(); //重新命名一個不會重複的照片ID進資料庫

                if (Coordinate.PostImages[temppi] != null)
                {
                    //照片路徑                   
                    photoname += Path.GetExtension(Coordinate.PostImages[temppi].FileName);//取得副檔名
                    Coordinate.PostImages[temppi].SaveAs(Server.MapPath("../Content/" + photoname)); //根目錄:~(不行),要用..回上一層
                    editphoto.fPath_Photo = "../Content/" + photoname;

                };//end

                editphoto.fDescript_Photo = Coordinate.fDescript_Photo[temppi];
                editphoto.fTitle_Photo = Coordinate.fTitle_Photo[temppi];
                editphoto.fTime_Photo = DateTime.Now;
                dbFundaytrip.SaveChanges();
                temppi++;
            }
            //修改路線
            tRoute editroute = (from r in dbFundaytrip.tRoutes
                                where r.fId_Route == createroute.fId_Route
                                select r).FirstOrDefault();
            //editroute.fDelete_Route = Convert.ToInt32(createroute.fDelete_Route);
            editroute.fId_ShareAuth = createroute.fId_ShareAuth;
            editroute.fDescript_Route = createroute.fDescript_Route;
            editroute.fName_Route = createroute.fName_Route;
            editroute.fPath_Route = createroute.fPath_Route;
            editroute.fTime_Route = DateTime.Now;

            if (Coordinate.fDelete_Route != null)
            {
                editroute.fDelete_Route = (int)createroute.fDelete_Route;
            }

            dbFundaytrip.SaveChanges();

            //修改地點路線關聯
            //============編輯時無法新增地點，並修改地點路線地點關聯，先註解掉============//
            //foreach (int item in tempcoordFid)
            //{
            //    int i = 0;
            //    List<string> lastestLocationFid = (from l in dbFundaytrip.tLocations
            //                                       where l.fId_Coordinate == item
            //                                       select l.fId_Location).ToList();

            //    //string lastestRouteFid = (from t in dbFundaytrip.tRoutes
            //    //                          orderby t.ID descending
            //    //                          select t.fId_Route).FirstOrDefault();

            //    var templrid = lastestLocationFid[i];

            //    tLR_Relation editlrrelation = (from lr in dbFundaytrip.tLR_Relation
            //                                  where lr.fId_Location == templrid
            //                                  select lr).FirstOrDefault();

            //    editlrrelation.fId_Location = createLRrelation.fId_Location;


            //    //createLRrelation.fId_Location = lastestLocationFid[i];
            //    //createLRrelation.fId_Route = lastestRouteFid;
            //    dbFundaytrip.SaveChanges();
            //    i++;
            //}
            return "修改成功";
        }

        // ===== 顯示相簿 by kevin 10/06 =====//
        public JsonResult showMyAlbums(int roleID)
        {
            List<tLocation> loclist = (from l in dbFundaytrip.tLocations
                                       where l.fId_Role == roleID
                                       select l).ToList();

            List<tLA_Relation> lalist = new List<tLA_Relation>();

            foreach (var item in loclist)
            {
                tLA_Relation las = (from la in dbFundaytrip.tLA_Relation
                                    where la.fId_Location == item.fId_Location
                                    select la).FirstOrDefault();
                if (las != null)
                    lalist.Add(las);
            }

            List<CAlbum> albumlist = new List<CAlbum>();
            foreach (var item in lalist)
            {
                bool AddAlbumEn = true;
                tAlbum albums = (from a in dbFundaytrip.tAlbums
                                 where a.fId_Album == item.fId_Album
                                 select a).FirstOrDefault();
                CAlbum cAlbum = new CAlbum(albums);

                for (int i = 0; i < albumlist.Count; i++)
                {
                    if (albumlist[i].fId_Album == cAlbum.fId_Album)
                    {
                        AddAlbumEn = false;
                        break;
                    }
                }
                if (albumlist.Count == 0 || AddAlbumEn)
                {
                    cAlbum.fPath_Photo = (from l in dbFundaytrip.tLocations.AsEnumerable()
                                          where l.fId_Location == item.fId_Location
                                          join p in dbFundaytrip.tPhotoes
                                          on l.fId_Location equals p.fId_Location
                                          select p.fPath_Photo).FirstOrDefault().ToString();
                    cAlbum.fId_Role = roleID;
                    cAlbum.fNickName_Role = (from r in dbFundaytrip.tRoles.AsEnumerable()
                                             where r.fId_Role == roleID
                                             select r.fNickName_Role).FirstOrDefault();
                    albumlist.Add(cAlbum);
                }
            }
            return Json(albumlist);
        }

        // ===== 相簿內地點 by kevin 10/06 =====//
        public JsonResult showAlbumDetail(int albumId)
        {
            List<tLA_Relation> lare = (from la in dbFundaytrip.tLA_Relation.AsEnumerable()
                                       where la.fId_Album == albumId
                                       select la).ToList();

            List<CLocation> cloc = new List<CLocation>();
            foreach (var item in lare)
            {
                tLocation q = (from l in dbFundaytrip.tLocations.AsEnumerable()
                               where l.fId_Location == item.fId_Location
                               select l).FirstOrDefault();

                CLocation cLocation = new CLocation(q);

                cLocation.fPath_Photo = (from l in dbFundaytrip.tLocations.AsEnumerable()
                                         where l.fId_Location == item.fId_Location
                                         join p in dbFundaytrip.tPhotoes
                                         on l.fId_Location equals p.fId_Location
                                         select p.fPath_Photo).FirstOrDefault().ToString();

                cloc.Add(cLocation);
            }
            return Json(cloc);
        }

        // ===== 相簿新增地點選項 by kevin 10/06 =====//
        public JsonResult showLocOptions(int roleID)
        {
            List<tLocation> loc = (from l in dbFundaytrip.tLocations.AsEnumerable()
                                   where l.fId_Role == roleID
                                   select l).ToList();
            List<CLocation> cloc = new List<CLocation>();
            foreach (var item in loc)
            {
                CLocation cLocation = new CLocation(item);
                cloc.Add(cLocation);
            }
            return Json(cloc);
        }

        // ===== 新增相簿 by kevin 10/06 =====//
        public string addAlbum(tLA_Relation tLA_Relation, tAlbum tAlbum, tLocation tLocation, CCreateAlbumAjax CCreateAlbumAjax)
        {
            tAlbum createAlbum = new tAlbum();
            createAlbum.fDelete_Album = 0;
            createAlbum.fDescript_Album = tAlbum.fDescript_Album;
            createAlbum.fId_ShareAuth = tAlbum.fId_ShareAuth;
            createAlbum.fName_Album = tAlbum.fName_Album;

            dbFundaytrip.tAlbums.Add(createAlbum);
            dbFundaytrip.SaveChanges();

            tLA_Relation createLArelation = new tLA_Relation();

            tAlbum als = (from a in dbFundaytrip.tAlbums
                          orderby a.fId_Album descending
                          select a).FirstOrDefault();

            tLocation locs = new tLocation();

            for (int i = 0; i < CCreateAlbumAjax.fId_Location.Length; i++)
            {
                var templid = CCreateAlbumAjax.fId_Location[i];
                locs = (from l in dbFundaytrip.tLocations
                        where l.fId_Location == templid
                        select l).FirstOrDefault();

                createLArelation.fId_Location = locs.fId_Location;
                createLArelation.fId_Album = als.fId_Album;
                dbFundaytrip.tLA_Relation.Add(createLArelation);
                dbFundaytrip.SaveChanges();
            }
            return "新增相簿成功";
        }

        // ===== 編輯相簿 by kevin 10/07 =====//
        public JsonResult editMyAlbums(int roleID, int albumID)
        {           
            tAlbum q = (from p in dbFundaytrip.tAlbums
                    where p.fId_Album == albumID
                    select p).FirstOrDefault();

            CAlbum albumlist = new CAlbum(q);
            return Json(albumlist);
        }

        // ===== 編輯相簿中的地點 by kevin 10/07 =====//
        public JsonResult editMyAlbumsLoc(int roleID, int albumID)
        {
            List<string> la = (from a in dbFundaytrip.tLA_Relation
                               where a.fId_Album == albumID
                               select a.fId_Location).ToList();

            List<CLocation> locationlist = new List<CLocation>();
            foreach (var item in la)
            {
                tLocation b = (from l in dbFundaytrip.tLocations
                               where l.fId_Location == item
                               select l).FirstOrDefault();
                CLocation loc = new CLocation(b);
                locationlist.Add(loc);
            }
            
            return Json(locationlist);
        }
        
    }

}