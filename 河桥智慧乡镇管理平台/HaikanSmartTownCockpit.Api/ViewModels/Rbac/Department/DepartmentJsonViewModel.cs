using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Rbac.Department
{
    public class DepartmentJsonViewModel
    {
        public Guid DepartmentUuid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string EstablishTime { get; set; }
        public string EstablishName { get; set; }
        public int? IsDeleted { get; set; }
    }
}
