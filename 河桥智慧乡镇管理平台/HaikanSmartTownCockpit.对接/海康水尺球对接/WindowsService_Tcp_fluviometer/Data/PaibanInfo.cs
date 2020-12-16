namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PaibanInfo")]
    public partial class PaibanInfo
    {
        [Key]
        public Guid PaibanInfoUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string ZbLingdao { get; set; }

        public string Zbrenyuan { get; set; }

        public string Zyrenyuan { get; set; }

        [StringLength(255)]
        public string ZbTime { get; set; }

        public int IsDeleted { get; set; }

        [StringLength(255)]
        public string ZbBc { get; set; }

        [StringLength(255)]
        public string ZbYear { get; set; }
    }
}
