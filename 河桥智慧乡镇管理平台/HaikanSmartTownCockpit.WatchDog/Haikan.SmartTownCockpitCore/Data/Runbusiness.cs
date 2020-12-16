namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Runbusiness")]
    public partial class Runbusiness
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        public Guid StudentUUID { get; set; }

        [StringLength(255)]
        public string StudentName { get; set; }

        [StringLength(100)]
        public string Sex { get; set; }

        [StringLength(255)]
        public string Birth { get; set; }

        [StringLength(255)]
        public string Education { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string HeadTime { get; set; }

        [StringLength(255)]
        public string Project { get; set; }

        [StringLength(255)]
        public string Policy { get; set; }

        [StringLength(255)]
        public string Condition { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }
    }
}
