using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class SecurityReport
    {
        public Guid SecurityReportUuid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Time { get; set; }
        public string State { get; set; }
        public string Situation { get; set; }
        public Guid? SecurityUuid { get; set; }
        public int? IsDeleted { get; set; }

        public virtual Security SecurityUu { get; set; }
    }
}
