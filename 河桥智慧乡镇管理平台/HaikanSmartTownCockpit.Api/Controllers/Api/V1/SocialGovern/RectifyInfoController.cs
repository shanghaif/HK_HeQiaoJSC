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
using HaikanSmartTownCockpit.Api.RequestPayload.SocialGovern;
using HaikanSmartTownCockpit.Api.ViewModels.SocialGovern;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.SocialGovern
{
    [Route("api/v1/socialgovern/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class RectifyInfoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public RectifyInfoController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult List(RectifyInfoRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.RectifyInfo
                            orderby p.Id descending
                            select new
                            {
                                p.RectifyInfoUuid,
                                p.RectifyInfoUnit,
                                p.RectifyInfoName,
                                p.RectifyInfoStaues,
                                p.DweiPhone,
                                p.ShangbanStaues,
                                p.RectifyType,
                                p.RectifyTiem,
                                p.KaishiTime,
                                p.JiesuTime,
                                p.IsDeleted
                            };
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.RectifyInfoUnit.Contains(payload.Kw.Trim()) || x.RectifyInfoName.Contains(payload.Kw.Trim()));
                }

                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => x.IsDeleted == Convert.ToInt32(((CommonEnum.IsDeleted)payload.IsDeleted)));
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:矫正人员信息数据", _dbContext);
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
        public IActionResult Create(RectifyInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                //if (_dbContext.RectifyInfo.Count(x => x.MemberName == model.MemberName) > 0)
                //{
                //    response.SetFailed("名称已存在");
                //    return Ok(response);
                //}

                var entity = _mapper.Map<RectifyInfoViewModel, RectifyInfo>(model);
                entity.RectifyInfoUuid = Guid.NewGuid();
                entity.IsDeleted = 0;

                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;

                _dbContext.RectifyInfo.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:矫正人员信息一条数据", _dbContext);
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
                var entity = _dbContext.RectifyInfo.FirstOrDefault(x => x.RectifyInfoUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<RectifyInfo, RectifyInfoViewModel>(entity));
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
        public IActionResult Edit(RectifyInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = _dbContext.RectifyInfo.FirstOrDefault(x => x.RectifyInfoUuid == model.RectifyInfoUuid);
                if (entity == null)
                {
                    response.SetFailed("不存在");
                    return Ok(response);
                }
                //if (_dbContext.RectifyInfo.Count(x => x.TeamName == model.TeamName && x.RectifyInfoUuid != model.RectifyInfoUuid) > 0)
                //{
                //    response.SetFailed("名称已存在");
                //    return Ok(response);
                //}
                entity.RectifyInfoUnit = model.RectifyInfoUnit;
                entity.RectifyInfoName = model.RectifyInfoName;
                entity.RectifyInfoStaues = model.RectifyInfoStaues;
                entity.DweiPhone = model.DweiPhone;
                entity.ShangbanStaues = model.ShangbanStaues;
                entity.RectifyType = model.RectifyType;
                entity.RectifyTiem = model.RectifyTiem;
                entity.KaishiTime = model.KaishiTime;
                entity.JiesuTime = model.JiesuTime;
                entity.Sex = model.Sex;
                entity.Birthday = model.Birthday;
                entity.IdentityCard = model.IdentityCard;
                entity.Cirterion = model.Cirterion;
                entity.Address = model.Address;
                entity.Nation = model.Nation;
                entity.Standard = model.Standard;
                entity.Service = model.Service;
                entity.ServicingTime = model.ServicingTime;
                entity.Marriage = model.Marriage;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:矫正人员信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE RectifyInfo SET IsDeleted=@IsDel WHERE RectifyInfoUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:矫正人员信息一条数据", _dbContext);
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
        public IActionResult RectifyInfoImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;
                DateTime now;
                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportSocialGovernExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = "社区矫正信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                        if (!dt.Columns.Contains("矫正单位"))
                        {
                            response.SetFailed("无‘矫正单位’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("姓名"))
                        {
                            response.SetFailed("无‘姓名’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("矫正状态"))
                        {
                            response.SetFailed("无‘矫正状态’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("定位手机"))
                        {
                            response.SetFailed("无‘定位手机’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("司法部上报状态"))
                        {
                            response.SetFailed("无‘司法部上报状态’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("矫正类别"))
                        {
                            response.SetFailed("无‘矫正类别’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("入矫时间"))
                        {
                            response.SetFailed("无‘入矫时间’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("矫正开始时间"))
                        {
                            response.SetFailed("无‘矫正开始时间’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("入矫时间"))
                        {
                            response.SetFailed("无‘入矫时间’列");
                            return Ok(response);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var entity = new RectifyInfo();
                            entity.RectifyInfoUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["矫正单位"].ToString()))
                            {
                                entity.RectifyInfoUnit = dt.Rows[i]["矫正单位"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString()))
                            {
                                entity.RectifyInfoName = dt.Rows[i]["姓名"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["矫正状态"].ToString()))
                            {
                                entity.RectifyInfoStaues = dt.Rows[i]["矫正状态"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["定位手机"].ToString()))
                            {
                                entity.DweiPhone = dt.Rows[i]["定位手机"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["司法部上报状态"].ToString()))
                            {
                                entity.ShangbanStaues = dt.Rows[i]["司法部上报状态"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["矫正类别"].ToString()))
                            {
                                entity.RectifyType = dt.Rows[i]["矫正类别"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["入矫时间"].ToString()))
                            {
                                var t = DateTimeHandle.TransStrToDateTime(dt.Rows[i]["入矫时间"].ToString());
                                if (t == DateTime.MinValue)
                                {
                                    entity.RectifyTiem = dt.Rows[i]["入矫时间"].ToString();
                                }
                                else
                                {
                                    var tcopy = DateTimeHandle.TransformDataShort(t);
                                    entity.RectifyTiem = tcopy;
                                }
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["矫正开始时间"].ToString()))
                            {
                                var t = DateTimeHandle.TransStrToDateTime(dt.Rows[i]["矫正开始时间"].ToString());
                                if (t == DateTime.MinValue)
                                {
                                    entity.KaishiTime = dt.Rows[i]["矫正开始时间"].ToString();
                                }
                                else
                                {
                                    var tcopy = DateTimeHandle.TransformDataShort(t);
                                    entity.KaishiTime = tcopy;
                                }
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["矫正结束时间"].ToString()))
                            {
                                var t = DateTimeHandle.TransStrToDateTime(dt.Rows[i]["矫正结束时间"].ToString());
                                if (t == DateTime.MinValue)
                                {
                                    entity.JiesuTime = dt.Rows[i]["矫正结束时间"].ToString();
                                }
                                else
                                {
                                    var tcopy = DateTimeHandle.TransformDataShort(t);
                                    entity.JiesuTime = tcopy;
                                }
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["性别"].ToString()))
                            {
                                entity.Sex = dt.Rows[i]["性别"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["出生日期"].ToString()))
                            {
                                entity.Birthday = dt.Rows[i]["出生日期"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["身份证"].ToString()))
                            {
                                entity.IdentityCard = dt.Rows[i]["身份证"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["原判刑期"].ToString()))
                            {
                                entity.Cirterion = dt.Rows[i]["原判刑期"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["现住地址"].ToString()))
                            {
                                entity.Address = dt.Rows[i]["现住地址"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["民族"].ToString()))
                            {
                                entity.Nation = dt.Rows[i]["民族"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["文化程度"].ToString()))
                            {
                                entity.Standard = dt.Rows[i]["文化程度"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["有无服务成员"].ToString()))
                            {
                                entity.Service = dt.Rows[i]["有无服务成员"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["服务时间"].ToString()))
                            {
                                entity.ServicingTime = dt.Rows[i]["服务时间"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["婚姻状态"].ToString()))
                            {
                                entity.Marriage = dt.Rows[i]["婚姻状态"].ToString();
                            }
                            entity.IsDeleted = 0;
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            _dbContext.RectifyInfo.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:矫正人员信息数据", _dbContext);
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
        public IActionResult RectifyInfoExport(string ids)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var column = new List<string>()
                    {
                        "矫正单位","姓名","矫正状态","定位手机","司法部上报状态","矫正类别","入矫时间","矫正开始时间","矫正结束时间","性别","出生日期","身份证","原判刑期","现住地址","民族","文化程度","有无服务成员","服务时间","婚姻状态"
                    };
                var column2 = new List<string>()
                    {
                        "RectifyInfoUnit","RectifyInfoName","RectifyInfoStaues","DweiPhone","ShangbanStaues","RectifyType","RectifyTiem","KaishiTime","JiesuTime","Sex","Birthday","IdentityCard","Cirterion","Address","Nation","Standard","Service","ServicingTime","Marriage"
                    };
                if (!string.IsNullOrEmpty(ids))
                {
                    var parameters = ids.Trim().Split(",");
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = parameters[i].ToUpper();
                    }
                    var query = _dbContext.RectifyInfo.OrderByDescending(x => x.Id).Where(x => x.IsDeleted != 1 && parameters.Contains(x.RectifyInfoUuid.ToString())).Select(x => new
                    {
                        RectifyInfoUnit = x.RectifyInfoUnit,
                        RectifyInfoName = x.RectifyInfoName,
                        RectifyInfoStaues = x.RectifyInfoStaues,
                        DweiPhone = x.DweiPhone,
                        ShangbanStaues = x.ShangbanStaues,
                        RectifyType = x.RectifyType,
                        RectifyTiem = x.RectifyTiem,
                        KaishiTime = x.KaishiTime,
                        JiesuTime = x.JiesuTime,
                        Sex = x.Sex,
                        Birthday = x.Birthday,
                        IdentityCard = x.IdentityCard,
                        Cirterion = x.Cirterion,
                        Address = x.Address,
                        Nation = x.Nation,
                        Standard = x.Standard,
                        Service = x.Service,
                        ServicingTime = x.ServicingTime,
                        Marriage = x.Marriage
                    }).ToList();



                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportSocialGovernExcel\\";
                    string uploadtitle = "社区矫正信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";

                    DataToExcel.TablesToExcel(query, sFileName, column, column2);
                    response.SetData("\\UploadFiles\\ImportSocialGovernExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:矫正人员信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {

                    var query = _dbContext.RectifyInfo.OrderByDescending(x => x.Id).Where(x => x.IsDeleted != 1).Select(x => new
                    {
                        RectifyInfoUnit = x.RectifyInfoUnit,
                        RectifyInfoName = x.RectifyInfoName,
                        RectifyInfoStaues = x.RectifyInfoStaues,
                        DweiPhone = x.DweiPhone,
                        ShangbanStaues = x.ShangbanStaues,
                        RectifyType = x.RectifyType,
                        RectifyTiem = x.RectifyTiem,
                        KaishiTime = x.KaishiTime,
                        JiesuTime = x.JiesuTime,
                        Sex = x.Sex,
                        Birthday = x.Birthday,
                        IdentityCard = x.IdentityCard,
                        Cirterion = x.Cirterion,
                        Address = x.Address,
                        Nation = x.Nation,
                        Standard = x.Standard,
                        Service = x.Service,
                        ServicingTime = x.ServicingTime,
                        Marriage = x.Marriage
                    }).ToList();



                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportSocialGovernExcel\\";
                    string uploadtitle = "社区矫正信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";

                    DataToExcel.TablesToExcel(query, sFileName, column, column2);
                    response.SetData("\\UploadFiles\\ImportSocialGovernExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:矫正人员信息数据", _dbContext);
                    return Ok(response);
                }
            }
        }
        #endregion
    }
}
