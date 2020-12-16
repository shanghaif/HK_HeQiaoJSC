using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Ezviz.AccessToken
{
    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public string accessToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long expireTime { get; set; }
    }

    public class AccessToken
    {
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 操作成功!
        /// </summary>
        public string msg { get; set; }
    }
}