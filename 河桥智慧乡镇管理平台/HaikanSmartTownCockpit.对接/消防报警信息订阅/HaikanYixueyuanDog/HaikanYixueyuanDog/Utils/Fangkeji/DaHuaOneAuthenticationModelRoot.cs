using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils.Fangkeji
{
    public class DaHuaOneAuthenticationModelRoot
    {

        /// <summary>
        /// 加密方式
        /// </summary>
        public string encryptType { get; set; }
        /// <summary>
        /// 随机值
        /// </summary>
        public string randomKey { get; set; }
        /// <summary>
        /// 域信息
        /// </summary>
        public string realm { get; set; }
    }
}
