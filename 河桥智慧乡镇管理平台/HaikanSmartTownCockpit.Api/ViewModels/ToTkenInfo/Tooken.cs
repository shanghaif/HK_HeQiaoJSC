using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.ToTkenInfo
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
