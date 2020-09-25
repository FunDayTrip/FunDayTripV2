using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.ViewModel
{
    public class CFollowlistViewModel
    {
        public string follow_Self_Name { get; set; }
        public int follow_Self_ID { get; set; }
        public string follow_Self_IMG { get; set; }
        public string follow_Target_Name { get; set; }
        public int follow_Target_ID { get; set; }
        public string follow_Target_IMG { get; set; }

    }
}