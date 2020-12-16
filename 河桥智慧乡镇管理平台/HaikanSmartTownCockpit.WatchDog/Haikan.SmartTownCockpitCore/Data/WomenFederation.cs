namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WomenFederation")]
    public partial class WomenFederation
    {
        [Key]
        public Guid WomenFederationUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string WomenFederationName { get; set; }

        public string Address { get; set; }

        [StringLength(255)]
        public string Zhuxi { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string VillageUuid { get; set; }

        [StringLength(255)]
        public string Staues { get; set; }

        public string Renshu { get; set; }
    }
}
