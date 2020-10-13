using homepage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.ViewModel
{
    public class CGameNavigationViewModel
    {
        public CGameGroup fGroup_GameNav { get; set; }
        public List<CGame> fItems_GameNav
        { get; set; }
        public string fPath_GameNav { get; set; }
        public string fPhoto_GameNav { get; set; }

        public int fPlaying_GameNav { get; set; }
        public int fStatus_GameNav { get; set; }
        public CGameRecord fRecords_GameNav {get;set;}
        public int fIsPass_GameNav { get; set; }
        
    }
}