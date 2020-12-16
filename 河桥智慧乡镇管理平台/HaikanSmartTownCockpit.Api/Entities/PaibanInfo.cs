using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class PaibanInfo
    {
        public Guid PaibanInfoUuid { get; set; }
        public int Id { get; set; }
        public string ZbLingdao { get; set; }
        public string Zbrenyuan { get; set; }
        public string Zyrenyuan { get; set; }
        public string ZbTime { get; set; }
        public int IsDeleted { get; set; }
        public string ZbBc { get; set; }
        public string ZbYear { get; set; }
    }
}
