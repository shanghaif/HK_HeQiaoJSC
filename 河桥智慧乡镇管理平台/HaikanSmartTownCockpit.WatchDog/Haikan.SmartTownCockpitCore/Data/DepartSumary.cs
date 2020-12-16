namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DepartSumary")]
    public partial class DepartSumary
    {
        [Key]
        public Guid DeSumaryUUID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeSumaryID { get; set; }

        public string DeSuHeadLine { get; set; }

        public string DeSuDescribe { get; set; }

        public string DeSuDocument { get; set; }

        public Guid? PriorityID { get; set; }

        public Guid? MissionID { get; set; }

        public Guid? SyUserUUID { get; set; }

        [StringLength(255)]
        public string DeSuAddTime { get; set; }

        [StringLength(255)]
        public string DepartName { get; set; }
    }
}
