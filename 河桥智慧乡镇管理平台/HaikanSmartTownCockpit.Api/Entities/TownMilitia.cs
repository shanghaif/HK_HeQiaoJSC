using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class TownMilitia
    {
        public Guid TownMilitiaUuid { get; set; }
        public int Id { get; set; }
        public string TownMilitiaName { get; set; }
        public string Sex { get; set; }
        public string Birth { get; set; }
        public string IdentityCard { get; set; }
        public string Census { get; set; }
        public string Phone { get; set; }
        public string Nation { get; set; }
        public string Occupation { get; set; }
        public string ArmyTime { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
