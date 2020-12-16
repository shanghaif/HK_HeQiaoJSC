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
using HaikanSmartTownCockpit.Api.RequestPayload.UserInfo;
using HaikanSmartTownCockpit.ViewModels.Cuisine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using System.Text.RegularExpressions;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.SightseerInfo
{
    [Route("api/v1/SightseerInfo/[controller]/[action]")]
    [ApiController]
    public class SightseerInfoController : ControllerBase
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
        public SightseerInfoController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult UserInfoList(UserInfoRequestpayload payload)
        {
            var query = from c in _dbContext.Sightseer
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.SightseerName,         //姓名
                            c.Phone,            //手机
                            c.Sex,              //性别
                            c.IdentityCard,     //身份证
                            c.Laiyuandi,        //来源地
                            c.Nation,           //民族
                            c.SightseerUuid,
                            c.Age,
                            c.Shengneiwai,
                        };

            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.SightseerName.Contains(payload.Kw.Trim()));
            }
            if (!string.IsNullOrEmpty(payload.Kw1))
            {
                query = query.Where(x => x.IdentityCard.Contains(payload.Kw1.Trim()));
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
            ToLog.AddLog("查询", "成功:查询:游客信息数据", _dbContext);
            return Ok(response);
        }


        /// <summary>
        /// 获取当前行信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UserInfoGet(Guid guid)
        {
            using (_dbContext)
            {
                //var entity = _dbContext.Cuisine.FirstOrDefault(x => x.CuisineUuid == guid);
                var entity = from c in _dbContext.Sightseer
                             where c.SightseerUuid == guid
                             select new
                             {
                                 c.Id,
                                 c.SightseerName,         //姓名
                                 c.Phone,            //手机
                                 c.Sex,              //性别
                                 c.IdentityCard,     //身份证
                                 c.Laiyuandi,        //来源地
                                 c.Nation,           //民族
                                 c.SightseerUuid,
                                 c.Age,
                                 c.Shengneiwai,
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
        public IActionResult UserInfoCreate(UsreInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.Sightseer();
                entity.SightseerUuid = Guid.NewGuid();
                entity.SightseerName = model.SightseerName;
                entity.Sex = model.Sex;
                entity.Nation = model.Nation;
                entity.Phone = model.Phone;
                entity.IdentityCard = model.IdentityCard;
                entity.Laiyuandi = model.Laiyuandi;
                entity.Age = model.Age;
                entity.Shengneiwai = model.Shengneiwai;
                //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                //entity.AddPeople = model.addPeople;
                entity.IsDeleted = 0;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                _dbContext.Sightseer.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:游客信息一条数据", _dbContext);
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
        public IActionResult UserInfoEdit(UsreInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            string guid = model.SightseerUuid;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.Sightseer.FirstOrDefault(x => x.SightseerUuid == Guid.Parse(guid));
                entity.SightseerName = model.SightseerName;
                entity.Sex = model.Sex;
                entity.Nation = model.Nation;
                entity.Phone = model.Phone;
                entity.IdentityCard = model.IdentityCard;
                entity.Laiyuandi = model.Laiyuandi;
                entity.Age = model.Age;
                entity.Shengneiwai = model.Shengneiwai;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:游客信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE Sightseer SET IsDeleted=@isDeleted WHERE SightseerUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:游客信息一条数据", _dbContext);
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
        public IActionResult UserInfoImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = " 游客库信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            var entity = new HaikanSmartTownCockpit.Api.Entities.Sightseer();
                            entity.SightseerUuid = Guid.NewGuid();
                            Regex regxm = new Regex("^([\\u4e00-\\u9fa5]){2,7}$");
                            if (regxm.IsMatch(dt.Rows[i]["姓名"].ToString()))
                            {
                                entity.SightseerName = dt.Rows[i]["姓名"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名格式不正确" + "</p></br>";
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
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行性别不正确" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            Regex regsfz = new Regex("^(^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$)|(^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[Xx])$)$");
                            if (regsfz.IsMatch(dt.Rows[i]["身份证号"].ToString()))
                            {
                                entity.IdentityCard = dt.Rows[i]["身份证号"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行身份证号格式不正确" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            Regex regnl = new Regex("^[0-9]*[1-9][0-9]*$");
                            if (regnl.IsMatch(dt.Rows[i]["年龄"].ToString()))
                            {
                                entity.Age = dt.Rows[i]["年龄"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行年龄格式不正确" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            Regex reg = new Regex("^(汉族|蒙古族|回族|藏族|维吾尔族|苗族|彝族|壮族|布依族|朝鲜族|满族|侗族|瑶族|白族|土家族|哈尼族|哈萨克族|傣族|黎族|僳僳族|佤族|畲族|高山族|拉祜族|水族|东乡族|纳西族|景颇族|柯尔克孜族|土族|达斡尔族|仫佬族|羌族|布朗族|撒拉族|毛南族|仡佬族|锡伯族|阿昌族|普米族|塔吉克族|怒族|乌孜别克族|俄罗斯族|鄂温克族|德昂族|保安族|京族|独龙族|鄂伦春族|赫哲族|裕固族|门巴族|珞巴族|基诺族)$");
                            if (reg.IsMatch(dt.Rows[i]["民族"].ToString()))
                            {
                                entity.Nation = dt.Rows[i]["民族"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行民族不正确" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["来源地"].ToString()))
                            {
                                entity.Laiyuandi = dt.Rows[i]["来源地"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["手机"].ToString()))
                            {
                                entity.Phone = dt.Rows[i]["手机"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["是否省内人员"].ToString()))
                            {
                                Regex regsnw = new Regex("^(是|否)$");
                                if (regsnw.IsMatch(dt.Rows[i]["是否省内人员"].ToString()))
                                {
                                    entity.Shengneiwai = dt.Rows[i]["是否省内人员"].ToString();
                                }
                                else
                                {
                                    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行格式不正确,应为:是|否" + "</p></br>";
                                    defaultcount++;
                                    continue;
                                }
                            }
                            else
                            {
                                entity.Shengneiwai = "是";
                            }
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            entity.IsDeleted = 0;
                            _dbContext.Sightseer.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:游客信息数据", _dbContext);
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
                    var query1 = _dbContext.Sightseer.Where(x => x.IsDeleted != 1 && parameters.Contains(x.SightseerUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.Sightseer
                    {
                        Id = x.Id,
                        SightseerName = x.SightseerName,
                        Sex = x.Sex,
                        Phone = x.Phone,
                        IdentityCard = x.IdentityCard,
                        Laiyuandi = x.Laiyuandi,
                        Nation = x.Nation,
                        Age = x.Age,
                        Shengneiwai = x.Shengneiwai,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "游客库信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:游客信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.Sightseer.Where(x => x.IsDeleted != 1).Select(x => new HaikanSmartTownCockpit.Api.Entities.Sightseer
                    {
                        Id = x.Id,
                        SightseerName = x.SightseerName,
                        Sex = x.Sex,
                        Phone = x.Phone,
                        IdentityCard = x.IdentityCard,
                        Laiyuandi = x.Laiyuandi,
                        Nation = x.Nation,
                        Age = x.Age,
                        Shengneiwai = x.Shengneiwai,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "游客库信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:游客信息数据", _dbContext);
                    return Ok(response);
                }


            }

        }





        public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.Sightseer> query, string filename)
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

        private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.Sightseer> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                " 姓名","性别","身份证号","年龄","民族","来源地","手机","是否省内人员"
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
                dataRow.CreateCell(0).SetCellValue(row.SightseerName);
                dataRow.CreateCell(1).SetCellValue(row.Sex);
                dataRow.CreateCell(2).SetCellValue(row.IdentityCard);
                dataRow.CreateCell(3).SetCellValue(row.Age);
                dataRow.CreateCell(4).SetCellValue(row.Nation);
                dataRow.CreateCell(5).SetCellValue(row.Laiyuandi);
                dataRow.CreateCell(6).SetCellValue(row.Phone); 
                dataRow.CreateCell(7).SetCellValue(row.Shengneiwai);
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
