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
    
    public partial class tGameQA
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
    
        public virtual tGame tGame { get; set; }
    }
}
