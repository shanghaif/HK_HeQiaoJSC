namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegionInfo")]
    public partial class RegionInfo
    {
        public int ID { get; set; }

        public int? RegionID { get; set; }

        [StringLength(255)]
        public string RegionX { get; set; }

        [StringLength(255)]
        public string RegionY { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? AddTime { get; set; }
    }
}
