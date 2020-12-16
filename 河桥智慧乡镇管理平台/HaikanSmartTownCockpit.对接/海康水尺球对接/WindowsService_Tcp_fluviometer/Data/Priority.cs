namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Priority")]
    public partial class Priority
    {
        [Key]
        public Guid PriorityUUID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string PriorityHeadline { get; set; }

        public string PriorityDescribe { get; set; }

        public string Principal { get; set; }

        public string Participant { get; set; }

        [StringLength(255)]
        public string EstablishTime { get; set; }

        [StringLength(255)]
        public string EstablishName { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        [StringLength(255)]
        public string Accomplish { get; set; }

        [StringLength(255)]
        public string Recycle { get; set; }

        public int? Sortord { get; set; }

        [StringLength(255)]
        public string ZhiDingTime { get; set; }

        [StringLength(255)]
        public string EndTime { get; set; }
    }
}
