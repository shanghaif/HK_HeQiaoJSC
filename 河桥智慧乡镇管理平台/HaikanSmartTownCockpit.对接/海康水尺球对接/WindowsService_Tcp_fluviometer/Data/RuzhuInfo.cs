namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RuzhuInfo")]
    public partial class RuzhuInfo
    {
        [Key]
        public Guid RuzhuInfoUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string RuzhuName { get; set; }

        [StringLength(255)]
        public string RuzhuPeople { get; set; }

        [StringLength(255)]
        public string RuzhuRoom { get; set; }

        [StringLength(255)]
        public string RuzhuMoney { get; set; }

        [StringLength(255)]
        public string RuzhuDay { get; set; }

        [StringLength(255)]
        public string RuzhuTime { get; set; }

        [StringLength(255)]
        public string LikaiTime { get; set; }

        public Guid? HotelUuid { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string HotelName { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
