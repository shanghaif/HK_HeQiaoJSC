using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class BuildHouse
    {
        public Guid BuildHouseUuid { get; set; }
        public int Id { get; set; }
        public string Household { get; set; }
        public string HouseAddress { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string IdentityCard { get; set; }
        public string Phone { get; set; }
        public string MonitorHouseId { get; set; }
        public string AdministratorUuid { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string Town { get; set; }
        public string ProjectCred { get; set; }
        public int? PeopleNum { get; set; }
        public string LandNum { get; set; }
        public string BuildArea { get; set; }
        public string OccupyArea { get; set; }
        public string PliesNum { get; set; }
        public string Way { get; set; }
        public string ArtisanCred { get; set; }
        public string ApproveTime { get; set; }
    }
}
