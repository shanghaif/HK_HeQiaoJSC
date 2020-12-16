using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Youth
    {
        public Guid YouthUuid { get; set; }
        public int Id { get; set; }
        public string YouthName { get; set; }
        public string Sex { get; set; }
        public string Birth { get; set; }
        public string IdentityCard { get; set; }
        public string Census { get; set; }
        public string Phone { get; set; }
        public string Nation { get; set; }
        public string Occupation { get; set; }
        public string ArmyWill { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
