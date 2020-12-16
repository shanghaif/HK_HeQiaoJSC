using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Administrator
    {
        public Guid AdministratorUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string AdministratorName { get; set; }
        public string Sex { get; set; }
        public string IdentityCard { get; set; }
        public string Phone { get; set; }
        public string AdminQuanxian { get; set; }
        public string AdminAddress { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string AdminVillages { get; set; }
        public string GriddingNum { get; set; }
        public string SuozaiWangge { get; set; }
        public string CunjiZhanghao { get; set; }
        public string WanggeZhanghao { get; set; }
        public string Wanggeyuan { get; set; }
    }
}
