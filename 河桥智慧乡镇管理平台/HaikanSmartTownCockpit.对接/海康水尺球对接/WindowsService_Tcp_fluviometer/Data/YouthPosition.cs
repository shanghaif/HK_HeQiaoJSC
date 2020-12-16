namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YouthPosition")]
    public partial class YouthPosition
    {
        public int ID { get; set; }

        public Guid YPositionUUID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public string Accessory { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        public int IsDelete { get; set; }
    }
}
