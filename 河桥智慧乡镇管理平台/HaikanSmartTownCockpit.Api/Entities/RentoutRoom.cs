using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class RentoutRoom
    {
        public Guid RentoutRoomUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string RentoutInfo { get; set; }
        public string RentoutYezhu { get; set; }
        public string RentoutZuhu { get; set; }
        public string RentoutTime { get; set; }
        public string DaoqiTime { get; set; }
        public string RentoutMoney { get; set; }
        public string RentoutStaues { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
