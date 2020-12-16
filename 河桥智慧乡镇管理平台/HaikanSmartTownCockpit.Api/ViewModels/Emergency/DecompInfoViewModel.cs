using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency
{
    public class DecompInfoViewModel
    {
        public Guid? TaskDecompInfoUuid { get; set; }
        public Guid Tduuid { get; set; }
        public string SpecialWorkTeam { get; set; }
        public string DedicatedTeamLeader { get; set; }
        public string RelatedCadres { get; set; }
        public string Principal { get; set; }
        public string Requirement { get; set; }
        public DateTime? AddTime { get; set; }
    }
}
