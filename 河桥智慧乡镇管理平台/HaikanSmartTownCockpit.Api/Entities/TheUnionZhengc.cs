using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class TheUnionZhengc
    {
        public Guid TheUnionZhengcUuid { get; set; }
        public int Id { get; set; }
        public int? State { get; set; }
        public int? IsDeleted { get; set; }
        public string TheUnionZhengcName { get; set; }
        public string Neirong { get; set; }
        public string Picture { get; set; }
    }
}
