using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace homepage.Models
{
    public class CGameNavigationViewModel
    {
        public CGameGroup fGroup_GameNav { get; set; }
        public List<CGame> fItems_GameNav 
        {  get; set; }
        public string fPath_GameNav { get; set; }
        
    }
}