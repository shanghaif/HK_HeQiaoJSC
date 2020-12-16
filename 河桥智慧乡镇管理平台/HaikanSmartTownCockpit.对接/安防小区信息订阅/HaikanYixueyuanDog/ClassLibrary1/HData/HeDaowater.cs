namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HeDaowater")]
    public partial class HeDaowater
    {
        [Key]
        public Guid HeDaowaterUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string HeDaowaterAddress { get; set; }

        [StringLength(255)]
        public string HeDaowaterTime { get; set; }

        [StringLength(255)]
        public string HeDaoHeDaowaterSw { get; set; }

        [StringLength(255)]
        public string Ljiedian { get; set; }

        [StringLength(255)]
        public string HeDaowaterYujin { get; set; }

        [StringLength(255)]
        public string FangxunDj { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }
    }
}
