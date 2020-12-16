using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Department
    {
        public Department()
        {
            SystemUser = new HashSet<SystemUser>();
        }

        public Guid DepartmentUuid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string EstablishTime { get; set; }
        public string EstablishName { get; set; }
        public int? IsDeleted { get; set; }
        public string Dingid { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }

        public virtual ICollection<SystemUser> SystemUser { get; set; }
    }
}
