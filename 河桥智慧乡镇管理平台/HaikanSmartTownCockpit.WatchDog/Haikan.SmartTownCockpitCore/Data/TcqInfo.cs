namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TcqInfo")]
    public partial class TcqInfo
    {
        [Key]
        public Guid Guid { get; set; }

        [StringLength(255)]
        public string detectorCode { get; set; }

        [StringLength(255)]
        public string detectorName { get; set; }

        [StringLength(255)]
        public string mainframeId { get; set; }

        [StringLength(255)]
        public string buildingId { get; set; }

        [StringLength(255)]
        public string detectorAddr { get; set; }

        [StringLength(255)]
        public string orgId { get; set; }

        [StringLength(255)]
        public string detectorType { get; set; }

        [StringLength(255)]
        public string ifcsSystemType { get; set; }

        [StringLength(255)]
        public string partunitloopCode { get; set; }

        [StringLength(255)]
        public string channelNo { get; set; }

        [StringLength(255)]
        public string hardwareVersion { get; set; }

        [StringLength(255)]
        public string softwareVersion { get; set; }

        [StringLength(255)]
        public string personId { get; set; }

        [StringLength(255)]
        public string minalarmValue { get; set; }

        [StringLength(255)]
        public string maxalarmValue { get; set; }

        [StringLength(255)]
        public string deviceRange { get; set; }

        [StringLength(255)]
        public string basalArea { get; set; }

        [StringLength(255)]
        public string maxHeight { get; set; }

        [StringLength(255)]
        public string registerTime { get; set; }

        [StringLength(255)]
        public string registerStatus { get; set; }

        [StringLength(255)]
        public string detectorRunBigStatus { get; set; }

        [StringLength(255)]
        public string detectorRunSmallStatus { get; set; }

        [StringLength(255)]
        public string producerId { get; set; }

        [StringLength(255)]
        public string produceDate { get; set; }

        [StringLength(255)]
        public string runDate { get; set; }

        [StringLength(255)]
        public string serviceLimit { get; set; }

        [StringLength(255)]
        public string mapType { get; set; }

        [StringLength(255)]
        public string gpsX3d { get; set; }

        [StringLength(255)]
        public string gpsY3d { get; set; }

        [StringLength(255)]
        public string gpsZ3d { get; set; }

        [StringLength(255)]
        public string planId { get; set; }

        [StringLength(255)]
        public string planX { get; set; }

        [StringLength(255)]
        public string planY { get; set; }

        [StringLength(255)]
        public string platformCode { get; set; }

        [StringLength(255)]
        public string recordCode { get; set; }

        [StringLength(255)]
        public string communicationId { get; set; }

        [StringLength(255)]
        public string hostSim { get; set; }

        [StringLength(255)]
        public string protocolType { get; set; }

        [StringLength(255)]
        public string firePartition { get; set; }

        [StringLength(255)]
        public string systemAddress { get; set; }

        [StringLength(1)]
        public string analogValueTypeId { get; set; }
    }
}
