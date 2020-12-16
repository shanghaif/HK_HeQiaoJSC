namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SecurityCode")]
    public partial class SecurityCode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime? CreateTime { get; set; }

        [Key]
        public Guid SecurityCodeUUID { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int? State { get; set; }

        [StringLength(20)]
        public string ChargeName { get; set; }

        [StringLength(20)]
        public string ChargePhone { get; set; }

        public int? IsDeleted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ScannTime { get; set; }
    }
}
