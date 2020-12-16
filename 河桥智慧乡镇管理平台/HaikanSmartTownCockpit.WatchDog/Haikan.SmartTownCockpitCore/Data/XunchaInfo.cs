namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XunchaInfo")]
    public partial class XunchaInfo
    {
        [Key]
        public Guid XunchaInfoUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string XunchaInfoRen { get; set; }

        [StringLength(255)]
        public string ShebeiId { get; set; }

        [StringLength(255)]
        public string XunchaAddress { get; set; }

        [StringLength(255)]
        public string XunchaTime { get; set; }

        [StringLength(255)]
        public string QingkShangbao { get; set; }

        [StringLength(255)]
        public string Staues { get; set; }

        [StringLength(255)]
        public string FuzerPhone { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }
    }
}
