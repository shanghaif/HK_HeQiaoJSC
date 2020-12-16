using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
using HaikanSmartTownCockpit.Api.RequestPayload.Intelligenttravel;
using HaikanSmartTownCockpit.Api.ViewModels.Intelligenttravel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Intelligenttravel
{
    [Route("api/v1/intelligenttravel/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class ParkinglotController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public ParkinglotController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult List(ParkingLotRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.ParkingLot
                            orderby p.AddTime
                            select new
                            {
                                p.ParkingLotUuid,
                                p.ParkingLotName,
                                p.ParkingLotAddress,
                                p.Zchewei,
                                p.Ychewei,
                                p.Schewei,
                                //p.ParkingLotsru,
                                p.IsDeleted,
                                p.Lon,
                                p.Lat,
                            };
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.ParkingLotName.Contains(payload.Kw.Trim()) || x.ParkingLotAddress.Contains(payload.Kw.Trim()));
                }

                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => x.IsDeleted == Convert.ToInt32(((CommonEnum.IsDeleted)payload.IsDeleted)));
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:停车场信息数据", _dbContext);
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
        public IActionResult Create(ParkingLotViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                //if (_dbContext.ParkingLot.Count(x => x.MemberName == model.MemberName) > 0)
                //{
                //    response.SetFailed("名称已存在");
                //    return Ok(response);
                //}

                var entity = _mapper.Map<ParkingLotViewModel, ParkingLot>(model);
                entity.ParkingLotUuid = Guid.NewGuid();
                entity.IsDeleted = 0;

                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;

                _dbContext.ParkingLot.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:停车场信息一条数据", _dbContext);
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
                var entity = _dbContext.ParkingLot.FirstOrDefault(x => x.ParkingLotUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<ParkingLot, ParkingLotViewModel>(entity));
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
        public IActionResult Edit(ParkingLotViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = _dbContext.ParkingLot.FirstOrDefault(x => x.ParkingLotUuid == model.ParkingLotUuid);
                if (entity == null)
                {
                    response.SetFailed("不存在");
                    return Ok(response);
                }
                //if (_dbContext.ParkingLot.Count(x => x.TeamName == model.TeamName && x.ParkingLotUuid != model.ParkingLotUuid) > 0)
                //{
                //    response.SetFailed("名称已存在");
                //    return Ok(response);
                //}
                entity.ParkingLotName = model.ParkingLotName;
                entity.ParkingLotAddress = model.ParkingLotAddress;
                entity.Zchewei = model.Zchewei;
                entity.Ychewei = model.Ychewei;
                entity.Schewei = model.Schewei;
                //entity.Schewei = model.Schewei;
                entity.Lon = model.Lon;
                entity.Lat = model.Lat;
                // entity.ParkingLotsru = model.ParkingLotsru;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:停车场信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE ParkingLot SET IsDeleted=@IsDel WHERE ParkingLotUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:停车场信息一条数据", _dbContext);
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
        public IActionResult ParkingLotImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportIntelligenttravelExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = "停车场信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                        if (!dt.Columns.Contains("名称"))
                        {
                            response.SetFailed("无‘名称’列");
                            return Ok(response);
                        }
                        //if (!dt.Columns.Contains("位置"))
                        //{
                        //    response.SetFailed("无‘位置’列");
                        //    return Ok(response);
                        //}
                        //if (!dt.Columns.Contains("总车位"))
                        //{
                        //    response.SetFailed("无‘总车位’列");
                        //    return Ok(response);
                        //}
                        //if (!dt.Columns.Contains("已用车位"))
                        //{
                        //    response.SetFailed("无‘已用车位’列");
                        //    return Ok(response);
                        //}
                        //if (!dt.Columns.Contains("剩余车位"))
                        //{
                        //    response.SetFailed("无‘剩余车位’列");
                        //    return Ok(response);
                        //}
                        //if (!dt.Columns.Contains("收入"))
                        //{
                        //    response.SetFailed("无‘收入’列");
                        //    return Ok(response);
                        //}
                    
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new ParkingLot();
                            entity.ParkingLotUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["名称"].ToString()))
                            {

                                entity.ParkingLotName = dt.Rows[i]["名称"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行名称为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["位置"].ToString()))
                            {

                                entity.ParkingLotAddress = dt.Rows[i]["位置"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["总车位"].ToString()))
                            {
                                entity.Zchewei = dt.Rows[i]["总车位"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["已用车位"].ToString()))
                            {
                                entity.Ychewei = dt.Rows[i]["已用车位"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["剩余车位"].ToString()))
                            {
                                entity.Schewei = dt.Rows[i]["剩余车位"].ToString();
                            }
                            //if (!string.IsNullOrEmpty(dt.Rows[i]["收入"].ToString()))
                            //{
                            //    entity.ParkingLotsru = dt.Rows[i]["收入"].ToString();
                            //}
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行收入为空" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            if (!string.IsNullOrEmpty(dt.Rows[i]["经度"].ToString()))
                            {
                                entity.Lon = dt.Rows[i]["经度"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["纬度"].ToString()))
                            {
                                entity.Lat = dt.Rows[i]["纬度"].ToString();
                            }
                            //Regex reg1 = new Regex(@"^[\-\+]?(0?\d{1,2}\.\d{1,6}|1[0-7]?\d{1}\.\d{1,6}|180\.0{1,6})$");
                            //if (reg1.IsMatch(dt.Rows[i]["经度"].ToString()))
                            //{
                            //    entity.Lon = dt.Rows[i]["经度"].ToString();
                            //}
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行经度格式错误！范围在-180.0~180.0(小数点至多6位)" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            //Regex reg2 = new Regex(@"^[\-\+]?([0-8]?\d{1}\.\d{1,6}|90\.0{1,6})$");
                            //if (reg2.IsMatch(dt.Rows[i]["纬度"].ToString()))
                            //{
                            //    entity.Lat = dt.Rows[i]["纬度"].ToString();
                            //}
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行经度格式错误！范围在-90.0~90.0(小数点至多6位)" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}

                            entity.IsDeleted = 0;
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;

                            _dbContext.ParkingLot.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:停车场信息数据", _dbContext);
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
        public IActionResult ParkingLotExport(string ids)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var column = new List<string>()
                    {
                        "名称","位置","总车位","已用车位","剩余车位","经度","纬度"
                    };
                var column2 = new List<string>()
                    {
                        "ParkingLotName","ParkingLotAddress","Zchewei","Ychewei","Schewei","Lon","Lat"
                    };
                if (!string.IsNullOrEmpty(ids))
                {
                    var parameters = ids.Trim().Split(",");
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = parameters[i].ToUpper();
                    }
                    var query1 = _dbContext.ParkingLot.Where(x => x.IsDeleted != 1 && parameters.Contains(x.ParkingLotUuid.ToString())).Select(x => new
                    {
                                            
                        Id = x.Id,
                        ParkingLotName = x.ParkingLotName,
                        ParkingLotAddress = x.ParkingLotAddress,
                        Zchewei = x.Zchewei,
                        Ychewei = x.Ychewei,
                        Schewei = x.Schewei,
                        ParkingLotsru = x.ParkingLotsru,
                        Lon = x.Lon,
                        Lat = x.Lat,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportIntelligenttravelExcel\\";
                    string uploadtitle = "停车场信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";

                    DataToExcel.TablesToExcel(query, sFileName, column, column2);
                    response.SetData("\\UploadFiles\\ImportIntelligenttravelExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:停车场信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {

                    var query1 = _dbContext.ParkingLot.Where(x => x.IsDeleted != 1).Select(x => new
                    {
                        Id = x.Id,
                        ParkingLotName = x.ParkingLotName,
                        ParkingLotAddress = x.ParkingLotAddress,
                        Zchewei = x.Zchewei,
                        Ychewei = x.Ychewei,
                        Schewei = x.Schewei,
                        ParkingLotsru = x.ParkingLotsru,
                        Lon = x.Lon,
                        Lat = x.Lat,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportIntelligenttravelExcel\\";
                    string uploadtitle = "停车场信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";

                    DataToExcel.TablesToExcel(query, sFileName, column, column2);
                    response.SetData("\\UploadFiles\\ImportIntelligenttravelExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:停车场信息数据", _dbContext);
                    return Ok(response);
                }
            }
        }
         #endregion
   }
}
