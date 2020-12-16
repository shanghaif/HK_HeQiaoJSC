using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class UnifiedAddressLibraryUserInfo
    {
        public int Id { get; set; }
        public int? UnifiedAddressLibraryId { get; set; }
        public string UserIdlist { get; set; }
        public DateTime? AddTime { get; set; }
    }
}
