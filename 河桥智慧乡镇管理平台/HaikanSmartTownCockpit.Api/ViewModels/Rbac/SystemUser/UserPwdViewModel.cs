using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Rbac.SystemUser
{
    public class UserPwdViewModel
    {
        public string oldpwd { get; set; }
        public string newpwd { get; set; }
        public string newpwd2 { get; set; }
    }
}
