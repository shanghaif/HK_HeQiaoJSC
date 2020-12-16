using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class RegionInfo
    {
        public int Id { get; set; }
        public int? RegionId { get; set; }
        public string RegionX { get; set; }
        public string RegionY { get; set; }
        public DateTime? AddTime { get; set; }
    }
}
