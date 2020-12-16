using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class RegionInfosCopy
    {
        public int Id { get; set; }
        public int? RegionId { get; set; }
        public string RegionXyinfo { get; set; }
        public DateTime? AddTime { get; set; }
        public string UnifiedAddressLibraryId { get; set; }
    }
}
