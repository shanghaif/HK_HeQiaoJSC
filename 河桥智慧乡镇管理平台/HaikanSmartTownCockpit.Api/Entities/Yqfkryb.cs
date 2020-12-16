using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Yqfkryb
    {
        public Guid Yqfkrybuuid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string EpidemicPreventionAndControlStatus { get; set; }
        public string Attention { get; set; }
        public string ReasonForConcern { get; set; }
        public string OwnedNetwork { get; set; }
        public string IdCard { get; set; }
        public string ContactPhone { get; set; }
        public string CurrentAddress { get; set; }
        public string ServicePremises { get; set; }
        public string IsolationTime { get; set; }
        public string Origin { get; set; }
        public string WhereToGo { get; set; }
        public string ToAddress { get; set; }
        public string FamilyMemberContactInformation { get; set; }
        public string GoToAddressContact { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ReturnOrEstimatedReturnTime { get; set; }
        public string Transportation { get; set; }
        public string YesNoSuspectedFever { get; set; }
        public string CheckMethod { get; set; }
        public string NameOfResponsibleDoctor { get; set; }
        public string PhoneNumberOfResponsibleDoctor { get; set; }
        public string Remarks { get; set; }
        public string ServiceMemberInformation { get; set; }
        public string GuardianInformation { get; set; }
        public int? IsDeleted { get; set; }
    }
}
