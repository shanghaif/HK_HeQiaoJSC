using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency.Emergencyresponse
{
    public class XunchaInfoViewModel
    {
        public Guid? XunchaInfoUuid { get; set; }
        public string XunchaInfoRen { get; set; }
        public string ShebeiId { get; set; }
        public string XunchaAddress { get; set; }
        public string XunchaTime { get; set; }
        public string QingkShangbao { get; set; }
        public string Staues { get; set; }
        public string FuzerPhone { get; set; }
    }
}
