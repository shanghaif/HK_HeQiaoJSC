using System.Collections.Generic;

namespace HaikanSmartTownCockpit.WatchDog.Region
{
    
    public class List
    {
        /// <summary>
        /// 
        /// </summary>
        public string indexCode { get; set; }
        /// <summary>
        /// 根节点
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string regionPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parentIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool available { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool leaf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cascadeCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cascadeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int catalogType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string externalIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updateTime { get; set; }
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

    public class Region
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
