using System.Collections.Generic;

namespace HaikanSmartTownCockpit.WatchDog.sqjz.model2
{
    
    public class List
    {
        /// <summary>
        /// 
        /// </summary>
        public string indexCode { get; set; }
        /// <summary>
        /// 中国
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parentIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parentResourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parkPath { get; set; }
        /// <summary>
        /// 接口自动化_停车场KziIK
        /// </summary>
        public string parkNamePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string regionIndexCode { get; set; }
        /// <summary>
        /// 根节点
        /// </summary>
        public string regionName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string regionPath { get; set; }
        /// <summary>
        /// 根节点
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

    public class ParkNodes
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
