using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.Models.Party
{
    public class TimeStreet
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
        public class ChildrenItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int geohash_len { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int count { get; set; }
            public int maxcount { get; set; }
            public int mincount { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 临安区
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int geohash_len { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<ChildrenItem> children { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int count { get; set; }
            public int maxcount { get; set; }
            public int mincount { get; set; }
        }
    }
}
