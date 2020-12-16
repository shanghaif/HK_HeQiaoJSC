namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Woods
    {
        [Key]
        public Guid WoodsUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string ShebeiId { get; set; }

        [StringLength(255)]
        public string ShebeiType { get; set; }

        [StringLength(255)]
        public string ShebeiAddress { get; set; }

        [StringLength(255)]
        public string Lon { get; set; }

        [StringLength(255)]
        public string Lat { get; set; }

        [StringLength(255)]
        public string Guanliren { get; set; }

        [StringLength(255)]
        public string ShebeiStaues { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string ForestName { get; set; }

        [StringLength(255)]
        public string ForestAddress { get; set; }

        [StringLength(255)]
        public string AdministratorName { get; set; }

        [StringLength(255)]
        public string AdministratorPhone { get; set; }

        public string MapRegion { get; set; }

        [StringLength(100)]
        public string ManagePhone { get; set; }
    }
}
