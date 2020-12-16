using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class SystemSetting
    {
        public Guid SystemSettingUuid { get; set; }
        public int Id { get; set; }
        public decimal? WaterLevelCriticalPoint { get; set; }
    }
}
