namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Userinfoty")]
    public partial class Userinfoty
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserInfoUUID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string RealName { get; set; }

        [StringLength(255)]
        public string UserIdCard { get; set; }

        [StringLength(255)]
        public string Wechat { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public Guid? DepartmentUUID { get; set; }

        [StringLength(255)]
        public string Birth { get; set; }

        [StringLength(255)]
        public string IdentityCard { get; set; }

        [StringLength(255)]
        public string Residence { get; set; }

        [StringLength(255)]
        public string Domicile { get; set; }

        [StringLength(255)]
        public string Nation { get; set; }

        [StringLength(255)]
        public string Education { get; set; }

        [StringLength(255)]
        public string QianYiSTime { get; set; }

        [StringLength(255)]
        public string QianYiETime { get; set; }

        [StringLength(255)]
        public string Settled { get; set; }

        [StringLength(255)]
        public string Occupation { get; set; }

        [StringLength(255)]
        public string DyStaues { get; set; }

        [StringLength(255)]
        public string Politics { get; set; }

        [StringLength(255)]
        public string position { get; set; }

        [StringLength(255)]
        public string Evaluate { get; set; }

        [StringLength(255)]
        public string OrganizationUuid { get; set; }

        [StringLength(255)]
        public string JoinArmy { get; set; }

        [StringLength(255)]
        public string ArmyTime { get; set; }

        [StringLength(255)]
        public string SettledTime { get; set; }

        public Guid? TownUuid { get; set; }

        [StringLength(255)]
        public string Defense { get; set; }

        [StringLength(255)]
        public string Category { get; set; }

        [StringLength(255)]
        public string Partybranch { get; set; }

        [StringLength(255)]
        public string JoinDate { get; set; }

        [StringLength(255)]
        public string Work { get; set; }

        public int? Age { get; set; }

        [StringLength(255)]
        public string Household { get; set; }

        [StringLength(100)]
        public string Relation { get; set; }

        [StringLength(100)]
        public string HouseholderName { get; set; }

        public virtual Town Town { get; set; }
    }
}
