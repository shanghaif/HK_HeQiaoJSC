namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class xf_Building
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateTime { get; set; }

        [Key]
        public Guid BuildingUuid { get; set; }

        [StringLength(255)]
        public string buildingName { get; set; }

        [StringLength(255)]
        public string buildingType { get; set; }

        [StringLength(255)]
        public string buildingUseNature { get; set; }

        [StringLength(255)]
        public string fireDanger { get; set; }

        [StringLength(255)]
        public string fireResistantLevel { get; set; }

        [StringLength(255)]
        public string structureType { get; set; }

        public double? buildingHeight { get; set; }

        [StringLength(255)]
        public string buildingAddr { get; set; }

        [StringLength(255)]
        public string regionCode { get; set; }

        [StringLength(255)]
        public string buildingState { get; set; }

        [StringLength(255)]
        public string buildingTime { get; set; }

        [StringLength(255)]
        public string buildingFinishTime { get; set; }

        [StringLength(255)]
        public string propertyRight { get; set; }

        public double? buildingArea { get; set; }

        public double? occupyArea { get; set; }

        public double? standardFloorArea { get; set; }

        public int? overFloorNum { get; set; }

        public double? overFloorArea { get; set; }

        public int? underFloorNum { get; set; }

        public double? underFloorArea { get; set; }

        public double? tunnelHeight { get; set; }

        public double? tunnelLength { get; set; }

        public int? controlRoomCondition { get; set; }

        [StringLength(255)]
        public string controlRoomPosition { get; set; }

        public int? workerDailyNum { get; set; }

        public int? buildingGalleryful { get; set; }

        public int? fireElevatorNum { get; set; }

        [StringLength(255)]
        public string fireElevatorPosition { get; set; }

        public double? fireElevatorCarrery { get; set; }

        public double? shelterFloorNum { get; set; }

        public double? shelterFloorArea { get; set; }

        [StringLength(255)]
        public string shelterFloorPosition { get; set; }

        public int? exitNum { get; set; }

        [StringLength(255)]
        public string exitPosition { get; set; }

        [StringLength(255)]
        public string exitForm { get; set; }

        [StringLength(255)]
        public string storageName { get; set; }

        public int? storageNum { get; set; }

        [StringLength(255)]
        public string storageNature { get; set; }

        [StringLength(255)]
        public string storageShape { get; set; }

        public double? storageSize { get; set; }

        [StringLength(255)]
        public string mainMaterial { get; set; }

        [StringLength(255)]
        public string mainProduct { get; set; }

        public int? enterOrgNum { get; set; }

        [StringLength(255)]
        public string orgAbutSituation { get; set; }

        [StringLength(255)]
        public string belongOrgId { get; set; }

        [StringLength(255)]
        public string managerOrgId { get; set; }

        public int? unitNum { get; set; }

        public int? floorNum { get; set; }

        public int? mapType { get; set; }

        public double? gpsX3d { get; set; }

        public double? gpsY3d { get; set; }

        public double? gpsZ3d { get; set; }

        public int? isMezzanine { get; set; }

        [StringLength(255)]
        public string memo { get; set; }

        [StringLength(255)]
        public string platformCode { get; set; }

        [StringLength(255)]
        public string recordCode { get; set; }
    }
}
