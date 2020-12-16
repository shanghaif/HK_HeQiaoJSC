using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class ParkingLot
    {
        public Guid ParkingLotUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string ParkingLotName { get; set; }
        public string ParkingLotAddress { get; set; }
        public string Zchewei { get; set; }
        public string Ychewei { get; set; }
        public string Schewei { get; set; }
        public string ParkingLotsru { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string ParkingIndexCode { get; set; }
    }
}
