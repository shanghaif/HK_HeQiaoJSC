namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PublicityType")]
    public partial class PublicityType
    {
        [Key]
        public Guid PublicityTypeUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? State { get; set; }

        public int? IsDeleted { get; set; }

        public string PublicityTypeName { get; set; }

        public string Title { get; set; }

        public string Picture { get; set; }

        [StringLength(255)]
        public string SumCount { get; set; }
    }
}
