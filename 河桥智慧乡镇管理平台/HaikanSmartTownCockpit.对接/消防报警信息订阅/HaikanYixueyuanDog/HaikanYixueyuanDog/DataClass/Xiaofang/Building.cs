using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.DataClass.Xiaofang
{
    public class Building
    {
        /// <summary>
        /// 
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ret_Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ret_msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
    }
    public class BuildingData
    {
        public string buildingName { get; set; }
        public string buildingType { get; set; }
        public string buildingUseNature { get; set; }
        public string fireDanger { get; set; }
        public string fireResistantLevel { get; set; }
        public string structureType { get; set; }
        public string buildingHeight { get; set; }
        public string buildingAddr { get; set; }
        public string regionCode { get; set; }
        public string buildingState { get; set; }
        public string buildingTime { get; set; }
        public string buildingFinishTime { get; set; }
        public string propertyRight { get; set; }
        public string buildingArea { get; set; }
        public string occupyArea { get; set; }
        public string standardFloorArea { get; set; }
        public string overFloorNum { get; set; }
        public string overFloorArea { get; set; }
        public string underFloorNum { get; set; }
        public string underFloorArea { get; set; }
        public string tunnelHeight { get; set; }
        public string tunnelLength { get; set; }
        public int controlRoomCondition { get; set; }
        public string controlRoomPosition { get; set; }
        public int workerDailyNum { get; set; }
        public int buildingGalleryful { get; set; }
        public int fireElevatorNum { get; set; }
        public string fireElevatorPosition { get; set; }
        public string fireElevatorCarrery { get; set; }
        public string shelterFloorNum { get; set; }
        public string shelterFloorArea { get; set; }
        public string shelterFloorPosition { get; set; }
        public string exitNum { get; set; }
        public string exitPosition { get; set; }
        public string exitForm { get; set; }
        public string storageName { get; set; }
        public int storageNum { get; set; }
        public string storageNature { get; set; }
        public string storageShape { get; set; }
        public double storageSize { get; set; }
        public string mainMaterial { get; set; }
        public string mainProduct { get; set; }
        public string enterOrgNum { get; set; }
        public string orgAbutSituation { get; set; }
        public string belongOrgId { get; set; }
        public string managerOrgId { get; set; }
        public string unitNum { get; set; }
        public int floorNum { get; set; }
        public string mapType { get; set; }
        public double gpsX3d { get; set; }
        public double gpsY3d { get; set; }
        public double gpsZ3d { get; set; }
        public string isMezzanine { get; set; }
        public string memo { get; set; }
        public string platformCode { get; set; }
        public string recordCode { get; set; }
    }
}
