using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Haikan3.Utils;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using HaikanSmartTownCockpit.Api.logs.TolLog;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.Emergency.Emergencyresponse;
using HaikanSmartTownCockpit.Api.ViewModels.Emergency.Emergencyresponse;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Emergency.Emergencyresponse
{
    [Route("api/v1/emergency/emergencyresponse/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class ResponseInfoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public ResponseInfoController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        #region 后台管理列表
        /// <summary>
        /// 显示所有信息
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(ResponseInfoRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.ResponseInfo
                            orderby p.AddTime
                            select new 
                            {
                                p.ResponseInfoUuid,
                                p.Village,
                                p.TongzhiRen,
                                p.XiangyingDj,
                                p.TongzhiQingk,
                                p.Phone,
                                //p.IsDeleted,
                                //p.AddTime,
                                //p.AddPeople,
                                //p.VillageMemberUuid,
                                //p.ResponseInitUuid,
                                p.State,
                                p.IsDeleted
                            };
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.Village.Contains(payload.Kw.Trim()) || x.TongzhiRen.Contains(payload.Kw.Trim()) || x.XiangyingDj.Contains(payload.Kw.Trim()) || x.TongzhiQingk.Contains(payload.Kw.Trim()));
                }
                if (payload.state > -1)
                {
                    query = query.Where(x => x.State == payload.state);
                }
                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => x.IsDeleted == Convert.ToInt32(((CommonEnum.IsDeleted)payload.IsDeleted)));
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:响应情况信息一条数据", _dbContext);
                return Ok(response);
            }
        }
        #endregion
        #region 创建
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(ResponseInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                //if (_dbContext.ResponseInfo.Count(x => x.MemberName == model.MemberName) > 0)
                //{
                //    response.SetFailed("名称已存在");
                //    return Ok(response);
                //}

                var entity = _mapper.Map<ResponseInfoViewModel, ResponseInfo>(model);
                entity.ResponseInfoUuid = Guid.NewGuid();
                entity.IsDeleted = 0;
                entity.State = 0;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;

                _dbContext.ResponseInfo.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:响应情况信息一条数据", _dbContext);
                }
                response.SetSuccess();
                return Ok(response);
            }
        }
        #endregion
        #region 编辑
       
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="guid">惟一编码</param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.ResponseInfo.FirstOrDefault(x => x.ResponseInfoUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<ResponseInfo, ResponseInfoViewModel>(entity));
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
        public IActionResult Edit(ResponseInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = _dbContext.ResponseInfo.FirstOrDefault(x => x.ResponseInfoUuid == model.ResponseInfoUuid);
                if (entity == null)
                {
                    response.SetFailed("不存在");
                    return Ok(response);
                }
                //if (_dbContext.ResponseInfo.Count(x => x.TeamName == model.TeamName && x.ResponseInfoUuid != model.ResponseInfoUuid) > 0)
                //{
                //    response.SetFailed("名称已存在");
                //    return Ok(response);
                //}
                entity.Village = model.Village;
                entity.TongzhiRen = model.TongzhiRen;
                entity.XiangyingDj = model.XiangyingDj;
                entity.TongzhiQingk = model.TongzhiQingk;
                entity.Phone = model.Phone;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:响应情况信息一条数据", _dbContext);
                }
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">ID,多个以逗号分隔</param>
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
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">ID,多个以逗号分隔</param>
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
        #region 私有方法

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE ResponseInfo SET IsDeleted=@IsDel WHERE ResponseInfoUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:响应情况信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
        #endregion

        #region 导入导出
        /// <summary>
        /// 导入信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ResponseInfoImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportEmergencyExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = "响应情况信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string sFileName = $"{uploadtitle}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                //string conStr = ConnectionStrings.DefaultConnection;
                string responsemsgsuccess = "";
                string responsemsgrepeat = "";
                string responsemsgdefault = "";
                int successcount = 0;
                int repeatcount = 0;
                int defaultcount = 0;
                //string today = DateTime.Now.ToString("yyyy-MM-dd");
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
                        if (!dt.Columns.Contains("村庄"))
                        {
                            response.SetFailed("无‘村庄’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("通知人员"))
                        {
                            response.SetFailed("无‘通知人员’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("响应等级"))
                        {
                            response.SetFailed("无‘响应等级’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("通知情况"))
                        {
                            response.SetFailed("无‘通知情况’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("联系电话"))
                        {
                            response.SetFailed("无‘联系电话’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("状态"))
                        {
                            response.SetFailed("无‘状态’列");
                            return Ok(response);
                        }


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new ResponseInfo();
                            entity.ResponseInfoUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["村庄"].ToString()))
                            {
                                
                                entity.Village = dt.Rows[i]["村庄"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行村庄为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["通知人员"].ToString()))
                            {

                                entity.TongzhiRen = dt.Rows[i]["通知人员"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行通知人员为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["响应等级"].ToString()))
                            {
                                entity.XiangyingDj = dt.Rows[i]["响应等级"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行响应等级为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["通知情况"].ToString()))
                            {
                                entity.TongzhiQingk = dt.Rows[i]["通知情况"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行通知情况为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["联系电话"].ToString()))
                            {
                                entity.Phone = dt.Rows[i]["联系电话"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行联系电话为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[i]["状态"].ToString()))
                            {
                                if (dt.Rows[i]["状态"].ToString() == "已响应")
                                    entity.State = 1;
                                else
                                    entity.State = 0;
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行状态为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }

                            entity.IsDeleted = 0;
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;

                            _dbContext.ResponseInfo.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:响应情况信息数据", _dbContext);
                    //DateTime endTime = DateTime.Now;
                    //TimeSpan useTime = endTime - beginTime;
                    //string taketime = "导入时间" + useTime.TotalSeconds.ToString() + "秒  ";
                    response.SetData(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(new
                    {
                        //time = taketime,
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
        public IActionResult ResponseInfoExport(string ids)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var column = new List<string>()
                    {
                        "通知村庄","通知人员","响应等级","通知情况","联系电话","状态"
                    };
                var column2 = new List<string>()
                    {
                        "Village","TongzhiRen","XiangyingDj","TongzhiQingk","Phone","State"
                    };
                if (!string.IsNullOrEmpty(ids))
                {
                    var parameters = ids.Trim().Split(",");
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = parameters[i].ToUpper();
                    }
                    var query = _dbContext.ResponseInfo.Where(x => x.IsDeleted != 1 && parameters.Contains(x.ResponseInfoUuid.ToString())).Select(x => new
                    {
                        Village = x.Village,
                        TongzhiRen = x.TongzhiRen,
                        XiangyingDj = x.XiangyingDj,
                        TongzhiQingk = x.TongzhiQingk,
                        Phone = x.Phone,
                        State = x.State == 1 ? "已响应" : "未响应"
                    }).ToList();
                   
                    

                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportEmergencyExcel\\";
                    string uploadtitle = "响应情况信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";

                    DataToExcel.TablesToExcel(query, sFileName, column, column2);
                    response.SetData("\\UploadFiles\\ImportEmergencyExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:响应情况信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {

                    var query = _dbContext.ResponseInfo.Where(x => x.IsDeleted != 1).Select(x => new
                    {
                        Village = x.Village,
                        TongzhiRen = x.TongzhiRen,
                        XiangyingDj = x.XiangyingDj,
                        TongzhiQingk = x.TongzhiQingk,
                        Phone = x.Phone,
                        State = x.State == 1 ? "已响应" : "未响应"
                    }).ToList();


                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportEmergencyExcel\\";
                    string uploadtitle = "响应情况信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";

                    DataToExcel.TablesToExcel(query, sFileName, column, column2);
                    response.SetData("\\UploadFiles\\ImportEmergencyExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:响应情况信息数据", _dbContext);
                    return Ok(response);
                }
            }
        }
        #endregion
    }
}
