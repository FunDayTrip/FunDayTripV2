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
    
    public partial class tAd
    {
        public int fId_Ad { get; set; }
        public int fId_Role { get; set; }
        public int fId_Admin_Role { get; set; }
        public string fId_Location { get; set; }
        public string fContent_Ad { get; set; }
        public string fPhoto_Ad { get; set; }
        public string fSolution_Ad { get; set; }
        public string fStatus_Ad { get; set; }
    
        public virtual tLocation tLocation { get; set; }
        public virtual tRole tRole { get; set; }
        public virtual tRole tRole1 { get; set; }
    }
}
