namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaskDecomposition")]
    public partial class TaskDecomposition
    {
        [Key]
        public Guid TaskDecompositionUUID { get; set; }

        [StringLength(250)]
        public string Commander { get; set; }

        [StringLength(500)]
        public string Member { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDelete { get; set; }

        public DateTime? AddTime { get; set; }
    }
}
