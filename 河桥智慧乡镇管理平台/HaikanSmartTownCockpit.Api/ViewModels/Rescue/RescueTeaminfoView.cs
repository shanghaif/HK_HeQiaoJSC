using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Rescue
{
    public class RescueTeaminfoView
    {
        public Guid? RescueTeamUuid { get; set; }
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string TeamCaptain { get; set; }
        public string TeamRenshu { get; set; }
        public string TeamPhone { get; set; }
        public string MemberNames { get; set; }
    }
}
