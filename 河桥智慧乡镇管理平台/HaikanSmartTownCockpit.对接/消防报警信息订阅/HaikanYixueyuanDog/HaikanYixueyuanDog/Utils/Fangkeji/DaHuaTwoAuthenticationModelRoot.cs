using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils.Fangkeji
{
    public class VersionInfo
    {
        /// <summary>
        /// 客户端版本信息
        /// </summary>
        public string lastVersion { get; set; }
        /// <summary>
        /// 客户端下载地址
        /// </summary>
        public string updateUrl { get; set; }
    }

    public class DaHuaTwoAuthenticationModelRoot
    {
        /// <summary>
        /// 保活时间30分钟
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sipNum { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public VersionInfo versionInfo { get; set; }
    }
}
