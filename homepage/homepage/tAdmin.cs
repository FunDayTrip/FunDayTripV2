//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace homepage
{
    using System;
    using System.Collections.Generic;
    
    public partial class tAdmin
    {
        public int ID { get; set; }
        public string fId_Admin { get; set; }
        public int fId_FunctionAuth { get; set; }
        public string fName_Admin { get; set; }
        public string fEmail_Admin { get; set; }
        public string fPassword_Admin { get; set; }
        public string fAdd_Admin { get; set; }
        public string fGender_Admin { get; set; }
        public Nullable<System.DateTime> fBirthday_Admin { get; set; }
        public string fPhone_Admin { get; set; }
    
        public virtual tFuntionAuth tFuntionAuth { get; set; }
    }
}
