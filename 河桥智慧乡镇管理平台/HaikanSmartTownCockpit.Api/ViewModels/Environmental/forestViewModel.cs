using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Environmental
{
    public class forestViewModel
    {
        public Guid WoodsUuid { get; set; }
        public string ShebeiId { get; set; }
        public string ShebeiType { get; set; }
        public string ShebeiAddress { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string Guanliren { get; set; }
        public string ShebeiStaues { get; set; }
        public int? IsDeleted { get; set; }
        public string AddPeople { get; set; }
        public string ForestName { get; set; }
        public string ForestAddress { get; set; }
        public string MapRegion { get; set; }
        public string ManagePhone { get; set; }
    }
}
