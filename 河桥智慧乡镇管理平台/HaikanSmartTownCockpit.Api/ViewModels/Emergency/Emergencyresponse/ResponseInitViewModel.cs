using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency.Emergencyresponse
{
    public class ResponseInitViewModel
    {
        public Guid? ResponseInitUuid { get; set; }
        public string Villages { get; set; }
        public string Level { get; set; }
        public string Situation { get; set; }
        public int? ReleaseState { get; set; }
    }
}
