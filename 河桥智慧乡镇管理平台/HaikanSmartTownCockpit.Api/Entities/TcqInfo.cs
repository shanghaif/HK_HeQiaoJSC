using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class TcqInfo
    {
        public Guid Guid { get; set; }
        public string DetectorCode { get; set; }
        public string DetectorName { get; set; }
        public string MainframeId { get; set; }
        public string BuildingId { get; set; }
        public string DetectorAddr { get; set; }
        public string OrgId { get; set; }
        public string DetectorType { get; set; }
        public string IfcsSystemType { get; set; }
        public string PartunitloopCode { get; set; }
        public string ChannelNo { get; set; }
        public string HardwareVersion { get; set; }
        public string SoftwareVersion { get; set; }
        public string PersonId { get; set; }
        public string MinalarmValue { get; set; }
        public string MaxalarmValue { get; set; }
        public string DeviceRange { get; set; }
        public string BasalArea { get; set; }
        public string MaxHeight { get; set; }
        public string RegisterTime { get; set; }
        public string RegisterStatus { get; set; }
        public string DetectorRunBigStatus { get; set; }
        public string DetectorRunSmallStatus { get; set; }
        public string ProducerId { get; set; }
        public string ProduceDate { get; set; }
        public string RunDate { get; set; }
        public string ServiceLimit { get; set; }
        public string MapType { get; set; }
        public string GpsX3d { get; set; }
        public string GpsY3d { get; set; }
        public string GpsZ3d { get; set; }
        public string PlanId { get; set; }
        public string PlanX { get; set; }
        public string PlanY { get; set; }
        public string PlatformCode { get; set; }
        public string RecordCode { get; set; }
        public string CommunicationId { get; set; }
        public string HostSim { get; set; }
        public string ProtocolType { get; set; }
        public string FirePartition { get; set; }
        public string SystemAddress { get; set; }
        public string AnalogValueTypeId { get; set; }
    }
}
