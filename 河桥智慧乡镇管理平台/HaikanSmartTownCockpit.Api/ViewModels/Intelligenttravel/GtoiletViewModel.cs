using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Intelligenttravel
{
    public class GtoiletViewModel
    {
        public Guid? GtoiletUuid { get; set; }
        public string GtoiletName { get; set; }
        public string GtoiletAddress { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string GtoiletStaues { get; set; }
        public string KongqiZhil { get; set; }
        public string WaterYujin { get; set; }
    }
}
