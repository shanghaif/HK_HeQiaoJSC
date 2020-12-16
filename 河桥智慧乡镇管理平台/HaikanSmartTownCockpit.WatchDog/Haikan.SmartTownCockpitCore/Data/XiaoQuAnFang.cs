namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XiaoQuAnFang")]
    public partial class XiaoQuAnFang
    {
        [Key]
        public Guid Guid { get; set; }

        [StringLength(255)]
        public string ability { get; set; }

        [StringLength(255)]
        public string URL { get; set; }

        [StringLength(255)]
        public string type { get; set; }

        [StringLength(255)]
        public string bkgUrl { get; set; }

        [StringLength(255)]
        public string cameraIndexCode { get; set; }

        [StringLength(255)]
        public string deviceIndexCode { get; set; }

        [StringLength(255)]
        public string faceTime { get; set; }

        [StringLength(255)]
        public string picServerIndexCode { get; set; }

        [StringLength(255)]
        public string channelID { get; set; }

        [StringLength(255)]
        public string dataType { get; set; }

        [StringLength(255)]
        public string ipAddress { get; set; }

        [StringLength(255)]
        public string portNo { get; set; }

        [StringLength(255)]
        public string eventId { get; set; }

        [StringLength(255)]
        public string srcIndex { get; set; }

        [StringLength(255)]
        public string srcType { get; set; }

        [StringLength(255)]
        public string srcName { get; set; }

        [StringLength(255)]
        public string eventType { get; set; }

        [StringLength(255)]
        public string status { get; set; }

        [StringLength(255)]
        public string happenTime { get; set; }

        [StringLength(255)]
        public string srcParentIndex { get; set; }

        [StringLength(255)]
        public string sendTime { get; set; }
    }
}
