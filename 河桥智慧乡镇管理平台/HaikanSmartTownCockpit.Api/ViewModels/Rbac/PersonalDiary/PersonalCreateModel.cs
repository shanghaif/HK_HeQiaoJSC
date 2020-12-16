using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Rbac.PersonalDiary
{
    public class PersonalCreateModel
    {
        public Guid PersonalDiaryUuid { get; set; }
        public string Headline { get; set; }
        public string Content { get; set; }
        public string WriteTime { get; set; }
        public string Accessory { get; set; }
        public string EstablishTime { get; set; }
        public string EstablishName { get; set; }
        public int? IsDeleted { get; set; }
    }
}
