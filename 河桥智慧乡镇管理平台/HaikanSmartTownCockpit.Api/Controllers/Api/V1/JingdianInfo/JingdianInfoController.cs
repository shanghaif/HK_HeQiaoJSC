using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.logs.TolLog;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.UserInfo;
using HaikanSmartTownCockpit.Api.ViewModels.JingdianInfo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.JingdianInfo
{
    [Route("api/v1/JingdianInfo/[controller]/[action]")]
    [ApiController]
    public class JingdianInfoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;

        public JingdianInfoController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult GetList(UserInfoRequestpayload payload)
        {
            var query = from c in _dbContext.Scenic
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.ScenicUuid,
                            c.ScenicName,
                            c.ScenicGrade,
                            c.ScenicTickets,
                            c.ScenicPeoPle,
                            c.ScenicAddress,
                            c.ScenicType,
                            renshu = GetRdingdan(c.ScenicName),
                        };
            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.ScenicName.Contains(payload.Kw.Trim()));
            }
            query = query.OrderByDescending(x => x.Id);
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            ToLog.AddLog("查询", "成功:查询:景点信息数据", _dbContext);
            return Ok(response);
        }
        /// <summary>
        /// 获取当日游客量
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static int GetRdingdan(string ScenicName)
        {
            using (haikanHeQiaoContext cc = new haikanHeQiaoContext())
            {
                var d5 = DateTime.Now.ToString("yyyy-MM-dd");
                int num = cc.Tourist.Where(x => x.ScenicName == ScenicName && x.IsDeleted != 1 && x.TouristTime.Substring(0, 10) == d5).Count();
                return num;
            }
        }

 

        /// <summary>
        /// 获取当前行信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetfoGet(Guid guid)
        {
            using (_dbContext)
            {
                var query1 = from c in _dbContext.Scenic
                             where c.ScenicUuid == guid
                             select new
                             {
                                 c.Id,
                                 c.ScenicUuid,
                                 c.ScenicName,
                                 c.ScenicGrade,
                                 c.ScenicAddress,
                                 c.ScenicRemarks,
                                 c.ScenicTickets,
                                 c.ScenicQuantity,
                                 c.Lon,
                                 c.Lat,
                                 c.ScenicType,
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
        public IActionResult GetCreate(JingdianInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.Scenic();
                entity.ScenicUuid = Guid.NewGuid();
                entity.ScenicName = model.ScenicName;
                entity.ScenicGrade = model.ScenicGrade;
                entity.ScenicAddress = model.ScenicAddress;
                entity.ScenicRemarks = model.ScenicRemarks;
                entity.ScenicTickets = model.ScenicTickets;
                entity.ScenicQuantity = model.ScenicQuantity;
                if (model.ScenicType == "")
                {
                    entity.ScenicType = "正常营业";
                }
                else
                {
                    entity.ScenicType = model.ScenicType;
                }
                entity.Lon = model.Lon;
                entity.Lat = model.Lat;
                //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                //entity.AddPeople = model.addPeople;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                entity.IsDeleted = 0;
                _dbContext.Scenic.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:景点信息一条数据", _dbContext);
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
        public IActionResult GetEdit(JingdianInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            string guid = model.ScenicUuid;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.Scenic.FirstOrDefault(x => x.ScenicUuid == Guid.Parse(guid));
                entity.ScenicName = model.ScenicName;
                entity.ScenicGrade = model.ScenicGrade;
                entity.ScenicAddress = model.ScenicAddress;
                entity.ScenicRemarks = model.ScenicRemarks;
                entity.ScenicTickets = model.ScenicTickets;
                entity.ScenicQuantity = model.ScenicQuantity;
                if (model.ScenicType == "")
                {
                    entity.ScenicType = "正常营业";
                }
                else
                {
                    entity.ScenicType = model.ScenicType;
                }
                entity.Lon = model.Lon;
                entity.Lat = model.Lat;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:景点信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE Scenic SET IsDeleted=@isDeleted WHERE ScenicUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:景点信息一条数据", _dbContext);
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
        public IActionResult GetImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = " 景点信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                        if (!dt.Columns.Contains("景点名称"))
                        {
                            response.SetFailed("无‘景点名称’列");
                            return Ok(response);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new HaikanSmartTownCockpit.Api.Entities.Scenic();
                            entity.ScenicUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["景点名称"].ToString()))
                            {
                                entity.ScenicName = dt.Rows[i]["景点名称"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行景点名称为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["景点等级"].ToString()))
                            {
                                entity.ScenicGrade = dt.Rows[i]["景点等级"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["景点地址"].ToString()))
                            {
                                entity.ScenicAddress = dt.Rows[i]["景点地址"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["景点介绍"].ToString()))
                            {
                                entity.ScenicRemarks = dt.Rows[i]["景点介绍"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["门票"].ToString()))
                            {
                                entity.ScenicTickets = dt.Rows[i]["门票"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["经度"].ToString()))
                            {
                                entity.Lon = dt.Rows[i]["经度"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["纬度"].ToString()))
                            {
                                entity.Lat = dt.Rows[i]["纬度"].ToString();
                            }
                            Regex reg1 = new Regex("^(正常营业|暂停营业)$");
                            if (reg1.IsMatch(dt.Rows[i]["状态"].ToString()))
                            {
                                entity.ScenicType = dt.Rows[i]["状态"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行状态不正确,应为正常营业或暂停营业" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            //entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            entity.IsDeleted = 0;
                            _dbContext.Scenic.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;
                    ToLog.AddLog("导入", "成功:导入:景点信息数据", _dbContext);
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
                if (ids != null)
                {
                    var parameters = ids.Split(",");
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = parameters[i].ToUpper();
                    }
                    var query1 = _dbContext.Scenic.Where(x => x.IsDeleted != 1 && parameters.Contains(x.ScenicUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.Scenic
                    {
                        Id = x.Id,
                        ScenicName = x.ScenicName,
                        ScenicGrade = x.ScenicGrade,
                        ScenicAddress = x.ScenicAddress,
                        ScenicRemarks = x.ScenicRemarks,
                        ScenicTickets = x.ScenicTickets,
                        Lon = x.Lon,
                        Lat = x.Lat,
                        ScenicType = x.ScenicType,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "景点信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:景点信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.Scenic.Where(x => x.IsDeleted != 1).Select(x => new HaikanSmartTownCockpit.Api.Entities.Scenic
                    {
                        Id = x.Id,
                        ScenicName = x.ScenicName,
                        ScenicGrade = x.ScenicGrade,
                        ScenicAddress = x.ScenicAddress,
                        ScenicRemarks = x.ScenicRemarks,
                        ScenicTickets = x.ScenicTickets,
                        Lon = x.Lon,
                        Lat = x.Lat,
                        ScenicType = x.ScenicType,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "景点信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:景点信息数据", _dbContext);
                    return Ok(response);
                }


            }

        }





        public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.Scenic> query, string filename)
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

        private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.Scenic> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "景点名称","景点等级","景点地址","景点介绍","门票","经度","纬度","状态"
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
                dataRow.CreateCell(0).SetCellValue(row.ScenicName);
                dataRow.CreateCell(1).SetCellValue(row.ScenicGrade);
                dataRow.CreateCell(2).SetCellValue(row.ScenicAddress);
                dataRow.CreateCell(3).SetCellValue(row.ScenicRemarks);
                dataRow.CreateCell(4).SetCellValue(row.ScenicTickets);
                dataRow.CreateCell(5).SetCellValue(row.Lon);
                dataRow.CreateCell(6).SetCellValue(row.Lat);
                dataRow.CreateCell(7).SetCellValue(row.ScenicType);
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
