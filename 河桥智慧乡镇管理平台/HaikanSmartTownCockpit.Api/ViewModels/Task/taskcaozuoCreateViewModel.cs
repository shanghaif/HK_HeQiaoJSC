using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Task
{
    public class taskcaozuoCreateViewModel
    {
        public Guid? MissionJournalUUID { get; set; }
        public int? ID { get; set; }
        public Guid? MissionUUID { get; set; }
        public string Content { get; set; }
        public string Accessory { get; set; }
        public string EstablishTime { get; set; }
        public string EstablishName { get; set; }
        public string IsDeleted { get; set; }
        public string Read { get; set; }
    }
}
