using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class OrganPeoInfo
    {
        public Guid OrganPeoInfoUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string OrganName { get; set; }
        public string Sex { get; set; }
        public string Birth { get; set; }
        public string IdentityCard { get; set; }
        public string Phone { get; set; }
        public string Education { get; set; }
        public string Duty { get; set; }
        public string ZjWork { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public Guid? OrganizationUuid { get; set; }
        public string TutelageName { get; set; }
        public string TutelagePhone { get; set; }
        public string HouseAddress { get; set; }
        public string Specific { get; set; }
        public string ManType { get; set; }

        public virtual Organization OrganizationUu { get; set; }
    }
}
