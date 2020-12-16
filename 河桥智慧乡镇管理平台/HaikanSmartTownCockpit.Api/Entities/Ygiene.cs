using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Ygiene
    {
        public Guid YgieneUuid { get; set; }
        public int Id { get; set; }
        public string YgieneName { get; set; }
        public string YgieneAddress { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string YgieneMonitorId { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string YgieneStaues { get; set; }
        public string YgieneType { get; set; }
        public int? UrlType { get; set; }
    }
}
