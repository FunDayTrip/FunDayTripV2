using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CGameSteps
    {
        public int fId_GameStep { get; set; }
        public int fId_Game { get; set; }
        public string fTitle_GameStep { get; set; }
        public string fContent_GameStep { get; set; }
        public Nullable<int> fReward_GameStep { get; set; }

    }
}