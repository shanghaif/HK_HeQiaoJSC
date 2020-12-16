using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class CpcmanInfo
    {
        public Guid CpcmanUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string RealName { get; set; }
        public string Sex { get; set; }
        public DateTime? Birth { get; set; }
        public string Education { get; set; }
        public string Politics { get; set; }
        public int? Age { get; set; }
        public string DangOrganizationName { get; set; }
        public string TableNum { get; set; }
    }
}
