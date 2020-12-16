using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Task
{
    public class AIItaskwhereViewModel
    {

                                public Guid renwuuuid{ get; set; }
                                public String renwubt{ get; set; }//任务标题
                                public String misname { get; set; }//任务标题
                                public int id { get; set; }//id
                                public String fuzerenuuid{ get; set; }//负责人uuid
                                public String fuzerename{ get; set; }
                                public String endtime { get; set; }//结束时间
                                public String auditstatus { get; set; }//审核状态
                                public String accomplish { get; set; }//是否完成
                                public String canyurenids{ get; set; }//参与人ids
    }
}
