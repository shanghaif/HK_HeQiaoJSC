using HaikanSmartTownCockpit.Api.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.RequestPayload.Emergency.Emergencyresponse
{
    public class XunchaDutyRequestPayload:RequestPayload
    {
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public CommonEnum.IsDeleted IsDeleted { get; set; }
    }
}
