namespace HaikanSmartTownCockpit.WatchDog.Model
{
    public class Token
    {
        /// <summary>
        /// 成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
    }
    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; }
    }
}
