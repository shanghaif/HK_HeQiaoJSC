using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.WorkforceManage
{
    public class WorkManageViewModel
    {
        public Guid PaibanInfoUuid { get; set; }
        public string ZbLingdao { get; set; }
        public string Zbrenyuan { get; set; }
        public string Zyrenyuan { get; set; }
        public int IsDeleted { get; set; }
        public string ZbTime { get; set; }
        public string ZbBc { get; set; }
        public string ZbYear { get; set; }

    }
}
