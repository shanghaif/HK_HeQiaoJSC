using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.RequestPayload.Task;
using HaikanSmartTownCockpit.Api.ViewModels.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task
{
    [Route("api/v1/task/[controller]/[action]")]
    [ApiController]
    public class woshenpitaskController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;

        public woshenpitaskController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        ///// <summary>
        ///// 查询数据
        ///// </summary>
        ///// <param name="payload"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public IActionResult List(alltaskrequestpayload payload)
        //{
        //    using (_dbContext)
        //    {
        //        var query = from m in _dbContext.Mission
        //                    join p in _dbContext.Priority
        //                    on m.PriorityUuid equals p.PriorityUuid
        //                    where m.Recycle == "0"
        //                    where m.Preserve == "1"//没有在回收站且没有在草稿箱中
        //                    where m.AdministrativeOffice == null || m.AdministrativeOffice == ""//不是科室工作总结
        //                    select new
        //                    {
        //                        priorityUUID = m.PriorityUuid,//所属重点工作uuid
        //                        priorityHeadline = p.PriorityHeadline,//所属重点工作
        //                        missionHeadline = m.MissionHeadline,//任务标题
        //                        principaluuid = m.Principal,//负责人uuid
        //                        principal = getfuzeren(m.Principal),//负责人姓名
        //                        startTime = m.StartTime,//开始时间
        //                        finishTime = m.FinishTime,//结束时间
        //                        missionDescribe = m.MissionDescribe,//任务描述
        //                        priority = m.Priority,//优先级
        //                        approver = m.Approver == "" ? "" : m.Approver == null ? "" : m.Approver,//审批人uuid
        //                        //approvername = us.RealName,//审批人
        //                        //approvername = m.Approver == "" ? "" : m.Approver == null ? "" : _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(m.Approver)).RealName,//审批人
        //                        approvername = getfuzeren(m.Approver),//审批人
        //                        auditStatus = m.AuditStatus,//审核状态
        //                        participant = m.Participant,//参与人
        //                        id = m.Id,//id
        //                        establishName = m.EstablishName,//创建人
        //                        auditOpinion = m.AuditOpinion == null ? "" : m.Approver,//审核意见
        //                    };

        //        if (!string.IsNullOrEmpty(payload.userguid))
        //        {
        //            query = query.Where(x => x.approver.Contains(payload.userguid)&&x.auditStatus=="1");//此账号所审批，并且状态为审核中
        //        }
        //        var lis = query.ToList();

        //        if (!string.IsNullOrEmpty(payload.kwbt))
        //        {
        //            query = query.Where(x => x.missionHeadline.Contains(payload.kwbt));//任务标题
        //        }
        //        if (!string.IsNullOrEmpty(payload.kwfzr))
        //        {
        //            query = query.Where(x => x.principaluuid.Contains(payload.kwfzr));//负责人
        //        }

        //        if (!string.IsNullOrEmpty(payload.zt))
        //        {
        //            query = query.Where(x => x.auditStatus.Contains(payload.zt));//状态
        //        }
        //        if (!string.IsNullOrEmpty(payload.yxj))
        //        {
        //            query = query.Where(x => x.priority.Contains(payload.yxj));//优先级
        //        }

        //        //排序
        //        if (payload.FirstSort != null)
        //        {
        //            query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
        //        }

        //        var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
        //        var totalCount = query.Count();
        //        //var data = list.Select(_mapper.Map<SystemUser, UserJsonModel>);
        //        var response = ResponseModelFactory.CreateResultInstance;
        //        response.SetData(lis, totalCount);
        //        return Ok(response);
        //    }
        //}



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


        /// <summary>
        /// 审批任务(通过)
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult shenhe(taskCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid == model.MissionUUID);
                entity.AuditStatus = "2";//审核通过
                entity.Accomplish = "1";//此任务已完成
                entity.AuditOpinion = model.auditOpinion;//审核意见
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }


        /// <summary>
        /// 审批任务(代办)
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult daiban(taskCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {

                var time = DateTime.Now;
                var nowdate = time.ToString("yyyy-MM-dd HH:mm:ss");
                var endtime = time.AddDays(+3).ToString("yyyy-MM-dd HH:mm:ss");//紧急为3天
                if (model.Priority == "普通")
                {
                    endtime = time.AddDays(+5).ToString("yyyy-MM-dd HH:mm:ss");//普通为5天
                }
                var entity = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid == model.MissionUUID);
                entity.AuditStatus = "0";//返回状态为办理中
                entity.Accomplish = "0";//未完成
                entity.FinishTime = endtime;
                entity.AuditOpinion = model.auditOpinion;//审核意见
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }


        /// <summary>
        /// 审批任务(不通过)
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult shenheontongguo(taskCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid == model.MissionUUID);
                entity.AuditStatus = "0";//返回状态为办理中
                entity.AuditOpinion = model.auditOpinion;//审核意见
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }


    }
}