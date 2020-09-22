using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CRoles : tRole
    {
        public static explicit operator CRoles(List<tRole> v)
        {
            throw new NotImplementedException();
        }
    }
}