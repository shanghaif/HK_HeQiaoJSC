namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UnifiedAddressLibraryUserInfo")]
    public partial class UnifiedAddressLibraryUserInfo
    {
        public int ID { get; set; }

        public int? UnifiedAddressLibraryID { get; set; }

        public string UserIDList { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? AddTime { get; set; }
    }
}
