using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Intelligenttravel
{
    public class ParkingLotViewModel
    {
        public Guid? ParkingLotUuid { get; set; }
        public string ParkingLotName { get; set; }
        public string ParkingLotAddress { get; set; }
        public string Zchewei { get; set; }
        public string Ychewei { get; set; }
        public string Schewei { get; set; }
        public string ParkingLotsru { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
    }
}
