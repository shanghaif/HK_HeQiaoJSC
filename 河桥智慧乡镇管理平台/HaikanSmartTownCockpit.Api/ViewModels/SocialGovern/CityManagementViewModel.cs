using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.SocialGovern
{
    public class CityManagementViewModel
    {
        public Guid? CityManagementUuid { get; set; }
        public string Incident { get; set; }
        public string ZhifaTime { get; set; }
        public string ZhifaAddress { get; set; }
        public string Chuliren { get; set; }
        public string BeiChulirren { get; set; }
        public string ZhifaRen { get; set; }
        public string ChuliQingk { get; set; }

    }
}
