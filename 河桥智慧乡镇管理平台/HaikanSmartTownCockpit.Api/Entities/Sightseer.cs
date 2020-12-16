using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Sightseer
    {
        public Guid SightseerUuid { get; set; }
        public int? Id { get; set; }
        public int? IsDeleted { get; set; }
        public string SightseerName { get; set; }
        public string Sex { get; set; }
        public string IdentityCard { get; set; }
        public string Nation { get; set; }
        public string Laiyuandi { get; set; }
        public string Phone { get; set; }
        public string Staues { get; set; }
        public Guid? ScenicUuid { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string Age { get; set; }
        public string Shengneiwai { get; set; }
    }
}
