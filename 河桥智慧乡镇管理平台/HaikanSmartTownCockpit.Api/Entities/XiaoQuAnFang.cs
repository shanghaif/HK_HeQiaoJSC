using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class XiaoQuAnFang
    {
        public Guid Guid { get; set; }
        public string Ability { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string BkgUrl { get; set; }
        public string CameraIndexCode { get; set; }
        public string DeviceIndexCode { get; set; }
        public string FaceTime { get; set; }
        public string PicServerIndexCode { get; set; }
        public string ChannelId { get; set; }
        public string DataType { get; set; }
        public string IpAddress { get; set; }
        public string PortNo { get; set; }
        public string EventId { get; set; }
        public string SrcIndex { get; set; }
        public string SrcType { get; set; }
        public string SrcName { get; set; }
        public string EventType { get; set; }
        public string Status { get; set; }
        public string HappenTime { get; set; }
        public string SrcParentIndex { get; set; }
        public string SendTime { get; set; }
    }
}
