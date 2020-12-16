namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Volunteers
    {
        [Key]
        public Guid VolunteersUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string VolunteersName { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        public Guid? VillageUuid { get; set; }

        public string Jieshao { get; set; }

        [StringLength(255)]
        public string Staues { get; set; }
    }
}
