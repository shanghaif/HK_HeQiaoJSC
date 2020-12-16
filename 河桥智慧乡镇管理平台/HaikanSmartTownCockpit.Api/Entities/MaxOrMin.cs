using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class MaxOrMin
    {
        public Guid Guid { get; set; }
        public string MaxCount { get; set; }
        public string MinCount { get; set; }
    }
}
