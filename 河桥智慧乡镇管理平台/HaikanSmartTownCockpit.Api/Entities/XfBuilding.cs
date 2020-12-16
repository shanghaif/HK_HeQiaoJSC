using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class XfBuilding
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid BuildingUuid { get; set; }
        public string BuildingName { get; set; }
        public string BuildingType { get; set; }
        public string BuildingUseNature { get; set; }
        public string FireDanger { get; set; }
        public string FireResistantLevel { get; set; }
        public string StructureType { get; set; }
        public double? BuildingHeight { get; set; }
        public string BuildingAddr { get; set; }
        public string RegionCode { get; set; }
        public string BuildingState { get; set; }
        public string BuildingTime { get; set; }
        public string BuildingFinishTime { get; set; }
        public string PropertyRight { get; set; }
        public double? BuildingArea { get; set; }
        public double? OccupyArea { get; set; }
        public double? StandardFloorArea { get; set; }
        public int? OverFloorNum { get; set; }
        public double? OverFloorArea { get; set; }
        public int? UnderFloorNum { get; set; }
        public double? UnderFloorArea { get; set; }
        public double? TunnelHeight { get; set; }
        public double? TunnelLength { get; set; }
        public int? ControlRoomCondition { get; set; }
        public string ControlRoomPosition { get; set; }
        public int? WorkerDailyNum { get; set; }
        public int? BuildingGalleryful { get; set; }
        public int? FireElevatorNum { get; set; }
        public string FireElevatorPosition { get; set; }
        public double? FireElevatorCarrery { get; set; }
        public double? ShelterFloorNum { get; set; }
        public double? ShelterFloorArea { get; set; }
        public string ShelterFloorPosition { get; set; }
        public int? ExitNum { get; set; }
        public string ExitPosition { get; set; }
        public string ExitForm { get; set; }
        public string StorageName { get; set; }
        public int? StorageNum { get; set; }
        public string StorageNature { get; set; }
        public string StorageShape { get; set; }
        public double? StorageSize { get; set; }
        public string MainMaterial { get; set; }
        public string MainProduct { get; set; }
        public int? EnterOrgNum { get; set; }
        public string OrgAbutSituation { get; set; }
        public string BelongOrgId { get; set; }
        public string ManagerOrgId { get; set; }
        public int? UnitNum { get; set; }
        public int? FloorNum { get; set; }
        public int? MapType { get; set; }
        public double? GpsX3d { get; set; }
        public double? GpsY3d { get; set; }
        public double? GpsZ3d { get; set; }
        public int? IsMezzanine { get; set; }
        public string Memo { get; set; }
        public string PlatformCode { get; set; }
        public string RecordCode { get; set; }
    }
}
