namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HqCommunal")]
    public partial class HqCommunal
    {
        [Key]
        public Guid HqCommunalUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string HqCommunalName { get; set; }

        [StringLength(255)]
        public string HqCommunalID { get; set; }

        [StringLength(50)]
        public string HqCommunalStaues { get; set; }

        public string HqCommunalLocation { get; set; }

        public Guid? TownUuid { get; set; }

        [StringLength(50)]
        public string HqCommunalType { get; set; }

        [StringLength(255)]
        public string Lon { get; set; }

        [StringLength(255)]
        public string Lat { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }
    }
}
