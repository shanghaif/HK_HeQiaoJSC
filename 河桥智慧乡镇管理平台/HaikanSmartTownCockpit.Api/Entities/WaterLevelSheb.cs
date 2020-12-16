using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class WaterLevelSheb
    {
        public Guid WaterLevelShebUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string MonitorWaterId { get; set; }
        public string Weizhi { get; set; }
        public string ShebeiType { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string AdminPepo { get; set; }
        public string AddTime { get; set; }
        public string YujingNum { get; set; }
        public string YujingType { get; set; }
    }
}
