using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class DangerousRoom
    {
        public Guid DangerousRoomUuid { get; set; }
        public int Id { get; set; }
        public string DangerousMaster { get; set; }
        public string DangerousPhone { get; set; }
        public string DangerousAddress { get; set; }
        public Guid? TownUuid { get; set; }
        public Guid? AdministratorUuid { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string AdministratorName { get; set; }
        public string AdministratorPhone { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string Areaname { get; set; }
        public string Streetname { get; set; }
        public string Communityname { get; set; }
        public string Yhorgid { get; set; }
        public string Jdjl { get; set; }
        public string Buildingid { get; set; }
        public string Zldz { get; set; }
        public string Xmmc { get; set; }
        public string Areaid { get; set; }
        public string Ib { get; set; }
        public string Gisx { get; set; }
        public string Gisy { get; set; }
        public string Xcyq { get; set; }
        public string Jdbgcjsj { get; set; }
    }
}
