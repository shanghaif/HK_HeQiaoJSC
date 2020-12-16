namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RenyuZhuany")]
    public partial class RenyuZhuany
    {
        [Key]
        public Guid RenyuZhuanyUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string Xzhuanyi { get; set; }

        [StringLength(255)]
        public string XiangyingDj { get; set; }

        [StringLength(255)]
        public string ZhaunyiQingk { get; set; }

        [StringLength(255)]
        public string Fuzeren { get; set; }

        [StringLength(255)]
        public string FuzerenPhone { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }
    }
}
