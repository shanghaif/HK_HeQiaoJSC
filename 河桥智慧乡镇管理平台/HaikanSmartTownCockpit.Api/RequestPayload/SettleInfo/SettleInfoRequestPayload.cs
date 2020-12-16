using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaikanSmartTownCockpit.Api.Entities.Enums;

namespace HaikanSmartTownCockpit.Api.RequestPayload.SettleInfo
{
    public class SettleInfoRequestPayload : RequestPayload
    {
        public string Name { get; set; }
        public string IdCard { get; set; }
        public string STime { get; set; }
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public CommonEnum.IsDeleted IsDeleted { get; set; }
    }
}
