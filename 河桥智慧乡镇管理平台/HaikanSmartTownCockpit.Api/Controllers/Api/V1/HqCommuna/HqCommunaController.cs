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
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.UserInfo;
using HaikanSmartTownCockpit.Api.ViewModels.HqCommuna;
using HaikanSmartTownCockpit.ViewModels.Cuisine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.HqCommuna
{
    [Route("api/v1/HqCommuna/[controller]/[action]")]
    [ApiController]
    public class HqCommunaController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public HqCommunaController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult HqCommunaList(UserInfoRequestpayload payload)
        {
            var query = from c in _dbContext.HqCommunal
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.HqCommunalName,         
                            c.HqCommunalId,          
                            c.HqCommunalStaues,            
                            c.HqCommunalLocation,            
                            c.HqCommunalType,              
                            c.Lon,            
                            c.Lat,     
                            c.HqCommunalUuid,
                        };
            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.HqCommunalName.Contains(payload.Kw.Trim()));
            }
            if (!string.IsNullOrEmpty(payload.Kw1))
            {
                query = query.Where(x => x.HqCommunalLocation.Contains(payload.Kw1.Trim()));
            }
            if (!string.IsNullOrEmpty(payload.Kw2))
            {
                query = query.Where(x => x.HqCommunalType.Contains(payload.Kw2.Trim()));
            }
            query = query.OrderByDescending(x => x.Id);
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            ToLog.AddLog("查询", "成功:查询:公共设施信息一条数据", _dbContext);
            return Ok(response);
        }


        /// <summary>
        /// 获取当前行信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult HqCommunafoGet(Guid guid)
        {
            using (_dbContext)
            {
                var query1 = from c in _dbContext.HqCommunal
                             where c.HqCommunalUuid == guid
                             select new
                            {
                                c.Id,
                                c.HqCommunalName,
                                c.HqCommunalId,
                                c.HqCommunalStaues,
                                c.HqCommunalLocation,
                                c.HqCommunalType,
                                c.Lon,
                                c.Lat,
                                c.HqCommunalUuid,
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
        public IActionResult HqCommunaCreate(HqCommunaViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.HqCommunal();
                entity.HqCommunalUuid = Guid.NewGuid();
                entity.HqCommunalName = model.HqCommunalName;
                entity.HqCommunalId = model.HqCommunalId;
                entity.HqCommunalStaues = model.HqCommunalStaues;
                entity.HqCommunalLocation = model.HqCommunalLocation;
                entity.HqCommunalType = model.HqCommunalType;
                entity.Lon = model.Lon;
                entity.Lat = model.Lat;
                //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                //entity.AddPeople = model.addPeople;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                entity.IsDeleted = 0;
                _dbContext.HqCommunal.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:公共设施信息一条数据", _dbContext);
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
        public IActionResult HqCommunaEdit(HqCommunaViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            string guid = model.HqCommunalUuid;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.HqCommunal.FirstOrDefault(x => x.HqCommunalUuid == Guid.Parse(guid));
                entity.HqCommunalName = model.HqCommunalName;
                entity.HqCommunalId = model.HqCommunalId;
                entity.HqCommunalStaues = model.HqCommunalStaues;
                entity.HqCommunalLocation = model.HqCommunalLocation;
                entity.HqCommunalType = model.HqCommunalType;
                entity.Lon = model.Lon;
                entity.Lat = model.Lat;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:公共设施信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE HqCommunal SET IsDeleted=@isDeleted WHERE HqCommunalUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:公共设施信息一条数据", _dbContext);
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
        public IActionResult HqCommunaImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = " 公共设施信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                        if (!dt.Columns.Contains("设施名称"))
                        {
                            response.SetFailed("无‘设施名称’列");
                            return Ok(response);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new HaikanSmartTownCockpit.Api.Entities.HqCommunal();
                            entity.HqCommunalUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["设施名称"].ToString()))
                            {
                                entity.HqCommunalName = dt.Rows[i]["设施名称"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行设施名称为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["设施编号"].ToString()))
                            {
                                entity.HqCommunalId = dt.Rows[i]["设施编号"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["设施状态"].ToString()))
                            {
                                entity.HqCommunalStaues = dt.Rows[i]["设施状态"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["设施位置"].ToString()))
                            {
                                entity.HqCommunalLocation = dt.Rows[i]["设施位置"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["设施类型"].ToString()))
                            {
                                entity.HqCommunalType = dt.Rows[i]["设施类型"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["经度"].ToString()))
                            {
                                entity.Lon = dt.Rows[i]["经度"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["纬度"].ToString()))
                            {
                                entity.Lat = dt.Rows[i]["纬度"].ToString();
                            }
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行价格为空" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            //entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            entity.IsDeleted = 0;
                            _dbContext.HqCommunal.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:公共设施信息数据", _dbContext);
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
                    var query1 = _dbContext.HqCommunal.Where(x => x.IsDeleted != 1 && parameters.Contains(x.HqCommunalUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.HqCommunal
                    {
                        Id = x.Id,
                        HqCommunalName = x.HqCommunalName,
                        HqCommunalId = x.HqCommunalId,
                        HqCommunalStaues = x.HqCommunalStaues,
                        HqCommunalLocation = x.HqCommunalLocation,
                        HqCommunalType = x.HqCommunalType,
                        Lon = x.Lon,
                        Lat = x.Lat,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "公共设施信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:公共设施信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.HqCommunal.Where(x => x.IsDeleted != 1).Select(x => new HaikanSmartTownCockpit.Api.Entities.HqCommunal
                    {
                        Id = x.Id,
                        HqCommunalName = x.HqCommunalName,
                        HqCommunalId = x.HqCommunalId,
                        HqCommunalStaues = x.HqCommunalStaues,
                        HqCommunalLocation = x.HqCommunalLocation,
                        HqCommunalType = x.HqCommunalType,
                        Lon = x.Lon,
                        Lat = x.Lat,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "公共设施信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:公共设施信息数据", _dbContext);
                    return Ok(response);
                }


            }

        }





        public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.HqCommunal> query, string filename)
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

        private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.HqCommunal> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "设施名称","设施编号","设施状态","设施位置","设施类型","经度","纬度"
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
                dataRow.CreateCell(0).SetCellValue(row.HqCommunalName);
                dataRow.CreateCell(1).SetCellValue(row.HqCommunalId);
                dataRow.CreateCell(2).SetCellValue(row.HqCommunalStaues);
                dataRow.CreateCell(3).SetCellValue(row.HqCommunalLocation);
                dataRow.CreateCell(4).SetCellValue(row.HqCommunalType);
                dataRow.CreateCell(5).SetCellValue(row.Lon);
                dataRow.CreateCell(6).SetCellValue(row.Lat);
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
