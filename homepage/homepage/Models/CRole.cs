using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CRole
    {
        public int fId_Role { get; set; }
        public string fId_Master_Role { get; set; }
        public string fId_Slave_Role { get; set; }
        public string fId_Slave_Type_Role { get; set; }
        public string fNickName_Role { get; set; }
        public DateTime? fEntryDate_Role { get; set; }
    }
}