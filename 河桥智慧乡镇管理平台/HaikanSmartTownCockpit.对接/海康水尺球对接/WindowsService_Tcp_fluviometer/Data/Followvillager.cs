namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Followvillager")]
    public partial class Followvillager
    {
        [Key]
        public Guid FollowvillagerUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string FollowvillagerName { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        [StringLength(255)]
        public string IdentityCard { get; set; }

        [StringLength(255)]
        public string Nation { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Staues { get; set; }

        public Guid? VillageUuid { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        public string Shiji { get; set; }

        public string Jieshao { get; set; }
    }
}
