namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CityManagement")]
    public partial class CityManagement
    {
        [Key]
        public Guid CityManagementUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string Incident { get; set; }

        [StringLength(255)]
        public string ZhifaTime { get; set; }

        [StringLength(255)]
        public string ZhifaAddress { get; set; }

        [StringLength(255)]
        public string Chuliren { get; set; }

        [StringLength(255)]
        public string BeiChulirren { get; set; }

        [StringLength(255)]
        public string ZhifaRen { get; set; }

        [StringLength(255)]
        public string ChuliQingk { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }
    }
}
