namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WaterLevelSheb")]
    public partial class WaterLevelSheb
    {
        [Key]
        public Guid WaterLevelShebUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string MonitorWaterId { get; set; }

        [StringLength(255)]
        public string Weizhi { get; set; }

        [StringLength(255)]
        public string ShebeiType { get; set; }

        [StringLength(255)]
        public string Lon { get; set; }

        [StringLength(255)]
        public string Lat { get; set; }

        [StringLength(255)]
        public string AdminPepo { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string YujingNum { get; set; }

        [StringLength(255)]
        public string YujingType { get; set; }
    }
}
