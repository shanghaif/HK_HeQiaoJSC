using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Ezviz.LiveVideo
{
    
    public class Page
    {
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public string deviceSerial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int channelNo { get; set; }
        /// <summary>
        /// 萤小石
        /// </summary>
        public string deviceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string liveAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hdAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rtmp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rtmpHd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long beginTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long endTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int exception { get; set; }
    }

    public class LiveVideo
    {
        /// <summary>
        /// 
        /// </summary>
        public Page page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Data> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 操作成功!
        /// </summary>
        public string msg { get; set; }
    }

}