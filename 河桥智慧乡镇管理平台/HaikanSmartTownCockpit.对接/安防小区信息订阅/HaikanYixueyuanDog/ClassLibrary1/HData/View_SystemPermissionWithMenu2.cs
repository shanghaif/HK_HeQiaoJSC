namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_SystemPermissionWithMenu2
    {
        [Key]
        [Column(Order = 0)]
        public Guid PermissionCode { get; set; }

        [StringLength(255)]
        public string PermissionActionCode { get; set; }

        [StringLength(255)]
        public string PermissionName { get; set; }

        public int? PermissionType { get; set; }

        [StringLength(255)]
        public string MenuName { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid MenuGuid { get; set; }

        [StringLength(255)]
        public string MenuAlias { get; set; }

        public int? IsDefaultRouter { get; set; }

        public int? pd { get; set; }

        public int? ps { get; set; }
    }
}
