using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class PerformanceDeclare
    {
        public Guid PerformanceDeclareUuid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public string Type { get; set; }
        public string Accessory { get; set; }
        public string EstablishTime { get; set; }
        public string EstablishName { get; set; }
        public string AuditOpinion { get; set; }
        public string AuditStatus { get; set; }
        public int? IsDeleted { get; set; }
    }
}
