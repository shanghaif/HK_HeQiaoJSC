using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency.Emergencyresponse
{
    public class ResponseInitList
    {
        public int Id { get; set; }
        public Guid? ResponseInitUuid { get; set; }
        public string Villages { get; set; }
        public string Level { get; set; }
        public string Situation { get; set; }
        public int? IsDelete { get; set; }
        public int? ReleaseState { get; set; }
        public string VillageName { get; set; }
    }
}
