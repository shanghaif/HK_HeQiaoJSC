using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.DataClass
{
    public class DataItem
    {
        /// <summary>
        /// 公钥内容
        /// </summary>
        public string publicKey { get; set; }
    }

    public class PublickeyRoot
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
        public List<DataItem> data { get; set; }
    }
}
