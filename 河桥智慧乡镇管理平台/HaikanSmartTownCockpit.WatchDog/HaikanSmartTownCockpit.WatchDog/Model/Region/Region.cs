using System.Collections.Generic;

namespace HaikanSmartTownCockpit.WatchDog.Model.Region
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
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parentIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string treeCode { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int pageNo { get; set; }
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
