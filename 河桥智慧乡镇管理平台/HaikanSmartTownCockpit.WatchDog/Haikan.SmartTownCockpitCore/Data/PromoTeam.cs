namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PromoTeam")]
    public partial class PromoTeam
    {
        [Key]
        public Guid PromoTeamUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string TeamType { get; set; }

        [StringLength(255)]
        public string TeamCaptain { get; set; }

        public string TeamChengyuan { get; set; }

        public string TeamJieshao { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        [StringLength(255)]
        public string Birth { get; set; }

        [StringLength(255)]
        public string Politics { get; set; }

        [StringLength(255)]
        public string Cunz { get; set; }
    }
}
