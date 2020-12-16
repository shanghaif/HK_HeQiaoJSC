using System.Collections.Generic;

namespace HaikanSmartTownCockpit.WatchDog.Camera
{
    
    public class List
    {
        /// <summary>
        /// 
        /// </summary>
        public string indexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string externalIndexCode { get; set; }
        /// <summary>
        /// 资源1
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int chanNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cascadeCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parentIndexCode { get; set; }
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
        public string elevation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cameraType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string capability { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recordLocation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string channelType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string regionIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string regionPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int transType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string treatyType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string installLocation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int disOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resourceIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string decodeTag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cameraRelateTalk { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string regionName { get; set; }
        /// <summary>
        /// 根节点/global_setUp_02178
        /// </summary>
        public string regionPathName { get; set; }
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
        public int pageNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<List> list { get; set; }
    }

    public class Camera
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
