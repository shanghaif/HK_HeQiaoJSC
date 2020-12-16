namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CarAnFang")]
    public partial class CarAnFang
    {
        [Key]
        public Guid Guid { get; set; }

        [StringLength(255)]
        public string ability { get; set; }

        [StringLength(255)]
        public string plateTypeName { get; set; }

        [StringLength(255)]
        public string crossTime { get; set; }

        [StringLength(255)]
        public string alarmTypeName { get; set; }

        [StringLength(255)]
        public string monitorName { get; set; }

        [StringLength(255)]
        public string mixedName { get; set; }

        [StringLength(255)]
        public string platePicUrl { get; set; }

        [StringLength(255)]
        public string vehiclePicUrl { get; set; }

        [StringLength(255)]
        public string imageIndexCode { get; set; }

        [StringLength(255)]
        public string personName { get; set; }

        [StringLength(255)]
        public string phoneNo { get; set; }

        [StringLength(255)]
        public string happenTime { get; set; }
    }
}
