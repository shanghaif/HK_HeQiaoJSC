using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class TouristOfDay
    {
        public long? Id { get; set; }
        public Guid? ScenicUuid { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? Num { get; set; }
    }
}
