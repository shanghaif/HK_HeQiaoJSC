using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Socialgovernance
{
    public class smokeInductionViewModel
    {
        public Guid SmokeGanUuid { get; set; }
        public int? IsDeleted { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string SmokeGanShebei { get; set; }
        public string SmokeGanStaues { get; set; }
        public string SmokeGanBaojin { get; set; }
        public string AddPeople { get; set; }
    }
}
