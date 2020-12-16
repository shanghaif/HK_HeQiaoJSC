namespace HaikanSmartTownCockpit.WatchDog.Root
{
    public class Root
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
    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public string indexCode { get; set; }
        /// <summary>
        /// 全国
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

    

}
