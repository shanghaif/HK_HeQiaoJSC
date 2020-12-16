using System.Collections.Generic;

namespace HaikanSmartTownCockpit.WatchDog.sqjz.model3
{
    
    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public string parkSyscode { get; set; }
        /// <summary>
        /// 停车库1
        /// </summary>
        public string parkName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parentParkSyscode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalPlace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalPermPlace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalReservePlace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int leftPlace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int leftPermPlace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int leftReservePlace { get; set; }
    }

    public class Carport
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
        public List<Data> data { get; set; }
    }

}
