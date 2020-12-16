namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TownMilitia")]
    public partial class TownMilitia
    {
        [Key]
        public Guid TownMilitiaUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string TownMilitiaName { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        [StringLength(255)]
        public string Birth { get; set; }

        [StringLength(255)]
        public string IdentityCard { get; set; }

        [StringLength(255)]
        public string Census { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Nation { get; set; }

        [StringLength(255)]
        public string Occupation { get; set; }

        [StringLength(255)]
        public string ArmyTime { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }
    }
}
