namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UAV")]
    public partial class UAV
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateTime { get; set; }

        [Key]
        public Guid UAVUUID { get; set; }

        [StringLength(255)]
        public string UAVName { get; set; }

        [StringLength(255)]
        public string UAVNumber { get; set; }

        [StringLength(255)]
        public string UAVUrl { get; set; }

        [StringLength(255)]
        public string UAVAddress { get; set; }

        public int? IsDeleted { get; set; }
    }
}
