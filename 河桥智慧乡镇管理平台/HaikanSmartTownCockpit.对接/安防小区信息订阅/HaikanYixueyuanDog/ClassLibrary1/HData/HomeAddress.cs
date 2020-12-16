namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HomeAddress")]
    public partial class HomeAddress
    {
        [Key]
        public Guid HomeAddressUUID { get; set; }

        public string Address { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string Addresscode { get; set; }

        [StringLength(255)]
        public string town { get; set; }

        [StringLength(255)]
        public string ccmmunity { get; set; }

        [StringLength(255)]
        public string squad { get; set; }

        [StringLength(255)]
        public string village { get; set; }

        [StringLength(255)]
        public string szone { get; set; }

        [StringLength(255)]
        public string street { get; set; }

        [StringLength(255)]
        public string door { get; set; }

        [StringLength(255)]
        public string resregion { get; set; }

        [StringLength(255)]
        public string building { get; set; }

        [StringLength(255)]
        public string building_num { get; set; }

        [StringLength(255)]
        public string unit { get; set; }

        [StringLength(255)]
        public string floor { get; set; }

        [StringLength(255)]
        public string room { get; set; }

        public int? room_floor { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        public int? IsDeleted { get; set; }

        public decimal? lon { get; set; }

        public decimal? lat { get; set; }
    }
}
