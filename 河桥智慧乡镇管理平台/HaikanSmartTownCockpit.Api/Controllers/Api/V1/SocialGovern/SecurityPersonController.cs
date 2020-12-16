using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.RequestPayload.SocialGovern;
using Microsoft.AspNetCore.Hosting;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using HaikanSmartTownCockpit.Api.ViewModels.SocialGovern;
using HaikanSmartTownCockpit.Api.Models.Response;
using AutoMapper.QueryableExtensions;
using Haikan3.Utils;
using System.Text.RegularExpressions;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.SocialGovern
{
    [Route("api/v1/SocialGovern/[controller]/[action]")]
    [ApiController]
    public class SecurityPersonController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public SecurityPersonController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 安防人员列表
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(SecurityPRequestpayload payload)
        {
            using (_dbContext)
            {
                var query = _dbContext.Security.Where(x => x.IsDeleted == 0);

                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.SecurityName.Contains(payload.Kw.Trim()));
                }

                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:安防人员信息数据", _dbContext);
                return Ok(response);
            }

        }


        /// <summary>
        /// 上报记录列表
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List2(SecurityPRequestpayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.SecurityReport.Where(x => x.IsDeleted == 0 && x.SecurityUuid == Guid.Parse(payload.Kw)).Select(x => new
                {
                    x.IsDeleted,
                    x.Id,
                    x.SecurityReportUuid,
                    x.SecurityUuid,
                    x.Situation,
                    Time = x.Time.Value.ToString("yyyy-MM-dd"),
                    x.State,
                });

                //if (!string.IsNullOrEmpty(payload.Kw2))
                //{
                //    query = query.Where(x => x.SecurityName.Contains(payload.Kw.Trim()));
                //}

                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                return Ok(response);
            }
        }

        /// <summary>
        /// 添加安防人员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(SecurityPViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (string.IsNullOrEmpty(model.SecurityName) || string.IsNullOrEmpty(model.IdentityCard
                ))
            {
                response.SetFailed("请输入名称和身份证号");
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.Security.Any(x => x.IdentityCard == model.IdentityCard && x.IsDeleted == 0))
                {
                    response.SetFailed("角色已存在");
                    return Ok(response);
                }
                var entity = new Security()
                {
                    SecurityUuid = Guid.NewGuid(),
                    SecurityName = model.SecurityName,
                    AddPeople = AuthContextService.CurrentUser.DisplayName,
                    AddTime = DateTime.Now.ToString("yyyy-MM-dd"),
                    IsDeleted = 0,
                    Phone = model.Phone,
                    SecurityTime
                    = model.SecurityTime ==""?null : Convert.ToDateTime(model.SecurityTime).ToString("yyyy-MM-dd"),
                    SecurityStaues = model.SecurityStaues,
                    IdentityCard
                    = model.IdentityCard.Trim(),
                    SecurityAddress = model.SecurityAddress,

                };
                _dbContext.Security.Add(entity);
                var num = _dbContext.SaveChanges();
                if (num > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:安防人员信息一条数据", _dbContext);
                    response.SetSuccess();
                }
                else
                {
                    response.SetFailed("添加失败");
                }
                return Ok(response);
            }
        }

        /// <summary>
        /// 添加上报记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create2(SecurityReportViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (string.IsNullOrEmpty(model.Time.Trim()))
            {
                response.SetFailed("请输入时间");
                return Ok(response);
            }
            if (string.IsNullOrEmpty(model.Situation.Trim()))
            {
                response.SetFailed("请输入上报情况");
                return Ok(response);
            }
            using (_dbContext)
            {
                if (!_dbContext.Security.Any(x => x.SecurityUuid == model.SecurityUuid && x.IsDeleted == 0))
                {
                    response.SetFailed("不存在此安全员");
                    return Ok(response);
                }

                var entity = new SecurityReport()
                {
                    SecurityUuid = model.SecurityUuid,
                    SecurityReportUuid = Guid.NewGuid(),
                    Time = Convert.ToDateTime(model.Time),
                    IsDeleted = 0,
                    Situation = model.Situation,
                    State
                    = model.State,
                };
                _dbContext.SecurityReport.Add(entity);
                var num = _dbContext.SaveChanges();
                if (num > 0)
                {
                    response.SetSuccess();
                }
                else
                {
                    response.SetFailed("添加失败");
                }
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取单条信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Load(Guid guid)
        {
            using (_dbContext)
            {
                var query = _dbContext.Security.FirstOrDefault(x => x.SecurityUuid == guid);
                var response = ResponseModelFactory.CreateInstance;

                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取单条上报信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Load2(Guid guid)
        {
            using (_dbContext)
            {
                var query = _dbContext.SecurityReport.FirstOrDefault(x => x.SecurityReportUuid == guid);
                var response = ResponseModelFactory.CreateInstance;

                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑保存人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(SecurityPViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = _dbContext.Security.FirstOrDefault(x => x.SecurityUuid == model.SecurityUuid && x.IsDeleted == 0);
                if (entity == null)
                {
                    response.SetFailed("该人员不存在");
                    return Ok(response);
                }
                if (entity.IdentityCard != model.IdentityCard && _dbContext.Security.Any(x => x.IdentityCard == model.IdentityCard && x.IsDeleted == 0))
                {
                    response.SetFailed("该身份证号已存在");
                    return Ok(response);
                }

                entity.SecurityName = model.SecurityName;
                entity.SecurityAddress = model.SecurityAddress;
                entity.IdentityCard = model.IdentityCard;
                entity.Phone = model.Phone;
                entity.SecurityTime = model.SecurityTime==null? null : model.SecurityTime == ""?null: Convert.ToDateTime(model.SecurityTime).ToString("yyyy-MM-dd");
                entity.SecurityStaues = model.SecurityStaues;


                var success = _dbContext.SaveChanges() > 0;
                if (success)
                {
                    ToLog.AddLog("编辑", "成功:编辑:安防人员信息一条数据", _dbContext);
                    response.SetSuccess();
                }
                else
                {
                    response.SetFailed("保存失败");
                }
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑保存上报信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit2(SecurityReportViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            if (string.IsNullOrEmpty(model.Time.Trim()))
            {
                response.SetFailed("请输入时间");
                return Ok(response);
            }
            if (string.IsNullOrEmpty(model.Situation.Trim()))
            {
                response.SetFailed("请输入上报情况");
                return Ok(response);
            }
            using (_dbContext)
            {

                if (!_dbContext.Security.Any(x => x.SecurityUuid == model.SecurityUuid && x.IsDeleted == 0))
                {
                    response.SetFailed("该人员不存在");
                    return Ok(response);
                }
                var entity = _dbContext.SecurityReport.FirstOrDefault(x => x.IsDeleted == 0 && x.SecurityReportUuid == model.SecurityReportUuid);

                entity.Time = Convert.ToDateTime(model.Time);
                entity.Situation = model.Situation;
                entity.State = model.State;

                var success = _dbContext.SaveChanges() > 0;
                if (success)
                {
                    response.SetSuccess();
                }
                else
                {
                    response.SetFailed("保存失败");
                }
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除人员
        /// </summary>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            response = UpdateIsDelete(CommonEnum.IsDeleted.Yes, ids);
            return Ok(response);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ids">ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete2(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            response = UpdateIsDelete2(CommonEnum.IsDeleted.Yes, ids);
            return Ok(response);
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":

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
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch2(string command, string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":

                    response = UpdateIsDelete2(CommonEnum.IsDeleted.Yes, ids);
                    break;
                case "recover":
                    response = UpdateIsDelete2(CommonEnum.IsDeleted.No, ids);
                    break;
                default:
                    break;
            }
            return Ok(response);

        }


        /// <summary>
        /// 删除人员
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
                var sql = string.Format("UPDATE Security SET IsDeleted=@isDeleted WHERE SecurityUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:安防人员信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }

        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">角色ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete2(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE SecurityReport SET IsDeleted=@isDeleted WHERE SecurityReportUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }

        }


        /// <summary>
        /// 导入信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Import(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportBuidingExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = "安防人员信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string sFileName = $"{uploadtitle}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                //string conStr = ConnectionStrings.DefaultConnection;
                string responsemsgsuccess = "";
                string responsemsgrepeat = "";
                string responsemsgdefault = "";
                int successcount = 0;
                int repeatcount = 0;
                int defaultcount = 0;
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                //var query = _dbContext.Organization.FirstOrDefault(x => x.OrganizationName == "共青团");
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
                        if (!dt.Columns.Contains("姓名"))
                        {
                            response.SetFailed("无‘姓名’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("身份证号"))
                        {
                            response.SetFailed("无‘身份证号’列");
                            return Ok(response);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new HaikanSmartTownCockpit.Api.Entities.Security();
                            entity.SecurityUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString()))
                            {
                                entity.SecurityName = dt.Rows[i]["姓名"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            //if (!string.IsNullOrEmpty(dt.Rows[i]["性别"].ToString()))
                            //{
                            //    if (dt.Rows[i]["性别"].ToString() != "男" && dt.Rows[i]["性别"].ToString() != "女")
                            //    {
                            //        responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行性别不正确" + "</p></br>";
                            //        defaultcount++;
                            //        continue;
                            //    }
                            //    else
                            //    {
                            //        entity.Sex = dt.Rows[i]["性别"].ToString();
                            //    }
                            //}
                            if (!string.IsNullOrEmpty(dt.Rows[i]["联系电话"].ToString()))
                            {
                                Regex reg = new Regex("^[1][3,4,5,7,8][0-9]{9}$");
                                if (reg.IsMatch(dt.Rows[i]["联系电话"].ToString()))
                                {
                                    entity.Phone = dt.Rows[i]["联系电话"].ToString();
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行联系电话格式不正确" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["时间"].ToString()))
                            {
                                Regex reg = new Regex("^((((1[6-9]|[2-9]\\d)\\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})-0?2-(0?[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
                                if (reg.IsMatch(dt.Rows[i]["时间"].ToString()))
                                {
                                    entity.SecurityTime = dt.Rows[i]["时间"].ToString();
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行时间不正确" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["身份证号"].ToString()))
                            {
                                
                                Regex reg = new Regex("^(^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$)|(^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[Xx])$)$");
                                if (reg.IsMatch(dt.Rows[i]["身份证号"].ToString()))
                                {
                                    var idcard = dt.Rows[i]["身份证号"].ToString();
                                    if (_dbContext.Security.Any(x => x.IdentityCard == idcard && x.IsDeleted == 0))
                                    {
                                        responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "已存在该身份证号" + "</p></br>";
                                        defaultcount++;
                                        continue;
                                    }
                                    else
                                    {
                                        entity.IdentityCard = idcard;
                                    }
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行身份证号格式不正确" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["状态"].ToString()))
                            {
                                entity.SecurityStaues = dt.Rows[i]["状态"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["地址"].ToString()))
                            {
                                entity.SecurityAddress = dt.Rows[i]["地址"].ToString();
                            }
                            entity.IsDeleted = 0;

                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            entity.IsDeleted = 0;
                            _dbContext.Security.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;


                    DateTime endTime = DateTime.Now;
                    TimeSpan useTime = endTime - beginTime;
                    string taketime = "导入时间" + useTime.TotalSeconds.ToString() + "秒  ";
                    response.SetData(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(new
                    {
                        time = taketime,
                        successmsg = responsemsgsuccess
                        ,
                        repeatmsg = responsemsgrepeat,
                        defaultmsg = responsemsgdefault
                    })));
                    return Ok(response);
                }
                catch (Exception ex)
                {

                    response.SetFailed(ex.Message);
                    return Ok(response);
                }
            }
        }


        /// <summary>
        /// 导出信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ExportPass(string ids)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var column = new List<string>()
                    {
                        "姓名","地点","身份证号","联系电话","时间","状态"
                    };
                var column2 = new List<string>()
                    {
                        "SecurityName","SecurityAddress","IdentityCard","Phone","SecurityTime","SecurityStaues"
                    };
                if (!string.IsNullOrEmpty(ids))
                {
                    var parameters = ids.Trim().Split(",");
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = parameters[i].ToUpper();
                    }
                    var query = _dbContext.Security.Where(x => x.IsDeleted != 1 && parameters.Contains(x.SecurityUuid.ToString())).Select(x => new Security
                    {
                        SecurityName = x.SecurityName,
                        SecurityAddress = x.SecurityAddress,
                        IdentityCard = x.IdentityCard,
                        Phone = x.Phone,
                        SecurityTime = x.SecurityTime,
                        SecurityStaues = x.SecurityStaues,
                    }).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportBuidingExcel\\";
                    string uploadtitle = "安防人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";

                    DataToExcel.TablesToExcel(query, sFileName, column, column2);
                    response.SetData("\\UploadFiles\\ImportBuidingExcel\\" + uploadtitle + ".xlsx");
                    return Ok(response);
                }
                else
                {

                    var query = _dbContext.Security.Where(x => x.IsDeleted != 1).Select(x => new Security
                    {
                        SecurityName = x.SecurityName,
                        SecurityAddress = x.SecurityAddress,
                        IdentityCard = x.IdentityCard,
                        Phone = x.Phone,
                        SecurityTime = x.SecurityTime,
                        SecurityStaues = x.SecurityStaues,
                    }).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportBuidingExcel\\";
                    string uploadtitle = "安防人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";

                    DataToExcel.TablesToExcel(query, sFileName, column, column2);
                    response.SetData("\\UploadFiles\\ImportBuidingExcel\\" + uploadtitle + ".xlsx");
                    return Ok(response);
                }


            }
        }


    }
}
