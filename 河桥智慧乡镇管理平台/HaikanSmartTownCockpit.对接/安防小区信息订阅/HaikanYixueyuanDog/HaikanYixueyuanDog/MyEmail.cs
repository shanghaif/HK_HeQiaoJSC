using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog
{
    public class MyEmail
    {
        /// <summary>
        /// 发送方发送方服务器地址
        /// </summary>
        public string strHost { get; set; }

        /// <summary>
        /// 发送方帐号
        /// </summary>
        public string strAccount { get; set; }

        /// <summary>
        /// 发送方密码
        /// </summary>
        public string strPwd { get; set; }

        /// <summary>
        /// 发送方邮件地址
        /// </summary>
        public string strFrom { get; set; }

        /// <summary>
        /// 接收方邮件地址
        /// </summary>
        public string to { get; set; }

        /// <summary>
        /// 邮件标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 邮件正文内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string sfile { get; set; }
    }
}
