using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency.Emergencyresponse
{
    public class XunchaDutyViewModel
    {
        public Guid? XunchaDutyUuid { get; set; }
        public string XunchaInfoRen { get; set; }
        public string ShebeiId { get; set; }
        public string XunchaAddress { get; set; }
        public string XunchaTime { get; set; }
        public string FuzerPhone { get; set; }
    }
}
