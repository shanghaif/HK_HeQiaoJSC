using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class DzhizaihaiInfo
    {
        public Guid DzhizaihaiInfoUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string Diqu { get; set; }
        public string Shuliang { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
