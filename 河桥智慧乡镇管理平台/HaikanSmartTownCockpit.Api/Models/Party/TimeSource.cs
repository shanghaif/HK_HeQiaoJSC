using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.Models.Party
{
    public class TimeSource
    {
        /// <summary>
        /// 
        /// </summary>
        public int errno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cost { get; set; }
        public class Top10Item
        {
            /// <summary>
            /// 
            /// </summary>
            public string adcode { get; set; }
            /// <summary>
            /// 上海市
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double rate { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public double local { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double in_province { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double out_province { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Top10Item> top10 { get; set; }
        }
    }
}
