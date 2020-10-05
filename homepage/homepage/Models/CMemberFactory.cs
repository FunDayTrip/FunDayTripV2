using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CMemberFactory
    {
        DB_FunDayTripEntities db = new DB_FunDayTripEntities();

        public string suspendMember(string id)
        {
            var q = from data in db.tMembers
                    where data.ID.ToString() == id
                    select data;

            q.FirstOrDefault().fId_FunctionAuth = 2;
            db.SaveChanges();
            return "ok";
        }
        public List<tMember> getAllMember()
        {
            var q = from data in db.tMembers
                    orderby data.fId_FunctionAuth descending
                    select data;
            var QList = q.ToList();
            return QList;
        }

        internal List<tMember> getByKeyword(string key)
        {
            var q = from data in db.tMembers
                    where data.fNickName_Member.Contains(key) || data.fEmail_Member.Contains(key)
                    select data;
            var QList = q.ToList();
            return QList;
        }
    }
}