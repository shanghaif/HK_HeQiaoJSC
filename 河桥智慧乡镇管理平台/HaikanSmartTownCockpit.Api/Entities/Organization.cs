using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Organization
    {
        public Organization()
        {
            OrganPeoInfo = new HashSet<OrganPeoInfo>();
        }

        public Guid OrganizationUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationPeople { get; set; }
        public string ChuangliTime { get; set; }
        public string OrganizationRemarks { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }

        public virtual ICollection<OrganPeoInfo> OrganPeoInfo { get; set; }
    }
}
