namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UnifiedAddressLibrary")]
    public partial class UnifiedAddressLibrary
    {
        [Key]
        [Column(Order = 0)]
        public Guid UnifiedAddressLibraryUUID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ID { get; set; }

        [StringLength(255)]
        public string oid { get; set; }

        [StringLength(255)]
        public string SOURCEADDRESS { get; set; }

        [StringLength(255)]
        public string CITY { get; set; }

        [StringLength(255)]
        public string COUNTY { get; set; }

        [StringLength(255)]
        public string TOWN { get; set; }

        [StringLength(255)]
        public string COMMUNITY { get; set; }

        [StringLength(255)]
        public string SQUAD { get; set; }

        [StringLength(255)]
        public string VILLAGE { get; set; }

        [StringLength(255)]
        public string SZONE { get; set; }

        [StringLength(255)]
        public string STREET { get; set; }

        [StringLength(255)]
        public string DOOR { get; set; }

        [StringLength(255)]
        public string RESREGION { get; set; }

        [StringLength(255)]
        public string BUILDING { get; set; }

        [StringLength(255)]
        public string BUILDING_NUM { get; set; }

        [StringLength(255)]
        public string UNIT { get; set; }

        [StringLength(255)]
        public string FLOOR { get; set; }

        [StringLength(255)]
        public string ROOM { get; set; }

        [StringLength(255)]
        public string GRID_CODE { get; set; }

        [StringLength(255)]
        public string BUILDING_CODE { get; set; }

        [StringLength(255)]
        public string HOUSE_CODE { get; set; }

        [StringLength(255)]
        public string CODE { get; set; }

        [StringLength(255)]
        public string CREATETIME { get; set; }

        [StringLength(255)]
        public string UPDATETIME { get; set; }

        public int? ISVALID { get; set; }

        [StringLength(255)]
        public string FROM_STATUS { get; set; }

        [StringLength(255)]
        public string TO_STATUS { get; set; }

        [StringLength(255)]
        public string BUILDING_PATH { get; set; }

        [StringLength(255)]
        public string DATASOURCE { get; set; }

        [StringLength(255)]
        public string REVERSE1 { get; set; }

        [StringLength(255)]
        public string REVERSE2 { get; set; }

        [StringLength(255)]
        public string LON { get; set; }

        [StringLength(255)]
        public string LAT { get; set; }

        [StringLength(255)]
        public string Z { get; set; }

        [StringLength(255)]
        public string ROOM_FLOOR { get; set; }

        [StringLength(255)]
        public string ADDRTYPE { get; set; }

        [StringLength(255)]
        public string GUID { get; set; }

        [StringLength(255)]
        public string INSERTTIME { get; set; }

        [StringLength(255)]
        public string SYSTEMID { get; set; }

        public int? ISDELETE { get; set; }

        [StringLength(255)]
        public string BELONG_BUILDING { get; set; }

        [StringLength(255)]
        public string ADDRESS_TYPE { get; set; }

        [StringLength(255)]
        public string REMARK { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }
    }
}
