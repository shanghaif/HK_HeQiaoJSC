using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Runbusiness
    {
        public int Id { get; set; }
        public Guid StudentUuid { get; set; }
        public string StudentName { get; set; }
        public string Sex { get; set; }
        public string Birth { get; set; }
        public string Education { get; set; }
        public string Address { get; set; }
        public string HeadTime { get; set; }
        public string Project { get; set; }
        public string Policy { get; set; }
        public string Condition { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
