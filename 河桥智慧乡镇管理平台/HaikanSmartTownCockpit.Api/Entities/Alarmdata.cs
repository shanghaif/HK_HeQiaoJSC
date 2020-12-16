using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Alarmdata
    {
        public int Id { get; set; }
        public Guid AlarmUuid { get; set; }
        public string AlarmTypeId { get; set; }
        public string AnalogValue { get; set; }
        public string AnalogValueTypeId { get; set; }
        public string AudioUrl { get; set; }
        public string AutoStatus { get; set; }
        public string BuildingId { get; set; }
        public string ChargingUserName { get; set; }
        public string ChargingUserTel { get; set; }
        public string ConfirmUserId { get; set; }
        public string DetectorId { get; set; }
        public string EndTime { get; set; }
        public string EventAddress { get; set; }
        public string EventContent { get; set; }
        public string EventStatus { get; set; }
        public string EventTypeId { get; set; }
        public string HandingTime { get; set; }
        public string HandlingSuggestion { get; set; }
        public string ImageUrl { get; set; }
        public string IsHandled { get; set; }
        public string MainframeId { get; set; }
        public string OrgId { get; set; }
        public string PlatformCode { get; set; }
        public string RecordCode { get; set; }
        public string Remarks { get; set; }
        public string ReportUserId { get; set; }
        public string ResetStatus { get; set; }
        public string ResetTime { get; set; }
        public string StartTime { get; set; }
        public string VideoUrl { get; set; }
        public string AddTime { get; set; }
        public string IsRead { get; set; }
    }
}
