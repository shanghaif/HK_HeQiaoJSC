using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.Models.SettleInfo
{
    public class SettleInfoModel
    {
        public int Id { get; set; }
        public Guid SettleUuid { get; set; }
        public string AddTime { get; set; }
        public int? IsDeleted { get; set; }
        public string AddPeople { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Birthdate { get; set; }
        public string NativePlace { get; set; }
        public string PlaceAbode { get; set; }
        public string SettleTime { get; set; }
        public string QianYiDe { get; set; }
        public string IdentityCard { get; set; }
    }
}
