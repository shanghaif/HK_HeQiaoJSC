using HaikanSmartTownCockpit.Api.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.RequestPayload.SecurityCode
{
    public class SecurityCodeRequestPayload : RequestPayload
    {
        public int state { get; set; }
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public CommonEnum.IsDeleted IsDeleted { get; set; }
    }
}
