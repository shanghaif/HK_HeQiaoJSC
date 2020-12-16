using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Propaganda
    {
        public Guid PropagandaTypeUuid { get; set; }
        public int Id { get; set; }
        public int? State { get; set; }
        public int? IsDeleted { get; set; }
        public string PropagandaTypeName { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string SumCount { get; set; }
    }
}
