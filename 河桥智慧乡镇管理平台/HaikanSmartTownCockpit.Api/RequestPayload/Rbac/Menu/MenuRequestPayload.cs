using System;
using HaikanSmartTownCockpit.Api.Entities.Enums;

namespace HaikanSmartTownCockpit.Api.RequestPayload.Rbac.Menu
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuRequestPayload : RequestPayload
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
        /// 上级菜单GUID
        /// </summary>
        public Guid? ParentGuid { get; set; }
    }
}
