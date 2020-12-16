namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemPermission")]
    public partial class SystemPermission
    {
        [Key]
        public Guid SystemPermissionUUID { get; set; }

        public Guid? SystemMenuUUID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string ActionCode { get; set; }

        [StringLength(255)]
        public string Icon { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int? Status { get; set; }

        public int? IsDeleted { get; set; }

        public int? Type { get; set; }

        public Guid? CreatedByUserGuid { get; set; }

        [StringLength(255)]
        public string CreatedOn { get; set; }

        [StringLength(255)]
        public string CreatedByUserName { get; set; }

        [StringLength(255)]
        public string ModifiedOn { get; set; }

        public Guid? ModifiedByUserGuid { get; set; }

        [StringLength(255)]
        public string ModifiedByUserName { get; set; }

        [StringLength(255)]
        public string CaPower { get; set; }

        public virtual SystemMenu SystemMenu { get; set; }
    }
}
