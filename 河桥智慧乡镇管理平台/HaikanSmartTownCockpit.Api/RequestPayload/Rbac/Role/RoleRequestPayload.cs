using HaikanSmartTownCockpit.Api.Entities.Enums;

namespace HaikanSmartTownCockpit.Api.RequestPayload.Rbac.Role
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleRequestPayload : RequestPayload
    {
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public CommonEnum.IsDeleted IsDeleted { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public CommonEnum.Status Status { get; set; }

        /// <summary>
        /// 搜索负责人
        /// </summary>
        public string kwfzr { get; set; }

        /// <summary>
        /// 搜索时间
        /// </summary>
        public string kwstartime { get; set; }

        /// <summary>
        /// 搜索时间
        /// </summary>
        public string kwendtime { get; set; }

        /// <summary>
        /// 搜索时间
        /// </summary>
        public string kwendtime2 { get; set; }
    }
}
