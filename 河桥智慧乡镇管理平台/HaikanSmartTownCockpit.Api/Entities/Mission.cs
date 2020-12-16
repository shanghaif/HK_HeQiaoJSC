using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Mission
    {
        public Guid MissionUuid { get; set; }
        public int Id { get; set; }
        public Guid? PriorityUuid { get; set; }
        public string MissionHeadline { get; set; }
        public string Principal { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public string MissionDescribe { get; set; }
        public string Priority { get; set; }
        public string Manhour { get; set; }
        public string Participant { get; set; }
        public string Approver { get; set; }
        public string Accomplish { get; set; }
        public string Recycle { get; set; }
        public string Remark { get; set; }
        public string Preserve { get; set; }
        public string EstablishTime { get; set; }
        public string EstablishName { get; set; }
        public string AuditOpinion { get; set; }
        public string AuditStatus { get; set; }
        public string AdministrativeOffice { get; set; }
        public string Sortord { get; set; }
        public string Missiontype { get; set; }
        public string ZhiDingTime { get; set; }
        public string Fujian { get; set; }
        public string Isouttime { get; set; }
        public string IsSave { get; set; }
        public string IsAssigned { get; set; }
    }
}
