using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.ViewModels.Rbac.SystemUser
{
    /// <summary>
    /// 
    /// </summary>
    public class UserCreateViewModel
    {
        public Guid SystemUserUuid { get; set; }
        public string LoginName { get; set; }
        public string RealName { get; set; }
        public string UserIdCard { get; set; }
        public string PassWord { get; set; }
        public int? UserType { get; set; }
        public Guid? DepartmentUuid { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }

        public string Phone { get; set; }
        public int? IsDeleted { get; set; }
        public string OldCard { get; set; }
        public Guid? SystemRoleUuid { get; set; }
    }
}
