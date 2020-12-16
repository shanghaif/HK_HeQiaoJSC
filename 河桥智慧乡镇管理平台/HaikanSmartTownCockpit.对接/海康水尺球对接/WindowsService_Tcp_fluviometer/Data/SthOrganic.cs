namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SthOrganic")]
    public partial class SthOrganic
    {
        public int ID { get; set; }

        public Guid SthOrganicUUID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Secretary { get; set; }

        [StringLength(255)]
        public string Cadre { get; set; }

        public int? Number { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        public int IsDelete { get; set; }
    }
}
