using System.Collections.Generic;

namespace HaikanSmartTownCockpit.WatchDog.Ezviz.Devices
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
        /// xiaoge的设备
        /// </summary>
        public string deviceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int defence { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceVersion { get; set; }
    }

    public class Devices
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
