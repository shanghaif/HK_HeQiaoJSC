using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class TaskDecomposition
    {
        public Guid TaskDecompositionUuid { get; set; }
        public string Commander { get; set; }
        public string Member { get; set; }
        public int Id { get; set; }
        public int? IsDelete { get; set; }
        public DateTime? AddTime { get; set; }
    }
}
