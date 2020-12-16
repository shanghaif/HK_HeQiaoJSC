namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PublicityFronts
    {
        [Key]
        public Guid PublicityFrontsUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(255)]
        public string PublicityFrontsName { get; set; }

        public string Title { get; set; }

        public string Introduction { get; set; }

        public string Address { get; set; }

        public string Picture { get; set; }

        public int? State { get; set; }

        public int? IsDelete { get; set; }

        public string Cover { get; set; }

        [StringLength(255)]
        public string Lon { get; set; }

        [StringLength(255)]
        public string Lat { get; set; }

        public string PublicityTypeUuid { get; set; }

        public string VillageUuid { get; set; }

        [StringLength(255)]
        public string AdminPeoPle { get; set; }

        public string KaifangTime { get; set; }
    }
}
