namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonalDiary")]
    public partial class PersonalDiary
    {
        [Key]
        public Guid PersonalDiaryUUID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string Headline { get; set; }

        [StringLength(255)]
        public string Content { get; set; }

        [StringLength(255)]
        public string WriteTime { get; set; }

        [StringLength(255)]
        public string Accessory { get; set; }

        [StringLength(255)]
        public string EstablishTime { get; set; }

        [StringLength(255)]
        public string EstablishName { get; set; }

        public int? IsDeleted { get; set; }
    }
}
