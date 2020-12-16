using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class HqCommunal
    {
        public Guid HqCommunalUuid { get; set; }
        public int Id { get; set; }
        public string HqCommunalName { get; set; }
        public string HqCommunalId { get; set; }
        public string HqCommunalStaues { get; set; }
        public string HqCommunalLocation { get; set; }
        public Guid? TownUuid { get; set; }
        public string HqCommunalType { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
