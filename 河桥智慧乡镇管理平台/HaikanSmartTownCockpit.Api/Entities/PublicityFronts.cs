using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class PublicityFronts
    {
        public Guid PublicityFrontsUuid { get; set; }
        public int Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string PublicityFrontsName { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
        public int? State { get; set; }
        public int? IsDelete { get; set; }
        public string Cover { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string PublicityTypeUuid { get; set; }
        public string VillageUuid { get; set; }
        public string AdminPeoPle { get; set; }
        public string KaifangTime { get; set; }
    }
}
