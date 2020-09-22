using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CRolesFactory
    {
        DB_FunDayTripEntities db = new DB_FunDayTripEntities();

        public List<tRole> readRoles(string member_id)
        {
            List<tRole> roles = new List<tRole>();

            var q = from r in db.tRoles
                    where r.fId_Master_Role == member_id
                    select r;

            if (q.Any())
            {
                foreach (var rol in q.ToList())
                {
                    roles.Add((tRole)rol);
                }
                return roles;
            }
            else
            {
                return roles;
            }
        }

    }
}