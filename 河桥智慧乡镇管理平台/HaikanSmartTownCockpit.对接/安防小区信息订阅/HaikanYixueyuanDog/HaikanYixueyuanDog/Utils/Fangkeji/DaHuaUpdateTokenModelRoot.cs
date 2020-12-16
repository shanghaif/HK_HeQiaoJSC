using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils.Fangkeji
{
    public class Data
    {
        /// <summary>
        /// 保活时间30分钟
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 新的token
        /// </summary>
        public string token { get; set; }
    }

    public class DaHuaUpdateTokenModelRoot
    {
        /// <summary>
        /// 返回码1000成功
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string desc { get; set; }
    }
}
