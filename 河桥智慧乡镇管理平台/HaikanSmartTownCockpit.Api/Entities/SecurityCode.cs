using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class SecurityCode
    {
        public int Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public Guid SecurityCodeUuid { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public int? State { get; set; }
        public string ChargeName { get; set; }
        public string ChargePhone { get; set; }
        public int? IsDeleted { get; set; }
        public DateTime? ScannTime { get; set; }
    }
}
