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
    }
}