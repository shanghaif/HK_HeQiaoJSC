using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Priority
{
    public class AllPriorityViewMosel
    {
        public Guid? PriorityUuid { get; set; }
        public int Id { get; set; }
    public string Headline { get; set; }//重点工作标题
        public string Head { get; set; }
        public string Describe { get; set; }//重点工作描述
        public string PrincipalName { get; set; }//负责人
        public string Principal { get; set; }//负责人
        public string ParticipantIds { get; set; }//参与人
        public string  Participant { get; set; }//参与人
        public string  EstablishTime { get; set; }//创建时间
        public string  EstablishName { get; set; }//创建人
        public string  Remark { get; set; }//备注
        public string  Accomplish { get; set; }//任务是否完成
        public string  Unfinished { get; set; }//任务完成数 / 任务总数
        public string  Recycle { get; set; }
        public int?  Sortord { get; set; }
        public string ParticipantName { get; set; }
        public string issfyc { get; set; }
        public string issfyc2 { get; set; }
    }
}
