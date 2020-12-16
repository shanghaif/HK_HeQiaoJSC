using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Configurations;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.ViewModels.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task.app
{
    [Route("api/v1/task/app/[controller]/[action]")]
    [ApiController]
    public class taskappController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private readonly MdDesEncrypt MdDesEncrypt;

        public taskappController(haikanHeQiaoContext dbContext, IMapper mapper, IOptions<MdDesEncrypt> mdDesEncrypt)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            MdDesEncrypt = mdDesEncrypt.Value;
        }


        /// <summary>
        /// 查询所有任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult getdatalist(string searchdata)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = (from m in _dbContext.Mission
                                 //join u in _dbContext.SystemUser
                                 //on m.Principal equals u.SystemUserUuid.ToString()
                             where m.Recycle == "0"
                             where m.Preserve == "1"//没有在回收站且没有在草稿箱中
                             where m.AdministrativeOffice == null || m.AdministrativeOffice == ""//不是科室工作总结
                             orderby m.Id descending
                             select new AllMissionViewModel
                             {
                                 missionUuid = m.MissionUuid,
                                 priorityUUID = m.PriorityUuid == null ? "" : m.PriorityUuid.ToString(),//所属重点工作uuid
                                 priorityHeadline = m.Approver == "" ? "" : m.PriorityUuid == null ? "" : _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid == m.PriorityUuid).PriorityHeadline,//所属重点工作
                                 missionHeadline = m.MissionHeadline,//任务标题
                                 principal = "",//u.RealName,//负责人姓名
                                 principaluuid = m.Principal,//负责人
                                 startTime = Convert.ToDateTime(m.StartTime).ToString("yyyy-MM-dd HH:mm"),//开始时间
                                 finishTime = Convert.ToDateTime(m.FinishTime).ToString("yyyy-MM-dd HH:mm"),//结束时间
                                 missionDescribe = m.MissionDescribe,//任务描述
                                 priority = m.Priority,//优先级
                                 manhour = m.Manhour,//计划工时

                                 approver = m.Approver,//审批人
                                 zhuangtai = getzhuangtai(m.AuditStatus),//状态
                                 auditStatus = m.AuditStatus,//审核状态
                                 participant = m.Participant,//参与人
                                 id = m.Id,//id
                             }).ToList();

                for (int i = 0; i < query.Count(); i++)
                {
                    var str = query[i].principaluuid.Split(',');
                    foreach (var item in str)
                    {
                        var user = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid.ToString() == item);
                        if (user != null)
                        {
                            query[i].principal += user.RealName;
                            break;
                        }
                    }
                }
                if (searchdata != "" && searchdata != null)//搜索查询
                {
                    var query1 = query.AsEnumerable().Where(x => x.missionHeadline.Contains(searchdata) || x.principal.Contains(searchdata) || x.zhuangtai.Contains(searchdata) || x.priority.Contains(searchdata));

                    response.SetData(query1.ToList());
                    return Ok(response);
                }
                response.SetData(query);
                return Ok(response);
            }
        }
        public class datacx
        {
            public string cxname { get; set; }
            public string uname { get; set; }
        }
        /// <summary>
        /// 免登(专有钉钉/浙政钉)
        /// </summary>
        /// <returns></returns>
        [HttpGet("{strlist}")]
        public IActionResult getzzduserinfo(string strlist)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var auth_code = strlist;
                //var auth_code = "b5a6a95d47f6453c8d31f76d0d43ac000a7d4501";
                var userinfo = ZWDingHelp.GetFreeLogin(auth_code);
                if (userinfo != null && userinfo.Success == true)
                {
                    if (userinfo.Content.Data != null)
                    {
                        //var userdata = _dbContext.SystemUser.Where(x => x.Dinguserid == userinfo.Content.Data.EmployeeCode).ToList().Count;
                        ////没有当前用户则添加
                        //if (userdata == 0)
                        //{
                        //    var entity = new SystemUser();
                        //    entity.SystemUserUuid = Guid.NewGuid();
                        //    entity.RealName = userinfo.Content.Data.LastName;
                        //    entity.Dinguserid = userinfo.Content.Data.EmployeeCode;
                        //    entity.DepartmentUuid = Guid.Parse("086B024D-85FF-4DD8-880C-3535B958EB30");//部门uuid
                        //    entity.SystemRoleUuid = _dbContext.SystemRole.FirstOrDefault(x => x.RoleName == "普通用户").SystemRoleUuid.ToString();//普通用户
                        //    entity.LoginName = userinfo.Content.Data.LastName;//登录名
                        //    entity.UserType = 0;//用户类型
                        //    entity.PassWord = Haikan3.Utils.DesEncrypt.Encrypt("123456", MdDesEncrypt.SecretKey);
                        //    entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //    entity.IsDeleted = 0;
                        //    _dbContext.SystemUser.Add(entity);
                        //    _dbContext.SaveChanges();

                        //    _dbContext.Database.ExecuteSqlCommand("DELETE FROM SystemUserRoleMapping WHERE SystemUserUUID={0}", entity.SystemUserUuid);
                        //    var roles = new SystemUserRoleMapping();
                        //    roles.SystemUserUuid = entity.SystemUserUuid;
                        //    roles.SystemRoleUuid = Guid.Parse(entity.SystemRoleUuid.ToString());
                        //    roles.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                        //    roles.AddPeople = "浙政钉免登";
                        //    _dbContext.SystemUserRoleMapping.AddRange(roles);
                        //    _dbContext.SaveChanges();
                        //}
                        //else
                        //{
                        //    //数据库中存在该人员--修改信息
                        //    var entity = _dbContext.SystemUser.FirstOrDefault(x => x.Dinguserid == userinfo.Content.Data.EmployeeCode);
                        //    entity.RealName = userinfo.Content.Data.LastName;
                        //    entity.DepartmentUuid = Guid.Parse("E453E86F-B465-42A4-B1DB-08D7DFA20326");//部门uuid
                        //    _dbContext.SaveChanges();
                        //}
                        //获取当前用户的id，uuid，姓名
                        var query = from s in _dbContext.SystemUser
                                    where s.IsDeleted == 0 && s.Dinguserid == userinfo.Content.Data.EmployeeCode
                                    select new
                                    {
                                        useruuid = s.SystemUserUuid,
                                        name = s.RealName,
                                        userid = s.Id,
                                        pwd = s.PassWord,
                                        names = s.LoginName
                                    };

                        response.SetData(query.ToList());
                    }
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// 查询所有任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getdatalistss(datacx model)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = from m in _dbContext.Mission
                            where m.Recycle == "0"
                            orderby m.Id descending
                            select new
                            {
                                priorityuuid = m.PriorityUuid.ToString(),//所属重点工作uuid
                                missionuuid = m.MissionUuid,
                                //priorityheadline = getPriorityHeadline(m.PriorityUuid.ToString()),//所属重点工作
                                missionheadline = m.MissionHeadline,//任务标题
                                //missionheadline2 = gettitle2(m.MissionHeadline),
                                principal = ReturnName(m.EstablishName),//负责人姓名
                                principaluuid = m.EstablishName,//负责人uid
                                starttime = Convert.ToDateTime(m.StartTime).ToString("yyyy-MM-dd"),//开始时间
                                finishtime = Convert.ToDateTime(m.FinishTime).ToString("yyyy-MM-dd"),//结束时间
                                missiondescribe = m.MissionDescribe,//任务描述
                                priority = m.Priority,//优先级
                                manhour = m.Manhour,//计划工时
                                //approver =isApprover(m.EstablishName, model.uname),//审批人
                                zhuangtai = getzhuangtai(m.AuditStatus),//状态
                                auditstatus = m.AuditStatus,//审核状态
                                participant = m.Participant,//参与人
                                isAllCount = getcount(m.AdministrativeOffice),//计算分配下去的科室总数
                                id = m.Id,//id
                                issfyc = getissfyc(m.Participant, m.EstablishName, model.uname, m.AdministrativeOffice, m.AuditStatus, m.IsAssigned),//判断是否参与或是指派人
                                iswancheng = getwancheng(m.EstablishName, model.uname, m.AuditStatus),
                            };
                if (model.cxname != "" && model.cxname != null)//搜索查询
                {
                    var query1 = query.AsEnumerable().Where(x => x.missionheadline.Contains(model.cxname));
                    response.SetData(query1.ToList());
                    return Ok(response);
                }
                var q = query.ToList();
                response.SetData(query.ToList());
                return Ok(response);
            }
        }

        /// <summary>
        /// 计算分配的科室总数
        /// </summary>
        /// <returns></returns>
        public static int getcount(string ads)
        {
            int count = 0;
            if (ads != "")
            {
                string[] arr = ads.Split(",");
                foreach (var item in arr)
                {
                    if (item != "")
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 根据任务id修改完成情况
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FinishWork(string uuid)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid.ToString().Contains(uuid));
                if (query != null)
                {
                    query.AuditStatus = "2";
                    _dbContext.SaveChanges();
                    response.SetSuccess();
                }
                return Ok(response);
            }
        }



        /// <summary>
        /// 查找姓名
        /// </summary>
        /// <param name="cyrid"></param>
        /// <param name="fzrid"></param>
        /// <param name="dlrid"></param>
        /// <returns></returns>
        public static string ReturnName(string guid)
        {
            string RealName = "";
            using (haikanHeQiaoContext context = new haikanHeQiaoContext())
            {
                RealName = context.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(guid)).RealName;
                return RealName;
            }
        }

        public static string getwancheng(string EstablishName, string uname, string stutas)
        {
            string str = "";
            if (EstablishName.ToLower() == uname.ToLower() && stutas != "2")
            {
                str = "1";
            }
            return str;
        }
        public static string getissfyc(string cyrid, string fzrid, string dlrid, string bmid, string stauts, string isAssigned)
        {
            haikanHeQiaoContext hc = new haikanHeQiaoContext();
            var str = "0";//什么都没有
            if (stauts != "")
            {
                if (stauts == "0")
                {
                    var res = hc.SystemUser.Where(x => x.SystemUserUuid.ToString() == dlrid).FirstOrDefault();
                    //if (fzrid.Contains(res.SystemUserUuid.ToString()))
                    //{
                    //    str = "fzr";//负责人
                    //}
                    if (cyrid.Contains(res.SystemUserUuid.ToString()))
                    {
                        str = "1";//参与人
                    }
                }
                else
                {
                    str = "0";
                }
            }
            else 
            { 
               var res = hc.SystemUser.Where(x => x.SystemUserUuid.ToString() == dlrid).FirstOrDefault();
                if (isAssigned != null)
                 {
                    var isBelone = bmid.Contains(res.DepartmentUuid.ToString());
                    if (isBelone)
                    {
                        if (isAssigned.Contains(res.DepartmentUuid.ToString()))
                        {
                            if (cyrid.Contains(res.SystemUserUuid.ToString()))
                            {
                                str = "1";//参与人
                            }
                        }
                        else
                        {
                          str = "2";//可指派的人
                        }
                    }
                }
                else
                {
                    str = "2";
                }
            }
            return str;
        }
        /// <summary>
        /// 已完成按钮
        /// </summary>
        /// <param name="fzrid"></param>
        /// <param name="dlrid"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string getissfycss(string fzrid, string bmid, string dlrid, string type, string status,string isAssigned)
        {
            haikanHeQiaoContext hc = new haikanHeQiaoContext();

            var str = "0";//什么都没有
            if (status != "")
            {
                if (status == "0")
                {

                    if (fzrid.Contains(dlrid) && type != "ywc")
                    {
                        str = "2";//审批人
                    }
                }
                else
                {
                    str = "";
                }
            }
            else
            {
                var res = hc.SystemUser.Where(x => x.SystemUserUuid.ToString() == dlrid).FirstOrDefault();
                var isBelone = bmid.Contains(res.DepartmentUuid.ToString());
                if (isBelone)
                {
                    str = "1";//可指派的人
                }

            }
            return str;
        }
        /// <summary>
        /// 已完成按钮
        /// </summary>
        /// <param name="fzrid"></param>
        /// <param name="dlrid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getissfycsss(string type)
        {
            haikanHeQiaoContext hc = new haikanHeQiaoContext();
            var str = "是";
            if (type != "ywc")
            {
                str = "否";
            }
            return str;
        }

        /// <summary>
        /// 通过工作uuid重新名称
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public string getPriorityHeadline(string uuid)
        {
            haikanHeQiaoContext hc = new haikanHeQiaoContext();
            var str = "";
            var res = hc.Priority.Where(x => x.PriorityUuid.ToString() == uuid).FirstOrDefault();
            if (res != null)
            {
                str = res.PriorityHeadline;
            }
            return str;
        }
        /// <summary>
        /// 查询任务的个数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult taskcount(string uuid)//负责人或者参与人是当前账号
        {
            using (_dbContext)
            {
                var allist = _dbContext.Mission.Where(x => x.Recycle == "0" && (x.EstablishName.ToUpper().Contains(uuid) || x.Participant.Contains(uuid))).ToList();
                int jxzcount = allist.Where(x => x.AuditStatus == "0" || x.AuditStatus == "").ToList().Count;//进行中
                var nowdate = DateTime.Now;
                int jjdqcount = allist.Where(x => Convert.ToDateTime(x.FinishTime).AddDays(-2) <= nowdate && Convert.ToDateTime(x.FinishTime) >= nowdate && x.AuditStatus != "2" && x.AuditStatus != "3").ToList().Count;//即将到期还未完成
                int ywccount = allist.Where(x => x.AuditStatus == "2").ToList().Count;//已完成
                                                                                      //定义一个键值对集合保存各个类型任务数
                Dictionary<string, int> allcount = new Dictionary<string, int>();
                allcount.Add("jxzcount", jxzcount);
                allcount.Add("jjdqcount", jjdqcount);
                allcount.Add("ywccount", ywccount);

                return Ok(allcount);
            }
        }

        //按条件查询
        [HttpGet]
        public IActionResult taskwhere(string type, string uuid, string searhdata)//负责人或者参与人是当前登陆人
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = (from m in _dbContext.Mission
                             where m.Recycle == "0" && (m.Participant.Contains(uuid) || m.EstablishName.ToLower() == uuid.ToLower())//不是科室工作总结
                             orderby m.EstablishTime descending
                             select new AllMissionViewModel
                             {
                                 //priorityUUID = m.PriorityUuid.ToString(),//所属重点工作uuid
                                 missionUuid = m.MissionUuid,
                                 //priorityHeadline = getPriorityHeadline(m.PriorityUuid.ToString()),//所属重点工作
                                 missionHeadline = m.MissionHeadline,//任务标题
                                 //missionHeadline2 = gettitle2(m.MissionHeadline),
                                 principal = ReturnName(m.EstablishName),//负责人姓名
                                 principaluuid = m.EstablishName,//负责人uif
                                 startTime = Convert.ToDateTime(m.StartTime).ToString("yyyy-MM-dd"),//开始时间
                                 finishTime = Convert.ToDateTime(m.FinishTime).ToString("yyyy-MM-dd"),//结束时间
                                 missionDescribe = m.MissionDescribe,//任务描述
                                 priority = m.Priority,//优先级
                                 //manhour = m.Manhour,//计划工时
                                 //approver = m.Approver,//审批人
                                 zhuangtai = getzhuangtai(m.AuditStatus),//状态
                                 auditStatus = m.AuditStatus,//审核状态
                                 participant = m.Participant,//参与人
                                 id = m.Id,//id
                                 issfyc = getissfycss(m.EstablishName, m.AdministrativeOffice, uuid, type, m.AuditStatus,m.IsAssigned),
                                 issfycss = getissfycsss(type),
                                 accomplish = m.Accomplish,
                             }).ToList();

                //for (int i = 0; i < query.Count(); i++)
                //{
                //    var str = query[i].principaluuid.Split(',');
                //    foreach (var item in str)
                //    {
                //        var user = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid.ToString() == item);
                //        if (user != null)
                //        {
                //            query[i].principal += user.RealName;
                //            break;
                //        }
                //    }
                //}
                if (searhdata != "" && searhdata != null)//搜索查询
                {
                    var query1 = query.AsEnumerable().Where(x => x.missionHeadline.Contains(searhdata));
                    query = query1.ToList();
                }
                if (type == "jxz")//进行中
                {
                    query = query.Where(x => x.zhuangtai == "进行中" || x.zhuangtai == "待指派").ToList();//审核状态为0则表示进行中
                }
                if (type == "jjdq")//即将到期
                {
                    var nowdate = DateTime.Now;
                    query = query.Where(x => Convert.ToDateTime(x.finishTime).AddDays(-2) <= nowdate && Convert.ToDateTime(x.finishTime) >= nowdate && x.zhuangtai != "完成" && x.zhuangtai != "逾期").ToList();//即将到期还未完成
                }
                if (type == "ywc")//已完成
                {
                    query = query.Where(x => x.zhuangtai == "完成").ToList();//已完成
                }
                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 添加任务app (负责人，审批人，添加人写死中）
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult appcreate(taskCreateViewModel model)
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
                ////如果选择了重点工作
                //if (!string.IsNullOrEmpty(model.PriorityUUID))
                //{
                //    entity.PriorityUuid = Guid.Parse(model.PriorityUUID);//所属重点工作             
                //}
                entity.MissionHeadline = model.MissionHeadline;//任务标题标题
                entity.Principal = model.EstablishName;//负责人就是创建人以及审批人
                entity.StartTime = Convert.ToDateTime(model.StartTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");//开始时间
                entity.FinishTime = Convert.ToDateTime(model.FinishTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");//结束时间
                entity.MissionDescribe = model.MissionDescribe;//任务描述
                entity.Remark = model.Remark;
                entity.Priority = model.Priority;//优先级
                entity.Manhour = model.Manhour;//计划工时
                entity.Approver = model.EstablishName;//审批人
                entity.AdministrativeOffice = model.AdministrativeOffice;//下派科室
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//创建时间
                entity.EstablishName = model.EstablishName;//创建人uuid
                entity.Recycle = "0";//未删除
                entity.AuditStatus = "";//审核状态为待审核
                entity.Missiontype = "0";
                entity.Isouttime = "0";
                entity.Sortord = "";//任务完成时间为空
                entity.Preserve = "1";//保存状态为未保存到草稿箱
                _dbContext.Mission.Add(entity);//添加
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑任务
        /// </summary>
        /// <param name="uuid">任务uuid</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AppEdit(Guid uuid)
        {
            using (_dbContext)
            {
                var missdbcontet = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid == uuid);
                var fzruuid = missdbcontet.EstablishName;//负责人id
                var fzrname = GetUUIDTrueName(fzruuid);
                //var spruuid = missdbcontet.Approver;//审批人id
                //var sprname = GetUUIDTrueName(spruuid);
                var cyruuid = missdbcontet.Participant;//参与人id
                var cyrname = GetIDTrueName(cyruuid);
                var query = (from m in _dbContext.Mission
                             where m.MissionUuid == uuid
                             select new
                             {
                                 missionUUID = m.MissionUuid,
                                 //priorityUUID = m.PriorityUuid == null ? "" : m.PriorityUuid.ToString(),//所属重点工作uuid
                                 //priorityname = gettitle(_dbContext.Priority.FirstOrDefault(x => x.PriorityUuid == m.PriorityUuid).PriorityHeadline),//所属重点工作
                                 //priorityduan = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid == m.PriorityUuid).PriorityHeadline,
                                 missionHeadline = m.MissionHeadline,//任务标题
                                 principal = m.Principal,//负责人uuid
                                 principalname = fzrname,//负责人
                                 startTime = m.StartTime,//开始时间
                                 finishTime = m.FinishTime,//结束时间
                                 missionDescribe = m.MissionDescribe,//任务描述
                                 priority = m.Priority,//优先级
                                                       //manhour = Convert.ToInt32(m.Manhour),//计划工时
                                                       //manhourt = m.Manhour + "天",//计划工时
                                                       //    approver = m.Approver == "" ? "" : m.Approver,//审批人uuid
                                                       //     //approvername = us.RealName,//审批人
                                                       //    approvername = m.Approver == "" ? "" : sprname,//审批人
                                                       //    auditStatusNum = m.AuditStatus,//审核状态(数字)
                                                       //    auditStatus = getzhuangtai(m.AuditStatus),//审核状态
                                 selectcanyurenname = cyrname.Trim(','),//参与人
                                                                        //    participant = m.Participant,//参与人id
                                                                        //    establishName = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(m.EstablishName)).RealName,//创建人姓名
                                                                        //    establisthtruename = m.EstablishName,//创建人uuid
                                 remark = m.Remark,//备注
                                                   //    establishTime = m.EstablishTime,//创建时间
                                                   //    auditOpinion = m.AuditOpinion,//审核意见
                             }).FirstOrDefault();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query);
                return Ok(response);
            }
        }
        /// <summary>
        /// 通过用户uuid集合获取用户名真实姓名集合(负责人+审批人)
        /// </summary>
        /// <returns></returns>
        public string GetUUIDTrueName(string strids)
        {
            string strlistname = "";
            //var patcount = strids.Split(',');
            //for (int i = 0; i < patcount.Length; i++)
            //{
            //    if (patcount[i].ToString() != "")
            //    {
            if (strids != null)
            {
                var systemuser = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(strids));
                if (systemuser != null)
                {
                    strlistname = systemuser.RealName;
                }
            }
            //    }
            //}
            return strlistname;
        }
        /// <summary>
        /// 通过用户uuid集合获取用户名真实姓名集合(参与人)
        /// </summary>
        /// <returns></returns>
        public string GetIDTrueName(string strids)
        {

            string strlistname = "";
            if (strids != null)
            {
                var patcount = strids.Split(',');
                for (int i = 0; i < patcount.Length; i++)
                {
                    if (patcount[i].ToString() != "")
                    {
                        var systemuser = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid.ToString() == patcount[i]);
                        if (systemuser != null)
                        {
                            strlistname += systemuser.RealName + ",";
                        }
                    }
                }
            }
            return strlistname;
        }

        public string gettitle2(string longtitle)
        {
            if (longtitle.Length > 5)
            {
                longtitle = longtitle.Substring(0, 5) + "...";
            }
            return longtitle;
        }
        /// <summary>
        /// 获取审批人
        /// </summary>
        /// <returns></returns>
        public string getshenpiname(string useruuid)
        {
            string username = "无";
            if (useruuid != null && useruuid != "")
            {
                username = _dbContext.SystemUser.Where(x => x.SystemUserUuid == Guid.Parse(useruuid)).FirstOrDefault().RealName;
            }
            return username;

        }

        /// <summary>
        /// 获取审核状态
        /// </summary>
        /// <param name="zt"></param>
        /// <returns></returns>
        public static string getzhuangtai(string zt)
        {
            string ztname = "";
            if (zt == "")
            {
                ztname = "待指派";
            }
            if (zt == "0")
            {
                ztname = "进行中";
            }
            if (zt == "1")
            {
                ztname = "审批中";
            }
            if (zt == "2")
            {
                ztname = "完成";
            }
            if (zt == "3")
            {
                ztname = "逾期";
            }

            return ztname;
        }


        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Appwanchen(taskCreateViewModel model)
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
                if (entity.Approver == "" || entity == null)
                {
                    entity.AuditStatus = "2";//没有审核人则直接审核通过
                    entity.Accomplish = "1";//任务为完成状态
                    entity.Sortord = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//任务完成时间
                }
                else
                {
                    entity.AuditStatus = "1";//审核状态为审核中              
                }

                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
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
                                priorityHeadlinelong = p.PriorityHeadline,
                                priorityHeadline = gettitle(p.PriorityHeadline),

                            };
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query.ToList());
                return Ok(response);
            }
        }

        public string gettitle(string longtitle)
        {
            if (longtitle != null)
            {
                if (longtitle.Length > 20)
                {
                    longtitle = longtitle.Substring(0, 20) + "...";
                }
            }
            else
            {
                longtitle = "";
            }

            return longtitle;
        }

        /// <summary>
        /// 查询出所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("{type}")]
        public IActionResult gealluser(string type)
        {
            using (_dbContext)
            {
                if (type == "duo")
                {
                    var query = from s in _dbContext.SystemUser
                                where s.IsDeleted == 0
                                select new
                                {
                                    systemUserUUID = s.SystemUserUuid,
                                    value = s.Id,
                                    loginName = s.LoginName,//登录名
                                    name = s.RealName,//真实姓名
                                    isDeleted = s.IsDeleted,
                                    checkeds = false,

                                };
                    var response = ResponseModelFactory.CreateResultInstance;
                    response.SetData(query.ToList());
                    return Ok(response);
                }
                else
                {
                    var query = from s in _dbContext.SystemUser
                                where s.IsDeleted == 0
                                select new
                                {
                                    value = s.SystemUserUuid,
                                    loginName = s.LoginName,//登录名
                                    name = s.RealName,//真实姓名
                                    isDeleted = s.IsDeleted,
                                    checkeds = false,

                                };
                    var response = ResponseModelFactory.CreateResultInstance;
                    response.SetData(query.ToList());
                    return Ok(response);
                }

            }
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{strlist}")]
        public IActionResult getdepartlists(string strlist)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var rwinfo = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid.ToString().ToLower() == strlist);
                if (rwinfo != null)
                {
                    var deplist = "";
                    var fzrlist = rwinfo.EstablishName;
                    var cyrlist = rwinfo.Participant;
                    var systemuser = _dbContext.SystemUser.Where(x => fzrlist.Contains(x.SystemUserUuid.ToString()) || cyrlist.Contains("," + x.SystemUserUuid.ToString() + ",")).ToList();
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
                                        name = u.Name,
                                        userid = u.DepartmentUuid,
                                        count = systemuser.Count
                                    };
                        var query1 = query.OrderBy(a => a.name).ToList();
                        response.SetData(query1);
                    }
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{strlist}")]
        public IActionResult getdepartlistsss(string strlist)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var rwinfo = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid.ToString().ToLower() == strlist);
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
                                        name = u.Name,
                                        id = u.DepartmentUuid,
                                        count = systemuser.Count
                                    };
                        var query1 = query.OrderBy(a => a.name).ToList();
                        response.SetData(query1);
                    }
                }
            }
            return Ok(response);
        }



        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult getdepartlist()
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = from u in _dbContext.Department
                            where u.IsDeleted == 0
                            orderby u.Id ascending
                            select new
                            {
                                name = u.Name,
                                id = u.DepartmentUuid,
                                count = _dbContext.SystemUser.Count(x => x.DepartmentUuid == u.DepartmentUuid && x.IsDeleted == 0)
                            };
                response.SetData(query.OrderBy(a => a.name).ToList());
            }
            return Ok(response);
        }

        /// <summary>
        /// 通过部门获取人员信息（点击下级）
        /// </summary>
        /// <returns></returns>
        [HttpGet("{strlist}")]
        public IActionResult getuserlistss(string strlist)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                //var depid = "";
                //var rwid = "";
                //var strlistss = strlist.Split('&');
                //if (strlistss.Length > 1)
                //{
                //    rwid = strlistss[0];
                //    depid = strlistss[1];
                //通过登录进来的uuid获取部门uuid
                var depid = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid.ToString() == strlist && x.IsDeleted == 0).DepartmentUuid;
                //var missinfo = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid.ToString().ToLower() == rwid);
                //if (missinfo != null)
                //{
                //var fzrlist = missinfo.Principal;
                //var cyrlist = missinfo.Participant;
                //var query = from u in _dbContext.SystemUser
                //            where u.IsDeleted == 0 && u.DepartmentUuid.ToString() == depid && (fzrlist.Contains(u.SystemUserUuid.ToString()) || cyrlist.Contains("," + u.Id + ","))
                //            select new
                //            {
                //                name = u.RealName,
                //                id = u.SystemUserUuid,
                //                uid = u.Id,
                //            };
                var query = _dbContext.SystemUser.Where(x => x.DepartmentUuid == depid).ToList();
                //var query1 = query.OrderBy(a => a.name).ToList();
                response.SetData(query);
                //}
                //}
            }
            return Ok(response);
        }
        /// <summary>
        /// 通过任务uuid添加参与人
        /// </summary>
        /// <returns></returns>
        public IActionResult editPerson(personList personl)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var depid = _dbContext.SystemUser.Where(x => x.SystemUserUuid==Guid.Parse(personl.id.ToLower())).FirstOrDefault().DepartmentUuid;

                var query = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid.ToString() == personl.uuid);
                if (query != null)
                {
                    query.IsAssigned += depid + ",";
                    query.Participant += personl.partName;
                    if (query.IsAssigned != null)
                    {
                        string[] opCount = query.IsAssigned.Split(',');
                        string[] xiCount = query.AdministrativeOffice.Split(',');
                        if (opCount.Count()  == xiCount.Count())
                        {
                            query.AuditStatus = "0";
                        }
                    }
                    _dbContext.SaveChanges();
                    response.SetSuccess();
                    return Ok(response);

                }
                response.SetFailed();
                return Ok(response);
            }
        }

        /// <summary>
        /// 通过部门获取人员信息（点击下级）
        /// </summary>
        /// <returns></returns>
        [HttpGet("{strlist}")]
        public IActionResult getuserprioritylist(string strlist)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var depid = "";
                var rwid = "";
                var strlistss = strlist.Split('&');
                if (strlistss.Length > 1)
                {
                    rwid = strlistss[0];
                    depid = strlistss[1];
                    var missinfo = _dbContext.Priority.FirstOrDefault(x => x.PriorityUuid.ToString().ToLower() == rwid);
                    if (missinfo != null)
                    {
                        var fzrlist = missinfo.Principal;
                        var cyrlist = missinfo.Participant;
                        var query = from u in _dbContext.SystemUser
                                    where u.IsDeleted == 0 && u.DepartmentUuid.ToString() == depid && (fzrlist.Contains(u.SystemUserUuid.ToString()) || cyrlist.Contains("," + u.Id + ","))
                                    select new
                                    {
                                        name = u.RealName,
                                        id = u.SystemUserUuid,
                                        uid = u.Id,
                                    };
                        var query1 = query.OrderBy(a => a.name).ToList();
                        response.SetData(query1);
                    }
                }
            }
            return Ok(response);
        }


        /// <summary>
        /// 通过任务获取人员汇报总结信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{strlist}")]
        public IActionResult getuserzongjiePriority(string strlist)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var userid = "";
                var rwid = "";
                var strlistss = strlist.Split('&');
                if (strlistss.Length > 1)
                {
                    rwid = strlistss[0];
                    userid = strlistss[1];
                    var query = from u in _dbContext.PriorityJournal
                                where u.PriorityUuid.ToString() == rwid.ToString() && u.EstablishName == userid
                                select new
                                {
                                    ywcinfos = u.Completed,
                                    xyxtinfo = u.Coordination,
                                    addtimes = u.EstablishTime,
                                    uid = u.PriorityJournalUuid,
                                    fjinfo = u.Accessory,
                                    rwuuid = u.PriorityUuid,
                                };
                    var query1 = query.ToList();
                    response.SetData(query1);
                }
            }
            return Ok(response);
        }
        /// <summary>
        /// 通过任务获取人员汇报总结信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{strlist}")]
        public IActionResult getuserzongjie(string strlist)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var userid = "";
                var rwid = "";
                var strlistss = strlist.Split('&');
                if (strlistss.Length > 1)
                {
                    rwid = strlistss[0];
                    userid = strlistss[1];
                    var query = from u in _dbContext.PersonalDiary
                                where u.MissionUuid.ToString() == rwid.ToString() && u.EstablishName == userid
                                select new
                                {
                                    headline = u.Headline,
                                    time = u.WriteTime,
                                    //adduser = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(u.EstablishName)).RealName,
                                    content = u.Content,
                                    establishName = u.EstablishName,
                                    accessory = u.Accessory,
                                    uid = u.PersonalDiaryUuid,
                                };
                    var query1 = query.ToList();
                    response.SetData(query1);
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// 通过部门获取人员信息（点击下级）
        /// </summary>
        /// <returns></returns>
        [HttpGet("{strlist}")]
        public IActionResult getuserlist(string strlist)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = from u in _dbContext.SystemUser
                                //where u.IsDeleted == 0 && u.UserType != 0 && u.DepartmentUuid.ToString() == strlist
                            where u.IsDeleted == 0 && u.DepartmentUuid.ToString() == strlist
                            select new
                            {
                                name = u.RealName,
                                id = u.SystemUserUuid,
                                uid = u.Id,
                            };
                response.SetData(query.OrderBy(a => a.name).ToList());
            }
            return Ok(response);
        }

        /// <summary>
        /// 通过部门获取人员信息（点击复选框）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getuserlists(datasinfo strlist)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = from u in _dbContext.SystemUser
                                //where u.IsDeleted == 0 && u.UserType != 0 && strlist.Listinfo.Contains(u.DepartmentUuid.ToString().ToLower())
                            where u.IsDeleted == 0 && strlist.Listinfo.Contains(u.DepartmentUuid.ToString().ToLower())
                            select new
                            {
                                name = u.RealName,
                                id = u.SystemUserUuid,
                                uid = u.Id,
                            };
                response.SetData(query.OrderBy(a => a.name).ToList());
            }
            return Ok(response);
        }

        public class datasinfo
        {
            public List<string> Listinfo { get; set; }
        }

        /// <summary>
        /// 获取(企业内部应用)
        /// </summary>
        /// <returns></returns>
        [HttpGet("{strlist}")]
        public IActionResult getuserinfos(string strlist)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var code = strlist;
                string suiteKey = "dingdhhaaylylww2i7fw";
                string suiteSecret = "sYgyZowkEoo4O9TQdqtaLdUcH2JyWfOsFmmRLkiGhDUC3-dvFp7j7mZ73Pd3KoOq";
                string timestamp = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000).ToString();
                string suiteTicket = "TestSuiteTicket";
                string signature1 = timestamp + "\n" + suiteTicket;
                string signature2 = HmacSHA256(signature1, suiteSecret);
                string signature = System.Web.HttpUtility.UrlEncode(signature2, System.Text.Encoding.UTF8);
                string auth_corpid = strlist;
                //string url = "https://oapi.dingtalk.com/service/get_corp_token?signature=" + signature + "&timestamp=" + timestamp + "&suiteTicket=" + suiteTicket + "&accessKey=" + suiteKey;

                //获取access_token
                string url = "https://oapi.dingtalk.com/gettoken?appkey=" + suiteKey + "&appsecret=" + suiteSecret;


                //string param = "{ \"auth_corpid\": \"ding9f15a00f7c763360a1320dcb25e91351\"}";
                //var response11 = Haikan3.Utils.DingDingHelper.HttpPost(url, param);
                var response11 = Haikan3.Utils.DingDingHelper.HttpGet(url);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PersistentCodeResult>(response11);
                if (result != null && result.errcode == "0")
                {
                    //获取人员信息
                    string url11 = "https://oapi.dingtalk.com/user/getuserinfo?access_token=" + result.access_token + "&code=" + code;
                    var response12 = Haikan3.Utils.DingDingHelper.HttpGet(url11);
                    var result12 = Newtonsoft.Json.JsonConvert.DeserializeObject<PersistentCodeResult12>(response12);
                    if (result12 != null && result12.errcode == 0)
                    {
                        ////获取人员信息
                        //var results = Haikan3.Utils.DingDingHelper.GetUserDetail(result.access_token, result12.userid);

                        var userdata = _dbContext.SystemUser.Where(x => x.Dinguserid == result12.userid).ToList().Count;
                        //没有当前用户则添加
                        if (userdata == 0)
                        {
                            var entity = new SystemUser();
                            entity.SystemUserUuid = Guid.NewGuid();
                            entity.RealName = result12.name;
                            entity.Dinguserid = result12.userid;
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            entity.IsDeleted = 0;
                            _dbContext.Add(entity);
                            _dbContext.SaveChanges();
                        }

                        //获取当前用户的id，uuid，姓名
                        var query = from s in _dbContext.SystemUser
                                    where s.IsDeleted == 0 && s.Dinguserid == result12.userid
                                    select new
                                    {
                                        useruuid = s.SystemUserUuid,
                                        name = s.RealName,
                                        userid = s.Id,
                                        pwd = s.PassWord,
                                        names = s.LoginName
                                    };

                        response.SetData(query.ToList());
                    }
                }
                return Ok(response);
            }
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
        public class personList
        {
            public string id { get; set; }
            public string uuid { get; set; }
            public string partName { get; set; }
        }


        /// <summary>
        /// 通过部门获取人员信息（点击下级）
        /// </summary>
        /// <returns></returns>
        [HttpGet("{strlist}")]
        public IActionResult getusername(string strlist)
        {


            var response = ResponseModelFactory.CreateResultInstance;
            List<SystemUser> list = new List<SystemUser>();

            using (_dbContext)
            {
                var depid = "";
                var rwid = "";
                var strlistss = strlist.Split(',');
                var sysuser = _dbContext.SystemUser.Where(x => x.DepartmentUuid == Guid.Parse(strlist)).ToList();
                foreach (var item in sysuser)
                {
                    SystemUser user = new SystemUser();
                    user.RealName = item.RealName;
                    user.Id = item.Id;
                    user.SystemUserUuid = item.SystemUserUuid;
                    list.Add(user);
                }
                response.SetData(list);
                //if (strlistss.Length > 0)
                //{
                //    foreach (var item in strlistss)
                //    {
                //        if (item != "")
                //        {

                //        }
                //    }
                //    response.SetData(list);
                //}
            }
            return Ok(response);
        }
    }
}