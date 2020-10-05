using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CAdFactory
    {
        DB_FunDayTripEntities db = new DB_FunDayTripEntities();

        internal void getPass(string id)
        {
            var q = from data in db.tAds
                    where data.fId_Ad.ToString() == id
                    select data;

            q.FirstOrDefault().fStatus_Ad = "通過";
            db.SaveChanges();
        }

        public tAd getAdDetail(string id) 
        {
            var q = from data in db.tAds
                    where data.fId_Ad.ToString() == id
                    select data;

            var item = q.FirstOrDefault();
            return item;
        }

        public List<tAd> getApplyAD()
        {
            var q = from data in db.tAds
                    where data.fStatus_Ad =="審核中"
                    select data;

            var QList = q.ToList();
            return QList;
        }
    }
}