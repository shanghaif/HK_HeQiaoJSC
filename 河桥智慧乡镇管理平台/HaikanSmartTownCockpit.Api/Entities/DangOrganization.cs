using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class DangOrganization
    {
        public Guid DangOrganizationUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string DangOrganizationName { get; set; }
        public string DangOrganizationPeople { get; set; }
        public string ChuangliTime { get; set; }
        public string DangOrganizationRemarks { get; set; }
        public string AddTime { get; set; }
        public string DangType { get; set; }
        public string DangClerk { get; set; }
    }
}
