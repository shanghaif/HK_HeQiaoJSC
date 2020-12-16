namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BuildHouse")]
    public partial class BuildHouse
    {
        [Key]
        public Guid BuildHouseUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string Household { get; set; }

        [StringLength(255)]
        public string HouseAddress { get; set; }

        [StringLength(255)]
        public string Lon { get; set; }

        [StringLength(255)]
        public string Lat { get; set; }

        [StringLength(255)]
        public string IdentityCard { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string MonitorHouseId { get; set; }

        [StringLength(255)]
        public string AdministratorUuid { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(255)]
        public string Town { get; set; }

        [StringLength(255)]
        public string ProjectCred { get; set; }

        public int? PeopleNum { get; set; }

        [StringLength(255)]
        public string LandNUm { get; set; }

        [StringLength(255)]
        public string BuildArea { get; set; }

        [StringLength(255)]
        public string OccupyArea { get; set; }

        [StringLength(255)]
        public string PliesNum { get; set; }

        [StringLength(255)]
        public string Way { get; set; }

        [StringLength(255)]
        public string ArtisanCred { get; set; }

        [StringLength(255)]
        public string ApproveTime { get; set; }
    }
}
