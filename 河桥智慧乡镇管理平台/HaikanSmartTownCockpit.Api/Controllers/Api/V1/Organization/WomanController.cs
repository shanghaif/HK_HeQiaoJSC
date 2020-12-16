using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.RequestPayload.Organization;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Models.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using System.Text.RegularExpressions;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Organization
{
    [Route("api/v1/Organization/[controller]/[action]")]
    [ApiController]
    public class WomanController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public WomanController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 列表显示
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(WomanRequestpayload payload)
        {
            var query = from t in _dbContext.OrganPeoInfo
                        join s in _dbContext.Organization
                        on t.OrganizationUuid equals s.OrganizationUuid
                        where t.IsDeleted != 1 && s.OrganizationName == "妇联" 
                        && t.ManType=="适龄妇女"
                        select new
                        {
                            t.Id,
                            t.OrganName,
                            t.Sex,
                            t.Birth,
                            t.Phone,
                            t.Education,
                            t.ZjWork,
                            t.Duty,
                            t.OrganPeoInfoUuid
                        };
            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.OrganName.Contains(payload.Kw));
            }
            if (payload.FirstSort != null)
            {
                query = query.OrderByDescending(x => x.Id);
            }
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            return Ok(response);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(dynamic model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var query = _dbContext.Organization.FirstOrDefault(x => x.OrganizationName == "妇联");
                var entity = new HaikanSmartTownCockpit.Api.Entities.OrganPeoInfo();
                entity.OrganPeoInfoUuid = Guid.NewGuid();
                entity.OrganName = model.organName;
                entity.Sex = model.sex;
                entity.Birth = model.birth;
                entity.IdentityCard = model.identityCard;
                entity.Phone = model.phone;
                entity.Education = model.education;
                entity.ZjWork = model.zjWork;
                entity.Duty = model.duty;
                entity.HouseAddress = model.houseAddress;
                entity.ManType = "适龄妇女";
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                entity.OrganizationUuid = query.OrganizationUuid;
                entity.IsDeleted = 0;
                _dbContext.OrganPeoInfo.Add(entity);
                _dbContext.SaveChanges();
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
        public IActionResult Edit(dynamic model)
        {
            var response = ResponseModelFactory.CreateInstance;
            Guid guid = model.organPeoInfoUuid;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.OrganPeoInfo.FirstOrDefault(x => x.OrganPeoInfoUuid == guid);
                entity.OrganName = model.organName;
                entity.Sex = model.sex;
                entity.IdentityCard = model.identityCard;
                entity.Phone = model.phone;
                entity.Education = model.education;
                entity.ZjWork = model.zjWork;
                entity.Duty = model.duty;
                entity.HouseAddress = model.houseAddress;
                if (model.birth != null)
                {
                    entity.Birth = model.birth.ToString("yyyy-MM-dd");
                }
                else
                {
                    entity.Birth = "";
                }
                _dbContext.SaveChanges();
                response.SetSuccess("修改成功");
                return Ok(response);
            }
        }

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
                var entity = _dbContext.OrganPeoInfo.FirstOrDefault(x => x.OrganPeoInfoUuid == guid);
                response.SetData(entity);
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
                var sql = string.Format("UPDATE OrganPeoInfo SET IsDeleted=@isDeleted WHERE OrganPeoInfoUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
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
        public IActionResult Import(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportBuidingExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = "适龄妇女信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                var query = _dbContext.Organization.FirstOrDefault(x => x.OrganizationName == "妇联");
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
                        if (!dt.Columns.Contains("性别"))
                        {
                            response.SetFailed("无‘性别’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("出生日期"))
                        {
                            response.SetFailed("无‘出生日期’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("学历"))
                        {
                            response.SetFailed("无‘学历’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("身份证号"))
                        {
                            response.SetFailed("无‘身份证号’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("手机号"))
                        {
                            response.SetFailed("无‘手机号’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("工作"))
                        {
                            response.SetFailed("无‘工作’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("职位"))
                        {
                            response.SetFailed("无‘职位’列");
                            return Ok(response);
                        }
                        if (!dt.Columns.Contains("地址"))
                        {
                            response.SetFailed("无‘地址’列");
                            return Ok(response);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new HaikanSmartTownCockpit.Api.Entities.OrganPeoInfo();
                            entity.OrganPeoInfoUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString()))
                            {
                                Regex reg = new Regex("^([\\u4e00-\\u9fa5]){2,7}$");
                                if (reg.IsMatch(dt.Rows[i]["姓名"].ToString()))
                                {
                                    entity.OrganName = dt.Rows[i]["姓名"].ToString();
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名格式不正确" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["性别"].ToString()))
                            {
                                if (dt.Rows[i]["性别"].ToString() != "男" && dt.Rows[i]["性别"].ToString() != "女")
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行性别不正确" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                                else
                                {
                                    entity.Sex = dt.Rows[i]["性别"].ToString();
                                }
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["出生日期"].ToString()))
                            {
                                Regex reg = new Regex("^((((1[6-9]|[2-9]\\d)\\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})-0?2-(0?[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
                                if (reg.IsMatch(dt.Rows[i]["出生日期"].ToString()))
                                {
                                    entity.Birth = dt.Rows[i]["出生日期"].ToString();
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行出生日期不正确" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["学历"].ToString()))
                            {
                                Regex reg = new Regex("^(小学|初中|中专|大专|本科|硕士|博士)$");
                                if (reg.IsMatch(dt.Rows[i]["学历"].ToString()))
                                {
                                    entity.Education = dt.Rows[i]["学历"].ToString();
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行学历类别不正确" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["身份证号"].ToString()))
                            {
                                Regex reg = new Regex("^(^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$)|(^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[Xx])$)$");
                                if (reg.IsMatch(dt.Rows[i]["身份证号"].ToString()))
                                {
                                    entity.IdentityCard = dt.Rows[i]["身份证号"].ToString();
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行身份证号格式不正确" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["手机号"].ToString()))
                            {
                                Regex reg = new Regex("^[1][3,4,5,7,8][0-9]{9}$");
                                if (reg.IsMatch(dt.Rows[i]["手机号"].ToString()))
                                {
                                    entity.Phone = dt.Rows[i]["手机号"].ToString();
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行手机号格式不正确" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["工作"].ToString()))
                            {
                                entity.ZjWork = dt.Rows[i]["工作"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["职位"].ToString()))
                            {
                                entity.Duty = dt.Rows[i]["职位"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["地址"].ToString()))
                            {
                                entity.HouseAddress = dt.Rows[i]["地址"].ToString();
                            }
                            entity.OrganizationUuid = query.OrganizationUuid;
                            entity.ManType = "适龄妇女";
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            entity.IsDeleted = 0;
                            _dbContext.OrganPeoInfo.Add(entity);
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
                if (ids != null)
                {
                    var parameters = ids.Split(",");
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = parameters[i].ToUpper();
                    }
                    var query = _dbContext.OrganPeoInfo.Where(x => x.IsDeleted != 1 && parameters.Contains(x.OrganPeoInfoUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.OrganPeoInfo
                    {
                        OrganName = x.OrganName,
                        Sex = x.Sex,
                        Birth = x.Birth,
                        IdentityCard = x.IdentityCard,
                        Phone = x.Phone,
                        Education = x.Education,
                        ZjWork = x.ZjWork,
                        Duty = x.Duty,
                        HouseAddress = x.HouseAddress
                    }).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportBuidingExcel\\";
                    string uploadtitle = "适龄妇女信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportBuidingExcel\\" + uploadtitle + ".xlsx");
                    return Ok(response);
                }
                else
                {
                    var oquery = _dbContext.Organization.FirstOrDefault(x => x.OrganizationName == "妇联");
                    var query = _dbContext.OrganPeoInfo.Where(x => x.IsDeleted != 1 && x.OrganizationUuid == oquery.OrganizationUuid && x.ManType=="适龄妇女").Select(x => new HaikanSmartTownCockpit.Api.Entities.OrganPeoInfo
                    {
                        OrganName = x.OrganName,
                        Sex = x.Sex,
                        Birth = x.Birth,
                        IdentityCard = x.IdentityCard,
                        Phone = x.Phone,
                        Education = x.Education,
                        ZjWork = x.ZjWork,
                        Duty = x.Duty,
                        HouseAddress = x.HouseAddress
                    }).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportBuidingExcel\\";
                    string uploadtitle = " 信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportBuidingExcel\\" + uploadtitle + ".xlsx");
                    return Ok(response);
                }


            }
        }

        public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.OrganPeoInfo> query, string filename)
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

            ISheet sheet = workBook.CreateSheet("适龄妇女信息表");

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

        private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.OrganPeoInfo> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "姓名","性别","出生日期","身份证号","手机号","学历","工作","职位","地址"
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
                dataRow.CreateCell(0).SetCellValue(row.OrganName);
                dataRow.CreateCell(1).SetCellValue(row.Sex);
                dataRow.CreateCell(2).SetCellValue(row.Birth);
                dataRow.CreateCell(3).SetCellValue(row.IdentityCard);
                dataRow.CreateCell(4).SetCellValue(row.Phone);
                dataRow.CreateCell(5).SetCellValue(row.Education);
                dataRow.CreateCell(6).SetCellValue(row.ZjWork);
                dataRow.CreateCell(7).SetCellValue(row.Duty);
                dataRow.CreateCell(8).SetCellValue(row.HouseAddress);
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
