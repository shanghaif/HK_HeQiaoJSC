using System;
using System.Linq;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.RequestPayload.Rbac.Role;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.Priority;
using Microsoft.AspNetCore.Mvc;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HaikanSmartTownCockpit.Api.Models.Response;
using System.Collections.Generic;
using System.Security.Cryptography;
using HaikanSmartTownCockpit.Api.ViewModels.Task;
using Newtonsoft.Json;
using HaikanSmartTownCockpit.Api.ViewModels.Priority;
using HaikanSmartTownCockpit.Api.RequestPayload.Rbac.Role;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.EmphasisWork
{
    [Route("api/v1/EmphasisWork/[controller]/[action]")]
    [ApiController]
    public class AllPriorityController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public AllPriorityController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// 已选择用户负责人
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult selectsystemuser2(int id)
        {
            using (_dbContext)
            {
                var participantids = _dbContext.Priority.Where(x => x.Id == id).ToList()[0].Principal;//负责人uuid

                List<studentyibangmodel2> stuyibangmodel = new List<studentyibangmodel2>();
                if (participantids != "" && participantids != null)
                {
                    var patcount = participantids.Trim(',').Split(',');

                    foreach (var item in patcount)
                    {
                        var chaxun = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid.ToString() == item);
                        if (chaxun != null)
                        {
                            studentyibangmodel2 stuyibang = new studentyibangmodel2();

                            stuyibang.key = item;
                            stuyibang.label = chaxun.RealName;
                            stuyibang.disabled = false;
                            stuyibangmodel.Add(stuyibang);
                        }
                    }

                }

                var list = stuyibangmodel.ToArray();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list);
                return Ok(response);
            }
        }
        /// <summary>
        /// 已选择用户参与人
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult selectsystemuser(int id)
        {
            using (_dbContext)
            {
                var participantids = _dbContext.Priority.Where(x => x.Id == id).ToList()[0].Participant;//参与人id

                List<studentyibangmodel> stuyibangmodel = new List<studentyibangmodel>();
                if (participantids != "" && participantids != null)
                {
                    var patcount = participantids.Trim(',').Split(',');

                    foreach (var item in patcount)
                    {
                        var chaxun = _dbContext.SystemUser.FirstOrDefault(x => x.Id.ToString() == item);
                        if (chaxun != null)
                        {
                            studentyibangmodel stuyibang = new studentyibangmodel();

                            stuyibang.key = Convert.ToInt32(item);
                            stuyibang.label = chaxun.RealName;
                            stuyibang.disabled = false;
                            stuyibangmodel.Add(stuyibang);
                        }
                    }

                }

                var list = stuyibangmodel.ToArray();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list);
                return Ok(response);
            }
        }



        /// <summary>
        /// 查询所有数据列表-重点工作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(RoleRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = from a in _dbContext.Priority
                            where a.Recycle == "0"
                            orderby a.ZhiDingTime descending
                            select new
                            {
                                PriorityUuid = a.PriorityUuid,
                                Id = a.Id,
                                PriorityHeadline = a.PriorityHeadline,//标题
                                PriorityDescribe = a.PriorityDescribe,//描述
                                Principaluuid = a.Principal,
                                Principal = getfuzeren(a.Principal),//负责人
                                Participant = a.Participant,//参与人
                                EstablishTime = a.EstablishTime,//添加时间
                                endTime = a.EndTime,//结束时间
                                EstablishName = a.EstablishName,//添加人
                                Remark = a.Remark,//备注
                                Accomplish = a.Accomplish,//是否完成
                                Recycle = a.Recycle,//是否删除
                                Sortord = a.Sortord,//排序字段
                                zhidingshijian = a.ZhiDingTime,//置顶字段
                                Unfinished = _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid && x.Accomplish == "0").ToString() + '/' + _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid).ToString(),
                            };
                //查询名称
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.PriorityHeadline.Contains(payload.Kw.Trim()));
                }
                //查询负责人
                if (!string.IsNullOrEmpty(payload.kwfzr))
                {
                    query = query.Where(x => x.Principaluuid.Contains(payload.kwfzr.Trim()));
                }
                //查询时间
                if (!string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime));
                }

                if (!string.IsNullOrEmpty(payload.kwendtime2))
                {
                    query = query.Where(x => Convert.ToDateTime(x.endTime) <= Convert.ToDateTime(payload.kwendtime2));
                }






                if (!string.IsNullOrEmpty(payload.kwendtime2) && !string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime) && Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime2));
                }
                //排序
                //if (payload.FirstSort != null)
                //{
                //    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                //}
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:重点工作信息数据", _dbContext);
                return Ok(response);

            }
        }

        /// <summary>
        /// 查询我负责的数据列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PrincipalList(RoleRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var useruuid = AuthContextService.CurrentUser.Guid.ToString();//当前登录人

                var query = from a in _dbContext.Priority
                            where a.Recycle == "0" && a.Accomplish == "0" && a.Principal.Contains(AuthContextService.CurrentUser.Guid.ToString())
                            orderby a.Accomplish
                            select new
                            {
                                PriorityUuid = a.PriorityUuid,
                                Id = a.Id,
                                PriorityHeadline = a.PriorityHeadline,
                                PriorityDescribe = a.PriorityDescribe,
                                principal = getfuzeren(a.Principal),
                                principaluuids = a.Principal,
                                Participant = a.Participant,
                                endTime = a.EndTime,//结束时间
                                EstablishTime = a.EstablishTime,
                                EstablishName = a.EstablishName,
                                Remark = a.Remark,
                                accomplish = a.Accomplish,
                                Recycle = a.Recycle,
                                Sortord = a.Sortord,
                                Unfinished = _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid && x.Accomplish == "0").ToString() + '/' + _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid).ToString(),
                            };





                //查询名称
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.PriorityHeadline.Contains(payload.Kw.Trim()));
                }
                //查询时间
                if (!string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime));
                }
                if (!string.IsNullOrEmpty(payload.kwendtime2))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime2));
                }
                if (!string.IsNullOrEmpty(payload.kwendtime2) && !string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime) && Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime2));
                }
                //排序
                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:我负责的信息数据", _dbContext);
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
        /// 查询我创建的数据列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EstablishList(RoleRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = from a in _dbContext.Priority
                            where a.Recycle == "0" && a.Accomplish == "0" && a.EstablishName == AuthContextService.CurrentUser.DisplayName
                            select new
                            {
                                PriorityUuid = a.PriorityUuid,
                                Id = a.Id,
                                PriorityHeadline = a.PriorityHeadline,
                                PriorityDescribe = a.PriorityDescribe,
                                Principaluuid = a.Principal,
                                princial = getfuzeren(a.Principal),
                                //Principal = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(a.Principal)).RealName.ToString(),
                                Participant = a.Participant,
                                EstablishTime = a.EstablishTime,
                                endTime = a.EndTime,//结束时间
                                EstablishName = a.EstablishName,
                                Remark = a.Remark,
                                Accomplish = a.Accomplish,
                                Recycle = a.Recycle,
                                Sortord = a.Sortord,
                                Unfinished = _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid && x.Accomplish == "0").ToString() + '/' + _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid).ToString(),
                            };
                //查询名称
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.PriorityHeadline.Contains(payload.Kw.Trim()));
                }
                //查询负责人
                if (!string.IsNullOrEmpty(payload.kwfzr))
                {
                    query = query.Where(x => x.Principaluuid.Contains(payload.kwfzr.Trim()));
                }
                //查询时间
                if (!string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime));
                }
                if (!string.IsNullOrEmpty(payload.kwendtime2))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime2));
                }
                if (!string.IsNullOrEmpty(payload.kwendtime2) && !string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime) && Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime2));
                }
                //排序
                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:我创建的信息数据", _dbContext);
                return Ok(response);
            }
        }

        /// <summary>
        /// 查询我参与的数据列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ParticipantList(RoleRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var userid = _dbContext.SystemUser.Where(x => x.SystemUserUuid == AuthContextService.CurrentUser.Guid).ToList()[0].Id;//登录账号id

                var query = from a in _dbContext.Priority
                            where a.Recycle == "0" && a.Accomplish == "0" && a.Participant.Contains("," + userid.ToString() + ",")
                            orderby a.Accomplish
                            select new
                            {
                                PriorityUuid = a.PriorityUuid,
                                Id = a.Id,
                                PriorityHeadline = a.PriorityHeadline,
                                PriorityDescribe = a.PriorityDescribe,
                                Principaluuid = a.Principal,
                                princial = getfuzeren(a.Principal),
                                //Principal = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(a.Principal)).RealName.ToString(),
                                Participant = a.Participant,
                                EstablishTime = a.EstablishTime,
                                endTime = a.EndTime,//结束时间
                                EstablishName = a.EstablishName,
                                Remark = a.Remark,
                                Accomplish = a.Accomplish,
                                Recycle = a.Recycle,
                                Sortord = a.Sortord,
                                Unfinished = _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid && x.Accomplish == "0").ToString() + '/' + _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid).ToString(),
                            };
                //查询名称
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.PriorityHeadline.Contains(payload.Kw.Trim()));
                }
                //查询负责人
                if (!string.IsNullOrEmpty(payload.kwfzr))
                {
                    query = query.Where(x => x.Principaluuid.Contains(payload.kwfzr.Trim()));
                }
                //查询时间
                if (!string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime));
                }
                if (!string.IsNullOrEmpty(payload.kwendtime2))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime2));
                }
                if (!string.IsNullOrEmpty(payload.kwendtime2) && !string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime) && Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime2));
                }
                //排序
                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:我参与的信息数据", _dbContext);
                return Ok(response);
            }
        }
        /// <summary>
        /// 查询已完成的数据列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AccomplishList(RoleRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = from a in _dbContext.Priority
                            where a.Recycle == "0" && a.Accomplish == "1"
                            select new
                            {
                                PriorityUuid = a.PriorityUuid,
                                Id = a.Id,
                                PriorityHeadline = a.PriorityHeadline,
                                PriorityDescribe = a.PriorityDescribe,
                                Principaluuid = a.Principal,
                                princial = getfuzeren(a.Principal),
                                //Principal = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(a.Principal)).RealName.ToString(),
                                Participant = a.Participant,
                                EstablishTime = a.EstablishTime,
                                endTime = a.EndTime,//结束时间
                                EstablishName = a.EstablishName,
                                Remark = a.Remark,
                                Accomplish = a.Accomplish,
                                Recycle = a.Recycle,
                                Sortord = a.Sortord,
                                Unfinished = _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid && x.Accomplish == "0").ToString() + '/' + _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid).ToString(),
                            };
                //查询名称
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.PriorityHeadline.Contains(payload.Kw.Trim()));
                }
                //查询负责人
                if (!string.IsNullOrEmpty(payload.kwfzr))
                {
                    query = query.Where(x => x.Principaluuid.Contains(payload.kwfzr.Trim()));
                }
                //查询时间
                if (!string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime));
                }
                if (!string.IsNullOrEmpty(payload.kwendtime2))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime2));
                }
                if (!string.IsNullOrEmpty(payload.kwendtime2) && !string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime) && Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime2));
                }
                //排序
                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:已完成的信息数据", _dbContext);
                return Ok(response);
            }
        }

        /// <summary>
        /// 查询回收站的数据列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RecycleList(RoleRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = from a in _dbContext.Priority
                            where a.Recycle == "1"
                            select new
                            {
                                PriorityUuid = a.PriorityUuid,
                                Id = a.Id,
                                PriorityHeadline = a.PriorityHeadline,
                                PriorityDescribe = a.PriorityDescribe,
                                Principaluuid = a.Principal,
                                princialname = getfuzeren(a.Principal),
                                //Principal = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(a.Principal)).RealName.ToString(),
                                Participant = a.Participant,
                                EstablishTime = a.EstablishTime,
                                endTime = a.EndTime,//结束时间
                                EstablishName = a.EstablishName,
                                Remark = a.Remark,
                                Accomplish = a.Accomplish,
                                Recycle = a.Recycle,
                                Sortord = a.Sortord,
                                Unfinished = _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid && x.Accomplish == "0").ToString() + '/' + _dbContext.Mission.Count(x => x.PriorityUuid == a.PriorityUuid).ToString(),
                            };
                //查询名称
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.PriorityHeadline.Contains(payload.Kw.Trim()));
                }
                //查询负责人
                if (!string.IsNullOrEmpty(payload.kwfzr))
                {
                    query = query.Where(x => x.Principaluuid.Contains(payload.kwfzr.Trim()));
                }
                //查询时间
                if (!string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime));
                }
                if (!string.IsNullOrEmpty(payload.kwendtime2))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime2));
                }
                if (!string.IsNullOrEmpty(payload.kwendtime2) && !string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) >= Convert.ToDateTime(payload.kwendtime) && Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime2));
                }
                //排序
                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:回收站的信息数据", _dbContext);
                return Ok(response);
            }
        }

        /// <summary>
        /// 创建重点工作
        /// </summary>
        /// <param name="model">重点工作视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(PriorityCreateModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            //if (model.Sortord == null)
            //{
            //    //response.SetFailed("请输入排序字段");
            //    //return Ok(response);
            //}
            //else
            //{
            //    try
            //    {
            //        int Sortord = int.Parse(model.Sortord);
            //    }
            //    catch (Exception)
            //    {
            //        response.SetFailed("排序字段必须为数字");
            //        return Ok(response);
            //    }
            //    var user = _dbContext.Priority.FirstOrDefault(x => x.Sortord == int.Parse(model.Sortord) && x.Recycle == "0" && x.Accomplish == "0");
            //    if (user != null)
            //    {
            //        response.SetFailed("排序已存在");
            //        return Ok(response);
            //    }
            //}

            using (_dbContext)
            {
                var priority = new HaikanSmartTownCockpit.Api.Entities.Priority();
                var zhiding = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                priority.PriorityHeadline = model.PriorityHeadline;//标题
                priority.PriorityDescribe = model.PriorityDescribe;//描述
                priority.Principal = model.Principal;//负责人
                //priority.Principal = model.principalid;//负责人
                //priority.Participant = model.Participant;//参与人
                priority.Participant = "," + model.participantid;//参与人id
                priority.EndTime = Convert.ToDateTime(model.EndTime).ToString("yyyy-MM-dd HH:mm:ss");
                priority.EstablishTime = zhiding;//创建时间
                priority.ZhiDingTime = zhiding;//初始化置顶时间
                priority.EstablishName = AuthContextService.CurrentUser.DisplayName;//创建人
                priority.Remark = model.Remark;//备注
                priority.Accomplish = "0";//是否完成
                priority.Recycle = "0";//是否删除 
                if (model.Sortord != "" && model.Sortord != null)
                {
                    priority.Sortord = int.Parse(model.Sortord);//排序
                }
                else
                {
                    priority.Sortord = null;
                }
                _dbContext.Priority.Add(priority);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:重点工作信息一条数据", _dbContext);
                }
                notes(priority.PriorityUuid.ToString());//添加后发送消息
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑重点工作-显示数据
        /// </summary>
        /// <param name="guid">重点工作惟一编码</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(int id)
        {
            var participantids = _dbContext.Priority.Where(x => x.Id == id).ToList()[0].Participant;//参与人id
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
            //获取负责人姓名
            var fuzerenuids = _dbContext.Priority.Where(x => x.Id == id).ToList()[0].Principal;//负责人uuid
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
            using (_dbContext)
            {
                var query = (from m in _dbContext.Priority
                                 //join u in _dbContext.SystemUser
                                 //on m.Principal equals u.SystemUserUuid.ToString()
                             where m.Id == id
                             select new
                             {
                                 priorityUUID = m.PriorityUuid,
                                 ID = m.Id,//id
                                 PriorityHeadline = m.PriorityHeadline,//重点工作标题
                                 PriorityDescribe = m.PriorityDescribe,//重点工作描述
                                 principal = m.Principal,//负责人uuid
                                 //principalname = u.RealName,//负责人 
                                 principalname = getfuzeren(m.Principal),//负责人
                                 //principalname = fuzename,
                                 endTime = m.EndTime,
                                 Participantid = m.Participant,//参与人 
                                 Participant = selectusername.Trim(','),//参与人 
                                 EstablishTime = m.EstablishTime,//创建时间
                                 EstablishName = m.EstablishName,//创建人
                                 Remark = m.Remark,//备注
                                 Accomplish = m.Accomplish,//是否完成
                                 Recycle = m.Recycle,//是否删除
                                 Sortord = m.Sortord,//排序字段
                             }).FirstOrDefault();
                var fuzeren = query;
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的重点工作信息
        /// </summary>
        /// <param name="model">重点工作视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(PriorityCreateModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                if (model.Sortord == null)
                {
                    //response.SetFailed("请输入排序字段");
                    //return Ok(response);
                }
                else
                {
                    try
                    {
                        int Sortord = int.Parse(model.Sortord);
                    }
                    catch (Exception)
                    {
                        response.SetFailed("排序字段必须为数字");
                        return Ok(response);
                    }
                    var user = _dbContext.Priority.FirstOrDefault(x => x.Sortord == int.Parse(model.Sortord) && x.Recycle == "0" && x.Accomplish == "0" && x.PriorityUuid != model.PriorityUuid);
                    if (user != null)
                    {
                        response.SetFailed("排序已存在");
                        return Ok(response);
                    }
                }
                var entity = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid == model.PriorityUuid);
                entity.PriorityHeadline = model.PriorityHeadline;//标题
                entity.PriorityDescribe = model.PriorityDescribe;//描述
                entity.EndTime = Convert.ToDateTime(model.EndTime).ToString("yyyy-MM-dd HH:mm:ss"); ;
                entity.Principal = model.Principal;//负责人
                entity.Participant = model.participantid;//参与人uuid
                entity.Remark = model.Remark;//备注
                if (model.Sortord != "" && model.Sortord != null)
                {
                    entity.Sortord = int.Parse(model.Sortord);//排序
                }
                else
                {
                    entity.Sortord = null;
                }
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:重点工作信息一条数据", _dbContext);
                }
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除重点工作-标记删除
        /// </summary>
        /// <param name="ids">重点工作uuID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            response = UpdateIsDelete(CommonEnum.IsDeleted.Yes, ids);
            return Ok(response);
        }

        /// <summary>
        /// 批量操作-标记
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">重点工作uuID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    if (ConfigurationManager.AppSettings.IsTrialVersion)
                    {
                        response.SetIsTrial();
                        return Ok(response);
                    }
                    response = UpdateIsDelete(CommonEnum.IsDeleted.Yes, ids);
                    break;
                case "recover":
                    response = UpdateIsDelete(CommonEnum.IsDeleted.No, ids);
                    break;
                default:
                    break;
            }
            return Ok(response);
        }

        /// <summary>
        /// 删除重点工作-真删
        /// </summary>
        /// <param name="ids">重点工作uuID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult DeleteRecycle(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            response = DeleteEwq(ids);
            return Ok(response);
        }

        /// <summary>
        /// 批量操作-真删
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">重点工作uuID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult BatchRecycle(string command, string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    if (ConfigurationManager.AppSettings.IsTrialVersion)
                    {
                        response.SetIsTrial();
                        return Ok(response);
                    }
                    response = DeleteEwq(ids);
                    break;
                case "recover":
                    response = UpdateIsDelete(CommonEnum.IsDeleted.No, ids);
                    break;
                default:
                    break;
            }
            return Ok(response);
        }

        /// <summary>
        /// 恢复重点工作
        /// </summary>
        /// <param name="ids">重点工作uuID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult DeleteRenew(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            response = UpdateIsDelete(CommonEnum.IsDeleted.No, ids);
            return Ok(response);
        }

        /// <summary>
        /// 完成重点工作
        /// </summary>
        /// <param name="model">重点工作视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult PriorityAccomplish(PriorityCreateModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid == model.PriorityUuid);
                entity.Accomplish = "1";//完成
                entity.Remark = model.Remark;//备注
                _dbContext.SaveChanges();
                return Ok(response);
            }
        }

        /// <summary>
        /// 重启重点工作
        /// </summary>
        /// <param name="ids">重点工作uuID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult PriorityRestart(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            response = UpdateIsAccomplish(CommonEnum.YnAccomplish.No, ids);
            return Ok(response);
        }

        /// <summary>
        /// 升序-ASC-置顶
        /// </summary>
        /// <param name="sortord"></param>
        /// <param name="guid">角色ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UpdateIsASC(string sortord, string guid)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                //拿到要升序-置顶的数据
                var entity = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid == Guid.Parse(guid));
                //if (entity.Sortord == null || entity.Sortord.ToString() == "")
                //{
                //    response.SetFailed("该条数据没有排序");
                //    return Ok(response);
                //}
                //拿到排序字段比他小的那个字段
                //var entity2 = _dbContext.Priority.Where(x => x.Sortord < int.Parse(sortord) && x.Accomplish == "0" && x.Recycle == "0").OrderByDescending(x => x.Sortord).FirstOrDefault();
                if (entity != null)
                {
                    entity.ZhiDingTime = "9999-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    entity.Sortord = entity.Sortord;
                    //entity.Sortord = s;
                    _dbContext.SaveChanges();
                    return Ok(response);
                }
                else
                {
                    response.SetFailed("没有了o");
                    return Ok(response);
                }
            }
        }
        /// <summary>
        /// 降序-DESC-取消置顶
        /// </summary>
        /// <param name="sortord"></param>
        /// <param name="guid">角色ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UpdateIsDESC(string sortord, string guid)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                var entity = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid == Guid.Parse(guid));
                if (entity != null)
                {
                    entity.ZhiDingTime = "";
                    entity.ZhiDingTime = entity.EstablishTime;
                    _dbContext.SaveChanges();
                    return Ok(response);
                }
                else
                {
                    response.SetFailed("没有了o");
                    return Ok(response);
                }
            }
        }

        /// <summary>
        /// 用户下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult systemuserList()
        {
            using (_dbContext)
            {
                var query = from s in _dbContext.SystemUser
                            where s.IsDeleted == 0
                            select new
                            {
                                systemUserUUID = s.SystemUserUuid,
                                id = s.Id,
                                loginName = s.LoginName,//登录名
                                realName = s.RealName,//真实姓名
                                isDeleted = s.IsDeleted
                            };
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query.ToList());
                return Ok(response);
            }
        }

        /// <summary>
        /// 用户参与人穿梭框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult allsystemuser()
        {
            using (_dbContext)
            {
                var query = from s in _dbContext.SystemUser
                            where s.IsDeleted == 0
                            select new
                            {
                                systemuseruuid = s.SystemUserUuid,
                                key = s.Id,
                                loginName = s.LoginName,//登录名
                                label = s.RealName,//真实姓名
                                isDeleted = s.IsDeleted,
                                disabled = false
                            };
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query.ToList());
                return Ok(response);
            }
        }



        /// <summary>
        /// 用户参与人穿梭框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult allsystemuser2()
        {
            using (_dbContext)
            {
                var query = from s in _dbContext.SystemUser
                            where s.IsDeleted == 0
                            select new
                            {

                                key = s.SystemUserUuid,
                                loginName = s.LoginName,//登录名
                                label = s.RealName,//真实姓名
                                isDeleted = s.IsDeleted,
                                disabled = false
                            };
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query.ToList());
                return Ok(response);
            }
        }




        /// <summary>
        /// 给参与人推送信息
        /// </summary>
        [HttpGet("{uuid}")]
        public IActionResult notescanyu(string uuid)
        {

            var response = ResponseModelFactory.CreateInstance;
            var xiaoxidata = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid == Guid.Parse(uuid));

            var tuisongrenuuid = AuthContextService.CurrentUser.Guid;//uuid
            var tuisongrenname = AuthContextService.CurrentUser.DisplayName;//姓名
            var dingdingid = "";//接收者的钉钉ID

            //参与人id
            string canyuren = xiaoxidata.Participant;

            if (canyuren != "" && canyuren != null)
            {
                var canyurenlist = canyuren.Split(',');
                for (int z = 0; z < canyurenlist.Length; z++)
                {
                    if (canyurenlist[z] != "" && canyurenlist[z] != null)
                    {
                        dingdingid += _dbContext.SystemUser.FirstOrDefault(x => x.Id == Convert.ToInt32(canyurenlist[z])).Dinguserid + ",";
                    }

                }


            }


            using (_dbContext)
            {
                var entity = new Note();
                entity.NoteUuid = Guid.NewGuid();
                entity.Naem = "重点工作完成提醒";
                entity.Content = tuisongrenname + "提醒您,近期需要完成重点工作" + xiaoxidata.PriorityHeadline + "重点工作结束时间:" + xiaoxidata.EndTime;
                entity.ModuleUuid = dingdingid.Trim(',');//接收人钉钉id
                entity.ModuleNaem = xiaoxidata.PriorityUuid.ToString();//重点工作uuid
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//添加时间
                entity.EstablishName = tuisongrenuuid.ToString();
                var result = Dingtongzhi(entity);
                if (result == "成功发送了工作通知")
                {//消息发送成功，将数据添加到数据库中
                    _dbContext.Note.Add(entity);
                    _dbContext.SaveChanges();
                }

                response.SetFailed(result);
            }







            return Ok(response);



        }


        /// <summary>
        /// 负责人，参与人推送信息
        /// </summary>
        [HttpGet("{uuid}")]
        public IActionResult notes(string uuid)
        {

            var response = ResponseModelFactory.CreateInstance;
            var xiaoxidata = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid == Guid.Parse(uuid));

            var tuisongrenuuid = AuthContextService.CurrentUser.Guid;//uuid
            var tuisongrenname = AuthContextService.CurrentUser.DisplayName;//姓名
            var dingdingid = "";//接收者的钉钉ID


            //负责人id
            string fuzeren = xiaoxidata.Principal;

            if (fuzeren != "" && fuzeren != null)
            {
                var fuzerenlist = fuzeren.Split(',');
                for (int k = 0; k < fuzerenlist.Length; k++)
                {
                    if (fuzerenlist[k] != "" && fuzerenlist[k] != null)
                    {
                        dingdingid += _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(fuzerenlist[k])).Dinguserid + ",";
                    }

                }


            }


            //参与人id
            string canyuren = xiaoxidata.Participant;

            if (canyuren != "" && canyuren != null)
            {
                var canyurenlist = canyuren.Split(',');
                for (int z = 0; z < canyurenlist.Length; z++)
                {
                    if (canyurenlist[z] != "" && canyurenlist[z] != null)
                    {
                        dingdingid += _dbContext.SystemUser.FirstOrDefault(x => x.Id == Convert.ToInt32(canyurenlist[z])).Dinguserid + ",";
                    }

                }


            }


            using (_dbContext)
            {
                var entity = new Note();
                entity.NoteUuid = Guid.NewGuid();
                entity.Naem = "工作提醒";
                entity.Content = tuisongrenname + "添加了重点工作：" + xiaoxidata.PriorityHeadline + ",重点工作结束时间:" + xiaoxidata.EndTime;
                entity.ModuleUuid = dingdingid.Trim(',');//接收人钉钉id
                entity.ModuleNaem = xiaoxidata.PriorityUuid.ToString();//重点工作uuid
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//添加时间
                entity.EstablishName = tuisongrenuuid.ToString();
                var result = Dingtongzhi(entity);
                if (result == "成功发送了工作通知")
                {//消息发送成功，将数据添加到数据库中
                    _dbContext.Note.Add(entity);
                    _dbContext.SaveChanges();
                }

                response.SetFailed(result);
            }


            return Ok(response);



        }




        /// <summary>
        /// 发送钉钉工作通知
        /// </summary>
        public string Dingtongzhi(Note note)
        {

            var didlist = note.ModuleUuid.ToString().Split(',');

            var fasongstr = "";
            var chaostr = "";

            var log = "工作通知发送失败";
            string suiteKey = "dingdhhaaylylww2i7fw";
            string suiteSecret = "sYgyZowkEoo4O9TQdqtaLdUcH2JyWfOsFmmRLkiGhDUC3-dvFp7j7mZ73Pd3KoOq";


            //获取access_token
            string urlt = "https://oapi.dingtalk.com/gettoken?appkey=" + suiteKey + "&appsecret=" + suiteSecret;


            try
            {

                var response11t = Haikan3.Utils.DingDingHelper.HttpGet(urlt);
                var resultt = Newtonsoft.Json.JsonConvert.DeserializeObject<PersistentCodeResult>(response11t);
                if (resultt != null && resultt.errcode == "0")
                {

                    //发送消息的人数超过90人
                    if (didlist.Length > 90)
                    {
                        for (int i = 0; i < didlist.Length; i++)
                        {
                            if (i < 90)
                            {

                                fasongstr += didlist[i] + ",";
                            }
                            else
                            {
                                chaostr += didlist[i] + ",";

                            }

                        }

                        string dingxiao = "https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2?access_token=" + resultt.access_token;
                        string paramdx = "{ \"agent_id\": \"826088208\",\"userid_list\":\"" + fasongstr.Trim(',') + "\",\"msg\":{\"msgtype\":\"text\",\"text\":{\"content\":\"" + note.Content + "\"}}}";
                        var sjggi = Haikan3.Utils.DingDingHelper.HttpPost(dingxiao, paramdx);
                        var fanhuidata = JsonConvert.DeserializeObject<PersistentCodeResult>(sjggi);
                        if (fanhuidata != null && fanhuidata.errcode == "0")
                        {
                            log = "成功发送了工作通知";
                        }


                        //再次调用发送信息的方法
                        note.ModuleUuid = chaostr.Trim(',');

                        Dingtongzhi(note);


                    }
                    else
                    {

                        string dingxiao = "https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2?access_token=" + resultt.access_token;
                        string paramdx = "{ \"agent_id\": \"826088208\",\"userid_list\":\"" + note.ModuleUuid + "\",\"msg\":{\"msgtype\":\"text\",\"text\":{\"content\":\"" + note.Content + "\"}}}";
                        var sjggi = Haikan3.Utils.DingDingHelper.HttpPost(dingxiao, paramdx);
                        var fanhuidata = JsonConvert.DeserializeObject<PersistentCodeResult>(sjggi);
                        if (fanhuidata != null && fanhuidata.errcode == "0")
                        {
                            log = "成功发送了工作通知";
                        }

                    }







                }

            }
            catch (Exception ex)
            {
                log = "工作通知发送失败：" + ex.Message;
                //Log.LogMsg("DingDing_GetPersistentCode_Exception", DateTime.Now, ex.Message);
                throw new Exception(ex.Message);
            }
            return log;

        }








        /// <summary>
        /// 添加汇报
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult createhuibao(priorityjournal_model model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = new PriorityJournal();
                entity.PriorityJournalUuid = Guid.NewGuid();
                entity.PriorityUuid = model.PriorityUuid;//重点工作uuid
                entity.Completed = model.Completed;//已完成
                entity.Coordination = model.Coordination;//需要协调
                entity.IsDeleted = 0;
                entity.Accessory = model.Accessory;//附件
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//添加时间
                entity.EstablishName = AuthContextService.CurrentUser.Guid.ToString();//添加人姓名
                _dbContext.PriorityJournal.Add(entity);//添加
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }





        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        public IActionResult depdata(string uuid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var rwinfo = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid.ToString().ToLower() == uuid);
                if (rwinfo != null)
                {
                    var deplist = "";
                    var fzrlist = rwinfo.Principal;
                    var cyrlist = rwinfo.Participant;
                    var systemuser = _dbContext.SystemUser.Where(x => fzrlist.Contains(x.SystemUserUuid.ToString()) || cyrlist.Contains("," + x.Id.ToString() + ",")).ToList();
                    if (systemuser != null && systemuser.Count > 0)
                    {
                        for (int i1 = 0; i1 < systemuser.Count; i1++)
                        {
                            if (i1 == 0)
                            {
                                deplist = systemuser[i1].DepartmentUuid.ToString();
                            }
                            else
                            {
                                deplist += "," + systemuser[i1].DepartmentUuid.ToString();
                            }
                        }
                    }
                    if (deplist != null)
                    {
                        var query = from u in _dbContext.Department
                                    where u.IsDeleted == 0
                                    && deplist.Contains(u.DepartmentUuid.ToString())
                                    orderby u.Id ascending
                                    select new
                                    {
                                        depUUID = u.DepartmentUuid,
                                        name = u.Name,
                                        id = u.DepartmentUuid,
                                        count = _dbContext.SystemUser.Count(x => x.DepartmentUuid == u.DepartmentUuid && x.IsDeleted == 0)
                                    };
                        var query1 = query.OrderBy(a => a.name).ToList();
                        response.SetData(query1);
                    }
                }
            }
            return Ok(response);

        }


        /// <summary>
        /// 绑定人员
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult userdata(string uuid, string depuuid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var rwinfo = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid.ToString().ToLower() == uuid);
                if (depuuid != null && depuuid != "")
                {
                    if (rwinfo != null)
                    {
                        var fzrlist = rwinfo.Principal;
                        var cyrlist = rwinfo.Participant;
                        var systemuser = _dbContext.SystemUser.Where(x => (fzrlist.Contains(x.SystemUserUuid.ToString()) || cyrlist.Contains("," + x.Id.ToString() + ",")) && x.DepartmentUuid.ToString() == depuuid).ToList();
                        response.SetData(systemuser);

                    }

                }
                else
                {

                    if (rwinfo != null)
                    {
                        var fzrlist = rwinfo.Principal;
                        var cyrlist = rwinfo.Participant;
                        var systemuser = _dbContext.SystemUser.Where(x => fzrlist.Contains(x.SystemUserUuid.ToString()) || cyrlist.Contains("," + x.Id.ToString() + ",")).ToList();
                        response.SetData(systemuser);

                    }
                }

            }
            return Ok(response);

        }




        /// <summary>
        /// 查询汇报
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult selecehuibao(string uuid, string useruuid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = from u in _dbContext.PriorityJournal
                            where u.IsDeleted == 0
                            && u.PriorityUuid == Guid.Parse(uuid)
                            orderby u.Id descending
                            select new
                            {
                                name = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid.ToString() == u.EstablishName).RealName,
                                completed = gettitle2(u.Completed),
                                coordination = gettitle2(u.Coordination),
                                accessory = gettitle2(u.Accessory),
                                username = u.EstablishName,
                                establishTime = u.EstablishTime,
                                priorityJournalUuid = u.PriorityJournalUuid,
                            };
                if (useruuid != null && useruuid != "")
                {
                    query = query.Where(x => x.username == useruuid);
                }
                response.SetData(query.ToList());
            }
            return Ok(response);

        }

        public string gettitle2(string longtitle)
        {
            if (longtitle.Length > 12)
            {
                longtitle = longtitle.Substring(0, 12) + "...";
            }
            return longtitle;
        }



        /// <summary>
        /// 查看汇报详细
        /// </summary>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        public IActionResult hblooks(string uuid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = (from u in _dbContext.PriorityJournal
                             where u.IsDeleted == 0
                             && u.PriorityJournalUuid == Guid.Parse(uuid)
                             orderby u.Id ascending
                             select new
                             {
                                 establisthuuid = u.EstablishName,
                                 establishName = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(u.EstablishName)).RealName,
                                 establishTime = u.EstablishTime,
                                 accessory = u.Accessory,
                                 completed = u.Completed,
                                 coordination = u.Coordination,
                             }).FirstOrDefault();
                response.SetData(query);
            }
            return Ok(response);

        }



















































        /// <summary>
        /// 免登
        /// </summary>
        /// <returns></returns>
        [HttpGet("{strlist}")]
        public IActionResult getuserinfo(string strlist)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var code = strlist;
                string suiteKey = "suitecy28eq7tsxaipeuw";
                string suiteSecret = "MZO9-0iBJurEjOaD_iz2ZcjJD_e4WTtHw0moXeHdnCzq6S8bNZbUkzSCdZbxQVTc";
                string timestamp = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000).ToString();
                string suiteTicket = "TestSuiteTicket";
                string signature1 = timestamp + "\n" + suiteTicket;
                string signature2 = HmacSHA256(signature1, suiteSecret);
                string signature = System.Web.HttpUtility.UrlEncode(signature2, System.Text.Encoding.UTF8);
                string auth_corpid = strlist;
                string url = "https://oapi.dingtalk.com/service/get_corp_token?signature=" + signature + "&timestamp=" + timestamp + "&suiteTicket=" + suiteTicket + "&accessKey=" + suiteKey;
                string param = "{ \"auth_corpid\": \"ding1989d454776d7e2cf2c783f7214b6d69\"}";
                string url1 = "https://oapi.dingtalk.com/service/get_auth_info?signature=" + signature + "&timestamp=" + timestamp + "&suiteTicket=" + suiteTicket + "&accessKey=" + suiteKey;
                string param1 = "{ \"auth_corpid\": \"ding1989d454776d7e2cf2c783f7214b6d69\"}";

                try
                {
                    var response11 = Haikan3.Utils.DingDingHelper.HttpPost(url, param);
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PersistentCodeResult>(response11);
                    if (result != null && result.errcode == "0")
                    {
                        string url11 = "https://oapi.dingtalk.com/user/getuserinfo?access_token=" + result.access_token + "&code=" + code;
                        var response12 = Haikan3.Utils.DingDingHelper.HttpGet(url11);
                        var result12 = Newtonsoft.Json.JsonConvert.DeserializeObject<PersistentCodeResult12>(response12);
                        if (result12 != null && result12.errcode == 0)
                        {
                            //获取人员信息
                            var results = Haikan3.Utils.DingDingHelper.GetUserDetail(result.access_token, result12.userid);

                            var userdata = _dbContext.SystemUser.Where(x => x.Dinguserid == results.userid).ToList().Count;
                            //没有当前用户则添加
                            if (userdata == 0)
                            {
                                var entity = new SystemUser();
                                entity.SystemUserUuid = Guid.NewGuid();
                                entity.RealName = results.name;
                                entity.Dinguserid = results.userid;
                                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                entity.IsDeleted = 0;
                                _dbContext.Add(entity);
                                _dbContext.SaveChanges();
                            }

                            //获取当前用户的id，uuid，姓名
                            var query = from s in _dbContext.SystemUser
                                        where s.IsDeleted == 0
                                        select new
                                        {
                                            useruuid = s.SystemUserUuid,
                                            name = s.RealName,
                                            userid = s.Id
                                        };

                            response.SetData(query.ToList());

                            return Ok(response);
                            //var response13 = Haikan3.Utils.DingDingHelper.HttpPost(url1, param1);
                            //var result13 = Newtonsoft.Json.JsonConvert.DeserializeObject<Getauthinfo>(response13);
                            //if (result13 != null && result13.errcode == 0)
                            //{
                            //    var agentid = result13.auth_info.agent[0].agentid;
                            //    var userid_list = result12.userid;
                            //    var dept_id_list = "";
                            //    var to_all_user = "false";
                            //    var msgs = "{\"msgtype\": \"action_card\",\"action_card\": { \"title\": \"是透出到会话列表和通知的\", \"markdown\": \"支持markdown格式的容\",\"single_title\": \"查看详情\",\"single_url\":\"https://open.dingtalk.com\"}}";
                            //    //var msg = Newtonsoft.Json.JsonConvert.DeserializeObject<RootMsg>(msgs);
                            //    string url2 = "https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2?access_token=" + result.access_token;
                            //    string param2 = "{ \"agent_id\": \"" + agentid + "\",\"userid_list\":\"" + userid_list + "\",\"to_all_user\":\"" + to_all_user + "\",\"msg\":" + msgs + "}";
                            //    var response14 = Haikan3.Utils.DingDingHelper.HttpPost(url2, param2);
                            //    response.SetData(response14);
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Log.LogMsg("DingDing_GetPersistentCode_Exception", DateTime.Now, ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            return Ok(response);
        }
        private string HmacSHA256(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        public class PersistentCodeResult
        {
            public string errcode { get; set; }
            public string access_token { get; set; }
            public string errmsg { get; set; }
            public int expires_in { get; set; }

        }
        public class PersistentCodeResult12
        {
            /// <summary>
            /// 
            /// </summary>
            public int errcode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int sys_level { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string is_sys { get; set; }
            /// <summary>
            /// 王志豪
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string errmsg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string deviceId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string userid { get; set; }

        }
        //如果好用，请收藏地址，帮忙分享。
        public class Auth_user_info
        {
            /// <summary>
            /// 
            /// </summary>
            public string userId { get; set; }
        }

        public class Auth_corp_info
        {
            /// <summary>
            /// 
            /// </summary>
            public int corp_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string corpid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int auth_level { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string auth_channel { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string industry { get; set; }
            /// <summary>
            /// 测试测试村上春树
            /// </summary>
            public string full_corp_name { get; set; }
            /// <summary>
            /// 测试测试村上春树
            /// </summary>
            public string corp_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string invite_url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string auth_channel_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string invite_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string is_authenticated { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string license_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string corp_logo_url { get; set; }
        }

        public class Channel_auth_info
        {
            /// <summary>
            /// 
            /// </summary>
            public List<string> channelAgent { get; set; }
        }

        public class AgentItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int agentid { get; set; }
            /// <summary>
            /// 多多控工资条
            /// </summary>
            public string agent_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string logo_url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int appid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> admin_list { get; set; }
        }

        public class Auth_info
        {
            /// <summary>
            /// 
            /// </summary>
            public List<AgentItem> agent { get; set; }
        }

        public class Auth_market_info
        {
        }

        public class Getauthinfo
        {
            /// <summary>
            /// 
            /// </summary>
            public int errcode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Auth_user_info auth_user_info { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Auth_corp_info auth_corp_info { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string errmsg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Channel_auth_info channel_auth_info { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Auth_info auth_info { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Auth_market_info auth_market_info { get; set; }
        }
        //如果好用，请收藏地址，帮忙分享。
        public class Action_card
        {
            /// <summary>
            /// 是透出到会话列表和通知的文案
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 支持markdown格式的正文内容
            /// </summary>
            public string markdown { get; set; }
            /// <summary>
            /// 查看详情
            /// </summary>
            public string single_title { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string single_url { get; set; }
        }

        public class RootMsg
        {
            /// <summary>
            /// 
            /// </summary>
            public string msgtype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Action_card action_card { get; set; }
        }
        #region 私有方法

        /// <summary>
        /// 删除-真删
        /// </summary>
        /// <param name="ids">ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel DeleteEwq(string ids)
        {
            using (_dbContext)
            {
                var sql = string.Format("DELETE FROM Priority WHERE PriorityUUID IN ('{0}')", ids);
                _dbContext.Database.ExecuteSqlRaw(sql);
                ToLog.AddLog("删除", "成功:删除:重点工作信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }

        /// <summary>
        /// 删除重点工作-标记删除
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">角色ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE Priority SET Recycle=@IsDeleted WHERE PriorityUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:重点工作信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }

        /// <summary>
        /// 完成重点工作
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">角色ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsAccomplish(CommonEnum.YnAccomplish isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE Priority SET Accomplish=@IsDeleted WHERE PriorityUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("完成", "成功:完成:重点工作信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }

        #endregion

    }
}