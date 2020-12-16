using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Socialgovernance
{
    public class HealthmanagementViewModel
    {

        public Guid YgieneUuid { get; set; }
        public string YgieneName { get; set; }
        public string YgieneAddress { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string YgieneMonitorId { get; set; }
        public int? IsDeleted { get; set; }
        public string AddPeople { get; set; }
        public string YgieneStaues { get; set; }
        public string YgieneType { get; set; }
        public string AddTime { get; set; }
    }
}
