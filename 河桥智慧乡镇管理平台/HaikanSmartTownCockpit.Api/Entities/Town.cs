using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Town
    {
        public Town()
        {
            Userinfoty = new HashSet<Userinfoty>();
        }

        public Guid TownUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string TownName { get; set; }
        public string TownPeople { get; set; }
        public string PartyMember { get; set; }
        public string Geographical { get; set; }
        public string Company { get; set; }
        public Guid? SjTownUuid { get; set; }
        public string TownGrade { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }

        public virtual ICollection<Userinfoty> Userinfoty { get; set; }
    }
}
