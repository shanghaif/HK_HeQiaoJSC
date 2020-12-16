using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.Models.Party
{
    public class Timedata
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
        public int cost { get; set; }
        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public string accessToken { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int expireIn { get; set; }
        }
    }
}
