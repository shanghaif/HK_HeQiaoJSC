namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FangXun")]
    public partial class FangXun
    {
        [Key]
        public Guid guid { get; set; }

        [StringLength(255)]
        public string member { get; set; }

        [StringLength(255)]
        public string command { get; set; }

        [StringLength(255)]
        public string workingclass { get; set; }

        [StringLength(255)]
        public string teamleader { get; set; }

        [StringLength(255)]
        public string governmentofficials { get; set; }

        [StringLength(255)]
        public string personinchargeofvillageenterprise { get; set; }

        public string jobrequirements { get; set; }

        public int? IsDeleted { get; set; }
    }
}
