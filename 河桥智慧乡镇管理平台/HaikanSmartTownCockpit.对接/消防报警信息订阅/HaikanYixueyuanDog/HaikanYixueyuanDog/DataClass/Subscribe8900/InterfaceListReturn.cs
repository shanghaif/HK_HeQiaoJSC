using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.DataClass.Subscribe8900
{
    public class InterfaceListReturn
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string success { get; set; }
        public string errMsg { get; set; }
    }
    public class CancellationReturn
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string success { get; set; }
        public string errMsg { get; set; }
    }

}
