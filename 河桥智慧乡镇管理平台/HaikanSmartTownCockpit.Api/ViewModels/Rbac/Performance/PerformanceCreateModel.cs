using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Rbac.Performance
{
    public class PerformanceCreateModel
    {
        public Guid PerformanceDeclareUuid { get; set; }
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
