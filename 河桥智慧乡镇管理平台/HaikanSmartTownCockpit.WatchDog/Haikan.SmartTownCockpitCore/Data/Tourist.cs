namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tourist")]
    public partial class Tourist
    {
        [Key]
        public Guid TouristUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string TouristName { get; set; }

        public Guid? ScenicUuid { get; set; }

        public double? TouristMoney { get; set; }

        [StringLength(255)]
        public string TouristTime { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string ScenicName { get; set; }

        [StringLength(255)]
        public string TjYear { get; set; }

        [StringLength(255)]
        public string TjMouth { get; set; }

        [StringLength(255)]
        public string TjJidu { get; set; }
    }
}
