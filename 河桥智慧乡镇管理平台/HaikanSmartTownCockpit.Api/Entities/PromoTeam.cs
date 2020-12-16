using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class PromoTeam
    {
        public Guid PromoTeamUuid { get; set; }
        public int Id { get; set; }
        public string TeamType { get; set; }
        public string TeamCaptain { get; set; }
        public string TeamChengyuan { get; set; }
        public string TeamJieshao { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string Sex { get; set; }
        public string Birth { get; set; }
        public string Politics { get; set; }
        public string Cunz { get; set; }
    }
}
