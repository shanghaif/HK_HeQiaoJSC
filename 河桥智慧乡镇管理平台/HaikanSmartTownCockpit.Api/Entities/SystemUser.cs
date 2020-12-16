using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class SystemUser
    {
        public Guid SystemUserUuid { get; set; }
        public string LoginName { get; set; }
        public string RealName { get; set; }
        public string UserIdCard { get; set; }
        public string PassWord { get; set; }
        public int? UserType { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public int? IsDeleted { get; set; }
        public int Id { get; set; }
        public string Wechat { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string SystemRoleUuid { get; set; }
        public string IdcardMd5 { get; set; }
        public Guid? DepartmentUuid { get; set; }
        public string Birth { get; set; }
        public string IdentityCard { get; set; }
        public string Residence { get; set; }
        public string Domicile { get; set; }
        public string Nation { get; set; }
        public string Education { get; set; }
        public string QianYiDe { get; set; }
        public string QianYiTime { get; set; }
        public string Settled { get; set; }
        public string Occupation { get; set; }
        public string DyStaues { get; set; }
        public string Politics { get; set; }
        public string Position { get; set; }
        public string Evaluate { get; set; }
        public string TownUuid { get; set; }
        public string OrganizationUuid { get; set; }
        public string JoinArmy { get; set; }
        public string ArmyTime { get; set; }
        public string SettledTime { get; set; }
        public string OldCard { get; set; }
        public string ManageDepartment { get; set; }
        public string Dinguserid { get; set; }

        public virtual Department DepartmentUu { get; set; }
    }
}
