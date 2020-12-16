using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Userinfoty
    {
        public Guid UserInfoUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string RealName { get; set; }
        public string UserIdCard { get; set; }
        public string Wechat { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public Guid? DepartmentUuid { get; set; }
        public string Birth { get; set; }
        public string IdentityCard { get; set; }
        public string Residence { get; set; }
        public string Domicile { get; set; }
        public string Nation { get; set; }
        public string Education { get; set; }
        public string QianYiStime { get; set; }
        public string QianYiEtime { get; set; }
        public string Settled { get; set; }
        public string Occupation { get; set; }
        public string DyStaues { get; set; }
        public string Politics { get; set; }
        public string Position { get; set; }
        public string Evaluate { get; set; }
        public string OrganizationUuid { get; set; }
        public string JoinArmy { get; set; }
        public string ArmyTime { get; set; }
        public string SettledTime { get; set; }
        public Guid? TownUuid { get; set; }
        public string Defense { get; set; }
        public string Category { get; set; }
        public string Partybranch { get; set; }
        public string JoinDate { get; set; }
        public string Work { get; set; }
        public int? Age { get; set; }
        public string Household { get; set; }
        public string Relation { get; set; }
        public string HouseholderName { get; set; }

        public virtual Town TownUu { get; set; }
    }
}
