using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CRolesFactory
    {
        DB_FunDayTripEntities db = new DB_FunDayTripEntities();

        public List<CRole> readRoles(string member_id)
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
    }
}