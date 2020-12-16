using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Sectarian
    {
        public Guid SectarianUuid { get; set; }
        public int Id { get; set; }
        public string SectarianName { get; set; }
        public string SectarianLocation { get; set; }
        public string SectarianTime { get; set; }
        public string SectarianRecommend { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string SectarianType { get; set; }
    }
}
