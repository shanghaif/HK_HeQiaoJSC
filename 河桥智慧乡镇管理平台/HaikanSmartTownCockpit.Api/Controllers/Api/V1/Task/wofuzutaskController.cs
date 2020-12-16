using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.RequestPayload.Task;
using HaikanSmartTownCockpit.Api.ViewModels.Task;
using Microsoft.AspNetCore.Mvc;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;
using Newtonsoft.Json;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task
{
    [Route("api/v1/task/[controller]/[action]")]
    [ApiController]
    public class wofuzutaskController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;

        public wofuzutaskController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取编辑权限
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns> 
        [HttpGet("{guid}")]
        public IActionResult EditPowers(string guid)
        {
            using (_dbContext)
            {
                var query = _dbContext.SystemUser.Where(x => x.SystemUserUuid == Guid.Parse(guid)).FirstOrDefault();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query);
                return Ok(response);
            }
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

                string deptuuid = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == deluuuid).DepartmentUuid.ToString();//当前登陆人所属科室


                var query = from m in _dbContext.Mission
                            where m.Recycle == "0" && (m.AuditStatus == "0" || m.AuditStatus == "" || m.AuditStatus == "1")//没有在回收站且没有在草稿箱中且未完成 状态为办理中
                            select new
                            {
                                missionUuid = m.MissionUuid,
                                isyuqi = panduanyuqi(m.FinishTime, m.MissionUuid),//判断是否逾期
                                missionHeadline = m.MissionHeadline,//任务标题
                                isouttime = m.Isouttime,//是否逾期
                                startTime = m.StartTime,//开始时间
                                finishTime = m.FinishTime,//结束时间
                                shenpishow = shenpishow(m.AuditStatus, RoleName),//判断是否能审批
                                wanchenshow = wanchengshow(deptuuid.ToString(), m.AdministrativeOffice, RoleName, m.AuditStatus),//判断是否能完成此任务
                                administrativeOffice = getdept(m.AdministrativeOffice),//下派部门(村庄,科室)
                                keshiuuid = m.AdministrativeOffice,
                                missionDescribe = m.MissionDescribe,//任务描述
                                priority = m.Priority,//优先级
                                auditStatus = m.AuditStatus,//审核状态
                                establishTime = m.EstablishTime,
                                auditOpinion = m.AuditOpinion,//审核意见
                                accomplish = m.Accomplish,
                                id = m.Id,//id
                            };


                if (RoleName.Contains("科室负责人"))//科室负责人查看分配给自己的
                {
                    query = query.Where(x => x.keshiuuid == deptuuid);
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
                ToLog.AddLog("查询", "成功:查询:任务信息数据", _dbContext);
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
        /// 查询数据(科室工作总结)-新建科室工作总结表
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult keshizongjies(alltaskrequestpayload payload)
        {
            using (_dbContext)
            {
                var query = from m in _dbContext.DepartSumary
                            join d in _dbContext.Department
                            on m.MissionId equals d.DepartmentUuid
                            orderby m.DeSuAddTime descending
                            select new
                            {
                                //missionUuid = m.MissionUuid,
                                //priorityUUID = m.AdministrativeOffice,//科室uuid
                                missionHeadline = m.DeSuHeadLine,//任务标题
                                priorityHeadline = m.DeSuDescribe,//内容
                                auditStatus = d.Name,//科室
                                //auditStatus = m.DepartName,//负责人uuid
                                //principal = getfuzeren(m.Principal),//负责人姓名
                                startTime = m.DeSuAddTime,//开始时间
                                //finishTime = m.FinishTime,//结束时间
                                missionDescribe = m.DeSuDescribe,//任务描述
                                MissionID = m.MissionId,//优先级
                                //manhour = m.Manhour,//计划工时                               
                                //participant = m.Participant,//参与人
                                //adduser = m.EstablishName,//添加人uuid
                                id = m.DeSumaryId//id
                            };


                if (!string.IsNullOrEmpty(payload.kwbt))
                {
                    query = query.Where(x => x.missionHeadline.Contains(payload.kwbt));//任务标题
                }
                if (!string.IsNullOrEmpty(payload.kwfzr))
                {
                    query = query.Where(x => x.auditStatus.Contains(payload.kwfzr));//科室选择
                }
                if (!string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => x.startTime.Contains(payload.kwendtime));//创建时间
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
        /// 查询数据(科室工作总结)
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult keshizongjie(alltaskrequestpayload payload)
        {
            using (_dbContext)
            {
                var query = from m in _dbContext.Mission
                            join d in _dbContext.Department
                            on m.AdministrativeOffice equals d.DepartmentUuid.ToString()
                            where m.Recycle == "0"
                            where m.Preserve == "1"//没有在回收站且没有在草稿箱中
                            where m.AdministrativeOffice != "" && m.AdministrativeOffice != null//科室工作总结
                            select new
                            {
                                missionUuid = m.MissionUuid,
                                priorityUUID = m.AdministrativeOffice,//科室uuid
                                priorityHeadline = d.Name,//科室
                                missionHeadline = m.MissionHeadline,//任务标题
                                principaluuid = m.Principal,//负责人uuid
                                principal = getfuzeren(m.Principal),//负责人姓名
                                startTime = m.StartTime,//开始时间
                                finishTime = m.FinishTime,//结束时间
                                missionDescribe = m.MissionDescribe,//任务描述
                                priority = m.Priority,//优先级
                                manhour = m.Manhour,//计划工时
                                approver = m.Approver == "" ? "" : m.Approver == null ? "" : m.Approver,//审批人uuid
                                approvername = getfuzeren(m.Approver),//审批人
                                auditStatus = m.AuditStatus,//审核状态
                                participant = m.Participant,//参与人
                                adduser = m.EstablishName,//添加人uuid
                                id = m.Id//id
                            };


                if (!string.IsNullOrEmpty(payload.kwbt))
                {
                    query = query.Where(x => x.missionHeadline.Contains(payload.kwbt));//任务标题
                }

                if (!string.IsNullOrEmpty(payload.kwfzr))
                {
                    query = query.Where(x => x.principaluuid.Contains(payload.kwfzr));//负责人
                }

                var data = payload.kwendtime;
                if (!string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => x.finishTime.Contains(payload.kwendtime));//结束时间
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
        /// 完成任务
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult wanchen(taskCreateViewModel model)
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


                entity.Remark = model.Remark;//备注
                entity.AuditStatus = "1";//状态为完成，待审核状态
                entity.Sortord = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//任务完成时间
                entity.Fujian = model.fujian;//附件
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);

            }
        }





        /// <summary>
        /// 添加汇报
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult createhuibao(missionJournal_model model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = new MissionJournal();
                entity.MissionJournalUuid = Guid.NewGuid();
                entity.MissionUuid = model.MissionUuid;//任务uuid
                entity.Completed = model.Completed;//已完成
                entity.Coordination = model.Coordination;//需要协调
                entity.IsDeleted = 0;
                entity.Accessory = model.Accessory;//附件
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//添加时间
                entity.EstablishName = AuthContextService.CurrentUser.Guid.ToString();//添加人姓名
                _dbContext.MissionJournal.Add(entity);//添加
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
                var rwinfo = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid.ToString().ToLower() == uuid);
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
                var rwinfo = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid.ToString().ToLower() == uuid);
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
                var query = from u in _dbContext.MissionJournal
                            where u.IsDeleted == 0
                            && u.MissionUuid == Guid.Parse(uuid)
                            orderby u.Id descending
                            select new
                            {
                                name = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid.ToString() == u.EstablishName).RealName,
                                completed = gettitle2(u.Completed),
                                coordination = gettitle2(u.Coordination),
                                accessory = gettitle2(u.Accessory),
                                username = u.EstablishName,
                                establishTime = u.EstablishTime,
                                missionjournaluuid = u.MissionJournalUuid,
                            };
                if (useruuid != null && useruuid != "")
                {
                    query = query.Where(x => x.username == useruuid);
                }
                response.SetData(query.ToList());
            }
            return Ok(response);

        }

        public static string gettitle2(string longtitle)
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
                var query = (from u in _dbContext.MissionJournal
                             where u.IsDeleted == 0
                             && u.MissionJournalUuid == Guid.Parse(uuid)
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
        /// 给参与人推送信息
        /// </summary>
        [HttpGet("{uuid}")]
        public IActionResult notes(string uuid)
        {

            var response = ResponseModelFactory.CreateInstance;
            var xiaoxidata = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid == Guid.Parse(uuid));

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
                entity.Naem = "任务完成提醒";
                entity.Content = tuisongrenname + "提醒您,近期需要完成任务" + xiaoxidata.MissionHeadline + ",任务到期时间:" + xiaoxidata.FinishTime + " 备注信息：";
                entity.ModuleUuid = dingdingid.Trim(',');//接收人钉钉id
                entity.ModuleNaem = xiaoxidata.MissionUuid.ToString();//任务uuid
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

        public class PersistentCodeResult
        {
            public string errcode { get; set; }
            public string access_token { get; set; }
            public string errmsg { get; set; }
            public int expires_in { get; set; }
        }






    }
}