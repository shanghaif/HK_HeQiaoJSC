namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alarmdata")]
    public partial class Alarmdata
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        public Guid AlarmUUID { get; set; }

        [StringLength(255)]
        public string AlarmTypeId { get; set; }

        [StringLength(255)]
        public string AnalogValue { get; set; }

        [StringLength(255)]
        public string AnalogValueTypeId { get; set; }

        [StringLength(255)]
        public string AudioUrl { get; set; }

        [StringLength(255)]
        public string AutoStatus { get; set; }

        [StringLength(255)]
        public string BuildingId { get; set; }

        [StringLength(255)]
        public string ChargingUserName { get; set; }

        [StringLength(255)]
        public string ChargingUserTel { get; set; }

        [StringLength(255)]
        public string ConfirmUserId { get; set; }

        [StringLength(255)]
        public string DetectorId { get; set; }

        [StringLength(255)]
        public string EndTime { get; set; }

        [StringLength(255)]
        public string EventAddress { get; set; }

        [StringLength(255)]
        public string EventContent { get; set; }

        [StringLength(255)]
        public string EventStatus { get; set; }

        [StringLength(255)]
        public string EventTypeId { get; set; }

        [StringLength(255)]
        public string HandingTime { get; set; }

        [StringLength(255)]
        public string HandlingSuggestion { get; set; }

        [StringLength(255)]
        public string ImageUrl { get; set; }

        [StringLength(255)]
        public string IsHandled { get; set; }

        [StringLength(255)]
        public string MainframeId { get; set; }

        [StringLength(255)]
        public string OrgId { get; set; }

        [StringLength(255)]
        public string PlatformCode { get; set; }

        [StringLength(255)]
        public string RecordCode { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        [StringLength(255)]
        public string ReportUserId { get; set; }

        [StringLength(255)]
        public string ResetStatus { get; set; }

        [StringLength(255)]
        public string ResetTime { get; set; }

        [StringLength(255)]
        public string StartTime { get; set; }

        [StringLength(255)]
        public string VideoUrl { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }
    }
}
