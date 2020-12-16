using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency
{
    public class VillageViewModel
    {
        public Guid? VillageUuid { get; set; }
        public string VillageName { get; set; }
        public int? OrderBy { get; set; }
    }
}
