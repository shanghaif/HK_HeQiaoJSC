using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency
{
    public class RescueMemberViewModel
    {
        public Guid? RescueMemberUuid { get; set; }
        public Guid? RescueTeamUuid { get; set; }
        public string MemberName { get; set; }
        //public string MemberSex { get; set; }
        public string MemberPhone { get; set; }
    }
}
