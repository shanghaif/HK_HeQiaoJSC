﻿using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Otherstaffs
    {
        public Guid OtherstaffsUuid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnedNetwork { get; set; }
        public string Sex { get; set; }
        public string DateOfBirth { get; set; }
        public string IdCard { get; set; }
        public string ResidenceAddress { get; set; }
        public string RegisteredAddress { get; set; }
        public string HouseholdRegistrationPoliceStation { get; set; }
        public string NumberOfTheHouse { get; set; }
        public string CurrentAddress { get; set; }
        public string NoRoomReason { get; set; }
        public string OtherAddress { get; set; }
        public string FormerName { get; set; }
        public string Employer { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public string Nation { get; set; }
        public string PoliticalStatus { get; set; }
        public string Education { get; set; }
        public string Occupation { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodType { get; set; }
        public string ReligiousBelief { get; set; }
        public string Height { get; set; }
        public string ServiceMembers { get; set; }
        public string LatestServiceHours { get; set; }
        public string Remarks { get; set; }
        public int? IsDeleted { get; set; }
        public string FamilyPhone { get; set; }
        public string Attention { get; set; }
        public string AttentionCause { get; set; }
        public string WorkCondition { get; set; }
    }
}
