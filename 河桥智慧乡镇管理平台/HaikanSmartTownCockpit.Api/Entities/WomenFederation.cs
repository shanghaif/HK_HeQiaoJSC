using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class WomenFederation
    {
        public Guid WomenFederationUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string WomenFederationName { get; set; }
        public string Address { get; set; }
        public string Zhuxi { get; set; }
        public string Phone { get; set; }
        public string VillageUuid { get; set; }
        public string Staues { get; set; }
        public string Renshu { get; set; }
    }
}
