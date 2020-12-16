using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Village
    {
        public Village()
        {
            VillageMember = new HashSet<VillageMember>();
        }

        public int Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public Guid VillageUuid { get; set; }
        public string AddPeople { get; set; }
        public string VillageName { get; set; }
        public int? OrderBy { get; set; }
        public int? IsDelete { get; set; }

        public virtual ICollection<VillageMember> VillageMember { get; set; }
    }
}
