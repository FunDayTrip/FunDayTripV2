using homepage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using homepage.SignalR;
using Microsoft.AspNet.SignalR;

namespace homepage.Controllers
{
    public class NoteController : Controller
    {
        DB_FunDayTripEntities dbFundaytrip = new DB_FunDayTripEntities();
        // GET: Note
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult get(int role_id, bool read)
        {
            var q = from n in dbFundaytrip.tNotes
                    where n.fId_Role == role_id
                    orderby n.fTime_Note descending
                    select n;

            List<CNotes> note = new List<CNotes>();
            foreach (var n in q.ToList())
            {
                CNotes nc = new CNotes();
                nc.fId_Note = n.fId_Note;
                nc.fId_Role = n.fId_Role;
                nc.fId_Admin_Role = n.fId_Admin_Role;
                nc.fMessage_Note = n.fMessage_Note;
                nc.fTime_Note = n.fTime_Note;
                nc.fTimeReadable_Note = Convert.ToString(n.fTime_Note);
                nc.fIsRead_Note = n.fIsRead_Note;

                if (read)
                {
                    n.fIsRead_Note = 1;
                }

                note.Add(nc);
                dbFundaytrip.SaveChanges();
            }

            return Json(note,JsonRequestBehavior.AllowGet);
        }
        public ActionResult post(int role_id, string message, int admin_id)
        {
            //寄通知給所有人，輸入-1 ~1007 王詠平
            if (role_id == -1)
            {
                var q = from n in dbFundaytrip.tRoles
                        select n;
                List<tNote> notes = new List<tNote>();
                List<CNotes> frontNotes = new List<CNotes>(); 
                foreach (var role in q.ToList())
                {
                    tNote _note = new tNote
                    {
                        fId_Admin_Role = admin_id,
                        fId_Role = role.fId_Role,
                        fMessage_Note = message,
                        fTime_Note = DateTime.Now,
                        fIsRead_Note = 0,
                    };
                    notes.Add(_note);

                    CNotes c_note = new CNotes
                    {
                        fId_Admin_Role = admin_id,
                        fId_Role = role.fId_Role,
                        fMessage_Note = message,
                        fTime_Note = DateTime.Now,
                        fIsRead_Note = 0,
                    };
                    frontNotes.Add(c_note);
                }

                dbFundaytrip.tNotes.AddRange(notes);
                dbFundaytrip.SaveChanges();
                return Json(frontNotes, JsonRequestBehavior.AllowGet);                
            }
            //寄通知給指定角色
            else
            {
                var q = from n in dbFundaytrip.tRoles
                        where n.fId_Role == role_id
                        select n;


                tNote _note = new tNote
                {
                    fId_Admin_Role = admin_id,
                    fId_Role = role_id,
                    fMessage_Note = message,
                    fTime_Note = DateTime.Now,
                    fIsRead_Note = 0,
                };

                CNotes c_note = new CNotes
                {
                    fId_Admin_Role = admin_id,
                    fId_Role = role_id,
                    fMessage_Note = message,
                    fTime_Note = DateTime.Now,
                    fIsRead_Note = 0,
                };

                dbFundaytrip.tNotes.Add(_note);
                dbFundaytrip.SaveChanges();


                return Json(c_note, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult postByAdmin(int type, string message)
        {
            CNotesFactory factory = new CNotesFactory();
            var q = from n in dbFundaytrip.tRoles
                    select n;
            CNotes note = new CNotes();
            switch (type)
            {
                case -1:
                    note = factory.createNotesToAll(message);
                    break;
                case 1:
                    note = factory.createNotesToNormal(message);
                    break;
                case 2:
                    note = factory.createNotesToProfit(message);
                    break;
            }

            return Json(note,JsonRequestBehavior.AllowGet);
        }
    }
}