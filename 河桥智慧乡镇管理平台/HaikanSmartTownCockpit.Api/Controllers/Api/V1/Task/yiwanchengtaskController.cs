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
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task
{
    [Route("api/v1/task/[controller]/[action]")]
    [ApiController]
    public class yiwanchengtaskController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;

        public yiwanchengtaskController(haikanHeQiaoContext dbContext, IMapper mapper)
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
                var deluuuid = AuthContextService.CurrentUser.Guid;//当前登录人uuid
                var RoleName = AuthContextService.CurrentUser.RoleName;//当前登陆人角色名

                var deptuuid = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == deluuuid).DepartmentUuid;//当前登陆人所属科室

                var query = from m in _dbContext.Mission
                            where m.Recycle == "0"&& m.Preserve == "1"&& m.Accomplish == "1"&&m.Isouttime=="0"//没有在回收站 没有在草稿箱中 已完成 并没有逾期
                            select new
                            {
                                missionUuid = m.MissionUuid,
                                missionHeadline = m.MissionHeadline,//任务标题
                                startTime = m.StartTime,//开始时间
                                finishTime = m.FinishTime,//结束时间
                                administrativeOffice = getdept(m.AdministrativeOffice),//下派部门(村庄,科室)
                                keshiuuid = m.AdministrativeOffice,
                                missionDescribe = m.MissionDescribe,//任务描述
                                priority = m.Priority,//优先级
                                auditStatus = m.AuditStatus,//审核状态
                                establishTime = m.EstablishTime,
                                auditOpinion = m.AuditOpinion,//审核意见
                                id = m.Id,//id
                                Sortord= m.Sortord
                            };

                if (RoleName.Contains("科室负责人"))//科室负责人查看分配给自己的
                {
                    query = query.Where(x => x.keshiuuid == deptuuid.ToString());
                }


                if (!string.IsNullOrEmpty(payload.kwbt))
                {
                    query = query.Where(x => x.missionHeadline.Contains(payload.kwbt));//任务标题
                }


                if (!string.IsNullOrEmpty(payload.zt))
                {
                    query = query.Where(x => x.auditStatus.Contains(payload.zt));//状态
                }
                if (!string.IsNullOrEmpty(payload.yxj))
                {
                    query = query.Where(x => x.priority.Contains(payload.yxj));//优先级
                }


                //按时完成
                if (payload.canshu == "aswc")
                {
                    query = query.Where(x => x.Sortord != "" && x.Sortord != null && Convert.ToDateTime(x.finishTime) >= Convert.ToDateTime(x.Sortord));
                }
                //逾期完成
                if (payload.canshu == "yqwc")
                {
                    query = query.Where(x => x.Sortord != "" && x.Sortord != null && Convert.ToDateTime(x.finishTime) < Convert.ToDateTime(x.Sortord));
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
                ToLog.AddLog("查询", "成功:查询:我完成的任务信息数据", _dbContext);
                return Ok(response);
            }
        }



        /// <summary>
        /// 获取村庄名或科室名
        /// </summary>
        /// <param name="uuid">村庄或科室uuid</param>

        public static string getdept(string uuid)
        {
            using (haikanHeQiaoContext haikan = new haikanHeQiaoContext())
            {
                var deptname = "";
                if (uuid != null && uuid != "")
                {
                    var keshidata = haikan.Department.FirstOrDefault(x => x.DepartmentUuid == Guid.Parse(uuid));
                    var czdata = haikan.Town.FirstOrDefault(x => x.TownUuid == Guid.Parse(uuid));
                    if (keshidata != null)
                    {
                        deptname = keshidata.Name;
                    }
                    if (czdata != null)
                    {
                        deptname = czdata.TownName;
                    }
                }

                return deptname;

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



        /// <summary>
        /// 重启(已完成的任务)
        /// </summary>
        /// <param name="model">需要重启的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult chongqitasktrue(taskCreateViewModel model)
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
                entity.Accomplish = "0";//任务回到未完成状态
                entity.AuditStatus = "0";//待审核(任务回到进行中)
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }


    }
}