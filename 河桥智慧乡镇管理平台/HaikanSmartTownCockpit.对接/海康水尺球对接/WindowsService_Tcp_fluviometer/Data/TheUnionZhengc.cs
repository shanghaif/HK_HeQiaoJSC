namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheUnionZhengc")]
    public partial class TheUnionZhengc
    {
        [Key]
        public Guid TheUnionZhengcUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? State { get; set; }

        public int? IsDeleted { get; set; }

        public string TheUnionZhengcName { get; set; }

        public string Neirong { get; set; }

        public string Picture { get; set; }
    }
}
