namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Canyin")]
    public partial class Canyin
    {
        [Key]
        public Guid CanyinUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string CanyinName { get; set; }

        [StringLength(255)]
        public string CanyinAddress { get; set; }

        [StringLength(255)]
        public string Fuzeren { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Town { get; set; }

        [StringLength(255)]
        public string Staues { get; set; }
    }
}
