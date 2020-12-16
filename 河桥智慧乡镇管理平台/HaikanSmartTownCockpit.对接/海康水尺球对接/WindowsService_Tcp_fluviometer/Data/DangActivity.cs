namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangActivity")]
    public partial class DangActivity
    {
        [Key]
        public Guid DangActivityUuid { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        public bool? IsRelease { get; set; }

        public string ReleaseTime { get; set; }

        public string Cover { get; set; }

        public string Content { get; set; }

        public DateTime? AddTime { get; set; }

        [StringLength(100)]
        public string AddPeople { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        public string Photo { get; set; }

        [StringLength(255)]
        public string VillageUuid { get; set; }
    }
}
