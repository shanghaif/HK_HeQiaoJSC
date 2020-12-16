using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Priority
{
    public class priorityCreateViewModel
    {
        public Guid PriorityUuid { get; set; }
        public int Id { get; set; }
        public string PriorityHeadline { get; set; }
        public string PriorityDescribe { get; set; }
        public string Principal { get; set; }
        public string Participant { get; set; }
        public string EstablishTime { get; set; }
        public string EstablishName { get; set; }
        public string Remark { get; set; }
        public string Accomplish { get; set; }
        public string Recycle { get; set; }
        public string EndTime { get; set; }
        public int? Sortord { get; set; }
    }
}
