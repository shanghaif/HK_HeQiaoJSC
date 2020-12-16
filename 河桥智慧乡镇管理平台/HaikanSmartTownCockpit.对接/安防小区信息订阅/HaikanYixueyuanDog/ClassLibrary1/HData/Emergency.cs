namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Emergency")]
    public partial class Emergency
    {
        [Key]
        public Guid EmergencyUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string EmergencyDengji { get; set; }

        [StringLength(255)]
        public string EmergencyQuyu { get; set; }

        [StringLength(255)]
        public string QuyuFuzer { get; set; }

        [StringLength(255)]
        public string FuzerPhone { get; set; }

        [StringLength(255)]
        public string RescueTeamUuid { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }
    }
}
