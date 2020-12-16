using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class SystemRole
    {
        public Guid SystemRoleUuid { get; set; }
        public string RoleName { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public int? IsDeleted { get; set; }
        public int Id { get; set; }
        public int? IsFixation { get; set; }
    }
}
