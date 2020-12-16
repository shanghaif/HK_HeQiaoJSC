using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.SocialGovern
{
    public class SecurityPViewModel
    {
        public Guid? SecurityUuid { get; set; }
        public int Id { get; set; }
        public string SecurityName { get; set; }
        public string SecurityAddress { get; set; }
        public string IdentityCard { get; set; }
        public string Phone { get; set; }
        public string SecurityTime { get; set; }
        public string SecurityStaues { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
