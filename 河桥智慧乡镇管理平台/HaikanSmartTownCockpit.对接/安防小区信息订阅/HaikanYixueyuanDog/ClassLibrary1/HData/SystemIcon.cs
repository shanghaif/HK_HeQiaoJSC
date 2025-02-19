namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemIcon")]
    public partial class SystemIcon
    {
        [Key]
        public Guid SystemIconUUID { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(20)]
        public string Size { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(60)]
        public string Custom { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public int IsDeleted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        public Guid CreatedByUserGuid { get; set; }

        public string CreatedByUserName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedOn { get; set; }

        public Guid? ModifiedByUserGuid { get; set; }

        public string ModifiedByUserName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }
}
