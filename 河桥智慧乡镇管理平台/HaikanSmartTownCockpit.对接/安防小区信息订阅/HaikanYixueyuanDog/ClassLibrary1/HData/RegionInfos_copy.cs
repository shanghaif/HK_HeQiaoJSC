namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RegionInfos_copy
    {
        public int ID { get; set; }

        public int? RegionID { get; set; }

        public string RegionXYInfo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? AddTime { get; set; }

        public string UnifiedAddressLibraryID { get; set; }
    }
}
