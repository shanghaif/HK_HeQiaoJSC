namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cunzhenmb")]
    public partial class Cunzhenmb
    {
        [Key]
        public Guid CunzhenmbUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public int? Age { get; set; }

        [StringLength(255)]
        public string Household { get; set; }

        [StringLength(100)]
        public string Relation { get; set; }
    }
}
