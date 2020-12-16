using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Canyin
    {
        public Guid CanyinUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string CanyinName { get; set; }
        public string CanyinAddress { get; set; }
        public string Fuzeren { get; set; }
        public string Phone { get; set; }
        public string Town { get; set; }
        public string Staues { get; set; }
    }
}
