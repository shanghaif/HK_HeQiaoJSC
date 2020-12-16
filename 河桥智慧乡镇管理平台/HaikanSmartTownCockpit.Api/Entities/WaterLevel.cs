using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class WaterLevel
    {
        public Guid WaterLevelUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string WaterName { get; set; }
        public string WaterInfo { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string Watersw { get; set; }
        public string Accurate { get; set; }
        public string MonitorWaterId { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string ShebeiType { get; set; }
        public string AdminPepo { get; set; }
        public string Weizhi { get; set; }
    }
}
