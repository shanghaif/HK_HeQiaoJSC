using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.DataClass.Subscribe8900
{
    public class SubRegister
    {
        //接入方名称
        public string name { get; set; }
        //回调地址
        public string callbackAddress { get; set; }
        //是否有效期 0:表示无期限;1:有效期,需填入接口调用截止日期
        public int hasExpir { get; set; }
        //有效期限 hasExpir为1时必填
        public string expirationTime { get; set; }
        //备注
        public string memo { get; set; }

    }
    public class SubRegisterReturnData
    {
        /// <summary>
        /// 
        /// </summary>
        public string callbackAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int encryType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string expirationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int hasExpir { get; set; }
        /// <summary>
        /// 江苏常熟颢裕科技与8900平台对接
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 江苏常熟颢裕科技
        /// </summary>
        public string name { get; set; }
    }
    public class SubRegisterReturn
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SubRegisterReturnData data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string success { get; set; }
        public string errMsg { get; set; }
    }
}
