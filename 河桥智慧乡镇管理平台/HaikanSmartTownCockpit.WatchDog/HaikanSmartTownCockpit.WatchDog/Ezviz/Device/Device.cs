namespace HaikanSmartTownCockpit.WatchDog.Ezviz.Device
{
    
    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public string deviceSerial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string model { get; set; }
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
        public int isEncrypt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int alarmSoundMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int offlineNotify { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string netType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signal { get; set; }
    }

    public class Device
    {
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
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
