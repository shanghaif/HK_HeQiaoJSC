using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class SKData
    {
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
    }

    public class SKVideo
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
        public SKData data { get; set; }
    }

}