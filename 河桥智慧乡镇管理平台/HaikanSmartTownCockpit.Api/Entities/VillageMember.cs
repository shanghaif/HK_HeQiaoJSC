using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class VillageMember
    {
        public VillageMember()
        {
            ResponseInfo = new HashSet<ResponseInfo>();
        }

        public int Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string AddPeople { get; set; }
        public Guid VillageMemberUuid { get; set; }
        public string MemberName { get; set; }
        public string MemberSex { get; set; }
        public string MemberPhone { get; set; }
        public int? IsDelete { get; set; }
        public Guid? VillageUuid { get; set; }

        public virtual Village VillageUu { get; set; }
        public virtual ICollection<ResponseInfo> ResponseInfo { get; set; }
    }
}
