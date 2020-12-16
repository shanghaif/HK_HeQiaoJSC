using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Emergency
    {
        public Guid EmergencyUuid { get; set; }
        public int Id { get; set; }
        public string EmergencyDengji { get; set; }
        public string EmergencyQuyu { get; set; }
        public string QuyuFuzer { get; set; }
        public string FuzerPhone { get; set; }
        public string RescueTeamUuid { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
