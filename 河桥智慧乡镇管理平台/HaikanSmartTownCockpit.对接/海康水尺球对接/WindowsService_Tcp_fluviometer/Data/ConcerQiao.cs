namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConcerQiao")]
    public partial class ConcerQiao
    {
        [Key]
        public Guid ConcerningQiaoUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string RealName { get; set; }

        [StringLength(255)]
        public string UserIdCardType { get; set; }

        [StringLength(255)]
        public string UserIdCardNum { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        public string GWAddress { get; set; }

        public string CNAddress { get; set; }

        public string XJAddress { get; set; }

        [StringLength(255)]
        public string ConcerningType { get; set; }
    }
}
