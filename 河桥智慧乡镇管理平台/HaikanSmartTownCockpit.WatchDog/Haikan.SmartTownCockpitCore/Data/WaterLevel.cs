namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WaterLevel")]
    public partial class WaterLevel
    {
        [Key]
        public Guid WaterLevelUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string WaterName { get; set; }

        [StringLength(255)]
        public string WaterInfo { get; set; }

        [StringLength(255)]
        public string Lon { get; set; }

        [StringLength(255)]
        public string Lat { get; set; }

        [StringLength(255)]
        public string Watersw { get; set; }

        [StringLength(255)]
        public string Accurate { get; set; }

        [StringLength(255)]
        public string MonitorWaterId { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string ShebeiType { get; set; }

        [StringLength(255)]
        public string AdminPepo { get; set; }

        [StringLength(255)]
        public string Weizhi { get; set; }
    }
}
