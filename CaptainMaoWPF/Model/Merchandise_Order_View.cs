//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaptainMaoWPF.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Merchandise_Order_View
    {
        public int id { get; set; }
        public int Order_ID { get; set; }
        public int Merchandise_ID { get; set; }
        public int merchandise_Volume { get; set; }
    
        public virtual Merchandise Merchandise { get; set; }
        public virtual Order Order { get; set; }
    }
}
