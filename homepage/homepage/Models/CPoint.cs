using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CPoint
    {
        public int fId_Point { get; set; }
        public int fId_Role { get; set; }
        public int? fPoint_Point { get; set; }
        public DateTime? fTime_Point { get; set; }
        public string fTimeReadable_Point { get; set; }
        public string fType_Point { get; set; }

    }
}