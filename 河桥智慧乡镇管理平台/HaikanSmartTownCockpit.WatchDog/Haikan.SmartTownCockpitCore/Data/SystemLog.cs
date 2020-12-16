namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemLog")]
    public partial class SystemLog
    {
        [Key]
        public Guid SystemLogUUID { get; set; }

        [StringLength(255)]
        public string UserID { get; set; }

        public int? UserIDType { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime? OperationTime { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string OperationContent { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        public int IsDelete { get; set; }

        public DateTime AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }
}
