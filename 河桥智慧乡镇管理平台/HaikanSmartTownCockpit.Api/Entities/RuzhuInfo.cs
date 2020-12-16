using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class RuzhuInfo
    {
        public Guid RuzhuInfoUuid { get; set; }
        public int Id { get; set; }
        public string RuzhuName { get; set; }
        public string RuzhuPeople { get; set; }
        public string RuzhuRoom { get; set; }
        public string RuzhuMoney { get; set; }
        public string RuzhuDay { get; set; }
        public string RuzhuTime { get; set; }
        public string LikaiTime { get; set; }
        public Guid? HotelUuid { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string HotelName { get; set; }

        public virtual Hotel HotelUu { get; set; }
    }
}
