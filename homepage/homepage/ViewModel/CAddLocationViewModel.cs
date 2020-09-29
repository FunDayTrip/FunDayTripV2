using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.ViewModel
{
    public class CAddLocationViewModel
    {
        public string txtLocationName { get; set; }
        public string fImage { get; set; }
        public HttpPostedFileBase image { get; set; }
    }
}