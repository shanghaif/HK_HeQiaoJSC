namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Qiye")]
    public partial class Qiye
    {
        [Key]
        public Guid QiyeUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string QiyeName { get; set; }

        [StringLength(255)]
        public string QiyeAddress { get; set; }

        [StringLength(255)]
        public string QiyeType { get; set; }

        [StringLength(255)]
        public string Faren { get; set; }

        [StringLength(255)]
        public string Guimo { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }
    }
}
