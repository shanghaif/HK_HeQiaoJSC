using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.RequestPayload.UserInfo
{
    public class UserInfoRequestpayload : RequestPayload
    {
        public string Kw { get; set; }
        public string Kw1 { get; set; }
        public string Kw2 { get; set; }
        public string Kw3 { get; set; }
        public List<string> Kw4 { get; set; }

    }
}
