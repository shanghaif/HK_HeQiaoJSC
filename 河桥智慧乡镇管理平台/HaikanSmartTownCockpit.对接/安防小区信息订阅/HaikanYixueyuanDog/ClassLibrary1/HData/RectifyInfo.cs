namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RectifyInfo")]
    public partial class RectifyInfo
    {
        [Key]
        public Guid RectifyInfoUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string RectifyInfoUnit { get; set; }

        [StringLength(255)]
        public string RectifyInfoName { get; set; }

        [StringLength(255)]
        public string RectifyInfoStaues { get; set; }

        [StringLength(255)]
        public string DweiPhone { get; set; }

        [StringLength(255)]
        public string ShangbanStaues { get; set; }

        [StringLength(255)]
        public string RectifyType { get; set; }

        [StringLength(255)]
        public string RectifyTiem { get; set; }

        [StringLength(255)]
        public string KaishiTime { get; set; }

        [StringLength(255)]
        public string JiesuTime { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string cym { get; set; }

        [StringLength(255)]
        public string whcd { get; set; }

        [StringLength(255)]
        public string sqjzryjsrq { get; set; }

        [StringLength(255)]
        public string sftg { get; set; }

        [StringLength(255)]
        public string dzdwfs { get; set; }

        [StringLength(255)]
        public string sfyqk { get; set; }

        [StringLength(255)]
        public string sfzh { get; set; }

        [StringLength(255)]
        public string jzryxzzcqk { get; set; }

        [StringLength(255)]
        public string jcqk { get; set; }

        [StringLength(255)]
        public string hzhm { get; set; }

        [StringLength(255)]
        public string datas { get; set; }

        [StringLength(255)]
        public string zyjwzxrystzk { get; set; }

        [StringLength(255)]
        public string ywhz { get; set; }

        [StringLength(255)]
        public string gattxzhm { get; set; }

        [StringLength(255)]
        public string gattxzlx { get; set; }

        [StringLength(255)]
        public string ywgztsfz { get; set; }

        [StringLength(255)]
        public string xb { get; set; }

        [StringLength(255)]
        public string gatsfzlx { get; set; }

        [StringLength(255)]
        public string gatsfzhm { get; set; }

        [StringLength(100)]
        public string tbzhm { get; set; }

        [StringLength(255)]
        public string sqjzqx { get; set; }

        [StringLength(255)]
        public string sqjzryjsfs { get; set; }

        [StringLength(255)]
        public string jtzm { get; set; }

        [StringLength(255)]
        public string fzlx { get; set; }

        [StringLength(255)]
        public string ywtbz { get; set; }

        [StringLength(255)]
        public string mz { get; set; }

        [StringLength(255)]
        public string jzlb { get; set; }

        [StringLength(255)]
        public string ywgattxz { get; set; }

        [StringLength(255)]
        public string sflf { get; set; }

        [StringLength(255)]
        public string hycd { get; set; }

        [StringLength(255)]
        public string fjx { get; set; }

        [StringLength(255)]
        public string csrq { get; set; }

        [StringLength(255)]
        public string sqjzrybh { get; set; }

        [StringLength(255)]
        public string sfcydzdwgl { get; set; }
    }
}
