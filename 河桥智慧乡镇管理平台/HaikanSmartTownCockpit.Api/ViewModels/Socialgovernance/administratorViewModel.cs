using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Socialgovernance
{
    public class administratorViewModel
    {
        public string AdministratorUuid { get; set; }
        public int? IsDeleted { get; set; }
        public string AdministratorName { get; set; }
        public string Sex { get; set; }
        public string IdentityCard { get; set; }
        public string Phone { get; set; }
        public string AdminQuanxian { get; set; }
        public string AdminAddress { get; set; }
        public string AddPeople { get; set; }
        public string AdminVillages { get; set; }
        public string GriddingNum { get; set; }
        public string SuozaiWangge { get; set; }
        public string CunjiZhanghao { get; set; }
        public string WanggeZhanghao { get; set; }
        public string Wanggeyuan { get; set; }
    }
}
