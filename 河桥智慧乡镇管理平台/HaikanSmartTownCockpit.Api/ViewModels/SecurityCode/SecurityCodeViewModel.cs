using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.SecurityCode
{
    public class SecurityCodeViewModel
    {
        public Guid? SecurityCodeUuid { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public int? State { get; set; }
        public string ChargeName { get; set; }
        public string ChargePhone { get; set; }
    }
}
