using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Barometric
    {
        public Guid BarometricUuid { get; set; }
        public int Id { get; set; }
        public string BarTime { get; set; }
        public string NowShuzhi { get; set; }
        public string Linjie { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
