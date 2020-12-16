using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Tourist
    {
        public Guid TouristUuid { get; set; }
        public int Id { get; set; }
        public string TouristName { get; set; }
        public Guid? ScenicUuid { get; set; }
        public double? TouristMoney { get; set; }
        public string TouristTime { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string ScenicName { get; set; }
        public string TjYear { get; set; }
        public string TjMouth { get; set; }
        public string TjJidu { get; set; }
    }
}
