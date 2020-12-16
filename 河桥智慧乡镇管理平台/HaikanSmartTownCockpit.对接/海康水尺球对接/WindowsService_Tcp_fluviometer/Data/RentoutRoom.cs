namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RentoutRoom")]
    public partial class RentoutRoom
    {
        [Key]
        public Guid RentoutRoomUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string RentoutInfo { get; set; }

        [StringLength(255)]
        public string RentoutYezhu { get; set; }

        [StringLength(255)]
        public string RentoutZuhu { get; set; }

        [StringLength(255)]
        public string RentoutTime { get; set; }

        [StringLength(255)]
        public string DaoqiTime { get; set; }

        [StringLength(255)]
        public string RentoutMoney { get; set; }

        [StringLength(255)]
        public string RentoutStaues { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }
    }
}
