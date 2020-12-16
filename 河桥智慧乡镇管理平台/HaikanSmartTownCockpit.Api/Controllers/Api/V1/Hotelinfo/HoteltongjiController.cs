using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Models.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using HaikanSmartTownCockpit.Api.RequestPayload.UserInfo;
using HaikanSmartTownCockpit.Api.ViewModels.Hotelinfo;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Hotelinfo
{
    [Route("api/v1/Hotelinfo/[controller]/[action]")]
    [ApiController]
    public class HoteltongjiController : ControllerBase
    {

        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public HoteltongjiController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        ///  列表显示
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RuzhuList(UserInfoRequestpayload payload)
        {
            var query = from c in _dbContext.Hotel
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.HotelName,
                            c.HotelUuid,
                            Rdingdan = GetRdingdan(c.HotelName),
                            Rrenshu = GetRrenshu(c.HotelName),
                            Ydingdan = GetYdingdan(c.HotelName),
                            Yrenshu = GetYrenshu(c.HotelName),
                        };

            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.HotelName.Contains(payload.Kw.Trim()));
            }
            //if (payload.FirstSort != null)
            //{
            //    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
            //}
            query = query.OrderByDescending(x => x.Id);
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            ToLog.AddLog("查询", "成功:查询:酒店入住信息数据", _dbContext);
            return Ok(response);
        }
        /// <summary>
        /// 获取酒店当日订单总数
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static int GetRdingdan(string HotelName)
        {
            using (haikanHeQiaoContext cc = new haikanHeQiaoContext())
            {
                var d5 = DateTime.Now.ToString("yyyy-MM-dd");
                int num = cc.RuzhuInfo.Where(x => x.HotelName == HotelName && x.IsDeleted != 1 && x.RuzhuTime.Substring(0, 10) == d5).Count();
                return num;
            }
        }

        /// <summary>
        /// 获取酒店当月订单总数
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static int GetYdingdan(string HotelName)
        {
            using (haikanHeQiaoContext cc = new haikanHeQiaoContext())
            {
                var d5 = DateTime.Now.ToString("yyyy-MM");
                int num = cc.RuzhuInfo.Where(x => x.HotelName == HotelName && x.IsDeleted != 1 && x.RuzhuTime.Substring(0, 7) == d5).Count();
                return num;
            }
        }
        /// <summary>
        /// 获取酒店当日入住总人数
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static int GetRrenshu(string HotelName)
        {
            using (haikanHeQiaoContext cc = new haikanHeQiaoContext())
            {
                var d5 = DateTime.Now.ToString("yyyy-MM-dd");
                int num = cc.RuzhuInfo.Where(x => x.HotelName == HotelName && x.IsDeleted != 1 && x.RuzhuTime.Substring(0, 10) == d5).Select(x=> new { x.RuzhuPeople }).Sum(x => Convert.ToInt32(x.RuzhuPeople) );

                return num;
            }
        }
        /// <summary>
        /// 获取酒店当月入住总人数
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static int GetYrenshu(string HotelName)
        {
            using (haikanHeQiaoContext cc = new haikanHeQiaoContext())
            {
                var d5 = DateTime.Now.ToString("yyyy-MM");
                int num = cc.RuzhuInfo.Where(x => x.HotelName == HotelName && x.IsDeleted != 1 && x.RuzhuTime.Substring(0, 7) == d5).Select(x => new { x.RuzhuPeople }).Sum(x => Convert.ToInt32(x.RuzhuPeople));
                return num;
            }
        }


        /// <summary>
        /// 获取酒店入住详情
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult HoruzhuGet(UserInfoRequestpayload payload)
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.RuzhuInfo
                             where c.HotelName == payload.Kw2 && c.IsDeleted == 0
                             select new
                             {
                                 c.Id,
                                 c.RuzhuName,
                                 c.RuzhuPeople,
                                 c.RuzhuRoom,
                                 c.RuzhuMoney,
                                 c.RuzhuDay,
                                 c.RuzhuTime,
                                 c.LikaiTime,
                                 c.RuzhuInfoUuid,
                                 ztianshu = Gettianshu(c.RuzhuInfoUuid),
                                 c.HotelName,
                             };
                if (!string.IsNullOrEmpty(payload.Kw3))
                {
                    query = query.Where(x => x.RuzhuName.Contains(payload.Kw3.Trim()));
                }
                //if (!string.IsNullOrEmpty(payload.Kw4[0]))
                //{
                //    DateTime d1 = DateTime.Parse(payload.Kw4[0]);
                //    DateTime d2 = DateTime.Parse(payload.Kw4[1]);
                //    d2 = d2.AddDays(1);
                //    query = query.Where(x => DateTime.Parse(x.RuzhuTime) >= d1 && DateTime.Parse(x.RuzhuTime) <= d2);
                //}
                query = query.OrderByDescending(x => x.Id);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取酒店当月入住总人数
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static int Gettianshu(Guid RuzhuInfoUuid)
        {
            using (haikanHeQiaoContext cc = new haikanHeQiaoContext())
            {
                var dd = cc.RuzhuInfo.FirstOrDefault(x => x.RuzhuInfoUuid == RuzhuInfoUuid);
                DateTime d1 = Convert.ToDateTime(dd.RuzhuTime);
                DateTime d2 = Convert.ToDateTime(dd.LikaiTime);
                DateTime d3 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d1.Year, d1.Month, d1.Day));
                DateTime d4 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d2.Year, d2.Month, d2.Day));
                int num = (d4 - d3).Days;
                return num;
            }
        }

        /// <summary>
        /// 获取当前行信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RuzhufoGet(Guid guid)
        {
            using (_dbContext)
            {
                var query1 = from c in _dbContext.RuzhuInfo
                             where c.RuzhuInfoUuid == guid
                             select new
                             {
                                 c.Id,
                                 c.RuzhuName,
                                 c.RuzhuPeople,
                                 c.RuzhuRoom,
                                 c.RuzhuMoney,
                                 c.RuzhuDay,
                                 rtime = c.RuzhuTime,
                                 ltime = c.LikaiTime,
                                 c.RuzhuInfoUuid,
                                 c.HotelName,
                             };
                var query = query1.ToList();
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RuzhuCreate(HoteltongjiViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.RuzhuInfo();
                entity.RuzhuInfoUuid = Guid.NewGuid();
                entity.RuzhuName = model.RuzhuName;
                entity.RuzhuPeople = model.RuzhuPeople;
                entity.RuzhuRoom = model.RuzhuRoom;
                entity.RuzhuMoney = model.RuzhuMoney;
                entity.RuzhuTime = DateTime.Parse(model.RuzhuTime[0]).ToString("yyyy-MM-dd");
                entity.LikaiTime = DateTime.Parse(model.RuzhuTime[1]).ToString("yyyy-MM-dd");
                if (entity.RuzhuTime != null && entity.LikaiTime != null)
                {
                    DateTime d1 = Convert.ToDateTime(entity.RuzhuTime);
                    DateTime d2 = Convert.ToDateTime(entity.LikaiTime);
                    DateTime d3 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d1.Year, d1.Month, d1.Day));
                    DateTime d4 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d2.Year, d2.Month, d2.Day));
                    int num = (d4 - d3).Days;
                    entity.RuzhuDay = num.ToString();
                }
                entity.HotelName = model.HotelName;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                //entity.AddPeople = model.addPeople;
                entity.IsDeleted = 0;
                _dbContext.RuzhuInfo.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:酒店入住信息一条数据", _dbContext);
                }
                response.SetSuccess("添加成功");
                return Ok(response);
            }
        }




        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RuzhuEdit(HoteltongjiViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            string guid = model.RuzhuInfoUuid;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.RuzhuInfo.FirstOrDefault(x => x.RuzhuInfoUuid == Guid.Parse(guid));
                entity.RuzhuName = model.RuzhuName;
                entity.RuzhuPeople = model.RuzhuPeople;
                entity.RuzhuRoom = model.RuzhuRoom;
                entity.RuzhuMoney = model.RuzhuMoney;
                entity.RuzhuDay = model.RuzhuDay;
                entity.RuzhuTime = DateTime.Parse(model.RuzhuTime[0]).ToString("yyyy-MM-dd");
                entity.LikaiTime = DateTime.Parse(model.RuzhuTime[1]).ToString("yyyy-MM-dd");
                if (entity.RuzhuTime != null && entity.LikaiTime != null)
                {
                    DateTime d1 = Convert.ToDateTime(entity.RuzhuTime);
                    DateTime d2 = Convert.ToDateTime(entity.LikaiTime);
                    DateTime d3 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d1.Year, d1.Month, d1.Day));
                    DateTime d4 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d2.Year, d2.Month, d2.Day));
                    int num = (d4 - d3).Days;
                    entity.RuzhuDay = num.ToString();
                }
                entity.HotelName = model.HotelName;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:酒店入住信息一条数据", _dbContext);
                }
                response.SetSuccess("修改成功");
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除单个角色
        /// </summary>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
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
                var sql = string.Format("UPDATE RuzhuInfo SET IsDeleted=@isDeleted WHERE RuzhuInfoUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:酒店入住信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        /// <summary>
        /// 删除多条批量操作
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
        /// 导入信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RuzhuImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = " 民宿入住信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                        if (!dt.Columns.Contains("入住登记姓名"))
                        {
                            response.SetFailed("无‘入住登记姓名’列");
                            return Ok(response);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new HaikanSmartTownCockpit.Api.Entities.RuzhuInfo();
                            entity.RuzhuInfoUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["入住登记姓名"].ToString()))
                            {
                                entity.RuzhuName = dt.Rows[i]["入住登记姓名"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行入住登记姓名为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["入住人数"].ToString()))
                            {
                                entity.RuzhuPeople = dt.Rows[i]["入住人数"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["房间号"].ToString()))
                            {
                                entity.RuzhuRoom = dt.Rows[i]["房间号"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["价格"].ToString()))
                            {
                                entity.RuzhuMoney = dt.Rows[i]["价格"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["入住时间"].ToString()))
                            {
                                entity.RuzhuTime = dt.Rows[i]["入住时间"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行入住时间为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["离开时间"].ToString()))
                            {
                                entity.LikaiTime = dt.Rows[i]["离开时间"].ToString();
                                if (entity.RuzhuTime != null && entity.LikaiTime != null)
                                {
                                    DateTime d1 = Convert.ToDateTime(entity.RuzhuTime);
                                    DateTime d2 = Convert.ToDateTime(entity.LikaiTime);
                                    DateTime d3 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d1.Year, d1.Month, d1.Day));
                                    DateTime d4 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d2.Year, d2.Month, d2.Day));
                                    int num = (d4 - d3).Days;
                                    entity.RuzhuDay = num.ToString();
                                }
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行离开时间为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["民宿名称"].ToString()))
                            {
                                entity.HotelName = dt.Rows[i]["民宿名称"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行民宿名称为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            //entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            entity.IsDeleted = 0;
                            _dbContext.RuzhuInfo.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:酒店入住信息数据", _dbContext);
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
        public IActionResult ExportPass(string ids,string htname)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                if (ids != null)
                {
                    var parameters = ids.Split(",");
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = parameters[i].ToUpper();
                    }
                    var query1 = _dbContext.RuzhuInfo.Where(x => x.IsDeleted != 1 && parameters.Contains(x.RuzhuInfoUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.RuzhuInfo
                    {
                        Id = x.Id,
                        RuzhuName = x.RuzhuName,
                        RuzhuPeople = x.RuzhuPeople,
                        RuzhuRoom = x.RuzhuRoom,
                        RuzhuMoney = x.RuzhuMoney,
                        RuzhuDay = x.RuzhuDay,
                        RuzhuTime = x.RuzhuTime,
                        LikaiTime = x.LikaiTime,
                        HotelName = x.HotelName,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "民宿入住信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:酒店入住信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.RuzhuInfo.Where(x => x.IsDeleted != 1 && x.HotelName == htname).Select(x => new HaikanSmartTownCockpit.Api.Entities.RuzhuInfo
                    {
                        Id = x.Id,
                        RuzhuName = x.RuzhuName,
                        RuzhuPeople = x.RuzhuPeople,
                        RuzhuRoom = x.RuzhuRoom,
                        RuzhuMoney = x.RuzhuMoney,
                        RuzhuDay = x.RuzhuDay,
                        RuzhuTime = x.RuzhuTime,
                        LikaiTime = x.LikaiTime,
                        HotelName = x.HotelName,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "民宿入住信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:酒店入住信息数据", _dbContext);
                    return Ok(response);
                }


            }

        }





        public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.RuzhuInfo> query, string filename)
        {
            MemoryStream ms = new MemoryStream();

            IWorkbook workBook;
            //IWorkbook workBook=WorkbookFactory.Create(filename);
            string suffix = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - filename.LastIndexOf(".") - 1);
            if (suffix == "xls")
            {
                workBook = new HSSFWorkbook();
            }
            else
                workBook = new XSSFWorkbook();

            ISheet sheet = workBook.CreateSheet(" 表");

            CreatSheet(sheet, query);


            workBook.Write(ms);
            try
            {
                SaveToFile(ms, filename);
                ms.Flush();
                return true;
            }
            catch
            {
                ms.Flush();
                throw;
            }

        }

        private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.RuzhuInfo> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "入住登记姓名","入住人数","房间号","价格","入住时间","离开时间","民宿名称"
            };

            //表头
            for (int i = 0; i < list.Count; i++)
            {
                headerRow.CreateCell(i).SetCellValue(list[i]);
            }

            int rowIndex = 1;
            foreach (var row in query)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(row.RuzhuName);
                dataRow.CreateCell(1).SetCellValue(row.RuzhuPeople);
                dataRow.CreateCell(2).SetCellValue(row.RuzhuRoom);
                dataRow.CreateCell(3).SetCellValue(row.RuzhuMoney);
                dataRow.CreateCell(4).SetCellValue(row.RuzhuTime);
                dataRow.CreateCell(5).SetCellValue(row.LikaiTime);
                dataRow.CreateCell(6).SetCellValue(row.HotelName);
                rowIndex++;
            }
        }
        private static void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();         //转为字节数组 
                fs.Write(data, 0, data.Length);     //保存为Excel文件
                fs.Flush();
                data = null;
            }
        }
    }
}
