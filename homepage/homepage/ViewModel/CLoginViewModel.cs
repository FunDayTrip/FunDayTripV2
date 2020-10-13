using homepage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homepage.ViewModel
{
    public class CLoginViewModel
    {
        public string loginMessage { get; set; }
        public string fId_Member { get; set; }
        public string fNickName_Member { get; set; }
        public string fEmail_Member { get; set; }
        public string fPassword_Member { get; set; }
        public int fNotesCount_Member { get; set; }
        public List<tNote> fNotes_Member { get; set; }
        public List<tRole> fRoles_Member { get; set; }
        public int fActiveRoleId_Member { get; set; }
        public string fActiveRoleName_Member { get; set; }
        public int fId_FuntionAuth_Member { get; set; }
        public int fPointTotal_Member { get; set; }
        
        public string fPhoto_Member { get; set; }
    }
}