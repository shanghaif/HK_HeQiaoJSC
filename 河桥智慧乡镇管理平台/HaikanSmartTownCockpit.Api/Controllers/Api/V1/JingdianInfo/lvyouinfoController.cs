using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.UserInfo;
using HaikanSmartTownCockpit.Api.ViewModels.JingdianInfo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using NPOI.HSSF.Record;
using System.Text.RegularExpressions;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.JingdianInfo
{
    [Route("api/v1/JingdianInfo/[controller]/[action]")]
    [ApiController]
    public class lvyouinfoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;

        public lvyouinfoController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
            var query = from c in _dbContext.Tourist
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.TouristUuid,
                            c.ScenicName,
                            c.TouristMoney,
                            c.TouristTime,
                            c.TouristName,
                        };
            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.TouristName.Contains(payload.Kw.Trim()));
            }
            query = query.OrderByDescending(x => x.Id);
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            ToLog.AddLog("查询", "成功:查询:旅游收入信息信息数据", _dbContext);
            return Ok(response);
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
                var query1 = from c in _dbContext.Tourist
                             where c.TouristUuid == guid
                             select new
                             {
                                 c.Id,
                                 c.TouristUuid,
                                 c.ScenicName,
                                 c.TouristMoney,
                                 c.TouristName,
                                 c.TouristTime,
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
                var entity = new HaikanSmartTownCockpit.Api.Entities.Tourist();
                entity.TouristUuid = Guid.NewGuid();
                entity.ScenicName = model.ScenicName;
                if (model.TouristMoney != "") {
                    entity.TouristMoney = double.Parse(model.TouristMoney);
                }

                entity.TouristName = model.TouristName;
                if (model.TouristTime != "")
                {
                    entity.TouristTime = DateTime.Parse(model.TouristTime).ToString("yyyy-MM-dd");
                    entity.TjYear = DateTime.Parse(model.TouristTime).Year.ToString();
                    entity.TjMouth = DateTime.Parse(model.TouristTime).Month.ToString();
                    entity.TjJidu = Jidu(DateTime.Parse(model.TouristTime).Month);
                }
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                entity.IsDeleted = 0;
                _dbContext.Tourist.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:旅游收入信息一条数据", _dbContext);
                }
                response.SetSuccess("添加成功");
                return Ok(response);
            }
        }

        public static string Jidu(int ttime)
        {
            string ds = "时间不规范,清规范时间";
            if (ttime>0&&ttime<4)
            {
                ds = "第一季度";
                return ds;
            }
            if (ttime > 3 && ttime < 7)
            {
                ds = "第二季度";
                return ds;
            }
            if (ttime > 6 && ttime < 10)
            {
                ds = "第三季度";
                return ds;
            }
            if (ttime > 9 && ttime < 13)
            {
                ds = "第四季度";
                return ds;
            }
            return ds;
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
            string guid = model.TouristUuid;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.Tourist.FirstOrDefault(x => x.TouristUuid == Guid.Parse(guid));
                entity.ScenicName = model.ScenicName;
                if (model.TouristMoney != "")
                {
                    entity.TouristMoney = double.Parse(model.TouristMoney);
                }
                entity.TouristName = model.TouristName;
                if (model.TouristTime != "")
                {
                    entity.TouristTime = DateTime.Parse(model.TouristTime).ToString("yyyy-MM-dd");
                    entity.TjYear = DateTime.Parse(model.TouristTime).Year.ToString();
                    entity.TjMouth = DateTime.Parse(model.TouristTime).Month.ToString();
                    entity.TjJidu = Jidu(DateTime.Parse(model.TouristTime).Month);
                }
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:旅游收入信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE Tourist SET IsDeleted=@isDeleted WHERE TouristUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:旅游收入信息一条数据", _dbContext);
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
                string uploadtitle = " 游客信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                        if (!dt.Columns.Contains("游客姓名"))
                        {
                            response.SetFailed("无‘游客姓名’列");
                            return Ok(response);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new HaikanSmartTownCockpit.Api.Entities.Tourist();
                            entity.TouristUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["游客姓名"].ToString()))
                            {
                                entity.TouristName = dt.Rows[i]["游客姓名"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行游客姓名为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
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
                            if (!string.IsNullOrEmpty(dt.Rows[i]["金额"].ToString()))
                            {
                                string je = dt.Rows[i]["金额"].ToString();
                                entity.TouristMoney = double.Parse(je);
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行金额为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            Regex regbr = new Regex("^((((1[6-9]|[2-9]\\d)\\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})-0?2-(0?[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
                            if (regbr.IsMatch(dt.Rows[i]["时间"].ToString()))
                            {
                                var datime = dt.Rows[i]["时间"].ToString();
                                entity.TouristTime = dt.Rows[i]["时间"].ToString();
                                if (entity.TouristTime != "")
                                {
                                    entity.TouristTime = DateTime.Parse(datime).ToString("yyyy-MM-dd");
                                    entity.TjYear = DateTime.Parse(datime).Year.ToString();
                                    entity.TjMouth = DateTime.Parse(datime).Month.ToString();
                                    entity.TjJidu = Jidu(DateTime.Parse(datime).Month);
                                }
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行时间格式不正确" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            //entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            entity.IsDeleted = 0;
                            _dbContext.Tourist.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:旅游收入信息数据", _dbContext);
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
                    var query1 = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && parameters.Contains(x.TouristUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.Tourist
                    {
                        Id = x.Id,
                        ScenicName = x.ScenicName,
                        TouristMoney = x.TouristMoney,
                        TouristName = x.TouristName,
                        TouristTime = x.TouristTime,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "游客信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:旅游收入信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.Tourist.Where(x => x.IsDeleted != 1).Select(x => new HaikanSmartTownCockpit.Api.Entities.Tourist
                    {
                        Id = x.Id,
                        ScenicName = x.ScenicName,
                        TouristMoney = x.TouristMoney,
                        TouristName = x.TouristName,
                        TouristTime = x.TouristTime,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "游客信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:旅游收入信息数据", _dbContext);
                    return Ok(response);
                }


            }

        }





        public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.Tourist> query, string filename)
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

        private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.Tourist> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "游客姓名","景点名称","金额","时间"
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
                dataRow.CreateCell(0).SetCellValue(row.TouristName);
                dataRow.CreateCell(1).SetCellValue(row.ScenicName);
                dataRow.CreateCell(2).SetCellValue((double)row.TouristMoney);
                dataRow.CreateCell(3).SetCellValue(row.TouristTime);
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
