using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Relationships
    {
        public int Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Addpeople { get; set; }
        public Guid RelationshipsUuid { get; set; }
        public string Picture { get; set; }
    }
}
