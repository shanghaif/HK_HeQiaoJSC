using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Security
    {
        public Security()
        {
            SecurityReport = new HashSet<SecurityReport>();
        }

        public Guid SecurityUuid { get; set; }
        public int Id { get; set; }
        public string SecurityName { get; set; }
        public string SecurityAddress { get; set; }
        public string IdentityCard { get; set; }
        public string Phone { get; set; }
        public string SecurityTime { get; set; }
        public string SecurityStaues { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }

        public virtual ICollection<SecurityReport> SecurityReport { get; set; }
    }
}
