using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class RenyuZhuany
    {
        public Guid RenyuZhuanyUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string Xzhuanyi { get; set; }
        public string XiangyingDj { get; set; }
        public string ZhaunyiQingk { get; set; }
        public string Fuzeren { get; set; }
        public string FuzerenPhone { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
