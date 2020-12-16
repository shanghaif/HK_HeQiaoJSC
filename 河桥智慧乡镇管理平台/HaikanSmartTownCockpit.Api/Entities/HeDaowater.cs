using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class HeDaowater
    {
        public Guid HeDaowaterUuid { get; set; }
        public int Id { get; set; }
        public string HeDaowaterAddress { get; set; }
        public string HeDaowaterTime { get; set; }
        public string HeDaoHeDaowaterSw { get; set; }
        public string Ljiedian { get; set; }
        public string HeDaowaterYujin { get; set; }
        public string FangxunDj { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public decimal? CurrentPrecipitation { get; set; }
    }
}
