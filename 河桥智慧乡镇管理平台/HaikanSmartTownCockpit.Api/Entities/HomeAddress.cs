using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class HomeAddress
    {
        public Guid HomeAddressUuid { get; set; }
        public string Address { get; set; }
        public int Id { get; set; }
        public string Addresscode { get; set; }
        public string Town { get; set; }
        public string Ccmmunity { get; set; }
        public string Squad { get; set; }
        public string Village { get; set; }
        public string Szone { get; set; }
        public string Street { get; set; }
        public string Door { get; set; }
        public string Resregion { get; set; }
        public string Building { get; set; }
        public string BuildingNum { get; set; }
        public string Unit { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
        public int? RoomFloor { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public int? IsDeleted { get; set; }
        public decimal? Lon { get; set; }
        public decimal? Lat { get; set; }
    }
}
