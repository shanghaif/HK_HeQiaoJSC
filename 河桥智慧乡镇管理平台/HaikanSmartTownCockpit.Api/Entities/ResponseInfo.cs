using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class ResponseInfo
    {
        public Guid ResponseInfoUuid { get; set; }
        public int Id { get; set; }
        public string Village { get; set; }
        public string TongzhiRen { get; set; }
        public string XiangyingDj { get; set; }
        public string TongzhiQingk { get; set; }
        public string ZaiciTongzhi { get; set; }
        public string Phone { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public Guid? VillageMemberUuid { get; set; }
        public Guid? ResponseInitUuid { get; set; }
        public int? State { get; set; }

        public virtual ResponseInit ResponseInitUu { get; set; }
        public virtual VillageMember VillageMemberUu { get; set; }
    }
}
