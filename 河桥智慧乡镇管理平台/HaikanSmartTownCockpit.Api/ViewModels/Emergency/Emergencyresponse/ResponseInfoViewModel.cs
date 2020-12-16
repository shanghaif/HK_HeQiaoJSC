using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency.Emergencyresponse
{
    public class ResponseInfoViewModel
    {
        public Guid? ResponseInfoUuid { get; set; }
        public string Village { get; set; }
        public string TongzhiRen { get; set; }
        public string XiangyingDj { get; set; }
        public string TongzhiQingk { get; set; }
        public string ZaiciTongzhi { get; set; }
        public string Phone { get; set; }
    }
}
