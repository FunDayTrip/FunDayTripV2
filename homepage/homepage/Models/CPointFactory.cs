using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CPointFactory
    {
        DB_FunDayTripEntities dbFundaytrip = new DB_FunDayTripEntities();
        public List<CPoint> getPoint(int role_id)
        {
            List<CPoint> p_List = new List<CPoint>();

            var q = from p in dbFundaytrip.tPoints
                    where p.fId_Role == role_id
                    select p;

            int totalPoint = Convert.ToInt32(q.Sum(a => a.fPoint_Point));

            foreach (var x in q.ToList())
            {
                CPoint cp = new CPoint();
                cp.fId_Role = x.fId_Role;
                cp.fId_Point = x.fId_Point;
                cp.fPoint_Point = x.fPoint_Point;
                cp.fTime_Point = x.fTime_Point;
                cp.fType_Point = x.fType_Point;
                p_List.Add(cp);
            }

            return p_List;
        }

        public void createPoint(tRole new_p, int num, string type)
        {
            tPoint db_point = new tPoint
            {
                fId_Role = new_p.fId_Role,
                fPoint_Point = num,
                fTime_Point = DateTime.Now,
                fType_Point = type
            };

            dbFundaytrip.tPoints.Add(db_point);
            dbFundaytrip.SaveChanges();
        }
    }
}