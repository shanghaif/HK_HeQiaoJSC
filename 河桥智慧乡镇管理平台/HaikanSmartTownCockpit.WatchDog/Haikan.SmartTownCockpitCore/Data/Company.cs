namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        [Key]
        public Guid CompanyUUID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(255)]
        public string EngName { get; set; }

        [StringLength(100)]
        public string LegalPeople { get; set; }

        [StringLength(255)]
        public string UnifiedSocialCode { get; set; }

        [StringLength(255)]
        public string BusinessLogin { get; set; }

        [StringLength(100)]
        public string CompanyPhone { get; set; }

        [StringLength(255)]
        public string CompanyEmail { get; set; }

        [StringLength(255)]
        public string Officialwebsite { get; set; }

        [StringLength(255)]
        public string CompanyAddress { get; set; }

        [StringLength(100)]
        public string industry { get; set; }

        [StringLength(100)]
        public string CompanyType { get; set; }

        [StringLength(100)]
        public string District { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        public int? IsDelete { get; set; }

        public Guid? SystemUserUUID { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }
    }
}
