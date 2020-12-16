using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class SthOrganic
    {
        public int Id { get; set; }
        public Guid SthOrganicUuid { get; set; }
        public string Name { get; set; }
        public string Secretary { get; set; }
        public string Cadre { get; set; }
        public int? Number { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public int IsDelete { get; set; }
    }
}
