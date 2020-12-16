using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class SmokeGan
    {
        public Guid SmokeGanUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string SmokeGanShebei { get; set; }
        public string SmokeGanStaues { get; set; }
        public string SmokeGanBaojin { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
