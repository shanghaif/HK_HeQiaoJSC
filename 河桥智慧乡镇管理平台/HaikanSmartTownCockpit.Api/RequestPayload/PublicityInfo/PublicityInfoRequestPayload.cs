using HaikanSmartTownCockpit.Api.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.RequestPayload.PublicityInfo
{
    public class PublicityInfoRequestPayload : RequestPayload
    {
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public CommonEnum.IsDeleted IsDeleted { get; set; }

        ///  /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        public string Kw { get; set; }
        public string Kw1 { get; set; }
        public string Kw2 { get; set; }
        public string Kw3 { get; set; }
        public List<string> Kw4 { get; set; }
    }
}
