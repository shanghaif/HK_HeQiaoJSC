using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Tourist
{
    public class TouristWeekData
    {
        public Guid ScenicUuid { get; set; }
        public string ScenicName { get; set; }

        public int First { get; set; }
        public int Second { get; set; }
        public int Third { get; set; }
        public int Fourth { get; set; }
        public int? Fifth { get; set; }
        public int? Sixth { get; set; }
        public long Sum { get; set; }

    }
}
