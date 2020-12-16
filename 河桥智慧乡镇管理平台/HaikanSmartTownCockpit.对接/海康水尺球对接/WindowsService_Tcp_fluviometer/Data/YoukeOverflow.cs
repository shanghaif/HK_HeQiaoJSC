namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YoukeOverflow")]
    public partial class YoukeOverflow
    {
        [Key]
        public Guid YoukeOverflowUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string JindianName { get; set; }

        [StringLength(255)]
        public string Rongliang { get; set; }

        [StringLength(255)]
        public string OverflowInfo { get; set; }

        [StringLength(255)]
        public string SsRenCount { get; set; }

        [Required]
        [StringLength(255)]
        public string YujinInfo { get; set; }
    }
}
