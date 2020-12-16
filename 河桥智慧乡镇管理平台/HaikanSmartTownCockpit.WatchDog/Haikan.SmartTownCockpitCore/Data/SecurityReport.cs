namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SecurityReport")]
    public partial class SecurityReport
    {
        [Key]
        public Guid SecurityReportUUID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? Time { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        public string Situation { get; set; }

        public Guid? SecurityUuid { get; set; }

        public int? IsDeleted { get; set; }

        public virtual Security Security { get; set; }
    }
}
