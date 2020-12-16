namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ListOfKeyPetitioners
    {
        [Key]
        public Guid ListOfKeyPetitionersUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string OwnedNetwork { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        [StringLength(255)]
        public string DateOfBirth { get; set; }

        [StringLength(255)]
        public string IdCard { get; set; }

        [StringLength(255)]
        public string ReasonForPetition { get; set; }

        [StringLength(255)]
        public string Attention { get; set; }

        [StringLength(255)]
        public string ResidenceAddress { get; set; }

        [StringLength(255)]
        public string RegisteredAddress { get; set; }

        [StringLength(255)]
        public string HouseholdRegistrationPoliceStation { get; set; }

        [StringLength(255)]
        public string NumberOfTheHouse { get; set; }

        [StringLength(255)]
        public string CurrentAddress { get; set; }

        [StringLength(255)]
        public string NoRoomReason { get; set; }

        [StringLength(255)]
        public string OtherAddress { get; set; }

        [StringLength(255)]
        public string FormerName { get; set; }

        [StringLength(255)]
        public string Employer { get; set; }

        [StringLength(255)]
        public string ContactNumber { get; set; }

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
        public string ReligiousBelief { get; set; }

        [StringLength(255)]
        public string Height { get; set; }

        [StringLength(255)]
        public string PetitionStatus { get; set; }

        [StringLength(255)]
        public string PetitionType { get; set; }

        [StringLength(255)]
        public string ServiceMembers { get; set; }

        [StringLength(255)]
        public string LatestServiceHours { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public int? IsDeleted { get; set; }
    }
}
