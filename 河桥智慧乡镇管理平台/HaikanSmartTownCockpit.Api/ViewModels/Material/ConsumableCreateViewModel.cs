using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Material
{
    public class ConsumableCreateViewModel
    {
        public Guid ConsumableUuid { get; set; }
        public Guid? SystemUserUuid { get; set; }
        public string ApplyTime { get; set; }
        public int? MaterialType { get; set; }
        public string MaterialName { get; set; }
        public int? Num { get; set; }
        public string Specification { get; set; }
        public string Remark { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public int? AuditState { get; set; }
        public string AuditOpinion { get; set; }
        public string AuditPeople { get; set; }
        public string AuditTime { get; set; }
        public int? IsDeleted { get; set; }
    }
}
