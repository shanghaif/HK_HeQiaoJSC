using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class DisasterInfo
    {
        public Guid DisasterInfoUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string DisasterName { get; set; }
        public string DisasterAddress { get; set; }
        public string DisasterStaues { get; set; }
        public string DisasterTime { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public Guid? TownUuid { get; set; }
        public Guid? AdministratorUuid { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string AdministratorName { get; set; }
        public string AdministratorPhone { get; set; }
        public string MapRegion { get; set; }
    }
}
