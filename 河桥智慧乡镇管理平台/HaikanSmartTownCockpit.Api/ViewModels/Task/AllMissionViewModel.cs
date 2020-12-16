using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Task
{
    public class AllMissionViewModel
    {
        public string priorityUUID { get; set; }
        public Guid? missionUuid { get; set; }
        public string priorityHeadline { get; set; }//所属重点工作
        public string missionHeadline { get; set; }//任务标题
        public string principal { get; set; }//u.RealName,//负责人姓名
        public string principaluuid { get; set; }//负责人
        public string startTime { get; set; }//开始时间
        public string finishTime { get; set; }//结束时间
        public string missionDescribe { get; set; }//任务描述
        public string priority { get; set; }//优先级
        public string manhour { get; set; }//计划工时
        public string approver { get; set; }//审批人
        public string zhuangtai { get; set; }//状态
        public string auditStatus { get; set; }//审核状态
        public string participant { get; set; }//参与人
        public int id { get; set; }//id
        public string missionHeadline2 { get; set; }
        public string issfyc { get; set; }
        public string issfycss { get; set; }

        public string accomplish { get; set; }
    }
}
