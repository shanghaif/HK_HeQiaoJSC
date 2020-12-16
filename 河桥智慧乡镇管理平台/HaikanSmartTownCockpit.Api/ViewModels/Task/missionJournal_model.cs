using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Task
{
    public class missionJournal_model
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
