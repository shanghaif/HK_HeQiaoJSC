using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Models.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.RequestPayload.UserInfo;
using HaikanSmartTownCockpit.ViewModels.Cuisine;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;
using System.Text.RegularExpressions;
using HaikanSmartTownCockpit.Api.ViewModels.DangJibenInfo;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.DangJibenInfo
{
    [Route("api/v1/DangJibenInfo/[controller]/[action]")]
    [ApiController]
    public class CpcmanInfoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        //private IHostingEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public CpcmanInfoController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult DangJibenInfoList(UserInfoRequestpayload payload)
        {
            var query = from c in _dbContext.CpcmanInfo
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.DangOrganizationName,//党组织
                            c.RealName,         //姓名
                            c.Sex,              //性别
                            Birth = c.Birth == null ? "" : c.Birth != null ? c.Birth.Value.ToString("yyyy-MM-dd") : "",    //生日
                            c.Education,        //学历
                            c.Politics,         //政治面貌
                            //c.Age,
                            c.CpcmanUuid,
                        };

            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.RealName.Contains(payload.Kw.Trim()));
            }
            if (!string.IsNullOrEmpty(payload.Kw2))
            {
                query = query.Where(x => x.Education.Contains(payload.Kw2.Trim()));
            }
            query = query.OrderByDescending(x => x.Id);
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            ToLog.AddLog("查询", "成功:查询:党员列表数据", _dbContext);
            return Ok(response);
        }

        /// <summary>
        /// 获取全部党组织
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult dangzuzhi()
        {
            using (_dbContext)
            {
                var entity = from c in _dbContext.DangOrganization
                             where c.IsDeleted == 0
                             select new
                             {
                                 Label = c.DangOrganizationName,
                                 Value = c.DangOrganizationName,
                             };
                var query = entity.ToList();
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(query);
                return Ok(response);
            }
        }
        /// <summary>
        /// 获取当前行信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DangJibenInfoGet(Guid guid)
        {
            using (_dbContext)
            {
                //var entity = _dbContext.Cuisine.FirstOrDefault(x => x.CuisineUuid == guid);
                var entity = from c in _dbContext.CpcmanInfo
                             where c.CpcmanUuid == guid
                             select new
                             {
                                 c.Id,
                                 c.RealName,         //姓名
                                 c.Sex,              //性别
                                 c.Birth,            //生日
                                 c.Education,        //学历
                                 c.Politics,         //政治面貌
                                 c.DangOrganizationName,
                                 c.Age,
                                 c.CpcmanUuid,
                             };
                var query = entity.ToList();
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
        public IActionResult DangJibenInfoCreate(DangJibenInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.CpcmanInfo();
                entity.CpcmanUuid = Guid.NewGuid();
                entity.RealName = model.RealName;
                entity.Sex = model.Sex;
                entity.Education = model.Education;
                entity.DangOrganizationName = model.DangOrganizationName;
                entity.Politics = model.Politics;
                if (model.Birth != null && model.Birth != "")
                {
                    entity.Birth = DateTime.Parse(model.Birth);
                    //DateTime now = DateTime.Now;
                    //DateTime birth = Convert.ToDateTime(model.Birth);
                    //int age = now.Year - birth.Year;
                    //if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
                    //{
                    //    age--;
                    //}
                    //entity.Age = age < 0 ? 0 : age;
                }
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                //entity.AddPeople = model.addPeople;
                entity.IsDeleted = 0;
                _dbContext.CpcmanInfo.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:党员信息一条数据", _dbContext);
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
        public IActionResult DangJibenInfoEdit(DangJibenInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            string guid = model.CpcmanUuid;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.CpcmanInfo.FirstOrDefault(x => x.CpcmanUuid == Guid.Parse(guid));
                entity.RealName = model.RealName;
                entity.Sex = model.Sex;
                entity.Education = model.Education;
                entity.Politics = model.Politics;
                entity.DangOrganizationName = model.DangOrganizationName;
                if (model.Birth != null && model.Birth != "")
                {
                    entity.Birth = DateTime.Parse(model.Birth);
                    //DateTime now = DateTime.Now;
                    //DateTime birth = Convert.ToDateTime(model.Birth);
                    //int age = now.Year - birth.Year;
                    //if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
                    //{
                    //    age--;
                    //}
                    //entity.Age = age < 0 ? 0 : age;
                }
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:党员信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE CpcmanInfo SET IsDeleted=@isDeleted WHERE CpcmanUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:党员信息一条数据", _dbContext);
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
        public IActionResult DangJibenInfoImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel";
                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = " 党员基本信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string sFileName = $"{uploadtitle}.xls";
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
                    DataTable dt = Haikan3.Utils.ExcelTools.ExcelToDataTable(file.ToString(), "党员信息", true);

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        response.SetFailed("表格无数据");
                        return Ok(response);
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var entity = new HaikanSmartTownCockpit.Api.Entities.CpcmanInfo();
                            entity.CpcmanUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["序号"].ToString()))
                            {
                                entity.TableNum = dt.Rows[i]["序号"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["党组织"].ToString()))
                            {
                                entity.DangOrganizationName = dt.Rows[i]["党组织"].ToString();
                            }
                            Regex regmm = new Regex("^(正式党员|预备党员|入党积极分子|入党申请人)$");
                            if (regmm.IsMatch(dt.Rows[i]["面貌"].ToString()))
                            {
                                entity.Politics = dt.Rows[i]["面貌"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行面貌不正确,应为:正式党员|预备党员|入党积极分子|入党申请人" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            Regex regxb = new Regex("^(男|女)$");
                            if (regxb.IsMatch(dt.Rows[i]["性别"].ToString()))
                            {
                                entity.Sex = dt.Rows[i]["性别"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行性别不正确,应为:男|女" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString()))
                            {
                                entity.RealName = dt.Rows[i]["姓名"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名不正确" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            //Regex regbr = new Regex("^((((1[6-9]|[2-9]\\d)\\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})-0?2-(0?[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
                            //if (regbr.IsMatch(dt.Rows[i]["出生日期"].ToString()))
                            //{
                            //    entity.Birth = (DateTime?)dt.Rows[i]["出生日期"];
                            //    DateTime now = DateTime.Now;
                            //    DateTime birth = Convert.ToDateTime(entity.Birth);
                            //    int age = now.Year - birth.Year;
                            //    if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
                            //    {
                            //        age--;
                            //    }
                            //    entity.Age = age < 0 ? 0 : age;
                            //}
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行出生日期不正确,例:1999-09-09" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}  
                            //Regex reg1 = new Regex("^(小学|初中|职高|高中|大专|大学|研究生)$");
                            //if (reg1.IsMatch(dt.Rows[i]["学历"].ToString()))
                            //{
                            //    entity.Education = dt.Rows[i]["学历"].ToString();
                            //}
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行学历不正确,应为:小学|初中|职高|高中|大专|大学|研究生" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            if (!string.IsNullOrEmpty(dt.Rows[i]["学历"].ToString()))
                            {
                                entity.Education = dt.Rows[i]["学历"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["出生年月"].ToString()))
                            {
                                entity.Birth = DateTime.Parse(dt.Rows[i]["出生年月"].ToString());
                                //DateTime now = DateTime.Now;
                                //DateTime birth = Convert.ToDateTime(entity.Birth);
                                //int age = now.Year - birth.Year;
                                //if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
                                //{
                                //    age--;
                                //}
                                //entity.Age = age < 0 ? 0 : age;
                            }
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            //entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            entity.IsDeleted = 0;
                            _dbContext.CpcmanInfo.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:党员信息数据", _dbContext);
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

        ///// <summary>
        ///// 导出信息
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public IActionResult ExportPass(string ids)
        //{
        //    var response = ResponseModelFactory.CreateResultInstance;
        //    using (_dbContext)
        //    {
        //        if (ids != null)
        //        {
        //            var parameters = ids.Split(",");
        //            for (int i = 0; i < parameters.Length; i++)
        //            {
        //                parameters[i] = parameters[i].ToUpper();
        //            }
        //            var query1 = _dbContext.CpcmanInfo.Where(x => x.IsDeleted != 1 && parameters.Contains(x.CpcmanUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.CpcmanInfo
        //            {
        //                Id = x.Id,
        //                RealName = x.RealName,
        //                Sex = x.Sex,
        //                Birth = x.Birth,
        //                Education = x.Education,
        //                Politics = x.Politics,
        //            });
        //            var query = query1.OrderByDescending(x => x.Id).ToList();
        //            string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
        //            string uploadtitle = "人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
        //            string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
        //            //CuisineViewModel model = new CuisineViewModel();
        //            //model.Demos = query;
        //            TablesToExcel(query, sFileName);
        //            response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
        //            return Ok(response);
        //        }
        //        else
        //        {
        //            var query1 = _dbContext.CpcmanInfo.Where(x => x.IsDeleted != 1).Select(x => new HaikanSmartTownCockpit.Api.Entities.CpcmanInfo
        //            {
        //                Id = x.Id,
        //                RealName = x.RealName,
        //                Sex = x.Sex,
        //                Birth = x.Birth,
        //                Education = x.Education,
        //                Politics = x.Politics,
        //            });
        //            var query = query1.OrderByDescending(x => x.Id).ToList();
        //            string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
        //            string uploadtitle = "人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
        //            string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
        //            //CuisineViewModel model = new CuisineViewModel();
        //            //model.Demos = query;
        //            TablesToExcel(query, sFileName);
        //            response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
        //            return Ok(response);
        //        }


        //    }

        //}





        //public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.CpcmanInfo> query, string filename)
        //{
        //    MemoryStream ms = new MemoryStream();

        //    IWorkbook workBook;
        //    //IWorkbook workBook=WorkbookFactory.Create(filename);
        //    string suffix = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - filename.LastIndexOf(".") - 1);
        //    if (suffix == "xls")
        //    {
        //        workBook = new HSSFWorkbook();
        //    }
        //    else
        //        workBook = new XSSFWorkbook();

        //    ISheet sheet = workBook.CreateSheet(" 表");

        //    CreatSheet(sheet, query);


        //    workBook.Write(ms);
        //    try
        //    {
        //        SaveToFile(ms, filename);
        //        ms.Flush();
        //        return true;
        //    }
        //    catch
        //    {
        //        ms.Flush();
        //        throw;
        //    }

        //}

        //private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.CpcmanInfo> query)
        //{
        //    IRow headerRow = sheet.CreateRow(0);
        //    List<string> list = new List<string>() {
        //        "姓名","性别","出生日期","身份证号","居住地","户籍地","民族","学历","迁移地","迁移时间","职业","政治面貌"
        //    };

        //    //表头
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        headerRow.CreateCell(i).SetCellValue(list[i]);
        //    }

        //    int rowIndex = 1;
        //    foreach (var row in query)
        //    {
        //        IRow dataRow = sheet.CreateRow(rowIndex);
        //        dataRow.CreateCell(0).SetCellValue(row.RealName);
        //        dataRow.CreateCell(1).SetCellValue(row.Sex);
        //        dataRow.CreateCell(2).SetCellValue(row.Birth);
        //        dataRow.CreateCell(3).SetCellValue(row.IdentityCard);
        //        dataRow.CreateCell(4).SetCellValue(row.Residence);
        //        dataRow.CreateCell(5).SetCellValue(row.Domicile);
        //        dataRow.CreateCell(6).SetCellValue(row.Nation);
        //        dataRow.CreateCell(7).SetCellValue(row.Education);
        //        dataRow.CreateCell(8).SetCellValue(row.QianYiDe);
        //        dataRow.CreateCell(9).SetCellValue(row.QianYiTime);
        //        dataRow.CreateCell(10).SetCellValue(row.Occupation);
        //        dataRow.CreateCell(11).SetCellValue(row.Politics);
        //        rowIndex++;
        //    }
        //}
        //private static void SaveToFile(MemoryStream ms, string fileName)
        //{
        //    using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        //    {
        //        byte[] data = ms.ToArray();         //转为字节数组 
        //        fs.Write(data, 0, data.Length);     //保存为Excel文件
        //        fs.Flush();
        //        data = null;
        //    }
        //}
    }
}
