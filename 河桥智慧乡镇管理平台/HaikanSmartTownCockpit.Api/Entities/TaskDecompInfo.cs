using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class TaskDecompInfo
    {
        public Guid TaskDecompInfoUuid { get; set; }
        public Guid? Tduuid { get; set; }
        public int Id { get; set; }
        public string SpecialWorkTeam { get; set; }
        public string DedicatedTeamLeader { get; set; }
        public string RelatedCadres { get; set; }
        public string Principal { get; set; }
        public string Requirement { get; set; }
        public int? IsDelete { get; set; }
        public DateTime? AddTime { get; set; }
        public string Phone { get; set; }
    }
}
