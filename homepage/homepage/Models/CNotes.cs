using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CNotes : tNote
    {
        public int fCount_Note { get; set; }
        public new string fMessage_Note { get; set; }
    }
}