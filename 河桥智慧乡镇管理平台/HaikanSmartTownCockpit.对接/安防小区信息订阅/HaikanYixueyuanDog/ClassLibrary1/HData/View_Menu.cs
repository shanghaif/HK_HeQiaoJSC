namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_Menu
    {
        [Key]
        [Column(Order = 0)]
        public Guid SystemMenuUUID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        [StringLength(255)]
        public string Alias { get; set; }

        [StringLength(255)]
        public string Icon { get; set; }

        public Guid? ParentGuid { get; set; }

        [StringLength(255)]
        public string ParentName { get; set; }

        public int? Level { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int? Sort { get; set; }

        public int? Status { get; set; }

        public int? IsDeleted { get; set; }

        public int? IsDefaultRouter { get; set; }

        [StringLength(255)]
        public string CreatedOn { get; set; }

        public Guid? CreatedByUserGuid { get; set; }

        [StringLength(255)]
        public string CreatedByUserName { get; set; }

        [StringLength(255)]
        public string ModifiedOn { get; set; }

        public Guid? ModifiedByUserGuid { get; set; }

        [StringLength(255)]
        public string ModifiedByUserName { get; set; }

        [StringLength(255)]
        public string Component { get; set; }

        public int? HideInMenu { get; set; }

        public int? NotCache { get; set; }

        [StringLength(255)]
        public string BeforeCloseFun { get; set; }

        public int? pt { get; set; }

        public int? ps { get; set; }

        public int? pd { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid SystemRoleUUID { get; set; }
    }
}
