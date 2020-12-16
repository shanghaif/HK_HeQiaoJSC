using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class UnifiedAddressLibrary
    {
        public Guid UnifiedAddressLibraryUuid { get; set; }
        public int Id { get; set; }
        public string Oid { get; set; }
        public string Sourceaddress { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Town { get; set; }
        public string Community { get; set; }
        public string Squad { get; set; }
        public string Village { get; set; }
        public string Szone { get; set; }
        public string Street { get; set; }
        public string Door { get; set; }
        public string Resregion { get; set; }
        public string Building { get; set; }
        public string BuildingNum { get; set; }
        public string Unit { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
        public string GridCode { get; set; }
        public string BuildingCode { get; set; }
        public string HouseCode { get; set; }
        public string Code { get; set; }
        public string Createtime { get; set; }
        public string Updatetime { get; set; }
        public int? Isvalid { get; set; }
        public string FromStatus { get; set; }
        public string ToStatus { get; set; }
        public string BuildingPath { get; set; }
        public string Datasource { get; set; }
        public string Reverse1 { get; set; }
        public string Reverse2 { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string Z { get; set; }
        public string RoomFloor { get; set; }
        public string Addrtype { get; set; }
        public string Guid { get; set; }
        public string Inserttime { get; set; }
        public string Systemid { get; set; }
        public int? Isdelete { get; set; }
        public string BelongBuilding { get; set; }
        public string AddressType { get; set; }
        public string Remark { get; set; }
        public string AddTime { get; set; }
    }
}
