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
    
    public partial class AccessControlRecord
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public System.Guid AccessControlRecordUuid { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string IdCard { get; set; }
        public string Phone { get; set; }
        public string ComeInTime { get; set; }
        public string GetOutTime { get; set; }
        public Nullable<System.Guid> ResearchBuildingUuid { get; set; }
        public string Mark { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    
        public virtual ResearchBuilding ResearchBuilding { get; set; }
    }
}
