namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DepartmentDeclare")]
    public partial class DepartmentDeclare
    {
        [Key]
        public Guid DepartmentDeclareUUID { get; set; }

        [StringLength(255)]
        public string DeclareName { get; set; }

        [StringLength(255)]
        public string DeclareDepartment { get; set; }

        [StringLength(255)]
        public string DeclareTime { get; set; }

        [StringLength(255)]
        public string BonusPoint { get; set; }

        public int? PlusScore { get; set; }

        [StringLength(255)]
        public string PlusContent { get; set; }

        [StringLength(255)]
        public string Deduction { get; set; }

        public int? DeductionScore { get; set; }

        [StringLength(255)]
        public string DeductionContent { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        [StringLength(255)]
        public string EstablishTime { get; set; }

        [StringLength(255)]
        public string EstablishName { get; set; }

        public int? IsDeleted { get; set; }

        public Guid? PerformanceDeclareUUID { get; set; }

        [StringLength(255)]
        public string AuditOpinion { get; set; }

        [StringLength(255)]
        public string AuditStatus { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }
}
