using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.WaterLevel
{
    public class WaterLevelViewModel
    {
        public string WaterLevelUuid { get; set; }
        public string WaterName { get; set; }
        public string WaterInfo { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string Watersw { get; set; }
        public string Accurate { get; set; }
        public string Weizhi { get; set; }
    }
}
