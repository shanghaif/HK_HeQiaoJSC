namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DangerousGoods
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        public Guid MentallyUuid { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string OwningGrid { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateBirth { get; set; }

        [StringLength(255)]
        public string IdCard { get; set; }

        [StringLength(255)]
        public string Danger { get; set; }

        [StringLength(255)]
        public string Employer { get; set; }

        [StringLength(255)]
        public string Attention { get; set; }

        [StringLength(255)]
        public string LegalPerson { get; set; }

        [StringLength(255)]
        public string LegalPersonPhone { get; set; }

        [StringLength(255)]
        public string CorporatePhone { get; set; }

        [StringLength(255)]
        public string ResidenceAddress { get; set; }

        [StringLength(255)]
        public string RegisteredAddress { get; set; }

        [StringLength(255)]
        public string PoliceStation { get; set; }

        [StringLength(255)]
        public string HousesNumber { get; set; }

        [StringLength(255)]
        public string CurrentAddress { get; set; }

        [StringLength(255)]
        public string RoomReason { get; set; }

        [StringLength(255)]
        public string OtherAddress { get; set; }

        [StringLength(255)]
        public string FormerName { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string ContactPhone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Nation { get; set; }

        [StringLength(255)]
        public string PoliticalStatus { get; set; }

        [StringLength(255)]
        public string Education { get; set; }

        [StringLength(255)]
        public string Occupation { get; set; }

        [StringLength(255)]
        public string MaritalStatus { get; set; }

        [StringLength(255)]
        public string BloodType { get; set; }

        [StringLength(255)]
        public string Religious { get; set; }

        [StringLength(255)]
        public string Height { get; set; }

        [StringLength(255)]
        public string Work { get; set; }

        [StringLength(255)]
        public string WorkNumber { get; set; }

        [Column("Effective time", TypeName = "datetime2")]
        public DateTime? Effective_time { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EndTime { get; set; }

        [StringLength(255)]
        public string Waiter { get; set; }

        [StringLength(255)]
        public string ServiceHours { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public int? IsDeleted { get; set; }
    }
}
