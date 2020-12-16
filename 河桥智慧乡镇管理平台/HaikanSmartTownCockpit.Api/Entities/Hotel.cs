using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Hotel
    {
        public Hotel()
        {
            RuzhuInfo = new HashSet<RuzhuInfo>();
        }

        public Guid HotelUuid { get; set; }
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string HotelLocation { get; set; }
        public string HotelPhone { get; set; }
        public string HotelRecommend { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string HotelType { get; set; }
        public string HotelPeoPle { get; set; }
        public string Village { get; set; }

        public virtual ICollection<RuzhuInfo> RuzhuInfo { get; set; }
    }
}
