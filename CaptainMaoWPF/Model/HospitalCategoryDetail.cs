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
    
    public partial class HospitalCategoryDetail
    {
        public int CountID { get; set; }
        public int HospitalID { get; set; }
        public int CategoryID { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Hospital Hospital { get; set; }
    }
}
