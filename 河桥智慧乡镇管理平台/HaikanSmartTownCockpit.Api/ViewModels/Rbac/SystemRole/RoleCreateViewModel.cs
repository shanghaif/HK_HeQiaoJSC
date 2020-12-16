using HaikanSmartTownCockpit.Api.Entities.Enums;
using System;

namespace HaikanSmartTownCockpit.Api.ViewModels.Rbac.SystemRole
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleCreateViewModel
    {
        public Guid SystemRoleUuid { get; set; }
        public string RoleName { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public int? IsDeleted { get; set; }
        public int Id { get; set; }
    }
}
