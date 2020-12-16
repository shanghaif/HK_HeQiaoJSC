namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReleasedPrison")]
    public partial class ReleasedPrison
    {
        [Key]
        public Guid ReleasedPrisonUuid { get; set; }

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
        public string ServiceMembers { get; set; }

        [StringLength(255)]
        public string LatestServiceHours { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string FamilyPhone { get; set; }

        [StringLength(255)]
        public string ControlClass { get; set; }

        [StringLength(255)]
        public string Charge { get; set; }

        [StringLength(255)]
        public string PeopleType { get; set; }

        [StringLength(255)]
        public string ServingPlace { get; set; }

        [StringLength(255)]
        public string Sentence { get; set; }

        [StringLength(255)]
        public string SolutionTime { get; set; }

        [StringLength(255)]
        public string Felony { get; set; }

        [StringLength(255)]
        public string YearFelony { get; set; }

        [StringLength(255)]
        public string KeytoControl { get; set; }

        public string CriminalAct { get; set; }

        [StringLength(255)]
        public string Professional { get; set; }

        [StringLength(255)]
        public string ArrangeTime { get; set; }

        public string ArrangeCause { get; set; }
    }
}
