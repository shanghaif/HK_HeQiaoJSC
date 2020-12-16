using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.HomeAddress
{
    public class HomeAddressViewModel
    {
        public string HomeAddressUuid { get; set; }
        
        public string Address { get; set; }
        public string Addresscode { get; set; }
        public string Village { get; set; }
        public string Town { get; set; }
        public string Door { get; set; }
        public string Unit { get; set; }
        public string Resregion { get; set; }
    }
}
