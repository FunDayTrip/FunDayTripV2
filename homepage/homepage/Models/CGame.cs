using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CGame
    {

        public int fId_Game { get; set; }
        public int fId_Coordinate { get; set; }
        public Nullable<int> fType_Game { get; set; }
        public string fName_Game { get; set; }
        public int fOrder_Game { get; set; }
        public int fId_GameGroup { get; set; }
        public Nullable<int> fAR_Game { get; set; }
        public string fSource_Game { get; set; }

        public decimal? fX_Coordinate { get; set; }
        public decimal? fY_Coordinate { get; set; }

        public CGameSteps cGamesteps { get; set; }
        public CGameQA cGameQA { get; set; }
    }
}