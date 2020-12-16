using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Qiye
    {
        public Guid QiyeUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string QiyeName { get; set; }
        public string QiyeAddress { get; set; }
        public string QiyeType { get; set; }
        public string Faren { get; set; }
        public string Guimo { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string Phone { get; set; }
    }
}
