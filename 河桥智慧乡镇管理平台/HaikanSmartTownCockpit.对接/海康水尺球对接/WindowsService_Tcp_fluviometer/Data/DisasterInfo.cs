namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DisasterInfo")]
    public partial class DisasterInfo
    {
        [Key]
        public Guid DisasterInfoUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string DisasterName { get; set; }

        [Required]
        [StringLength(255)]
        public string DisasterAddress { get; set; }

        [StringLength(255)]
        public string DisasterStaues { get; set; }

        [StringLength(255)]
        public string DisasterTime { get; set; }

        [StringLength(255)]
        public string Lon { get; set; }

        [StringLength(255)]
        public string Lat { get; set; }

        public Guid? TownUuid { get; set; }

        public Guid? AdministratorUuid { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string AdministratorName { get; set; }

        [StringLength(255)]
        public string AdministratorPhone { get; set; }

        public string MapRegion { get; set; }
    }
}
