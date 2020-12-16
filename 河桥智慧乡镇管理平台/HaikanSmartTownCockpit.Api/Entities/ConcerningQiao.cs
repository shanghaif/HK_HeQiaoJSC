using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class ConcerningQiao
    {
        public Guid ConcerningQiaoUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string RealName { get; set; }
        public string UserIdCardType { get; set; }
        public string UserIdCardNum { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Gwaddress { get; set; }
        public string Cnaddress { get; set; }
        public string Xjaddress { get; set; }
        public string ConcerningType { get; set; }
    }
}
