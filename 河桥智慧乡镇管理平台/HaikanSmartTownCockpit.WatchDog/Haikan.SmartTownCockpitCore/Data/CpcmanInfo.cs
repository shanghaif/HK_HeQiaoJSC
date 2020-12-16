namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CpcmanInfo")]
    public partial class CpcmanInfo
    {
        [Key]
        public Guid CpcmanUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string RealName { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        public DateTime? Birth { get; set; }

        [StringLength(255)]
        public string Education { get; set; }

        [StringLength(255)]
        public string Politics { get; set; }

        public int? Age { get; set; }

        [StringLength(255)]
        public string DangOrganizationName { get; set; }

        [StringLength(255)]
        public string TableNum { get; set; }
    }
}
