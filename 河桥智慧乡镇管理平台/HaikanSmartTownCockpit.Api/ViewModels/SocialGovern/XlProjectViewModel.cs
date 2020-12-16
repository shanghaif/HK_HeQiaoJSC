using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.SocialGovern
{
    public class XlProjectViewModel
    {
        public Guid? XlProjectUuid { get; set; }
        public string XlShebeiId { get; set; }
        public string XlShebeiType { get; set; }
        public string ShebeiAddress { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string AdminInfo { get; set; }
        public string ShebeiType { get; set; }
    }
}
