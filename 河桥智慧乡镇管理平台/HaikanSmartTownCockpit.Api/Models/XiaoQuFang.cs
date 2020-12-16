using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.Models
{


    public class Age
    {
        /// <summary>
        /// 
        /// </summary>
        public string ageGroup { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int range { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int value { get; set; }
    }

    public class Beard
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
    }

    public class FaceExpression
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
    }

    public class FaceRect
    {
        /// <summary>
        /// 
        /// </summary>
        public double height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double y { get; set; }
    }

    public class FaceScore
    {
        /// <summary>
        /// 
        /// </summary>
        public string enable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double totalScore { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
    }

    public class Gender
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
    }

    public class Glass
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
    }

    public class Hat
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
    }

    public class Mask
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
    }

    public class Race
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
    }

    public class RecommendFaceRect
    {
        /// <summary>
        /// 
        /// </summary>
        public double height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double y { get; set; }
    }

    public class Smile
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
    }
    public class FacesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Age age { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Beard beard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string captureEndMark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FaceExpression faceExpression { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int faceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FaceRect faceRect { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FaceScore faceScore { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string faceSnapThermometryEnabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string faceTemperature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Gender gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Glass glass { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Hat hat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isAbnomalTemperature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Mask mask { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Race race { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RecommendFaceRect recommendFaceRect { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Smile smile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int stayDuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string thermometryUnit { get; set; }
    }

    public class Rect
    {
        /// <summary>
        /// 
        /// </summary>
        public double height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double y { get; set; }
    }

    public class TargetAttrs
    {
        /// <summary>
        /// 
        /// </summary>
        public string bkgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cameraIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string faceTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imageServerCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picServerIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Rect rect { get; set; }
    }

    public class CaptureLibResultItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int errorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FacesItem> faces { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TargetAttrs targetAttrs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int traceIdx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string traceUuid { get; set; }
    }

    public class Data2
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CaptureLibResultItem> captureLibResult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int channelID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dataProcInterval { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dataType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string eventDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string eventType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ipAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUploadInterval { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int portNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recvTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sendTime { get; set; }
    }

    public class EventsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public Data2 data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string eventId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int eventType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string happenTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcParentIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int timeout { get; set; }
    }

    public class Params
    {
        /// <summary>
        /// 
        /// </summary>
        public string ability { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dataProcInterval { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<EventsItem> events { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sendTime { get; set; }
    }
    public class XiaoQuFang
    {
        /// <summary>
        /// 
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Params @params { get; set; }
    }
}
