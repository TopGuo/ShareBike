//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yuyi.Oa.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActionInfo
    {
        public ActionInfo()
        {
            this.RoleInfo = new HashSet<RoleInfo>();
            this.R_User_Action = new HashSet<R_User_ActionInfo>();
        }
    
        public int ID { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public System.DateTime ModifyOn { get; set; }
        public System.DateTime SubTime { get; set; }
        public string ActionName { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsMenu { get; set; }
        public short DelFlag { get; set; }
        public Nullable<int> Sort { get; set; }
        public string IsIcon { get; set; }
    
        public virtual ICollection<RoleInfo> RoleInfo { get; set; }
        public virtual ICollection<R_User_ActionInfo> R_User_Action { get; set; }
    }
}