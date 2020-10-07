using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CGameRecord
    {
        public int fId_GameRecord { get; set; }
        public int? fId_GameGroup { get; set; }
        public int? fOrder_Game { get; set; }
        public int? fId_Role { get; set; }
        public DateTime fTime_GameRecord { get; set; }
        public string fTime_Readable_GameRecord { get; set; }
        public int? fFinished_GameRecord { get; set; }
    }
}