using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.SocialGovern
{
    public class SecurityReportViewModel
    {
        public Guid? SecurityReportUuid { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string State { get; set; }
        public string Situation { get; set; }
        public Guid? SecurityUuid { get; set; }
        public int? IsDeleted { get; set; }
    }
}
