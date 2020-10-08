using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CGameQA
    {
        public int fId_GameQA { get; set; }
        public int fId_Game { get; set; }
        public string fQuestion_GameQA { get; set; }
        public string fOption_1_GameQA { get; set; }
        public string fOption_2_GameQA { get; set; }
        public string fOption_3_GameQA { get; set; }
        public string fOption_4_GameQA { get; set; }
        public Nullable<int> fAnswer_GameQA { get; set; }
        public Nullable<int> fReward_GameQA { get; set; }

    }
}