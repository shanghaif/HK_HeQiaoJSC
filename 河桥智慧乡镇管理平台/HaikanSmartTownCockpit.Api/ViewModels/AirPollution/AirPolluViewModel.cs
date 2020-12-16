using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Environmental
{
    public class AirPolluViewModel
    {
        public Guid? BarometricUuid { get; set; }
        public string BarTime { get; set; }
        public string NowShuzhi { get; set; }
        public string Linjie { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
