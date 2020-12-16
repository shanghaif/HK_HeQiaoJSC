using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Yclactivity
    {
        public Guid YclactivityUuid { get; set; }
        public string Title { get; set; }
        public bool? IsRelease { get; set; }
        public DateTime? ReleaseTime { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
        public DateTime? AddTime { get; set; }
        public string AddPeople { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string Photo { get; set; }
        public string VillageUuid { get; set; }
    }
}
