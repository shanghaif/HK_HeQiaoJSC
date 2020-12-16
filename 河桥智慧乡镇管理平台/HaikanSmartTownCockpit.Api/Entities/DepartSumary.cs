using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class DepartSumary
    {
        public Guid DeSumaryUuid { get; set; }
        public int DeSumaryId { get; set; }
        public string DeSuHeadLine { get; set; }
        public string DeSuDescribe { get; set; }
        public string DeSuDocument { get; set; }
        public Guid? PriorityId { get; set; }
        public Guid? MissionId { get; set; }
        public Guid? SyUserUuid { get; set; }
        public string DeSuAddTime { get; set; }
        public string DepartName { get; set; }
    }
}
