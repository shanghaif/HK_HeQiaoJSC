using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class RescueMember
    {
        public int Id { get; set; }
        public Guid RescueMemberUuid { get; set; }
        public DateTime? CreateTime { get; set; }
        public Guid? RescueTeamUuid { get; set; }
        public string MemberName { get; set; }
        public string MemberSex { get; set; }
        public string MemberPhone { get; set; }
        public int? IsDelete { get; set; }

        public virtual RescueTeam RescueTeamUu { get; set; }
    }
}
