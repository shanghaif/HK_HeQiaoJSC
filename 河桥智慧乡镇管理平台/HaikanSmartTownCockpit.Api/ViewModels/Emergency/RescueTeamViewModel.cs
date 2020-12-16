using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency
{
    public class RescueTeamViewModel
    {
        public Guid? RescueTeamUuid { get; set; }
        public string TeamName { get; set; }
        public string TeamCaptain { get; set; }
        public string TeamRenshu { get; set; }
        public string TeamPhone { get; set; }
    }
}
