using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class XunchaInfo
    {
        public Guid XunchaInfoUuid { get; set; }
        public int Id { get; set; }
        public string XunchaInfoRen { get; set; }
        public string ShebeiId { get; set; }
        public string XunchaAddress { get; set; }
        public string XunchaTime { get; set; }
        public string QingkShangbao { get; set; }
        public string Staues { get; set; }
        public string FuzerPhone { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
