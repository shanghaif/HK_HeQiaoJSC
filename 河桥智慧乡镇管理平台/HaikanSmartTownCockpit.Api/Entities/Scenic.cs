using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Scenic
    {
        public Guid ScenicUuid { get; set; }
        public int Id { get; set; }
        public string ScenicName { get; set; }
        public string ScenicGrade { get; set; }
        public string ScenicAddress { get; set; }
        public string ScenicRemarks { get; set; }
        public string ScenicTickets { get; set; }
        public string ScenicQuantity { get; set; }
        public string ScenicPeoPle { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string ScenicType { get; set; }
    }
}
