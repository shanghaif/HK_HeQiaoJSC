namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mission")]
    public partial class Mission
    {
        [Key]
        public Guid MissionUUID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public Guid? PriorityUUID { get; set; }

        [StringLength(255)]
        public string MissionHeadline { get; set; }

        public string Principal { get; set; }

        [StringLength(255)]
        public string StartTime { get; set; }

        [StringLength(255)]
        public string FinishTime { get; set; }

        [StringLength(255)]
        public string MissionDescribe { get; set; }

        [StringLength(255)]
        public string Priority { get; set; }

        [StringLength(255)]
        public string Manhour { get; set; }

        public string Participant { get; set; }

        public string Approver { get; set; }

        [StringLength(255)]
        public string Accomplish { get; set; }

        [StringLength(255)]
        public string Recycle { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        [StringLength(255)]
        public string Preserve { get; set; }

        [StringLength(255)]
        public string EstablishTime { get; set; }

        [StringLength(255)]
        public string EstablishName { get; set; }

        [StringLength(255)]
        public string AuditOpinion { get; set; }

        [StringLength(255)]
        public string AuditStatus { get; set; }

        [StringLength(255)]
        public string AdministrativeOffice { get; set; }

        [StringLength(255)]
        public string Sortord { get; set; }

        [StringLength(255)]
        public string Missiontype { get; set; }

        [StringLength(255)]
        public string ZhiDingTime { get; set; }

        [StringLength(255)]
        public string Fujian { get; set; }

        [StringLength(255)]
        public string Isouttime { get; set; }
    }
}
