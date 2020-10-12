using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace homepage.Models
{
    public class CMember
    {
        public int ID { get; set; }
        public string fId_Member { get; set; }
        public int fId_FunctionAuth { get; set; }
        [Display(Name = "暱稱")]
        [Required]
        public string fNickName_Member { get; set; }
        public string fEmail_Member { get; set; }
        [Display(Name = "密碼")]
        [Required]
        public string fPassword_Member { get; set; }
        public string fAdd_Member { get; set; }
        public string fGender_Member { get; set; }
        public Nullable<System.DateTime> fBirthday_Member { get; set; }
        public string fPhone_Member { get; set; }
        public string fCountry_Member { get; set; }
        public string fFirstName_Member { get; set; }
        public string fLastName_Member { get; set; }
        public Nullable<int> fGameLV_Member { get; set; }

        public string fPhoto_Member { get; set; }
        public HttpPostedFileBase image { get; set; }

        public Nullable<decimal> fGpsX_Member { get; set; }
        public Nullable<decimal> fGpsY_Member { get; set; }
        [Display(Name = "確認密碼")]
        [Compare("fPassword_Member")]
        public string confirmPW { get; set; }



    }
}