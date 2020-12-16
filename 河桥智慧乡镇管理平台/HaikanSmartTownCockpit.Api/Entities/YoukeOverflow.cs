using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class YoukeOverflow
    {
        public Guid YoukeOverflowUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string JindianName { get; set; }
        public string Rongliang { get; set; }
        public string OverflowInfo { get; set; }
        public string SsRenCount { get; set; }
        public string YujinInfo { get; set; }
    }
}
