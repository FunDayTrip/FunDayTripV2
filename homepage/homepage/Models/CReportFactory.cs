using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CReportFactory
    {

        DB_FunDayTripEntities db = new DB_FunDayTripEntities();

        internal List<tMember> getReportedMember(string rId)
        {
            var resultFromRole = db.tRoles.FirstOrDefault(data => data.fId_Role.ToString() == rId);
            var q = from data in db.tMembers
                    where data.fId_Member == resultFromRole.fId_Master_Role
                    select data;
            var QList = q.ToList();
            return QList;
        }

        public List<tReport> getAllReport()
        {
            var q = from data in db.tReports
                    orderby data.fId_Reported_Role ascending
                    select data;
            var QList = q.ToList();
            return QList;
        }

    }
}