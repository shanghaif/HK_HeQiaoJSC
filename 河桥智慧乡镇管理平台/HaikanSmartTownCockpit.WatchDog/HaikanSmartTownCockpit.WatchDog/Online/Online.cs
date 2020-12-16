using System.Collections.Generic;

namespace HaikanSmartTownCockpit.WatchDog.Online
{
    public class List
    {
        /// <summary>
        /// 
        /// </summary>
        public string deviceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string regionIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string collectTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 根节点
        /// </summary>
        public string regionName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string indexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int online { get; set; }
        /// <summary>
        /// IPdome--球机123
        /// </summary>
        public string cn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string treatyType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string manufacturer { get; set; }
    }

    public class Data
    {
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
        public int totalPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<List> list { get; set; }
    }

    public class Online
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
