using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class YouthPosition
    {
        public int Id { get; set; }
        public Guid YpositionUuid { get; set; }
        public string Name { get; set; }
        public string Accessory { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public int IsDelete { get; set; }
    }
}
