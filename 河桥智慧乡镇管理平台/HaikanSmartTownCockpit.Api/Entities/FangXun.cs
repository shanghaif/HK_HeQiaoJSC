using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class FangXun
    {
        public Guid Guid { get; set; }
        public string Member { get; set; }
        public string Command { get; set; }
        public string Workingclass { get; set; }
        public string Teamleader { get; set; }
        public string Governmentofficials { get; set; }
        public string Personinchargeofvillageenterprise { get; set; }
        public string Jobrequirements { get; set; }
        public int? IsDeleted { get; set; }
    }
}
