namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangOrganization")]
    public partial class DangOrganization
    {
        [Key]
        public Guid DangOrganizationUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string DangOrganizationName { get; set; }

        [StringLength(255)]
        public string DangOrganizationPeople { get; set; }

        [StringLength(255)]
        public string ChuangliTime { get; set; }

        [StringLength(255)]
        public string DangOrganizationRemarks { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string DangType { get; set; }

        [StringLength(255)]
        public string DangClerk { get; set; }
    }
}
