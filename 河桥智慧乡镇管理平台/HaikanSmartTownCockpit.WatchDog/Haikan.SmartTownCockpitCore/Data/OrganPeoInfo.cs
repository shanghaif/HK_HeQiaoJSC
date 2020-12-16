namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrganPeoInfo")]
    public partial class OrganPeoInfo
    {
        [Key]
        public Guid OrganPeoInfoUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string OrganName { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        [StringLength(255)]
        public string Birth { get; set; }

        [StringLength(255)]
        public string IdentityCard { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Education { get; set; }

        [StringLength(255)]
        public string Duty { get; set; }

        [StringLength(255)]
        public string zjWork { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        public Guid? OrganizationUuid { get; set; }

        [StringLength(255)]
        public string TutelageName { get; set; }

        [StringLength(255)]
        public string TutelagePhone { get; set; }

        [StringLength(255)]
        public string HouseAddress { get; set; }

        public string Specific { get; set; }

        [StringLength(255)]
        public string ManType { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
