using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.Models.Party
{
    public class TimePostWork
    {
        /// <summary>
        /// 
        /// </summary>
        public int error_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cost { get; set; }
        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public int worknum { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int homenum { get; set; }
        }
    }
}
