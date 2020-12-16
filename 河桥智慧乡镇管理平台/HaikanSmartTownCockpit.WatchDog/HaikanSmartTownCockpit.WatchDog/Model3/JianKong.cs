using System.Collections.Generic;

namespace HaikanSmartTownCockpit.WatchDog.Model3
{
    public class List
    {
        /// <summary>
        /// 
        /// </summary>
        public string cameraIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gbIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string altitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? pixel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? cameraType { get; set; }
        /// <summary>
        /// 半球
        /// </summary>
        public string cameraTypeName { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string installPlace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string matrixCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? chanNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string viewshed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string capabilitySet { get; set; }
        /// <summary>
        /// 人脸采集能力
        /// </summary>
        public string capabilitySetName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string intelligentSet { get; set; }
        /// <summary>
        /// 人脸结构化能力
        /// </summary>
        public string intelligentSetName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recordLocation { get; set; }
        /// <summary>
        /// 中心存储
        /// </summary>
        public string recordLocationName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? ptzController { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ptzControllerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceResourceType { get; set; }
        /// <summary>
        /// 编码设备
        /// </summary>
        public string deviceResourceTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string channelType { get; set; }
        /// <summary>
        /// 数字通道
        /// </summary>
        public string channelTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? transType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string transTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unitIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string treatyType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string treatyTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? status { get; set; }
        /// <summary>
        /// 不在线
        /// </summary>
        public string statusName { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<List> list { get; set; }
    }

    public class JianKong
    {
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
    }


}
