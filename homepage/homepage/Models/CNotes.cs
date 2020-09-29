using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CNotes
    {
        public int fId_Note { get; set; }
        public int fId_Role { get; set; }
        public int fId_Admin_Role { get; set; }
        public string fMessage_Note { get; set; }
        public DateTime? fTime_Note { get; set; }
        public string fTimeReadable_Note { get; set; }
        public int? fIsRead_Note { get; set; }
    }
}