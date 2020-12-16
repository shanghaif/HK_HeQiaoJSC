using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.DataClass
{
    public class Data
    {
        /// <summary>
        /// token令牌
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 对应的平台编号信息
        /// </summary>
        public string platformCode { get; set; }
        /// <summary>
        /// token失效时间
        /// </summary>
        public string expired { get; set; }
    }

    public class TokenDataRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string ret_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ret_msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
    }
}
