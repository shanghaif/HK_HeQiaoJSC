namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangerousRoom")]
    public partial class DangerousRoom
    {
        [Key]
        public Guid DangerousRoomUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string DangerousMaster { get; set; }

        [StringLength(255)]
        public string DangerousPhone { get; set; }

        public string DangerousAddress { get; set; }

        public Guid? TownUuid { get; set; }

        public Guid? AdministratorUuid { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string AdministratorName { get; set; }

        [StringLength(255)]
        public string AdministratorPhone { get; set; }

        [StringLength(255)]
        public string Lon { get; set; }

        [StringLength(255)]
        public string Lat { get; set; }

        [StringLength(255)]
        public string Areaname { get; set; }

        [StringLength(255)]
        public string Streetname { get; set; }

        [StringLength(255)]
        public string Communityname { get; set; }

        [StringLength(255)]
        public string Yhorgid { get; set; }

        [StringLength(255)]
        public string Jdjl { get; set; }

        [StringLength(255)]
        public string Buildingid { get; set; }

        [StringLength(255)]
        public string Zldz { get; set; }

        [StringLength(255)]
        public string Xmmc { get; set; }

        public string Areaid { get; set; }

        [StringLength(255)]
        public string Ib { get; set; }

        [StringLength(1)]
        public string Gisx { get; set; }

        [StringLength(255)]
        public string Gisy { get; set; }

        [StringLength(255)]
        public string Xcyq { get; set; }

        public string Jdbgcjsj { get; set; }
    }
}
