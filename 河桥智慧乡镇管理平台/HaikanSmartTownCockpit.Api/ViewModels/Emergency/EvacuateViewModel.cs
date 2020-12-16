using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency
{
    public class EvacuateViewModel
    {
        public Guid? EvacuateUuid { get; set; }
        public string SceneSpot { get; set; }
        public string EvacuateTime { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string IdCard { get; set; }
        public string Nation { get; set; }
        public string Origion { get; set; }
        public string Phone { get; set; }
        public int? State { get; set; }
    }
}
