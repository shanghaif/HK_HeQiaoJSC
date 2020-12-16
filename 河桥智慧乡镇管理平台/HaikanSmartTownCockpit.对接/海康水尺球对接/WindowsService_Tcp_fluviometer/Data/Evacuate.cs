namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Evacuate")]
    public partial class Evacuate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateTime { get; set; }

        [Key]
        public Guid EvacuateUUID { get; set; }

        [StringLength(50)]
        public string SceneSpot { get; set; }

        [StringLength(50)]
        public string EvacuateTime { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        [StringLength(2)]
        public string Sex { get; set; }

        [StringLength(20)]
        public string IdCard { get; set; }

        [StringLength(5)]
        public string Nation { get; set; }

        [StringLength(50)]
        public string Origion { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public int? State { get; set; }

        public int? IsDeleted { get; set; }
    }
}
