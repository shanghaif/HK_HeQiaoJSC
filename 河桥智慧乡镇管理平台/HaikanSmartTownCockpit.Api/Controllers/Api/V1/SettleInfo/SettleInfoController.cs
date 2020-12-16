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
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using HaikanSmartTownCockpit.Api.logs.TolLog;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.Models.SettleInfo;
using HaikanSmartTownCockpit.Api.RequestPayload.SettleInfo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.SettleInfo
{
    [Route("api/v1/SettleInfo/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class SettleInfoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public SettleInfoController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult List(SettleInfoRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = _dbContext.SettleInfo.Where(x => x.IsDeleted != 1);
                if (!string.IsNullOrEmpty(payload.Name))
                {
                    query = query.Where(x => x.Name.Contains(payload.Name.Trim()));
                }
                if (!string.IsNullOrEmpty(payload.IdCard))
                {
                    query = query.Where(x => x.IdentityCard.Contains(payload.IdCard));
                }
                if (!string.IsNullOrEmpty(payload.STime))
                {
                    var time= Convert.ToDateTime(payload.STime).ToString("yyyy-MM-dd");
                    query = query.Where(x => x.SettleTime == time);
                }

                query = query.OrderByDescending(x => x.Id);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:落户人员信息数据", _dbContext);
                return Ok(response);
            }
        }
        #endregion

        #region 添加落户人员
        [HttpPost]
        public IActionResult Create(SettleInfoModel model)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var tBool = false;
                var query = _dbContext.Town.Where(x => x.IsDeleted != 1).ToList();
                foreach (var item in query.Where(item => model.PlaceAbode.Contains(item.TownName)))
                {
                    tBool = true;
                }
                if (!tBool)
                {
                    response.SetFailed("居住地请包含村镇");
                    return Ok(response);
                }
                var entity = new Entities.SettleInfo();
                entity.SettleUuid = Guid.NewGuid();
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                entity.IsDeleted = 0;
                entity.Name = model.Name;
                entity.Sex = model.Sex;
                if (!string.IsNullOrEmpty(model.Birthdate))
                {
                    entity.Birthdate = Convert.ToDateTime(model.Birthdate).ToString("yyyy-MM-dd");
                }
                
                entity.NativePlace = model.NativePlace;
                entity.PlaceAbode = model.PlaceAbode;
                if (!string.IsNullOrEmpty(model.SettleTime))
                {
                    entity.SettleTime = Convert.ToDateTime(model.SettleTime).ToString("yyyy-MM-dd");
                }
                entity.QianYiDe = model.QianYiDe;
                entity.IdentityCard = model.IdentityCard;
                _dbContext.SettleInfo.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:落户人员信息一条数据", _dbContext);
                }
                return Ok(response);
            }
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Show(Guid guid)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = _dbContext.SettleInfo.FirstOrDefault(x => x.SettleUuid == guid);
                response.SetData(entity);
                return Ok(response);
            }
        }
        #endregion

        #region 编辑

        [HttpPost]
        public IActionResult Edit(SettleInfoModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }

            using (_dbContext)
            {
                var tBool = false;
                var query = _dbContext.Town.Where(x => x.IsDeleted != 1).ToList();
                foreach (var item in query.Where(item => model.PlaceAbode.Contains(item.TownName)))
                {
                    tBool = true;
                }
                if (!tBool)
                {
                    response.SetFailed("居住地请包含村镇");
                    return Ok(response);
                }
                var entity = _dbContext.SettleInfo.FirstOrDefault(x => x.SettleUuid == model.SettleUuid);
                entity.Name = model.Name;
                entity.Sex = model.Sex;
                if (!string.IsNullOrEmpty(model.Birthdate))
                {
                    entity.Birthdate = Convert.ToDateTime(model.Birthdate).ToString("yyyy-MM-dd");
                }

                entity.NativePlace = model.NativePlace;
                entity.PlaceAbode = model.PlaceAbode;
                if (!string.IsNullOrEmpty(model.SettleTime))
                {
                    entity.SettleTime = Convert.ToDateTime(model.SettleTime).ToString("yyyy-MM-dd");
                }
                entity.QianYiDe = model.QianYiDe;
                entity.IdentityCard = model.IdentityCard;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:落户人员信息一条数据", _dbContext);
                }
                response.SetSuccess("修改成功");
                return Ok(response);
            }
        }
        #endregion

        #region 删除

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
                var sql = string.Format("UPDATE SettleInfo SET IsDeleted=@isDeleted WHERE SettleUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:落户人员信息一条数据", _dbContext);
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

        #endregion

        #region 导入

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="excelfile"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Import(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;
                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportBuidingExcel";
                string uploadtitle = "落户人员信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                        if (!dt.Columns.Contains("姓名"))
                        {
                            response.SetFailed("无‘姓名’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("籍贯"))
                        {
                            response.SetFailed("无‘籍贯’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("居住地"))
                        {
                            response.SetFailed("无‘居住地’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("落户时间"))
                        {
                            response.SetFailed("无‘落户时间’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("迁移地"))
                        {
                            response.SetFailed("无‘迁移地’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("身份证"))
                        {
                            response.SetFailed("无‘身份证’列");
                            return Ok(response);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString().Trim()))
                            {
                                var entity = new HaikanSmartTownCockpit.Api.Entities.SettleInfo();
                                entity.SettleUuid = Guid.NewGuid();
                                if (!string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString().Trim()))
                                {
                                    entity.Name = dt.Rows[i]["姓名"].ToString().Trim();
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名不能为空" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                                if (!string.IsNullOrEmpty(dt.Rows[i]["籍贯"].ToString().Trim()))
                                {
                                    entity.NativePlace = dt.Rows[i]["籍贯"].ToString().Trim();
                                }
                                if (!string.IsNullOrEmpty(dt.Rows[i]["居住地"].ToString().Trim()))
                                {
                                    entity.PlaceAbode = dt.Rows[i]["居住地"].ToString().Trim();
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行居住地不能为空" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                                if (!string.IsNullOrEmpty(dt.Rows[i]["落户时间"].ToString().Trim()))
                                {
                                    Regex reg = new Regex("^((((1[6-9]|[2-9]\\d)\\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})-0?2-(0?[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
                                    if (reg.IsMatch(dt.Rows[i]["落户时间"].ToString()))
                                    {
                                        entity.SettleTime = dt.Rows[i]["落户时间"].ToString().Trim();
                                    }
                                    else
                                    {
                                        responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行落户时间格式不正确" + "</p></br>";
                                        defaultcount++;
                                        continue;
                                    }
                                }
                                if (!string.IsNullOrEmpty(dt.Rows[i]["迁移地"].ToString().Trim()))
                                {
                                    entity.QianYiDe = dt.Rows[i]["迁移地"].ToString().Trim();
                                }
                                Regex regsfz = new Regex("^(^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$)|(^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[Xx])$)$");
                                if (regsfz.IsMatch(dt.Rows[i]["身份证"].ToString().Trim()))
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[i]["身份证"].ToString().Trim()))
                                    {
                                        var ddd = dt.Rows[i]["身份证"].ToString().Trim();
                                        entity.IdentityCard = dt.Rows[i]["身份证"].ToString().Trim();
                                        if (ddd != "")
                                        {
                                            var sex = "";
                                            switch (ddd.Length)
                                            {
                                                //判断身份证是否18位
                                                case 18:
                                                {
                                                    sex = ddd.Substring(14, 3);
                                                    if (sex != "")
                                                    {
                                                        entity.Sex = int.Parse(sex) % 2 == 0 ? "女" : "男";
                                                    }
                                                    var d1 = ddd.Substring(6, 4) + "-" + ddd.Substring(10, 2) + "-" + ddd.Substring(12, 2);
                                                    sex = ddd.Substring(14, 3);
                                                    entity.Birthdate = DateTime.Parse(d1).ToString("yyyy-MM-dd");
                                                    break;
                                                }
                                                //判断身份证是否15位
                                                case 15:
                                                {
                                                    sex = ddd.Substring(12, 3);
                                                    if (sex != "")
                                                    {
                                                        entity.Sex = int.Parse(sex) % 2 == 0 ? "女" : "男";
                                                    }
                                                    var d1 = "19" + ddd.Substring(6, 2) + "-" + ddd.Substring(8, 2) + "-" + ddd.Substring(10, 2);
                                                    sex = ddd.Substring(12, 3);
                                                    entity.Birthdate = DateTime.Parse(d1).ToString("yyyy-MM-dd");
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行身份证不能为空" + "</p></br>";
                                        defaultcount++;
                                        continue;
                                    }
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行身份证格式不正确" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                                
                                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                                entity.IsDeleted = 0;
                                _dbContext.SettleInfo.Add(entity);
                                _dbContext.SaveChanges();
                                successcount++;
                            }

                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;
                    ToLog.AddLog("导入", "成功:导入:落户人员信息数据", _dbContext);
                    DateTime endTime = DateTime.Now;
                    TimeSpan useTime = endTime - beginTime;
                    string taketime = "导入时间" + useTime.TotalSeconds.ToString() + "秒";
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
                    response.SetFailed(ex.Message);
                    return Ok(response);
                }
            }
        }

        #endregion

        #region 导出

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
                    var query = _dbContext.SettleInfo.Where(x => x.IsDeleted != 1 && parameters.Contains(x.SettleUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.SettleInfo()
                    {
                        Name = x.Name,
                        Sex = x.Sex,
                        Birthdate = x.Birthdate,
                        NativePlace = x.NativePlace,
                        PlaceAbode = x.PlaceAbode,
                        SettleTime = x.SettleTime,
                        QianYiDe = x.QianYiDe,
                        IdentityCard = x.IdentityCard
                    }).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportBuidingExcel\\";
                    string uploadtitle = "落户人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportBuidingExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:落户人员信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query = _dbContext.SettleInfo.Where(x => x.IsDeleted == 0).Select(x => new HaikanSmartTownCockpit.Api.Entities.SettleInfo()
                    {
                        Name = x.Name,
                        Sex = x.Sex,
                        Birthdate = x.Birthdate,
                        NativePlace = x.NativePlace,
                        PlaceAbode = x.PlaceAbode,
                        SettleTime = x.SettleTime,
                        QianYiDe = x.QianYiDe,
                        IdentityCard = x.IdentityCard
                    }).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportBuidingExcel\\";
                    string uploadtitle = "落户人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportBuidingExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:落户人员信息数据", _dbContext);
                    return Ok(response);
                }
            }
        }

        public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.SettleInfo> query, string filename)
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

            ISheet sheet = workBook.CreateSheet("Sheet1");

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

        private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.SettleInfo> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "姓名","性别","出生日期","籍贯","居住地","落户时间","迁移地","身份证"
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
                dataRow.CreateCell(0).SetCellValue(row.Name);
                dataRow.CreateCell(1).SetCellValue(row.Sex);
                dataRow.CreateCell(2).SetCellValue(row.Birthdate);
                dataRow.CreateCell(3).SetCellValue(row.NativePlace);
                dataRow.CreateCell(4).SetCellValue(row.PlaceAbode);
                dataRow.CreateCell(5).SetCellValue(row.SettleTime);
                dataRow.CreateCell(6).SetCellValue(row.QianYiDe);
                dataRow.CreateCell(7).SetCellValue(row.IdentityCard);
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

        #endregion
    }
}
