using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.RequestPayload.Intelligenttravel
{
    public class TouristRequestPayload : RequestPayload
    {
        public int year { get; set; }
        public int SType { get; set; }
        public int Month { get; set; }
    }
}
