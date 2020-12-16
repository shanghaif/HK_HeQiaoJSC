using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Task
{
    public class taskCreateViewModel
    {
        public Guid? MissionUUID { get; set; }
        public int ID { get; set; }
        public string PriorityUUID { get; set; }
        public string MissionHeadline { get; set; }
        public string Principal { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public string MissionDescribe { get; set; }
        public string Priority { get; set; }
        public string Manhour { get; set; }
        public string Participant { get; set; }
        public List<string> Participant2 { get; set; }
        public string Approver { get; set; }
        public string EstablishName { get; set; }
        public string AuditStatus { get; set; }
        public string EstablishTime { get; set; }
        public string selectcanyurenname { get; set; }
        public string Remark { get; set; }
        public string establisthtruename { get; set; }
        public string auditOpinion { get; set; }
        public string Sortord { get; set; }
        public string AdministrativeOffice { get; set; }
        public List<string> AdministrativeOffice2 { get; set; }
        public string accessory { get; set; }
        public string fujian { get; set; }
        public string missiontype { get; set; }
       public List<string> keshilist { get; set; }
        //public string Recycle { get; set; }

        //public string Preserve { get; set; }
        //public string AuditOpinion { get; set; }

    }

    public class studentyibangmodel
    {
        public int key { get; set; }
        public string label { get; set; }
        public bool disabled { get; set; }

    }

    public class studentyibangmodel2
    {
        public string key { get; set; }
        public string label { get; set; }
        public bool disabled { get; set; }

    }


}
