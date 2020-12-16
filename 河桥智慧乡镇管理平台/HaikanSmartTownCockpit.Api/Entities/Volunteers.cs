using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Volunteers
    {
        public Guid VolunteersUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string VolunteersName { get; set; }
        public string Phone { get; set; }
        public Guid? VillageUuid { get; set; }
        public string Jieshao { get; set; }
        public string Staues { get; set; }
    }
}
