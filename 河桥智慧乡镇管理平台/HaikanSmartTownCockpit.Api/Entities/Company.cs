using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Company
    {
        public Guid CompanyUuid { get; set; }
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string EngName { get; set; }
        public string LegalPeople { get; set; }
        public string UnifiedSocialCode { get; set; }
        public string BusinessLogin { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string Officialwebsite { get; set; }
        public string CompanyAddress { get; set; }
        public string Industry { get; set; }
        public string CompanyType { get; set; }
        public string District { get; set; }
        public string Status { get; set; }
        public int? IsDelete { get; set; }
        public Guid? SystemUserUuid { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
