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
    public class caogaotaskController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;

        public caogaotaskController(haikanHeQiaoContext dbContext, IMapper mapper)
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

                var query = from m in _dbContext.Mission
                            join p in _dbContext.Priority
                            on m.PriorityUuid equals p.PriorityUuid
                            where m.Recycle == "0"//没有在回收站
                            where m.Preserve == "2"
                            where m.AdministrativeOffice == null || m.AdministrativeOffice == ""//不是科室工作总结
                            select new
                            {
                                priorityUUID = m.PriorityUuid,//所属重点工作uuid
                                priorityHeadline = p.PriorityHeadline,//所属重点工作
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
                                approvername=getfuzeren(m.Approver),//审批人
                                auditStatus = m.AuditStatus,//审核状态
                                participant = m.Participant,//参与人
                                id = m.Id,//id
                                establishName = m.EstablishName,//创建人
                                auditOpinion = m.AuditOpinion == null ? "" : m.Approver,//审核意见
                            };


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
                if (data.Length - 1 > 3)//人数超过3人
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
        /// 创建任务到草稿箱
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult create(taskCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                if (model.StartTime.ToString() != "" && model.FinishTime.ToString() != "" && Convert.ToDateTime(model.StartTime) >= Convert.ToDateTime(model.FinishTime))
                {
                    response.SetFailed("任务开始时间不能大于或等于结束时间！");
                    return Ok(response);
                }

                var entity = new Mission();
                entity.MissionUuid = Guid.NewGuid();
                entity.PriorityUuid = Guid.Parse(model.PriorityUUID);//所属重点工作
                entity.MissionHeadline = model.MissionHeadline;//任务标题标题
                entity.Principal = model.Principal;//负责人
                entity.StartTime = model.StartTime;//开始时间
                entity.FinishTime = model.FinishTime;//结束时间
                entity.MissionDescribe = model.MissionDescribe;//任务描述
                entity.Priority = model.Priority;//优先级
                entity.Manhour = model.Manhour;//计划工时
                entity.Approver = model.Approver;//审批人
                entity.Participant = "," + model.Participant;//参与人
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");//创建时间
                entity.EstablishName = model.establisthtruename;//创建人uuid
                entity.Recycle = "0";//未删除
                entity.AuditStatus = "0";//审核状态为待审核
                entity.Accomplish = "0";//完成状态为未完成
                entity.Preserve = "2";//保存状态为保存到草稿箱
                _dbContext.Mission.Add(entity);//添加
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }



        /// <summary>
        /// 编辑任务草稿箱
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            using (_dbContext)
            {


                //获取负责人姓名
                var fuzerenuids = _dbContext.Mission.Where(x => x.Id == id).ToList()[0].Principal;//负责人uuid
                var fuzename = "";
                if (fuzerenuids != "" && fuzerenuids != null)
                {
                    var patcount = fuzerenuids.Split(',');
                    for (int i = 0; i < patcount.Length; i++)
                    {
                        if (patcount[i].ToString() != "")
                        {
                            fuzename += _dbContext.SystemUser.Where(x => x.SystemUserUuid == Guid.Parse(patcount[i])).ToList()[0].RealName + ",";
                        }

                    }
                }


                //获取审批人姓名
                var shenpiuids = _dbContext.Mission.Where(x => x.Id == id).ToList()[0].Approver;//审批人uuid
                var shenpiname = "";
                if (shenpiuids != "" && shenpiuids != null)
                {
                    var patcount = shenpiuids.Split(',');
                    for (int i = 0; i < patcount.Length; i++)
                    {
                        if (patcount[i].ToString() != "")
                        {
                            shenpiname += _dbContext.SystemUser.Where(x => x.SystemUserUuid == Guid.Parse(patcount[i])).ToList()[0].RealName + ",";
                        }

                    }
                }


                var participantids = _dbContext.Mission.Where(x => x.Id == id).ToList()[0].Participant;//参与人id
                var selectusername = "";
                if (participantids != "" && participantids != null)
                {
                    var patcount = participantids.Split(',');
                    for (int i = 0; i < patcount.Length; i++)
                    {
                        if (patcount[i].ToString() != "")
                        {
                            selectusername += _dbContext.SystemUser.Where(x => x.Id == Convert.ToInt32(patcount[i])).ToList()[0].RealName + ",";
                        }

                    }
                }



                var query = (from m in _dbContext.Mission
                             where m.Id == id
                             select new
                             {
                                 missionUUID = m.MissionUuid,
                                 priorityUUID = m.PriorityUuid,//所属重点工作uuid
                                 priorityname = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid == m.PriorityUuid).PriorityHeadline,//所属重点工作
                                 missionHeadline = m.MissionHeadline,//任务标题
                                 principal = m.Principal,//负责人uuid
                                 principalname = fuzename.Trim(','),//负责人
                                 startTime = m.StartTime,//开始时间
                                 finishTime = m.FinishTime,//结束时间
                                 missionDescribe = m.MissionDescribe,//任务描述
                                 priority = m.Priority,//优先级
                                 //manhour = Convert.ToInt32(m.Manhour),//计划工时
                                 //manhourt = m.Manhour + "天",//计划工时
                                 approver = m.Approver == null ? "" : m.Approver,//审批人uuid
                                 //approvername = us.RealName,//审批人
                                 selectshenpiname = shenpiname.Trim(','),//审批人
                                 auditStatusNum = m.AuditStatus,//审核状态(数字)
                                 auditStatus = getzhuangtai(m.AuditStatus),//审核状态
                                 selectcanyurenname = selectusername.Trim(','),//参与人
                                 participant = m.Participant,//参与人id
                                 establishName = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(m.EstablishName)).RealName,//创建人姓名
                                 establisthtruename = m.EstablishName,//创建人uuid
                                 remark = m.Remark,//备注
                                 establishTime = m.EstablishTime,//创建时间
                             }).FirstOrDefault();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query);
                return Ok(response);
            }
        }

        //获取审核状态
        public string getzhuangtai(string zt)
        {
            string ztname = "";
            if (zt == "0")
            {
                ztname = "进行中";
            }
            if (zt == "1")
            {
                ztname = "审核中";
            }
            if (zt == "2")
            {
                ztname = "已完成";
            }

            return ztname;
        }

        /// <summary>
        /// 保存编辑的任务到草稿箱
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult baocunEdit(taskCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {

                if (model.StartTime.ToString() != "" && model.FinishTime.ToString() != "" && Convert.ToDateTime(model.StartTime) >= Convert.ToDateTime(model.FinishTime))
                {
                    response.SetFailed("任务开始时间不能大于或等于结束时间！");
                    return Ok(response);
                }


                var entity = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid == model.MissionUUID);

                entity.PriorityUuid = Guid.Parse(model.PriorityUUID);//所属重点工作
                entity.MissionHeadline = model.MissionHeadline;//任务标题标题
                entity.Principal = model.Principal;//负责人
                entity.StartTime = model.StartTime;//开始时间
                entity.FinishTime = model.FinishTime;//结束时间
                entity.MissionDescribe = model.MissionDescribe;//任务描述
                entity.Priority = model.Priority;//优先级
                entity.Manhour = model.Manhour;//计划工时
                entity.Participant = model.Participant;//参与人
                entity.Approver = model.Approver;//审批人
                entity.Participant = model.Participant.Trim(',');//参与人
                entity.Preserve = "1";//移除草稿箱
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }






    }
}