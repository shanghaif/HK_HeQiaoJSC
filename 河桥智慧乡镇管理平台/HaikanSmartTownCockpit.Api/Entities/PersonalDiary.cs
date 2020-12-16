using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class PersonalDiary
    {
        public Guid PersonalDiaryUuid { get; set; }
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Content { get; set; }
        public string WriteTime { get; set; }
        public string Accessory { get; set; }
        public string EstablishTime { get; set; }
        public string EstablishName { get; set; }
        public int? IsDeleted { get; set; }
        public Guid? MissionUuid { get; set; }
        public string Type { get; set; }
    }
}
