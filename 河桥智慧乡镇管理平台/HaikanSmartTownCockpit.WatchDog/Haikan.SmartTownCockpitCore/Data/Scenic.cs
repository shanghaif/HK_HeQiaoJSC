namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scenic")]
    public partial class Scenic
    {
        [Key]
        public Guid ScenicUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string ScenicName { get; set; }

        [StringLength(255)]
        public string ScenicGrade { get; set; }

        [StringLength(255)]
        public string ScenicAddress { get; set; }

        public string ScenicRemarks { get; set; }

        [StringLength(255)]
        public string ScenicTickets { get; set; }

        [StringLength(255)]
        public string ScenicQuantity { get; set; }

        [StringLength(255)]
        public string ScenicPeoPle { get; set; }

        [StringLength(255)]
        public string Lon { get; set; }

        [StringLength(255)]
        public string Lat { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string ScenicType { get; set; }
    }
}
