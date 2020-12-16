namespace HaikanSmartTownCockpit.WatchDog.sqjz.model
{
    
    public class Datas
    {
        /// <summary>
        /// 
        /// </summary>
        public int requestSecretEndTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string requestSecret { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refreshSecret { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int refreshSecretEndTime { get; set; }
    }

    public class TokenResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Datas datas { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int dataCount { get; set; }
    }
}
