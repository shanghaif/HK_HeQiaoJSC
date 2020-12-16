using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.Task;
using HaikanSmartTownCockpit.Api.ViewModels.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;
using System.Security.Cryptography;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using HaikanSmartTownCockpit.Api.logs.TolLog;
using System.Data;
using HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task.app;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task
{
    [Route("api/v1/task/[controller]/[action]")]
    [ApiController]
    public class taskController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private readonly MdDesEncrypt MdDesEncrypt;

        //用来获取路径相关
        private IHostingEnvironment _hostingEnvironment;

        public taskController(haikanHeQiaoContext dbContext, IMapper mapper, IOptions<MdDesEncrypt> mdDesEncrypt)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            MdDesEncrypt = mdDesEncrypt.Value;
        }
        /// <summary>
        /// 创建科室工作总结-新科室工作总结表
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult createkeshis(taskCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {

                var entity = new DepartSumary();
                entity.DeSumaryUuid = Guid.NewGuid();
                entity.MissionId = Guid.Parse(model.AdministrativeOffice);//所属科室
                entity.DeSuHeadLine = model.MissionHeadline;//任务标题标题
                entity.SyUserUuid = Guid.Parse(model.establisthtruename);//创建人uuid
                entity.DeSuAddTime = model.StartTime;//开始时间
                //entity.FinishTime = model.FinishTime;//结束时间
                entity.DeSuDescribe = model.MissionDescribe;//任务描述
                entity.DeSuDocument = model.accessory;//附件
                //entity.Manhour = model.Manhour;//计划工时
                //entity.Approver = model.Approver;//审批人
                //entity.Participant = "," + model.Participant;//参与人
                _dbContext.DepartSumary.Add(entity);//添加
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
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
                var deName = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == deluuuid);//当前登录人名字
                //var deptuuid = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == deluuuid).DepartmentUuid;//当前登陆人所属科室`
                var query = from m in _dbContext.Mission
                            where m.Recycle == "0"
                            where m.Preserve == "1"//没有在回收站且没有在草稿箱中
                            select new
                            {
                                principal=m.Principal,//负责人
                                participant=m.Participant,//参与人
                                approver=m.Approver,//审批人
                                missionUuid = m.MissionUuid,
                                isyuqi = panduanyuqi(m.FinishTime, m.MissionUuid),//判断是否逾期
                                missionHeadline = m.MissionHeadline,//任务标题
                                accomplish = m.Accomplish,
                                isouttime = m.Isouttime,//是否逾期
                                startTime = m.StartTime,//开始时间
                                finishTime = m.FinishTime,//结束时间
                                missionDescribe = m.MissionDescribe,//任务描述
                                priority = m.Priority,//优先级
                                administrativeOffice = getdept(m.AdministrativeOffice),//下派部门(村庄,科室)
                                administrativeOfficeid= m.AdministrativeOffice,
                                keshiuuid = m.AdministrativeOffice,
                                auditStatus = m.AuditStatus,//审核状态
                                establishTime = m.EstablishTime,
                                auditOpinion = m.AuditOpinion,//审核意见
                                missiontype = m.Missiontype,
                                id = m.Id,//id
                            };

                //if (RoleName.Contains("科室负责人"))//科室负责人查看分配给自己的
                //{
                //    query = query.Where(x => x.keshiuuid == deptuuid.ToString());
                //}

                //上级交办
                if (payload.misstype == "shang")
                {
                    query = query.Where(x => x.missiontype == "0");
                }

                //下级上传
                if (payload.misstype == "xia")
                {
                    query = query.Where(x => x.missiontype == "1");
                }

                //今日到期
                if (payload.canshu == "jrdq")//今日到期
                {

                    var nowdate = DateTime.Now.ToString("yyyy-MM-dd");
                    query = query.Where(x => x.finishTime.Contains(nowdate));//今日到期
                }
                //负责人
                if (!string.IsNullOrEmpty(payload.kwfzr))
                {
                    if (payload.kwfzr == "与我相关")
                    {
                        query = query.Where(x => x.participant.Contains(deName.RealName) || x.approver.Contains(deName.RealName) || x.participant.Contains(deName.RealName));
                    }
                }
                if (!string.IsNullOrEmpty(payload.kwbt))
                {
                    query = query.Where(x => x.missionHeadline.Contains(payload.kwbt));//任务标题
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

                if (!string.IsNullOrEmpty(payload.zt))
                {
                    query = query.Where(x => x.auditStatus.Contains(payload.zt));//状态
                }
                if (!string.IsNullOrEmpty(payload.yxj))
                {
                    query = query.Where(x => x.priority.Contains(payload.yxj));//优先级
                }

                //进行中
                var nowdatestr = DateTime.Now.ToString("yyyy-MM-dd");
                if (payload.canshu == "jxz")
                {
                    query = query.Where(x => x.auditStatus == "0");
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
                    string[] arr = uuid.Split(',');
                    foreach (var item in arr)
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
        /// 创建任务
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



                var entity = new Mission();
                entity.MissionUuid = Guid.NewGuid();
                //var time = DateTime.Now;
                //var nowdate = time.ToString("yyyy-MM-dd HH:mm:ss");
                //var endtime = time.AddDays(+3).ToString("yyyy-MM-dd HH:mm:ss");//紧急为3天
                //if (model.Priority == "普通")
                //{
                //    endtime = time.AddDays(+5).ToString("yyyy-MM-dd HH:mm:ss");//普通为5天
                //}

                var nowdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var ktime = model.StartTime;//开始时间
                var datetimes = Convert.ToDateTime(ktime);
                var endtime = datetimes.AddDays(+3).ToString("yyyy-MM-dd HH:mm:ss");//紧急为3天
                if (model.Priority == "普通")
                {
                    endtime = datetimes.AddDays(+5).ToString("yyyy-MM-dd HH:mm:ss");//普通为5天
                }
                foreach (var item in model.AdministrativeOffice2)
                {
                    entity.AdministrativeOffice += item + ",";
                }
                //entity.AdministrativeOffice = model.AdministrativeOffice;//下派的科室uuid
                entity.MissionHeadline = model.MissionHeadline;//任务标题标题
                entity.StartTime = ktime;//开始时间(下派时间)
                entity.FinishTime = endtime;//结束时间
                entity.MissionDescribe = model.MissionDescribe;//任务问题描述
                entity.Priority = model.Priority;//优先级
                entity.EstablishTime = nowdate;//创建时间
                entity.EstablishName = model.establisthtruename;//创建人uuid
                entity.Recycle = "0";//未删除
                entity.Missiontype = model.missiontype;//任务类型
                if (model.AdministrativeOffice == "")
                {
                    entity.AuditStatus = "";//审核状态待下派
                }
                else
                {
                    entity.AuditStatus = "0";//审核状态办理中               
                }

                entity.Accomplish = "0";//完成状态为未完成
                entity.Sortord = "";//任务完成时间为空
                entity.Preserve = "1";//保存状态为未保存到草稿箱
                entity.Isouttime = "0";//是否逾期
                _dbContext.Mission.Add(entity);//添加
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:任务信息一条数据", _dbContext);
                }
                var xiaoxi = notes(entity.MissionUuid.ToString());//发送提醒消息
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }



        /// <summary>
        /// 负责人，参与人推送信息
        /// </summary>
        [HttpGet("{uuid}")]
        public IActionResult notes(string uuid)
        {

            var response = ResponseModelFactory.CreateInstance;
            var xiaoxidata = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid == Guid.Parse(uuid));

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
                entity.Naem = "任务提醒";
                entity.Content = tuisongrenname + "添加了任务：" + xiaoxidata.MissionHeadline + ",任务到期时间:" + xiaoxidata.FinishTime;
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



        /// <summary>
        /// 创建科室工作总结
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult createkeshi(taskCreateViewModel model)
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
                entity.AdministrativeOffice = model.AdministrativeOffice;//所属科室
                entity.MissionHeadline = model.MissionHeadline;//任务标题标题
                entity.Principal = model.Principal;//负责人
                entity.StartTime = model.StartTime;//开始时间
                entity.FinishTime = model.FinishTime;//结束时间
                entity.MissionDescribe = model.MissionDescribe;//任务描述
                //entity.Priority = model.Priority;//优先级
                //entity.Manhour = model.Manhour;//计划工时
                entity.Approver = model.Approver;//审批人
                entity.Participant = "," + model.Participant;//参与人
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//创建时间
                entity.EstablishName = model.establisthtruename;//创建人uuid
                entity.Recycle = "0";//未删除
                entity.AuditStatus = "0";//审核状态为待审核
                entity.Accomplish = "0";//完成状态为未完成
                entity.Sortord = "";//任务完成时间为空
                entity.Preserve = "1";//保存状态为未保存到草稿箱
                _dbContext.Mission.Add(entity);//添加
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }
        /// <summary>
        /// 同步部门和人员信息(浙政釘/專有釘釘)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Getalluseranddeps()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                //获取通讯录权限范围(根组织)
                var userinfo = ZWDingHelp.GetscopesV2();
                if (userinfo.success == true)
                {
                    if (userinfo.content != null)
                    {
                        //获取所有的组织部门
                        var dempartlist = GetDepartList(userinfo.content.deptVisibleScopes[0]);
                        if (dempartlist != "")
                        {
                            var dempartlists = dempartlist.TrimEnd(',').Split(',');
                            if (dempartlists.Length > 0)
                            {
                                for (int i = 0; i < dempartlists.Length; i++)
                                {
                                    string zzcode = dempartlists[i].ToString();
                                    //获取组织详细信息
                                    var userinfo1 = ZWDingHelp.GetgetOrganizationByCode(zzcode);
                                    if (userinfo1.success == true && userinfo1.content.data != null)
                                    {
                                        //部门/组织名称
                                        var dingid = userinfo1.content.data.organizationCode;
                                        var DempartName = userinfo1.content.data.organizationName;
                                        var dssdepid = _dbContext.Department.Count(x => x.Dingid == dingid);
                                        //数据库中没有查到此部门--将数据添加到数据库中
                                        if (dssdepid == 0)
                                        {
                                            var entity = new Department();
                                            entity.Name = DempartName;//部门名称
                                            entity.Dingid = dingid;//部门钉钉id
                                            entity.IsDeleted = 0;//未删除
                                            entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//添加时间
                                            entity.EstablishName = "浙政釘同步";//添加人
                                            entity.Remark = ""; //备注
                                            entity.DepartmentUuid = Guid.NewGuid();//部门guid

                                            _dbContext.Department.Add(entity);//添加
                                            _dbContext.SaveChanges();
                                        }
                                        else//此部门已存在---更新部门数据
                                        {
                                            var entity = _dbContext.Department.FirstOrDefault(x => x.Dingid == dingid);
                                            entity.Name = DempartName;//更新部门名称
                                            _dbContext.SaveChanges();
                                        }

                                        //获取部门uuid
                                        var depuuid = _dbContext.Department.FirstOrDefault(x => x.Dingid == dingid).DepartmentUuid;

                                        //查询组织下人员详情，包含个人信息和任职信息
                                        var userinfo2 = ZWDingHelp.GetpageOrganizationEmployeePositions(zzcode);
                                        if (userinfo2.success == true && userinfo2.content.data != null)
                                        {
                                            var userdata = userinfo2.content.data;
                                            if (userdata.Count > 0)
                                            {
                                                for (int i1 = 0; i1 < userdata.Count; i1++)
                                                {
                                                    //姓名
                                                    string employeeName = userdata[i1].employeeName;
                                                    //編號
                                                    string employeeCode = userdata[i1].employeeCode;
                                                    var dssuserid = _dbContext.SystemUser.Count(x => x.Dinguserid == employeeCode);
                                                    //数据库中没有该人员信息--添加到数据库中
                                                    if (dssuserid == 0)
                                                    {
                                                        var entity = new SystemUser();
                                                        entity.SystemUserUuid = Guid.NewGuid();
                                                        entity.RealName = employeeName;
                                                        entity.Dinguserid = employeeCode;
                                                        entity.DepartmentUuid = depuuid;//部门uuid
                                                        entity.SystemRoleUuid = _dbContext.SystemRole.FirstOrDefault(x => x.RoleName == "普通用户").SystemRoleUuid.ToString();//普通用户
                                                        entity.LoginName = employeeName;//登录名
                                                        entity.UserType = 0;//用户类型
                                                        entity.PassWord = Haikan3.Utils.DesEncrypt.Encrypt("123456", MdDesEncrypt.SecretKey);
                                                        entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                        entity.IsDeleted = 0;
                                                        _dbContext.SystemUser.Add(entity);
                                                        _dbContext.SaveChanges();


                                                        _dbContext.Database.ExecuteSqlCommand("DELETE FROM SystemUserRoleMapping WHERE SystemUserUUID={0}", entity.SystemUserUuid);
                                                        var roles = new SystemUserRoleMapping();
                                                        roles.SystemUserUuid = entity.SystemUserUuid;
                                                        roles.SystemRoleUuid = Guid.Parse(entity.SystemRoleUuid.ToString());
                                                        roles.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                                                        roles.AddPeople = "浙政钉同步";
                                                        var success = true;
                                                        _dbContext.SystemUserRoleMapping.AddRange(roles);
                                                        success = _dbContext.SaveChanges() > 0;
                                                        if (success)
                                                        {
                                                            response.SetSuccess();
                                                        }
                                                        else
                                                        {
                                                            _dbContext.Database.ExecuteSqlCommand("DELETE FROM SystemUser WHERE SystemUserUUID={0}", entity.SystemUserUuid);
                                                            response.SetFailed("保存用户角色数据失败");
                                                        }
                                                    }
                                                    else
                                                    { //数据库中存在该人员--修改信息
                                                        var entity = _dbContext.SystemUser.FirstOrDefault(x => x.Dinguserid == employeeCode);
                                                        entity.RealName = employeeName;
                                                        entity.DepartmentUuid = depuuid;//部门uuid
                                                        _dbContext.SaveChanges();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// 编辑科室工作总结
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Editkeshi(int id)
        {
            using (_dbContext)
            {
                var syuser = _dbContext.SystemUser.Where(x => x.IsDeleted == 0).ToList();


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
                                 administrativeOffice = m.AdministrativeOffice,//科室uuid
                                 priorityname = _dbContext.Department.FirstOrDefault(x => x.DepartmentUuid == Guid.Parse(m.AdministrativeOffice)).Name,//科室
                                 missionHeadline = m.MissionHeadline,//任务标题
                                 principal = m.Principal,//负责人uuid
                                 //principalname = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(m.Principal)).RealName,//负责人
                                 selectfuzename = fuzename.Trim(','),//负责人
                                 startTime = m.StartTime,//开始时间
                                 finishTime = m.FinishTime,//结束时间
                                 missionDescribe = m.MissionDescribe,//任务描述
                                 //priority = m.Priority,//优先级
                                 //manhour = Convert.ToInt32(m.Manhour),//计划工时
                                 //manhourt = m.Manhour + "天",//计划工时
                                 approver = m.Approver == "" ? "" : m.Approver == null ? "" : m.Approver,//审批人uuid
                                 //approvername = us.RealName,//审批人
                                 //approvername = m.Approver == "" ? "" : _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(m.Approver)).RealName,//审批人
                                 selectshenpiname = shenpiname.Trim(','),//审批人
                                 auditStatusNum = m.AuditStatus,//审核状态(数字)
                                 auditStatus = getzhuangtai(m.AuditStatus),//审核状态
                                 selectcanyurenname = selectusername.Trim(','),//参与人
                                 participant = m.Participant,//参与人id
                                 establishName = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(m.EstablishName)).RealName,//创建人姓名
                                 establisthtruename = m.EstablishName,//创建人uuid
                                 remark = m.Remark,//备注
                                 establishTime = m.EstablishTime,//创建时间
                                 auditOpinion = m.AuditOpinion,//审核意见
                                 
                             }).FirstOrDefault();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑任务
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            using (_dbContext)
            {

                ////获取科室
                //var keshiuuid = _dbContext.Mission.FirstOrDefault(x => x.Id == id).AdministrativeOffice;
                //var keshiname = "";
                //if (keshiuuid != "" && keshiuuid != null)
                //{
                //    var keshilist = keshiuuid.Split(',');
                //    for (int i = 0; i < keshilist.Length; i++)
                //    {
                //        if (keshilist[i] != "")
                //        {
                //            keshiname += _dbContext.Department.FirstOrDefault(x => x.DepartmentUuid == Guid.Parse(keshilist[i])).Name + ",";
                //        }
                //    }
                //}


                var keshiuuid = _dbContext.Mission.FirstOrDefault(x => x.Id == id).AdministrativeOffice;
                var keshiname = "";
                if (keshiuuid != "" && keshiuuid != null)
                {
                    string[] arr = keshiuuid.Split(',');
                    foreach (var item in arr)
                    {
                        if (item != "")
                        {
                            var dep = _dbContext.Department.Where(x => x.DepartmentUuid == Guid.Parse(item)).ToList();
                            var town = _dbContext.Town.Where(x => x.TownUuid == Guid.Parse(item)).ToList();

                            if (dep.Count > 0)
                            {
                                keshiname += dep[0].Name + ",";
                            }

                            if (town.Count > 0)
                            {
                                keshiname += town[0].TownName + ",";
                            }
                        }
                    }
                }
                var query = (from m in _dbContext.Mission
                             where m.Id == id
                             select new
                             {
                                 missionUUID = m.MissionUuid,
                                 missionHeadline = m.MissionHeadline,//任务标题
                                 startTime = m.StartTime,//开始时间
                                 finishTime = m.FinishTime,//结束时间(逾期时间)
                                 missionDescribe = m.MissionDescribe,//任务描述
                                 priority = m.Priority,//优先级
                                 keshiname = keshiname,//科室
                                 administrativeOffice = m.AdministrativeOffice,//科室uuid
                                 auditStatusNum = m.AuditStatus,//审核状态(数字)
                                 auditStatus = getzhuangtai(m.AuditStatus),//审核状态
                                 //establishName = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(m.EstablishName)).RealName,//创建人姓名
                                 //establisthtruename = m.EstablishName,//创建人uuid
                                 remark = m.Remark,//备注
                                 establishTime = m.EstablishTime,//创建时间
                                 auditOpinion = m.AuditOpinion == null ? "" : m.AuditOpinion,//审核意见
                                 fujian = m.Fujian,//附件
                                 m.Participant
                             }).FirstOrDefault();

                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query);
                return Ok(response);
            }
        }


        /// <summary>
        /// 获取审核状态
        /// </summary>
        /// <param name="zt"></param>
        /// <returns></returns>
        public static string getzhuangtai(string zt)
        {
            string ztname = "待下派";
            if (zt == "0")
            {
                ztname = "办理中";
            }
            if (zt == "1")
            {
                ztname = "审核中";
            }
            if (zt == "2")
            {
                ztname = "已完成";
            }
            if (zt == "3")
            {
                ztname = "已逾期";
            }

            return ztname;
        }

        /// <summary>
        /// 保存编辑的任务
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
                //var time = DateTime.Now;
                //var nowdate = time.ToString("yyyy-MM-dd HH:mm:ss");
                //var endtime = time.AddDays(+3).ToString("yyyy-MM-dd HH:mm:ss");//紧急为3天
                //if (model.Priority == "普通")
                //{
                //    endtime = time.AddDays(+5).ToString("yyyy-MM-dd HH:mm:ss");//普通为5天
                //}

                var ktime = model.StartTime;//开始时间
                var datetimes = Convert.ToDateTime(ktime);
                var endtime = datetimes.AddDays(+3).ToString("yyyy-MM-dd HH:mm:ss");//紧急为3天
                if (model.Priority == "普通")
                {
                    endtime = datetimes.AddDays(+5).ToString("yyyy-MM-dd HH:mm:ss");//普通为5天
                }
                var entity = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid == model.MissionUUID);
                entity.AdministrativeOffice= "";
                foreach (var item in model.keshilist)
                {
                    entity.AdministrativeOffice += item+",";//下派的科室uuid
                }
                entity.MissionHeadline = model.MissionHeadline;//任务标题标题
                entity.StartTime = ktime;//开始时间
                entity.FinishTime = endtime;//结束时间
                entity.MissionDescribe = model.MissionDescribe;//问题描述
                entity.Priority = model.Priority;//优先级
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:任务信息一条数据", _dbContext);
                }
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }


        /// <summary>
        /// 保存编辑的任务(科室工作总结)
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult baocunEditkeshi(taskCreateViewModel model)
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

                entity.AdministrativeOffice = model.AdministrativeOffice;//所属科室
                entity.MissionHeadline = model.MissionHeadline;//任务标题标题
                entity.Principal = model.Principal;//负责人
                entity.StartTime = model.StartTime;//开始时间
                entity.FinishTime = model.FinishTime;//结束时间
                entity.MissionDescribe = model.MissionDescribe;//任务描述
                entity.Priority = model.Priority;//优先级
                entity.Manhour = model.Manhour;//计划工时
                entity.Participant = "," + model.Participant;//参与人
                entity.Approver = model.Approver;//审批人
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }
        /// <summary>
        /// 保存编辑的任务(新科室工作总结表)
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult baocunEditkeshis(taskCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.DepartSumary.FirstOrDefault(x => x.DeSumaryUuid == model.MissionUUID);

                entity.MissionId = Guid.Parse(model.AdministrativeOffice);//所属科室
                entity.DeSuHeadLine = model.MissionHeadline;//任务标题标题
                                                            // entity.SyUserUuid = Guid.Parse(model.establisthtruename);//创建人UUid
                entity.DeSuAddTime = model.StartTime;//开始时间
                //entity.FinishTime = model.FinishTime;//结束时间
                entity.DeSuDescribe = model.MissionDescribe;//任务描述
                //entity.Priority = model.Priority;//优先级
                //entity.Manhour = model.Manhour;//计划工时
                //entity.Participant = "," + model.Participant;//参与人
                //entity.Approver = model.Approver;//审批人
                entity.DeSuDocument = model.accessory;//附件
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }

        /// <summary>
        /// 详情、编辑科室工作总结--新建的表
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Editkeshis(int id)
        {
            using (_dbContext)
            {
                var query = (from m in _dbContext.DepartSumary
                             where m.DeSumaryId == id
                             select new
                             {
                                 missionUUID = m.DeSumaryUuid,
                                 administrativeOffice = m.MissionId,//科室uuid
                                 priorityname = _dbContext.Department.FirstOrDefault(x => x.DepartmentUuid == m.MissionId).Name,//科室
                                 missionHeadline = m.DeSuHeadLine,//标题
                                 establishName = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == m.SyUserUuid).RealName,//创建人
                                 establishTime = m.DeSuAddTime,//开始时间
                                 startTime = m.DeSuAddTime,//开始时间
                                 //finishTime = m.FinishTime,//结束时间
                                 missionDescribe = m.DeSuDescribe,//任务描述
                                 accessory = m.DeSuDocument,
                                 //priority = m.Priority,//优先级
                                 //manhour = Convert.ToInt32(m.Manhour),//计划工时
                                 //auditOpinion = m.AuditOpinion,//审核意见
                             }).FirstOrDefault();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id,多个以逗号分隔</param>
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
            response = Deletemiss(ids);
            return Ok(response);
        }
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete2(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            response = DeleteEwqs(ids);
            return Ok(response);
        }
        /// <summary>
        /// 删除--科室工作日志
        /// </summary>
        /// <param name="ids">ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel DeleteEwqs(string ids)
        {
            using (_dbContext)
            {
                _dbContext.Database.ExecuteSqlCommand("DELETE DepartSumary WHERE DeSumaryID in(" + ids + ")");
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name = "ids" > ID字符串, 多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel Deletemiss(string ids)
        {
            using (_dbContext)
            {
                _dbContext.Database.ExecuteSqlCommand("update Mission set Recycle='1' where ID in(" + ids + ")");//删除状态为删除
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }


        /// <summary>
        /// 重点工作下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult personaldiaryList()
        {
            using (_dbContext)
            {

                var query = from p in _dbContext.Priority
                            where p.Recycle == "0"
                            select new
                            {
                                priorityUUID = p.PriorityUuid,
                                id = p.Id,
                                priorityHeadline = gettitle(p.PriorityHeadline),
                                priorityHeadlinelong = p.PriorityHeadline,
                                recycle = p.Recycle
                            };
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query.ToList());
                return Ok(response);
            }
        }


        public string gettitle(string longtitle)
        {
            if (longtitle.Length > 20)
            {
                longtitle = longtitle.Substring(0, 20) + "...";
            }
            return longtitle;
        }

        /// <summary>
        /// 科室下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult keshidata()
        {
            using (_dbContext)
            {

                var query = from d in _dbContext.Department
                            //where d.IsDeleted == 0
                            orderby d.Id ascending
                            select new
                            {
                                priorityUUID = d.DepartmentUuid,
                                id = d.Id,
                                priorityHeadline = d.Name,
                                value = d.DepartmentUuid,
                                label = d.Name,
                            };
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query.ToList());
                return Ok(response);
            }
        }

        /// <summary>
        /// 科室村庄下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult keshidata2()
        {
            using (_dbContext)
            {

                var query = (from d in _dbContext.Department
                             where d.IsDeleted == 0
                             orderby d.Id ascending
                             select new
                             {
                                 value = d.DepartmentUuid,
                                 label = d.Name,
                             }).ToList();
                var query2 = (from d in _dbContext.Town
                              where d.IsDeleted == 0
                              orderby d.Id ascending
                              select new
                              {
                                  value = d.TownUuid,
                                  label = d.TownName,
                              }).ToList();


                //科室和村庄
                List<dynamic> data = new List<dynamic>();
                for (int i = 0; i < query.Count; i++)
                {
                    data.Add(new { query[i].label, query[i].value });
                }

                for (int i = 0; i < query2.Count; i++)
                {
                    data.Add(new { query2[i].label, query2[i].value });
                }

                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(data);
                return Ok(response);
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
        /// 用户穿梭框部分审批人
        /// </summary>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        public IActionResult systemuserlistuuid(string uuid)
        {
            using (_dbContext)
            {
                var detuuid = _dbContext.Department.FirstOrDefault(x => x.DepartmentUuid == Guid.Parse(uuid)).DepartmentUuid;


                var query = from s in _dbContext.SystemUser
                            where s.IsDeleted == 0 && s.DepartmentUuid == detuuid
                            orderby s.RealName ascending
                            select new
                            {
                                Id = s.Id,
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
        /// 用户穿梭框部分参与人
        /// </summary>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        public IActionResult systemuserlistid(string uuid)
        {
            using (_dbContext)
            {
                var detuuid = _dbContext.Department.FirstOrDefault(x => x.DepartmentUuid == Guid.Parse(uuid)).DepartmentUuid;


                var query = from s in _dbContext.SystemUser
                            where s.IsDeleted == 0 && s.DepartmentUuid == detuuid
                            orderby s.RealName ascending
                            select new
                            {
                                key = s.Id,
                                uuid = s.SystemUserUuid,
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
        /// 用户穿梭框负责人
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult allsystemuser2()
        {
            using (_dbContext)
            {


                var query = from s in _dbContext.SystemUser
                            where s.IsDeleted == 0
                            orderby s.RealName ascending
                            select new
                            {
                                Id = s.Id,
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
        /// 已选择用户审批人
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult selectsystemuser3(int id)
        {
            using (_dbContext)
            {
                var participantids = _dbContext.Mission.Where(x => x.Id == id).ToList()[0].Approver;//审批人uuid

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
        /// 已选择用户负责人
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult selectsystemuser2(string id)
        {
            using (_dbContext)
            {
                var participantids = _dbContext.Mission.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefault().Principal;//负责人uuid

                List<studentyibangmodel2> stuyibangmodel = new List<studentyibangmodel2>();
                if (participantids != "" && participantids != null)
                {
                    var patcount = participantids.Trim(',').Split(',');

                    foreach (var item in patcount)
                    {
                        var chaxun = _dbContext.VillageMember.FirstOrDefault(x => x.VillageUuid.ToString() == item);
                        if (chaxun != null)
                        {
                            studentyibangmodel2 stuyibang = new studentyibangmodel2();

                            stuyibang.key = item;
                            stuyibang.label = chaxun.MemberName;
                            stuyibang.disabled = false;
                            stuyibangmodel.Add(stuyibang);
                        }
                    }

                }
                else
                {
                    var patcount2 = from a in _dbContext.Mission
                                    where a.Id == Convert.ToInt32(id)
                                    select new
                                    {
                                        a.AdministrativeOffice
                                    };
                    var patcount = patcount2.FirstOrDefault().AdministrativeOffice.Trim(',').Split(',');
                    foreach (var item in patcount)
                    {
                        var chaxun = _dbContext.Department.FirstOrDefault(x => x.DepartmentUuid.ToString() == item);
                        if (chaxun != null)
                        {
                            studentyibangmodel2 stuyibang = new studentyibangmodel2();

                            stuyibang.key = item;
                            stuyibang.label = chaxun.Name;
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
        /// 用户穿梭框参与人
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
        /// 已选择用户参与人
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult selectsystemuser(int id)
        {
            using (_dbContext)
            {
                var participantids = _dbContext.Mission.Where(x => x.Id == id).ToList()[0].Participant;//参与人id

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
        /// 树形控件-绑定科室及人员
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult depadnuser(string id)
        {
            using (_dbContext)
            {
                
                string[] arr = id.Split(',');
                List<Department> querydep = new List<Department>();
                foreach (var item in arr)
                {
                    if (item!="")
                    {
                        Department data = _dbContext.Department.Where(x => x.DepartmentUuid == Guid.Parse(item)).FirstOrDefault();
                        querydep.Add(data);
                    }
                }
                //var returndata = "[{name:'临时组',uuid:'abc',id:1,children:[{name:'张三',uuid:'acd',id:4},{name:'李四',uuid:'act',id:5}]},{name:'临时组2',uuid:'acd',id:2,children:[{name:'王五',uuid:'ace',id:7}]}]";
                var returndata = "[";
                if (querydep.Count > 0)
                {
                    for (int i = 0; i < querydep.Count; i++)
                    {
                        returndata += "{name:'" + querydep[i].Name + "',id:'" + querydep[i].Id + "',children:" + getuserdata(querydep[i].DepartmentUuid.ToString()) + "},";
                    }
                }
                returndata = returndata.Trim(',') + "]";

                return Ok(JsonConvert.DeserializeObject(returndata));
            }
        }



        /// <summary>
        /// 通过科室uuid获取下级人员
        /// </summary>
        /// <param name="departuuid">科室uuid</param>
        /// <returns></returns>
        public string getuserdata(string departuuid)
        {
            //var userdata = _dbContext.SystemUser.Where(x => x.DepartmentUuid == Guid.Parse(departuuid)).ToList();
            var userdata = (from d in _dbContext.SystemUser
                            where d.IsDeleted == 0 && d.DepartmentUuid == Guid.Parse(departuuid)
                            orderby d.RealName ascending
                            select new
                            {
                                d.Id,
                                d.RealName,
                                d.SystemUserUuid
                            }).ToList();
            string returnuser = "[";
            if (userdata.Count > 0)
            {
                for (int i = 0; i < userdata.Count; i++)
                {
                    returnuser += "{name:'" + userdata[i].RealName + "',uuid:'" + userdata[i].SystemUserUuid + "',id:'" + userdata[i].Id + "'},";
                }
            }
            return returnuser.Trim(',') + "]";
        }







        /// <summary>
        /// 按条件查询日志
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        public IActionResult caozuorizhi(string uuid)//任务uuid
        {
            using (_dbContext)
            {
                var participantids = _dbContext.Mission.Where(x => x.MissionUuid == Guid.Parse(uuid)).ToList()[0].Participant;//参与人id

                var query = from m in _dbContext.MissionJournal
                            join u in _dbContext.SystemUser
                            on m.EstablishName equals u.SystemUserUuid.ToString()
                            where m.MissionUuid == Guid.Parse(uuid)
                            orderby m.Id ascending//排序
                            select new
                            {
                                content = m.Content,//内容
                                establishTime = m.EstablishTime,//创建时间
                                id = m.Id,//id
                                establishName = m.EstablishName,//创建人uuid
                                chuanjianname = u.RealName,//创建人姓名
                                Accessory = m.Accessory,//附件
                                read = m.Read,//已读人员id
                                //weiducount= selectweidu(uuid,m.MissionJournalUuid.ToString(),"weidu"),//未读人数
                            };
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query.ToList());
                return Ok(response);
            }
        }

        /// <summary>
        /// 附件类型
        /// </summary>
        /// <param name="Accessory"></param>
        /// <returns></returns>
        public bool Accessory(string Accessory)
        {
            string[] sArray = Accessory.Split(".");

            if (sArray[1].ToUpper() == "JPG" || sArray[1].ToUpper() == "GIF" || sArray[1].ToUpper() == "BMP" || sArray[1].ToUpper() == "JPEG" || sArray[1].ToUpper() == "PNG")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加任务操作日志
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult appcreaterizhi(taskcaozuoCreateViewModel model)
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
                entity.MissionUuid = Guid.Parse(model.MissionUUID.ToString());//所属任务
                entity.Content = model.Content;//日志内容
                entity.Accessory = model.Accessory;//附件
                entity.EstablishName = model.EstablishName;//创建人
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//创建时间
                entity.Read = "";//已读人员
                _dbContext.MissionJournal.Add(entity);//添加
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }

        /// <summary>
        /// 同步部门和人员信息(企业内部应用)
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public IActionResult Getalluseranddep()
        //{
        //    string access_token = Haikan3.Utils.DingDingHelper.GetAccessToken0().access_token;
        //    var response = ResponseModelFactory.CreateInstance;
        //    using (_dbContext)
        //    {

        //        var code = access_token;
        //        string suiteKey = "dingdhhaaylylww2i7fw";
        //        string suiteSecret = "sYgyZowkEoo4O9TQdqtaLdUcH2JyWfOsFmmRLkiGhDUC3-dvFp7j7mZ73Pd3KoOq";
        //        string timestamp = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000).ToString();
        //        string suiteTicket = "TestSuiteTicket";
        //        string signature1 = timestamp + "\n" + suiteTicket;
        //        string signature2 = HmacSHA256(signature1, suiteSecret);
        //        string signature = System.Web.HttpUtility.UrlEncode(signature2, System.Text.Encoding.UTF8);
        //        string auth_corpid = access_token;
        //        //string url = "https://oapi.dingtalk.com/service/get_corp_token?signature=" + signature + "&timestamp=" + timestamp + "&suiteTicket=" + suiteTicket + "&accessKey=" + suiteKey;
        //        //string param = "{ \"auth_corpid\": \"ding9f15a00f7c763360a1320dcb25e91351\"}";//ding1989d454776d7e2cf2c783f7214b6d69      

        //        //获取access_token
        //        string url = "https://oapi.dingtalk.com/gettoken?appkey=" + suiteKey + "&appsecret=" + suiteSecret;

        //        //var response11 = Haikan3.Utils.DingDingHelper.HttpPost(url, param);
        //        var response11 = Haikan3.Utils.DingDingHelper.HttpGet(url);
        //        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PersistentCodeResult>(response11);
        //        if (result != null && result.errcode == "0")
        //        {

        //            //获取部门列表
        //            string urldep = "https://oapi.dingtalk.com/department/list?access_token=" + result.access_token;
        //            var responseldep = Haikan3.Utils.DingDingHelper.HttpGet(urldep);
        //            var resultdep = Newtonsoft.Json.JsonConvert.DeserializeObject<departmentAlldata>(responseldep);
        //            //将获取的部门信息保存到数据库
        //            for (int i = 0; i < resultdep.department.Count; i++)
        //            {
        //                var depid = _dbContext.Department.Count(x => x.Dingid == resultdep.department[i].id);
        //                //数据库中没有查到此部门--将数据添加到数据库中
        //                if (depid == 0)
        //                {
        //                    var entity = new Department();
        //                    entity.Name = resultdep.department[i].name;//部门名称
        //                    entity.Dingid = resultdep.department[i].id;//部门钉钉id
        //                    entity.IsDeleted = 0;//未删除
        //                    entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//添加时间
        //                    entity.EstablishName = "钉钉同步";//添加人
        //                    entity.Remark = ""; //备注
        //                    entity.DepartmentUuid = Guid.NewGuid();//部门guid

        //                    _dbContext.Department.Add(entity);//添加
        //                    _dbContext.SaveChanges();
        //                }
        //                else//此部门已存在---更新部门数据
        //                {
        //                    var entity = _dbContext.Department.FirstOrDefault(x => x.Dingid == resultdep.department[i].id);
        //                    entity.Name = resultdep.department[i].name;//更新部门名称
        //                    _dbContext.SaveChanges();
        //                }

        //                //获取该部门的所有用户
        //                string urldepuser = "https://oapi.dingtalk.com/user/simplelist?access_token=" + result.access_token + "&department_id=" + resultdep.department[i].id;

        //                //获取部门uuid
        //                var depuuid = _dbContext.Department.FirstOrDefault(x => x.Dingid == resultdep.department[i].id).DepartmentUuid;

        //                var responsedepuser = Haikan3.Utils.DingDingHelper.HttpGet(urldepuser);
        //                var resdepuser = Newtonsoft.Json.JsonConvert.DeserializeObject<depauser>(responsedepuser);
        //                //将获取到的人员信息保存到数据库中
        //                for (int j = 0; j < resdepuser.userlist.Count; j++)
        //                {
        //                    var userid = _dbContext.SystemUser.Count(x => x.Dinguserid == resdepuser.userlist[j].userid);

        //                    //获取人员信息
        //                    var results = Haikan3.Utils.DingDingHelper.HttpGet("https://oapi.dingtalk.com/user/get?access_token=" + result.access_token + "&userid=" + resdepuser.userlist[j].userid);
        //                    var usersxinxi = Newtonsoft.Json.JsonConvert.DeserializeObject<usersdata>(results);



        //                    //数据库中没有该人员信息--添加到数据库中
        //                    if (userid == 0)
        //                    {
        //                        var entity = new SystemUser();
        //                        entity.SystemUserUuid = Guid.NewGuid();
        //                        entity.RealName = resdepuser.userlist[j].name;
        //                        entity.Dinguserid = resdepuser.userlist[j].userid;
        //                        entity.DepartmentUuid = depuuid;//部门uuid
        //                        entity.SystemRoleUuid = _dbContext.SystemRole.FirstOrDefault(x => x.RoleName == "普通用户").SystemRoleUuid.ToString();//普通用户
        //                        entity.LoginName = resdepuser.userlist[j].name;//登录名
        //                        entity.UserType = 0;//用户类型
        //                        entity.PassWord = Haikan3.Utils.DesEncrypt.Encrypt("123456", MdDesEncrypt.SecretKey);
        //                        entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //                        entity.IsDeleted = 0;
        //                        _dbContext.SystemUser.Add(entity);
        //                        _dbContext.SaveChanges();


        //                        _dbContext.Database.ExecuteSqlCommand("DELETE FROM SystemUserRoleMapping WHERE SystemUserUUID={0}", entity.SystemUserUuid);
        //                        var roles = new SystemUserRoleMapping();
        //                        roles.SystemUserUuid = entity.SystemUserUuid;
        //                        roles.SystemRoleUuid = Guid.Parse(entity.SystemRoleUuid.ToString());
        //                        roles.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
        //                        roles.AddPeople = AuthContextService.CurrentUser.DisplayName;
        //                        var success = true;
        //                        _dbContext.SystemUserRoleMapping.AddRange(roles);
        //                        success = _dbContext.SaveChanges() > 0;
        //                        if (success)
        //                        {
        //                            response.SetSuccess();
        //                        }
        //                        else
        //                        {
        //                            _dbContext.Database.ExecuteSqlCommand("DELETE FROM SystemUser WHERE SystemUserUUID={0}", entity.SystemUserUuid);
        //                            response.SetFailed("保存用户角色数据失败");
        //                        }



        //                    }
        //                    else
        //                    { //数据库中存在该人员--修改信息
        //                        var entity = _dbContext.SystemUser.FirstOrDefault(x => x.Dinguserid == resdepuser.userlist[j].userid);
        //                        entity.RealName = resdepuser.userlist[j].name;
        //                        entity.DepartmentUuid = depuuid;//部门uuid
        //                        _dbContext.SaveChanges();

        //                    }

        //                }

        //                ////获取子部门id列表
        //                //string urlzidep = "https://oapi.dingtalk.com/department/list_ids?access_token=" + result.access_token + "&id=" + resultdep.department[i].id;
        //                //    var responselzidep = Haikan3.Utils.DingDingHelper.HttpGet(urlzidep);

        //            }

        //        }


        //        return Ok(response);
        //    }
        //}


        /// <summary>
        /// 无线向下获取部门信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetDepartList(string code)
        {
            string str = "";
            var userinfo1 = ZWDingHelp.GetSubOrganizationCodes(code);
            if (userinfo1.Count > 0)
            {
                for (int i = 0; i < userinfo1.Count; i++)
                {
                    str += userinfo1[i] + ",";
                    str += GetDepartList(userinfo1[i]);
                }
            }
            return str;
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

        /// <summary>
        /// 部门信息
        /// </summary>
        public class departmentAlldata
        {
            public string errcode { get; set; }
            public string errmsg { get; set; }

            public List<department> department { get; set; }
        }

        public class department
        {
            public string name { get; set; }
            public int id { get; set; }
            public string parentid { get; set; }
            public bool createDeptGroup { get; set; }
            public bool autoAddUser { get; set; }
        }


        /// <summary>
        /// 人员信息
        /// </summary>
        public class depauser
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public bool hasMore { get; set; }
            public List<userlist> userlist { get; set; }
        }

        public class userlist
        {
            public string userid { get; set; }
            public string name { get; set; }

        }


        public class usersdata
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public string name { get; set; }
            public string unionid { get; set; }
            public string userid { get; set; }
            public string isLeaderInDepts { get; set; }
            public bool isBoss { get; set; }
            public string hiredDate { get; set; }
            public bool isSenior { get; set; }
            public int[] department { get; set; }
            public string orderInDepts { get; set; }
            public bool active { get; set; }
            public string avatar { get; set; }
            public bool isAdmin { get; set; }
            public bool isHide { get; set; }
            public string jobnumber { get; set; }
            public string position { get; set; }
            public List<roles> roles { get; set; }
        }


        public class roles
        {
            public string id { get; set; }
            public string name { get; set; }
            public string groupName { get; set; }
        }


        
        [HttpGet("{id}")]
        public IActionResult GetPepole(string id)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var data = (from a in _dbContext.VillageMember
                            select new
                            {
                                a.VillageUuid,
                                a.VillageMemberUuid,
                                a.IsDelete,
                                a.MemberSex,
                                a.AddPeople,
                            }).ToList();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }

        [HttpGet("{id}")]
        public IActionResult zhidingrenlist(string id)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var list = (from a in _dbContext.SystemUser
                            where a.DepartmentUuid == Guid.Parse(id)
                            select new
                            {
                                value = a.SystemUserUuid,
                                label = a.RealName,
                            }).ToList();
                response.SetData(list);
                return Ok(response);
            }
        }

        [HttpPost]
        public IActionResult zhidingren(taskCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                Mission data = _dbContext.Mission.Where(x => x.MissionUuid == model.MissionUUID).FirstOrDefault();
                for (int i = 0; i < model.Participant2.Count; i++)
                {
                    if (model.Participant2[i] != "")
                    {
                        if (i == 0)
                        {
                            data.Participant += "," + model.Participant2[i] + ",";
                        }
                        else
                        {
                            data.Participant += model.Participant2[i] + ",";
                        }
                        data.AuditStatus = "0";
                    }
                }
                //foreach (var item in model.Participant2)
                //{
                //    if (item != "")
                //    {
                //        data.Participant += item + ",";
                //        data.AuditStatus = "0";
                //    }
                //}
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }
    }
}