namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YQFKRYB")]
    public partial class YQFKRYB
    {
        [Key]
        public Guid YQFKRYBUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Sex { get; set; }

        [StringLength(255)]
        public string EpidemicPreventionAndControlStatus { get; set; }

        [StringLength(255)]
        public string Attention { get; set; }

        [StringLength(255)]
        public string ReasonForConcern { get; set; }

        [StringLength(255)]
        public string OwnedNetwork { get; set; }

        [StringLength(255)]
        public string IdCard { get; set; }

        [StringLength(255)]
        public string ContactPhone { get; set; }

        [StringLength(255)]
        public string CurrentAddress { get; set; }

        [StringLength(255)]
        public string ServicePremises { get; set; }

        [StringLength(255)]
        public string IsolationTime { get; set; }

        [StringLength(255)]
        public string Origin { get; set; }

        [StringLength(255)]
        public string WhereToGo { get; set; }

        [StringLength(255)]
        public string ToAddress { get; set; }

        [StringLength(255)]
        public string FamilyMemberContactInformation { get; set; }

        [StringLength(255)]
        public string GoToAddressContact { get; set; }

        [StringLength(255)]
        public string ContactPhoneNumber { get; set; }

        [StringLength(255)]
        public string ReturnOrEstimatedReturnTime { get; set; }

        [StringLength(255)]
        public string Transportation { get; set; }

        [StringLength(255)]
        public string YesNoSuspectedFever { get; set; }

        [StringLength(255)]
        public string CheckMethod { get; set; }

        [StringLength(255)]
        public string NameOfResponsibleDoctor { get; set; }

        [StringLength(255)]
        public string PhoneNumberOfResponsibleDoctor { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        [StringLength(255)]
        public string ServiceMemberInformation { get; set; }

        [StringLength(255)]
        public string GuardianInformation { get; set; }

        public int? IsDeleted { get; set; }
    }
}
