
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

namespace homepage.Controllers
{
    public class HomeController : Controller
    {
        DB_FunDayTripEntities dbFundaytrip = new DB_FunDayTripEntities();
        CLoginViewModel loginMember = new CLoginViewModel();


        CMapItem mapItem = new CMapItem();
        // GET: Home _verna_0930
        public ActionResult Index()
        {
            List<tLocation> location = (from s in dbFundaytrip.tLocations.AsEnumerable()
                                        where s.fId_Coordinate == s.tCoordinate.fId_Coordinate && s.fDelete_Location == 0
                                        join s2 in dbFundaytrip.tPhotoes on s.fId_Location equals s2.fId_Location
                                        select s).ToList();

            List<CLocation> clocations = new List<CLocation>();

            foreach (var item in location)
            {
                CLocation clocation = new CLocation(item);
                clocations.Add(clocation);
            }

            List<tRoute> route = (from l in dbFundaytrip.tRoutes.AsEnumerable()
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
      
        //public string login(string email, string pwd)
        //{
        //    string loginMsg = "";

        //    if (email == "0000" && pwd == "0000" )
        //    {
        //        loginMsg = "登入成功";
        //    }            
        //    else
        //    {
        //        loginMsg = "登入失敗";
        //    }

        //    return loginMsg;
        //}
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


        //載入所有路線//verna_0930
        public JsonResult getAllRoutes()
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
            createlocation.fId_Role = 3;
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

        //k的getloc
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
        //k-getRoute
        public JsonResult GetRoute(string RouteID)
        {
            tRoute Route = (from s in dbFundaytrip.tRoutes
                            where s.fId_Route == RouteID
                            select s).FirstOrDefault();
            CRoute cRoute = new CRoute(Route);

            return Json(cRoute);
        }
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
    }

}