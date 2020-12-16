namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Activity")]
    public partial class Activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        public Guid ActivityUUID { get; set; }

        [StringLength(255)]
        public string ActivityName { get; set; }

        [StringLength(255)]
        public string ActivityTime { get; set; }

        [StringLength(255)]
        public string ActivityWay { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }
    }
}
