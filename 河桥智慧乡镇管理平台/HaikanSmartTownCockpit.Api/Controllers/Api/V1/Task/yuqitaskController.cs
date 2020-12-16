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
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task
{
    [Route("api/v1/task/[controller]/[action]")]
    [ApiController]
    public class yuqitaskController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;

        public yuqitaskController(haikanHeQiaoContext dbContext, IMapper mapper)
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

                var deptuuid = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == deluuuid).DepartmentUuid;


                var query = from m in _dbContext.Mission
                            where m.Recycle == "0" //没有在回收站且没有在草稿箱中且未完成
                            select new
                            {
                                missionUuid = m.MissionUuid,
                                isyuqi = panduanyuqi(m.FinishTime, m.MissionUuid),//判断是否逾期
                                missionHeadline = m.MissionHeadline,//任务标题
                                startTime = m.StartTime,//开始时间
                                accomplish = m.Accomplish,
                                shenpishow = shenpishow(m.AuditStatus, RoleName),//判断是否能审批
                                wanchenshow = wanchengshow(deptuuid.ToString(), m.AdministrativeOffice, RoleName, m.AuditStatus),//判断是否能完成此任务
                                administrativeOffice = getdept(m.AdministrativeOffice),//下派部门(村庄,科室)
                                keshiuuid = m.AdministrativeOffice,
                                isouttime = m.Isouttime,//是否逾期
                                finishTime = m.FinishTime,//结束时间
                                missionDescribe = m.MissionDescribe,//任务描述
                                priority = m.Priority,//优先级
                                auditStatus = m.AuditStatus,//审核状态
                                establishTime = m.EstablishTime,
                                auditOpinion = m.AuditOpinion,//审核意见
                                id = m.Id,//id
                            };

                if (RoleName.Contains("科室负责人"))//科室负责人查看分配给自己的
                {
                    query = query.Where(x => x.keshiuuid == deptuuid.ToString());
                }


                query = query.Where(x => x.isouttime == "1");//查询已逾期的


                if (payload.canshu == "yqwc")//逾期完成
                {
                    query = query.Where(x => x.accomplish == "1");//完成的
                }

                //查询时间
                if (!string.IsNullOrEmpty(payload.kwstartime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.startTime) >= Convert.ToDateTime(payload.kwstartime));
                }

                if (!string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.finishTime) <= Convert.ToDateTime(payload.kwendtime));
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
                ToLog.AddLog("查询", "成功:查询:已逾期的任务信息数据", _dbContext);
                return Ok(response);
            }
        }


        /// <summary>
        /// 判断是否可以完成
        /// </summary>
        /// <returns></returns>
        public static string wanchengshow(string dlrdept, string admrdept, string RoleName, string auditStatus)
        {
            var wancheng = "否";
            if (RoleName.Contains("超级管理员") && auditStatus == "0")
            {
                wancheng = "是";
            }
            else
            {
                if (admrdept != "" && admrdept != null && auditStatus == "0")
                {
                    if (dlrdept == admrdept)
                    {
                        wancheng = "是";
                    }
                }
            }


            return wancheng;

        }

        /// <summary>
        /// 判断是否可以审批
        /// </summary>
        /// <returns></returns>
        public static string shenpishow(string auditStatus, string name)
        {
            var wancheng = "否";
            if (name.Contains("超级管理员") && auditStatus == "1")
            {
                wancheng = "是";
            }

            return wancheng;

        }



        /// <summary>
        /// 判断任务是否逾期
        /// </summary>
        /// <returns></returns>
        public static string panduanyuqi(string time, Guid uuid)
        {
            string yuqi = "否";
            if (Convert.ToDateTime(time) < DateTime.Now)//已经逾期
            {
                using (haikanHeQiaoContext hk = new haikanHeQiaoContext())
                {
                    var entity = hk.Mission.FirstOrDefault(x => x.MissionUuid == uuid);
                    if (entity.Isouttime != "1")
                    {
                        entity.Isouttime = "1";//变为逾期
                        hk.SaveChanges();
                    }

                }
                yuqi = "是";
            }
            return yuqi;

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
                    string[] deName = uuid.Split(",");
                    foreach (var item in deName)
                    {
                        if (item != "")
                        {
                            var keshidata = haikan.Department.FirstOrDefault(x => x.DepartmentUuid == Guid.Parse(item));
                            var czdata = haikan.Town.FirstOrDefault(x => x.TownUuid == Guid.Parse(item));
                            if (keshidata != null)
                            {
                                deptname += keshidata.Name + ",";
                            }
                            if (czdata != null)
                            {
                                deptname += czdata.TownName + ",";
                            }
                        }
                    }

                }

                return deptname;

            }

        }





    }
}