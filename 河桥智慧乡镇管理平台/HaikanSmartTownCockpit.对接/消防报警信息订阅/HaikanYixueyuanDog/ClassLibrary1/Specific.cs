//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Specific
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public System.Guid SpecificUuid { get; set; }
        public string TypeName { get; set; }
        public string EventScore { get; set; }
        public string EventName { get; set; }
        public string Events { get; set; }
        public string Points { get; set; }
        public string RectificationTime { get; set; }
        public string RectificationScore { get; set; }
        public string RectificationPoints { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> IsDelete { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public Nullable<System.Guid> SchoolDistrictUuid { get; set; }
    
        public virtual SchoolDistrict SchoolDistrict { get; set; }
    }
}
