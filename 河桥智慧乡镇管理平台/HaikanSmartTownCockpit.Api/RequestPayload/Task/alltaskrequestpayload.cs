using HaikanSmartTownCockpit.Api.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.RequestPayload.Task
{
    public class alltaskrequestpayload : RequestPayload
    {
        //登录用户guid
        public string userguid { get; set; }

        //任务标题
        public string kwbt { get; set; }

        //负责人
        public string kwfzr { get; set; }

        //所属重点工作
        public string kwsszdgz { get; set; }

        //开始时间
        public string kwstartime { get; set; }

        //结束时间
        public string kwendtime { get; set; }

        public string canshu { get; set; }

        public string zt { get; set; }

        public string yxj { get; set; }
        public string MissionID { get; set; }
        //任务类型：上级交办  下级上传
        public string misstype { get; set; }

    }
}
