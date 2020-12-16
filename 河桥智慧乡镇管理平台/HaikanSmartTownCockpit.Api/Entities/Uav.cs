using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Uav
    {
        public int Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public Guid Uavuuid { get; set; }
        public string Uavname { get; set; }
        public string Uavnumber { get; set; }
        public string Uavurl { get; set; }
        public string Uavaddress { get; set; }
        public int? IsDeleted { get; set; }
    }
}
