using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Activity
    {
        public int Id { get; set; }
        public Guid ActivityUuid { get; set; }
        public string ActivityName { get; set; }
        public string ActivityTime { get; set; }
        public string ActivityWay { get; set; }
        public string Address { get; set; }
        public int? IsDeleted { get; set; }
        public string AddPeople { get; set; }
        public string AddTime { get; set; }
    }
}
