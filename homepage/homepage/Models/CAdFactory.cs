using homepage.ViewModel;
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

        public CAdDetailVM getAdDetail(string id) 
        {
            var q = from data in db.tAds
                    from data2 in db.tMembers
                    where data.fId_Ad.ToString() == id && data.tRole.fId_Master_Role==data2.fId_Member
                    /*orderby *//*data.fId_Ad, data.fId_Role, data.fId_Location, data.fSolution_Ad,data.fPhoto_Ad, data2.fId_Member,data2.fNickName_Member,data2.fAdd_Member,data2.fPhone_Member,data2.fCountry_Member,data2.fLastName_Member,data2.fFirstName_Member*/
                    select new  CAdDetailVM{ fId_Ad = data.fId_Ad.ToString(), fId_Role=data.fId_Role.ToString(), fId_Location=data.fId_Location, fSolution_Ad=data.fSolution_Ad, fPhoto_Ad=data.fPhoto_Ad, fId_Member=data2.fId_Member, fNickName_Member=data2.fNickName_Member, fAdd_Member=data2.fAdd_Member, fPhone_Member=data2.fPhone_Member, fCountry_Member=data2.fCountry_Member, fLastName_Member=data2.fLastName_Member, fFirstName_Member=data2.fFirstName_Member, fContent_Ad=data.fContent_Ad , fPhoto_Member=data2.fPhoto_Member};

            return  q.FirstOrDefault();
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