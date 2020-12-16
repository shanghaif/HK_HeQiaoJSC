using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency
{
    public class UAVViewModel
    {
        public Guid? Uavuuid { get; set; }
        public string Uavname { get; set; }
        public string Uavnumber { get; set; }
        public string Uavurl { get; set; }
        public string Uavaddress { get; set; }
    }
}
