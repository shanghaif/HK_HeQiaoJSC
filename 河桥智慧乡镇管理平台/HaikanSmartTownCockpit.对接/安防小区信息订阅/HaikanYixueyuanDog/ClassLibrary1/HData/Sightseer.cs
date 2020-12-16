namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sightseer")]
    public partial class Sightseer
    {
        [Key]
        public Guid SightseerUuid { get; set; }

        public int? ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string SightseerName { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        [StringLength(255)]
        public string IdentityCard { get; set; }

        [StringLength(255)]
        public string Nation { get; set; }

        [StringLength(255)]
        public string Laiyuandi { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Staues { get; set; }

        public Guid? ScenicUuid { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string Age { get; set; }

        [StringLength(255)]
        public string Shengneiwai { get; set; }
    }
}
