namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TcqType")]
    public partial class TcqType
    {
        [Key]
        public Guid Tcqguid { get; set; }

        public int? Tcqid { get; set; }

        [StringLength(255)]
        public string TcqName { get; set; }
    }
}
