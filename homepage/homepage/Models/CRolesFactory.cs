using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;

namespace homepage.Models
{
    public class CRolesFactory
    {
        DB_FunDayTripEntities db = new DB_FunDayTripEntities();

        public List<CRole> getRoleList(string member_id)
        {
            List<CRole> roles = new List<CRole>();

            var q = from r in db.tRoles
                    where r.fId_Master_Role == member_id
                    select r;

            if (q.Any())
            {
                foreach (var rol in q.ToList())
                {
                    CRole r = new CRole();
                    r.fId_Role = rol.fId_Role;
                    r.fId_Master_Role = rol.fId_Master_Role;
                    r.fId_Slave_Role = rol.fId_Slave_Role;
                    r.fId_Slave_Type_Role = rol.fId_Slave_Type_Role;
                    r.fNickName_Role = rol.fNickName_Role;
                    r.fEntryDate_Role = rol.fEntryDate_Role;
                    roles.Add(r);
                }
                return roles;
            }
            else
            {
                return roles;
            }
        }
        public CRole getRole(int role_id)
        {
            var q = (from r in db.tRoles
                     where r.fId_Role == role_id
                     select r).FirstOrDefault();

            CRole rol = new CRole();
            rol.fId_Role = q.fId_Role;
            rol.fId_Master_Role = q.fId_Master_Role;
            rol.fId_Slave_Role = q.fId_Slave_Role;
            rol.fId_Slave_Type_Role = q.fId_Slave_Type_Role;
            rol.fNickName_Role = q.fNickName_Role;
            rol.fEntryDate_Role = q.fEntryDate_Role;

            return rol;
        }

        //新增會員 0924 郭松明/王詠平
        public void createRole(tMember member)
        {
            if (member != null)
            {
                tRole ro = new tRole {
                    fId_Master_Role = member.fId_Member,
                    fId_Slave_Role = member.fId_Member,
                    fId_Slave_Type_Role = "u",
                    fNickName_Role = member.fNickName_Member,
                    fEntryDate_Role = DateTime.Now
                };

                db.tRoles.Add(ro);
                db.SaveChanges();

                //新增會員送50點
                CPointFactory pointFactory = new CPointFactory();
                pointFactory.createPoint(ro, 50, "註冊會員");
            }
        }

        public void createRole(string member_id ,tProfitMember p_member)
        {
            if (p_member != null)
            {
                tRole ro = new tRole
                {
                    fId_Master_Role = member_id,
                    fId_Slave_Role = p_member.fId_ProfitMember,
                    fId_Slave_Type_Role = "b",
                    fNickName_Role = p_member.fName_ProfitMember,
                    fEntryDate_Role = DateTime.Now
                };

                db.tRoles.Add(ro);
                db.SaveChanges();
            }
        }


    }
}