namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MaxOrMin")]
    public partial class MaxOrMin
    {
        [Key]
        public Guid Guid { get; set; }

        public string MaxCount { get; set; }

        public string MinCount { get; set; }
    }
}
