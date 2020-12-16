namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XiaoQuAnFangDev")]
    public partial class XiaoQuAnFangDev
    {
        [Key]
        public Guid Guid { get; set; }

        [StringLength(255)]
        public string DevGuid { get; set; }

        [StringLength(255)]
        public string DevName { get; set; }
    }
}
