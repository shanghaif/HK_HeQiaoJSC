using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.RequestPayload.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task
{
    [Route("api/v1/task/[controller]/[action]")]
    [ApiController]
    public class wocanyutaskController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;

        public wocanyutaskController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(alltaskrequestpayload payload)
        {
            using (_dbContext)
            {
                var userid = _dbContext.SystemUser.Where(x => x.SystemUserUuid == Guid.Parse(payload.userguid)).ToList()[0].Id;//登录账号id

                var query = from m in _dbContext.Mission
                            where m.Recycle == "0"
                            where m.Preserve == "1"//没有在回收站且没有在草稿箱中
                            where m.AdministrativeOffice == null || m.AdministrativeOffice == ""//不是科室工作总结
                            select new
                            {
                                missionUuid = m.MissionUuid,
                                accomplish=m.Accomplish,
                                priorityUUID = m.PriorityUuid == null ? "" : m.PriorityUuid.ToString(),//所属重点工作uuid
                                priorityHeadline = m.PriorityUuid == null ? "" : _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid == m.PriorityUuid).PriorityHeadline,//所属重点工作
                                missionHeadline = m.MissionHeadline,//任务标题
                                principaluuid = m.Principal,//负责人uuid
                                principal = getfuzeren(m.Principal),//负责人姓名
                                startTime = m.StartTime,//开始时间
                                finishTime = m.FinishTime,//结束时间
                                missionDescribe = m.MissionDescribe,//任务描述
                                priority = m.Priority,//优先级
                                approver = m.Approver == "" ? "" : m.Approver == null ? "" : m.Approver,//审批人uuid
                                //approvername = us.RealName,//审批人
                                //approvername = m.Approver == "" ? "" : m.Approver == null ? "" : _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(m.Approver)).RealName,//审批人
                                approvername = getfuzeren(m.Approver),//审批人
                                auditStatus = m.AuditStatus,//审核状态
                                participant = m.Participant,//参与人
                                id = m.Id,//id
                                establishName = m.EstablishName,//创建人
                                auditOpinion = m.AuditOpinion == null ? "" : m.Approver,//审核意见
                            };

                if (!string.IsNullOrEmpty(payload.userguid))
                {
                    query = query.Where(x => x.participant.Contains("," + userid.ToString() + ","));//所参与
                }

                if (!string.IsNullOrEmpty(payload.kwsszdgz))
                {
                    query = query.Where(x => x.priorityUUID == payload.kwsszdgz);//所属重点工作
                }

                if (!string.IsNullOrEmpty(payload.kwbt))
                {
                    query = query.Where(x => x.missionHeadline.Contains(payload.kwbt));//任务标题
                }
                if (!string.IsNullOrEmpty(payload.kwfzr))
                {
                    query = query.Where(x => x.principaluuid.Contains(payload.kwfzr));//负责人
                }

                if (!string.IsNullOrEmpty(payload.zt))
                {
                    query = query.Where(x => x.auditStatus.Contains(payload.zt));//状态
                }
                if (!string.IsNullOrEmpty(payload.yxj))
                {
                    query = query.Where(x => x.priority.Contains(payload.yxj));//优先级
                }
                //排序
                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }

                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                //var data = list.Select(_mapper.Map<SystemUser, UserJsonModel>);
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:我参与的任务信息数据", _dbContext);
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取负责人
        /// </summary>
        /// <returns></returns>
        public static string getfuzeren(string uuids)
        {
            string name = "";
            using (haikanHeQiaoContext hc = new haikanHeQiaoContext())
            {
                var data = uuids.Split(',');
                if (data.Length-1 > 3)//人数超过3人
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (data[i] != "" && data[i] != null)
                        {
                            name += hc.SystemUser.First(x => x.SystemUserUuid == Guid.Parse(data[i])).RealName + ",";
                        }

                    }
                    name = name.Trim(',') + "...";
                }
                else
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] != "" && data[i] != null)
                        {
                            name += hc.SystemUser.First(x => x.SystemUserUuid == Guid.Parse(data[i])).RealName + ",";
                        }

                    }

                    name = name.Trim(',');
                }


            }

            return name;

        }



    }
}