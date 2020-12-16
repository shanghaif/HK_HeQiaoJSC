using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class XlProject
    {
        public Guid XlProjectUuid { get; set; }
        public int Id { get; set; }
        public string XlShebeiId { get; set; }
        public string XlShebeiType { get; set; }
        public string ShebeiAddress { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string AdminInfo { get; set; }
        public string ShebeiType { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public int? UrlType { get; set; }
    }
}
