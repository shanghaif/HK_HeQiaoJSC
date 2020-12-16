using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class MissionJournal
    {
        public Guid MissionJournalUuid { get; set; }
        public int Id { get; set; }
        public Guid? MissionUuid { get; set; }
        public string Content { get; set; }
        public string Accessory { get; set; }
        public string EstablishTime { get; set; }
        public string EstablishName { get; set; }
        public int? IsDeleted { get; set; }
        public string Read { get; set; }
        public string Completed { get; set; }
        public string Coordination { get; set; }
    }
}
