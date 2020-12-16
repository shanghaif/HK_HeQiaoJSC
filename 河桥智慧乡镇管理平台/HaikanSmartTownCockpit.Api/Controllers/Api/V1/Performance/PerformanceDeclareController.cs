using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.RequestPayload.Rbac.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.Performance;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using HaikanSmartTownCockpit.Api.Configurations;
using System.Text;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Performance
{
    [Route("api/v1/Performance/[controller]/[action]")]
    [ApiController]
    public class PerformanceDeclareController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        //用来获取路径相关
        private IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        /// <param name="mdDesEncrypt"></param>
        /// <param name="hostingEnvironment"></param>
        public PerformanceDeclareController(haikanHeQiaoContext dbContext, IMapper mapper, IOptions<MdDesEncrypt> mdDesEncrypt, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        ///// <summary>
        ///// 汇总查询数据列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public IActionResult CollectList(RoleRequestPayload payload)
        //{
        //    var response = ResponseModelFactory.CreateResultInstance;
        //    using (_dbContext)
        //    {
        //        StringBuilder sql = new StringBuilder(@"SELECT DeclareName,DeclareTime,dt.Name DeclareDepartment, 
        //        MAX(d.ID) ID,
        //        STUFF( ( SELECT ','+ BonusPoint
        //                                FROM DepartmentDeclare a
        //                                WHERE d.DeclareName = a.DeclareName AND d.DeclareTime=a.DeclareTime AND d.DeclareDepartment=a.DeclareDepartment
        //                                FOR XML PATH('')),1 ,1, '')  BonusPoint,
        //        SUM(d.PlusScore) PlusScore,
        //        STUFF( ( SELECT ','+ PlusContent
        //                                FROM DepartmentDeclare a
        //                                WHERE d.DeclareName = a.DeclareName AND d.DeclareTime=a.DeclareTime AND d.DeclareDepartment=a.DeclareDepartment
        //                                FOR XML PATH('')),1 ,1, '')  PlusContent,
        //        STUFF( ( SELECT ','+ Deduction
        //                                FROM DepartmentDeclare a
        //                                WHERE d.DeclareName = a.DeclareName AND d.DeclareTime=a.DeclareTime AND d.DeclareDepartment=a.DeclareDepartment
        //                                FOR XML PATH('')),1 ,1, '')  Deduction,
        //        SUM(d.DeductionScore) DeductionScore,
        //        STUFF( ( SELECT ','+ DeductionContent
        //                                FROM DepartmentDeclare a
        //                                WHERE d.DeclareName = a.DeclareName AND d.DeclareTime=a.DeclareTime AND d.DeclareDepartment=a.DeclareDepartment
        //                                FOR XML PATH('')),1 ,1, '')  DeductionContent
        //        FROM DepartmentDeclare d join Department dt on d.DeclareDepartment=dt.DepartmentUUID WHERE d.IsDeleted ='0'
        //        GROUP BY d.DeclareName,d.DeclareTime,d.DeclareDepartment,dt.Name");
        //        var query = _dbContext.PerformanceDepartmentDeclare.FromSql(sql.ToString());
        //        //姓名查询
        //        if (!string.IsNullOrEmpty(payload.Kw))
        //        {
        //            query = query.Where(x => x.DeclareName.Contains(payload.Kw.Trim()));
        //        }
        //        //时间查询
        //        if (!string.IsNullOrEmpty(payload.kwendtime))
        //        {
        //            query = query.Where(x => x.DeclareTime.Contains(payload.kwendtime.Trim()));
        //        }
        //        //排序
        //        if (payload.FirstSort != null)
        //        {
        //            query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
        //        }
        //        var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
        //        var totalCount = query.Count();
        //        response.SetData(list, totalCount);
        //        return Ok(response);
        //    }
        //}

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(RoleRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = from a in _dbContext.DepartmentDeclare
                            where a.IsDeleted == 0
                            select new
                            {
                                DepartmentDeclareUuid = a.DepartmentDeclareUuid,
                                Id = a.Id,
                                DeclareName = a.DeclareName,//姓名
                                DeclareDepartment = _dbContext.Department.FirstOrDefault(x => x.DepartmentUuid == Guid.Parse(a.DeclareDepartment)).Name.ToString(),//部门
                                DeclareTime = a.DeclareTime,//时间
                                BonusPoint = a.BonusPoint,//加分项
                                PlusScore = a.PlusScore,//加分值
                                PlusContent = a.PlusContent,//加分内容
                                Deduction = a.Deduction,//减分项
                                DeductionScore = a.DeductionScore,//减分值
                                DeductionContent = a.DeductionContent,//加分内容
                                Remark = a.Remark,//备注
                                EstablishName = a.EstablishName,//添加人
                                AuditOpinion = a.AuditOpinion,//审核意见
                                AuditStatus = a.AuditStatus,//审核状态
                                IsDeleted = a.IsDeleted,//是否删除
                            };
                //姓名查询
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.DeclareName.Contains(payload.Kw.Trim()));
                }
                //时间查询
                if (!string.IsNullOrEmpty(payload.kwendtime))
                {
                    query = query.Where(x => x.DeclareTime.Contains(payload.kwendtime.Trim()));
                }
                //排序
                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:绩效申报信息数据", _dbContext);
                return Ok(response);
            }
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(DepartmentDeclareModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var personaldiary = new DepartmentDeclare();
                personaldiary.DeclareName = model.DeclareName;//姓名
                personaldiary.DeclareDepartment = model.DeclareDepartment;//部门
                personaldiary.DeclareTime = model.DeclareTime;//时间
                personaldiary.BonusPoint = model.BonusPoint;//加分项
                personaldiary.PlusScore = model.PlusScore;//加分分值
                personaldiary.PlusContent = model.PlusContent;//加分内容
                personaldiary.Deduction = model.Deduction;//减分项
                personaldiary.DeductionScore = model.DeductionScore;//减分分值
                personaldiary.DeductionContent = model.DeductionContent;//减分内容
                personaldiary.Remark = model.Remark;//备注
                personaldiary.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//添加时间
                personaldiary.EstablishName = AuthContextService.CurrentUser.DisplayName;//添加人
                personaldiary.IsDeleted = 0;//是否删除
                _dbContext.DepartmentDeclare.Add(personaldiary);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:绩效申报信息数据", _dbContext);
                }
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 导入信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult daoru(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\PerformanceDeclare";

                string uploadtitle = "绩效申报导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string sFileName = $"{uploadtitle}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

                string responsemsgsuccess = "";//成功提示
                string responsemsgrepeat = "";//重复提示
                string responsemsgdefault = "";//为空提示
                int successcount = 0;//成功条数
                int repeatcount = 0;//重复条数
                int defaultcount = 0;//为空条数
                try
                {
                    //把excelfile中的数据复制到file中
                    using (FileStream fs = new FileStream(file.ToString(), FileMode.Create)) //初始化一个指定路径和创建模式的FileStream
                    {
                        excelfile.CopyTo(fs);
                        fs.Flush();  //清空stream的缓存，并且把缓存中的数据输出到file
                    }
                    DataTable dt = Haikan3.Utils.ExcelTools.ExcelToDataTable(file.ToString(), "Sheet1", true);

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        response.SetFailed("表格无数据");
                        return Ok(response);
                    }
                    else
                    {
                        if (!dt.Columns.Contains("部门"))
                        {
                            response.SetFailed("无‘部门’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("姓名"))
                        {
                            response.SetFailed("无‘姓名’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("时间"))
                        {
                            response.SetFailed("无‘时间’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("加分项"))
                        {
                            response.SetFailed("无‘加分项’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("加分内容"))
                        {
                            response.SetFailed("无‘加分内容’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("加分分值"))
                        {
                            response.SetFailed("无‘加分分值’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("减分项"))
                        {
                            response.SetFailed("无‘减分项’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("减分内容"))
                        {
                            response.SetFailed("无‘减分内容’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("减分分值"))
                        {
                            response.SetFailed("无‘减分分值’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("备注"))
                        {
                            response.SetFailed("无‘备注’列");
                            return Ok(response);
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var entity = new DepartmentDeclare();

                            if (!string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString()))
                            {
                                entity.DeclareName = dt.Rows[i]["姓名"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["部门"].ToString()))
                            {
                                entity.DeclareDepartment = _dbContext.Department.FirstOrDefault(x => x.Name == dt.Rows[i]["部门"].ToString()).DepartmentUuid.ToString();//部门guid
                                //entity.DeclareDepartment = dt.Rows[i]["部门"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行部门为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["时间"].ToString()))
                            {
                                string s = Convert.ToDateTime(dt.Rows[i]["时间"].ToString()).ToString("yyyy-MM");
                                entity.DeclareTime = Convert.ToDateTime(dt.Rows[i]["时间"].ToString()).ToString("yyyy-MM");

                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行时间为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["加分项"].ToString()))
                            {
                                entity.BonusPoint = dt.Rows[i]["加分项"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["加分内容"].ToString()))
                            {
                                entity.PlusContent = dt.Rows[i]["加分内容"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["加分分值"].ToString()))
                            {
                                entity.PlusScore = int.Parse(dt.Rows[i]["加分分值"].ToString());
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["减分项"].ToString()))
                            {
                                entity.Deduction = dt.Rows[i]["减分项"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["减分内容"].ToString()))
                            {
                                entity.DeductionContent = dt.Rows[i]["减分内容"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["减分分值"].ToString()))
                            {
                                entity.DeductionScore = int.Parse(dt.Rows[i]["减分分值"].ToString());
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["备注"].ToString()))
                            {
                                entity.Remark = dt.Rows[i]["备注"].ToString();
                            }
                            entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//添加时间
                            entity.EstablishName = AuthContextService.CurrentUser.DisplayName;//添加人
                            entity.IsDeleted = 0;//是否删除
                            if (!string.IsNullOrEmpty(dt.Rows[i]["加分项"].ToString()) || !string.IsNullOrEmpty(dt.Rows[i]["减分项"].ToString()))
                            {
                                string ssss = dt.Rows[i]["加分项"].ToString();

                                if (dt.Rows[i]["加分项"].ToString() == "1" || dt.Rows[i]["减分项"].ToString() == "1")
                                {
                                    entity.AuditStatus = "2";//审核状态
                                }
                                else
                                {
                                    entity.AuditStatus = "1";//审核状态
                                }
                            }
                            _dbContext.DepartmentDeclare.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                            responsemsgsuccess += "<p style='color:green'>" + "第" + (i + 2) + "行信息导入成功" + "</p></br>";
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;
                    ToLog.AddLog("导入", "成功:导入:绩效申报信息数据", _dbContext);
                    DateTime endTime = DateTime.Now;
                    TimeSpan useTime = endTime - beginTime;
                    string taketime = "导入时间" + useTime.TotalSeconds.ToString() + "秒  ";
                    response.SetData(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(new
                    {
                        time = taketime,
                        successmsg = responsemsgsuccess,
                        repeatmsg = responsemsgrepeat,
                        defaultmsg = responsemsgdefault
                    })));
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    response.SetFailed("请上传与表字符相符的表格");
                    return Ok(response);
                }
            }
        }

        /// <summary>
        /// 编辑-显示数据
        /// </summary>
        /// <param name="guid">唯一编码</param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid guid)
        {
            //using (_dbContext)
            //{
            //    var entity = _dbContext.DepartmentDeclare.FirstOrDefault(x => x.DepartmentDeclareUuid == guid);
            //    var response = ResponseModelFactory.CreateInstance;
            //    response.SetData(_mapper.Map<DepartmentDeclare, DepartmentDeclareModel>(entity));
            //    return Ok(response);
            //}

            using (_dbContext)
            {
                var query = (from m in _dbContext.DepartmentDeclare
                             join u in _dbContext.Department
                             on m.DeclareDepartment equals u.DepartmentUuid.ToString()
                             where m.DepartmentDeclareUuid == guid
                             select new
                             {
                                 DepartmentDeclareUuid = m.DepartmentDeclareUuid,
                                 ID = m.Id,//id
                                 DeclareName = m.DeclareName,//姓名
                                 DeclareDepartment = m.DeclareDepartment,//部门guid
                                 DeclareDepartmentName = u.Name,//部门名称
                                 DeclareTime = m.DeclareTime,//时间
                                 BonusPoint = m.BonusPoint,//加分项
                                 PlusScore = m.PlusScore,//加分分值
                                 PlusContent = m.PlusContent,//加分内容
                                 Deduction = m.Deduction,//减分项
                                 DeductionScore = m.DeductionScore,//减分分值
                                 DeductionContent = m.DeductionContent,//减分内容
                                 Remark = m.Remark,//备注
                                 EstablishTime = m.EstablishTime,//添加入
                                 EstablishName = m.EstablishName,//添加时间
                                 IsDeleted = m.IsDeleted,//是否删除
                             }).FirstOrDefault();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的信息
        /// </summary>
        /// <param name="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(DepartmentDeclareModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.DepartmentDeclare.FirstOrDefault(x => x.DepartmentDeclareUuid == model.DepartmentDeclareUuid);
                entity.DeclareName = model.DeclareName;//姓名
                entity.DeclareDepartment = model.DeclareDepartment;//部门
                entity.DeclareTime = model.DeclareTime;//时间
                entity.BonusPoint = model.BonusPoint;//加分项
                entity.PlusScore = model.PlusScore;//加分分值
                entity.PlusContent = model.PlusContent;//加分内容
                entity.Deduction = model.Deduction;//减分项
                entity.DeductionScore = model.DeductionScore;//减分分值
                entity.DeductionContent = model.DeductionContent;//减分内容
                entity.Remark = model.Remark;//备注
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:绩效申报信息数据", _dbContext);
                }
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">GUID,多个以逗号分隔</param>
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
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">GUID,多个以逗号分隔</param>
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
        /// 部门下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DepartmentList()
        {
            using (_dbContext)
            {
                var query = from s in _dbContext.Department
                            where s.IsDeleted == 0
                            orderby s.Id ascending
                            select new
                            {
                                DepartmentUUID = s.DepartmentUuid,
                                id = s.Id,
                                Name = s.Name,//名称
                                Remark = s.Remark,//备注
                                isDeleted = s.IsDeleted
                            };
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query.ToList());
                return Ok(response);
            }
        }

        #region 私有方法

        /// <summary>
        /// 删除角色
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
                var sql = string.Format("UPDATE DepartmentDeclare SET IsDeleted=@IsDeleted WHERE DepartmentDeclareUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:绩效申报信息数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }

        #endregion

    }
}