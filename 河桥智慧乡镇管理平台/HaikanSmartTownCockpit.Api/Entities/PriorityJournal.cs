using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class PriorityJournal
    {
        public Guid PriorityJournalUuid { get; set; }
        public int Id { get; set; }
        public Guid? PriorityUuid { get; set; }
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
