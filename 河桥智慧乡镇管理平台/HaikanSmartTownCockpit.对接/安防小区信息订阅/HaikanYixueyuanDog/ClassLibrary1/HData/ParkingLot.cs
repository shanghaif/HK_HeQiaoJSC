namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParkingLot")]
    public partial class ParkingLot
    {
        [Key]
        public Guid ParkingLotUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string ParkingLotName { get; set; }

        [StringLength(255)]
        public string ParkingLotAddress { get; set; }

        [StringLength(255)]
        public string Zchewei { get; set; }

        [StringLength(255)]
        public string Ychewei { get; set; }

        [StringLength(255)]
        public string Schewei { get; set; }

        [StringLength(255)]
        public string ParkingLotsru { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string Lon { get; set; }

        [StringLength(255)]
        public string Lat { get; set; }

        [StringLength(255)]
        public string ParkingIndexCode { get; set; }
    }
}
