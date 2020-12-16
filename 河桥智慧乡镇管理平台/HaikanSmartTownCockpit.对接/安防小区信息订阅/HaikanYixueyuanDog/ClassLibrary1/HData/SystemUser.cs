namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemUser")]
    public partial class SystemUser
    {
        [Key]
        public Guid SystemUserUUID { get; set; }

        [StringLength(255)]
        public string LoginName { get; set; }

        [StringLength(255)]
        public string RealName { get; set; }

        [StringLength(255)]
        public string UserIdCard { get; set; }

        [StringLength(255)]
        public string PassWord { get; set; }

        public int? UserType { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        public int? IsDeleted { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

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

        [StringLength(255)]
        public string SystemRoleUUid { get; set; }

        [StringLength(255)]
        public string IDCardMD5 { get; set; }

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
        public string QianYiDe { get; set; }

        [StringLength(255)]
        public string QianYiTime { get; set; }

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
        public string TownUuid { get; set; }

        [StringLength(255)]
        public string OrganizationUuid { get; set; }

        [Column("JoinArmy	")]
        [StringLength(255)]
        public string JoinArmy_ { get; set; }

        [StringLength(255)]
        public string ArmyTime { get; set; }

        [StringLength(255)]
        public string SettledTime { get; set; }

        [StringLength(255)]
        public string OldCard { get; set; }

        [Column(TypeName = "text")]
        public string ManageDepartment { get; set; }

        [StringLength(255)]
        public string Dinguserid { get; set; }

        public virtual Department Department { get; set; }
    }
}
