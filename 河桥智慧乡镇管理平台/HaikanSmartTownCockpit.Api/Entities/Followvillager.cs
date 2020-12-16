using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Followvillager
    {
        public Guid FollowvillagerUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string FollowvillagerName { get; set; }
        public string Sex { get; set; }
        public string IdentityCard { get; set; }
        public string Nation { get; set; }
        public string Phone { get; set; }
        public string Staues { get; set; }
        public Guid? VillageUuid { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string Shiji { get; set; }
        public string Jieshao { get; set; }
    }
}
