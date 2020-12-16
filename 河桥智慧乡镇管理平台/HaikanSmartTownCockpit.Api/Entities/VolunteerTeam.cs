using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class VolunteerTeam
    {
        public int Id { get; set; }
        public Guid VteamUuid { get; set; }
        public string Name { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public int IsDelete { get; set; }
        public int? Number { get; set; }
    }
}
