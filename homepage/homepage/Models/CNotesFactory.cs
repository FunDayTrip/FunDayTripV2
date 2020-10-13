using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace homepage.Models
{
    public class CNotesFactory
    {
        DB_FunDayTripEntities db = new DB_FunDayTripEntities();
        public List<tNote> readNotes(int role_id)
        {
            List<tNote> notes = new List<tNote>();
            var q = from n in db.tNotes
                    where n.fId_Role == role_id
                    select n;
            foreach(var n in q.ToList())
            {
                notes.Add(n);
            }
            return notes;            
        }
        public CNotes createNotesToAll(string message)
        {
            var q = from n in db.tRoles
                    select n;

            List<tNote> notes = new List<tNote>();
            List<CNotes> frontNotes = new List<CNotes>();
            foreach (var role in q.ToList())
            {
                tNote _note = new tNote
                {
                    fId_Admin_Role = 9,
                    fId_Role = role.fId_Role,
                    fMessage_Note = message,
                    fTime_Note = DateTime.Now,
                    fIsRead_Note = 0,
                };
                notes.Add(_note);

                CNotes c_note = new CNotes
                {
                    fId_Admin_Role = 9,
                    fId_Role = role.fId_Role,
                    fMessage_Note = message,
                    fTime_Note = DateTime.Now,
                    fIsRead_Note = 0,
                };
                frontNotes.Add(c_note);
            }

            db.tNotes.AddRange(notes);
            db.SaveChanges();
            return frontNotes.FirstOrDefault();
        }
        public CNotes createNotesToProfit(string message)
        {
            var q = from n in db.tRoles
                    where n.fId_Slave_Type_Role == "b"
                    select n;

            List<tNote> notes = new List<tNote>();
            List<CNotes> frontNotes = new List<CNotes>();
            foreach (var role in q.ToList())
            {
                tNote _note = new tNote
                {
                    fId_Admin_Role = 9,
                    fId_Role = role.fId_Role,
                    fMessage_Note = message,
                    fTime_Note = DateTime.Now,
                    fIsRead_Note = 0,
                };
                notes.Add(_note);

                CNotes c_note = new CNotes
                {
                    fId_Admin_Role = 9,
                    fId_Role = role.fId_Role,
                    fMessage_Note = message,
                    fTime_Note = DateTime.Now,
                    fIsRead_Note = 0,
                };
                frontNotes.Add(c_note);
            }

            db.tNotes.AddRange(notes);
            db.SaveChanges();
            return frontNotes.FirstOrDefault();

        }

        public CNotes createNotesToNormal(string message)
        {
            var q = from n in db.tRoles
                    where n.fId_Slave_Type_Role == "u"
                    select n;

            List<tNote> notes = new List<tNote>();
            List<CNotes> frontNotes = new List<CNotes>();
            foreach (var role in q.ToList())
            {
                tNote _note = new tNote
                {
                    fId_Admin_Role = 9,
                    fId_Role = role.fId_Role,
                    fMessage_Note = message,
                    fTime_Note = DateTime.Now,
                    fIsRead_Note = 0,
                };
                notes.Add(_note);

                CNotes c_note = new CNotes
                {
                    fId_Admin_Role = 9,
                    fId_Role = role.fId_Role,
                    fMessage_Note = message,
                    fTime_Note = DateTime.Now,
                    fIsRead_Note = 0,
                };
                frontNotes.Add(c_note);
            }

            db.tNotes.AddRange(notes);
            db.SaveChanges();
            return frontNotes.FirstOrDefault();

        }

    }
}