using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class CarAnFang
    {
        public Guid Guid { get; set; }
        public string Ability { get; set; }
        public string PlateTypeName { get; set; }
        public string CrossTime { get; set; }
        public string AlarmTypeName { get; set; }
        public string MonitorName { get; set; }
        public string MixedName { get; set; }
        public string PlatePicUrl { get; set; }
        public string VehiclePicUrl { get; set; }
        public string ImageIndexCode { get; set; }
        public string PersonName { get; set; }
        public string PhoneNo { get; set; }
        public string HappenTime { get; set; }
    }
}
