using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class CityManagement
    {
        public Guid CityManagementUuid { get; set; }
        public int Id { get; set; }
        public int? IsDeleted { get; set; }
        public string Incident { get; set; }
        public string ZhifaTime { get; set; }
        public string ZhifaAddress { get; set; }
        public string Chuliren { get; set; }
        public string BeiChulirren { get; set; }
        public string ZhifaRen { get; set; }
        public string ChuliQingk { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
