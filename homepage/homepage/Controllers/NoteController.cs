using homepage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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
    }
}