﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CAd:tAd
    {
        public HttpPostedFileBase image { get; set; }
    }
}