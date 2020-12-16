using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Drug
    {
        public int Id { get; set; }
        public Guid DrugUuid { get; set; }
        public string Name { get; set; }
        public string OwningGrid { get; set; }
        public string Sex { get; set; }
        public DateTime? DateBirth { get; set; }
        public string IdCard { get; set; }
        public string SourceDrugs { get; set; }
        public string Detoxification { get; set; }
        public string Attention { get; set; }
        public string ResidenceAddress { get; set; }
        public string RegisteredAddress { get; set; }
        public string PoliceStation { get; set; }
        public string HousesNumber { get; set; }
        public string CurrentAddress { get; set; }
        public string RoomReason { get; set; }
        public string OtherAddress { get; set; }
        public string FormerName { get; set; }
        public string Employer { get; set; }
        public string Phone { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public string Nation { get; set; }
        public string PoliticalStatus { get; set; }
        public string Education { get; set; }
        public string Occupation { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodType { get; set; }
        public string Religious { get; set; }
        public string Height { get; set; }
        public string DrugStatus { get; set; }
        public string Species { get; set; }
        public string ControlStatus { get; set; }
        public string Detoxificationes { get; set; }
        public string Seizeddate { get; set; }
        public DateTime? FirstTime { get; set; }
        public string Waiter { get; set; }
        public string ServiceHours { get; set; }
        public string Remarks { get; set; }
        public int? IsDeleted { get; set; }
    }
}
