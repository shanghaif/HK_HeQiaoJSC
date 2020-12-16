namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administrator")]
    public partial class Administrator
    {
        [Key]
        public Guid AdministratorUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AdministratorName { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        [StringLength(255)]
        public string IdentityCard { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string AdminQuanxian { get; set; }

        [StringLength(255)]
        public string AdminAddress { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(100)]
        public string AdminVillages { get; set; }

        [StringLength(255)]
        public string GriddingNum { get; set; }

        [StringLength(255)]
        public string SuozaiWangge { get; set; }

        [StringLength(255)]
        public string CunjiZhanghao { get; set; }

        [StringLength(255)]
        public string WanggeZhanghao { get; set; }

        [StringLength(255)]
        public string Wanggeyuan { get; set; }
    }
}
