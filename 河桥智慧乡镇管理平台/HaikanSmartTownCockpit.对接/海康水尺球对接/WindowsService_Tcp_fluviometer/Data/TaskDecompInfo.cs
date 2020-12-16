namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaskDecompInfo")]
    public partial class TaskDecompInfo
    {
        [Key]
        public Guid TaskDecompInfoUUID { get; set; }

        public Guid? TDUUID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(200)]
        public string SpecialWorkTeam { get; set; }

        [StringLength(100)]
        public string DedicatedTeamLeader { get; set; }

        [StringLength(200)]
        public string RelatedCadres { get; set; }

        [StringLength(500)]
        public string Principal { get; set; }

        public string Requirement { get; set; }

        public int? IsDelete { get; set; }

        public DateTime? AddTime { get; set; }

        [StringLength(500)]
        public string Phone { get; set; }
    }
}
