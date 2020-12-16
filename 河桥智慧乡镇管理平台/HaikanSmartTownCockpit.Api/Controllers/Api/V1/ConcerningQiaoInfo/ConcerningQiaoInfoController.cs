using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using HaikanSmartTownCockpit.Api.RequestPayload.UserInfo;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Models.Response;
using System.IO;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.ViewModels.ConcerningQiaoInfo;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.ConcerningQiaoInfo
{
    [Route("api/v1/ConcerningQiaoInfo/[controller]/[action]")]
    [ApiController]
    public class ConcerningQiaoInfoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public ConcerningQiaoInfoController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult HomeAddressList(UserInfoRequestpayload payload)
        {
            var query = from c in _dbContext.ConcerQiao
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.RealName,          //姓名
                            c.UserIdCardType,     //证件类型
                            c.UserIdCardNum,      //证件号码
                            c.Phone,         //手机号
                            c.Email,         //邮箱
                            c.Sex,         //性别
                            c.Gwaddress,       //国外所在地
                            c.Cnaddress,       //中国居住地
                            c.Xjaddress,       //现居地址
                            c.ConcerningType,   //涉侨类型:归侨、侨眷、港澳同胞眷属、归国留学人员、海外留学人员眷属、华侨、港澳同胞及外籍华人
                            c.ConcerningQiaoUuid,
                        };

            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.RealName.Contains(payload.Kw.Trim()));
            }
            if (!string.IsNullOrEmpty(payload.Kw1))
            {
                query = query.Where(x => x.ConcerningType.Contains(payload.Kw1.Trim()));
            }
            query = query.OrderByDescending(x => x.Id);
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            ToLog.AddLog("查询", "成功:查询:涉侨人员信息数据", _dbContext);
            return Ok(response);
        }


        /// <summary>
        /// 获取当前行信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult HomeAddressfoGet(Guid guid)
        {
            using (_dbContext)
            {
                var query1 = from c in _dbContext.ConcerQiao
                             where c.ConcerningQiaoUuid == guid
                             select new
                             {
                                 c.Id,
                                 c.RealName,          //姓名
                                 c.UserIdCardType,     //证件类型
                                 c.UserIdCardNum,      //证件号码
                                 c.Phone,         //手机号
                                 c.Email,         //邮箱
                                 c.Sex,         //性别
                                 c.Gwaddress,       //国外所在地
                                 c.Cnaddress,       //中国居住地
                                 c.Xjaddress,       //现居地址
                                 c.ConcerningType,   //涉侨类型
                                 c.ConcerningQiaoUuid,
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
        public IActionResult HomeAddressCreate(ConcerningQiaoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.ConcerQiao();
                entity.ConcerningQiaoUuid = Guid.NewGuid();
                entity.RealName = model.RealName;
                entity.UserIdCardType = model.UserIdCardType;
                entity.UserIdCardNum = model.UserIdCardNum;
                entity.Phone = model.Phone;
                entity.Email = model.Email;
                entity.Sex = model.Sex;
                entity.Gwaddress = model.Gwaddress;
                entity.Cnaddress = model.Cnaddress;
                entity.Xjaddress = model.Xjaddress;
                entity.ConcerningType = model.ConcerningType;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                entity.IsDeleted = 0;
                _dbContext.ConcerQiao.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:涉侨人员信息数据", _dbContext);
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
        public IActionResult HomeAddressEdit(ConcerningQiaoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            string guid = model.ConcerningQiaoUuid;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.ConcerQiao.FirstOrDefault(x => x.ConcerningQiaoUuid == Guid.Parse(guid));
                entity.RealName = model.RealName;
                entity.UserIdCardType = model.UserIdCardType;
                entity.UserIdCardNum = model.UserIdCardNum;
                entity.Phone = model.Phone;
                entity.Email = model.Email;
                entity.Sex = model.Sex;
                entity.Gwaddress = model.Gwaddress;
                entity.Cnaddress = model.Cnaddress;
                entity.Xjaddress = model.Xjaddress;
                entity.ConcerningType = model.ConcerningType;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:涉侨人员信息数据", _dbContext);
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
                var sql = string.Format("UPDATE ConcerQiao SET IsDeleted=@isDeleted WHERE ConcerningQiaoUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:涉侨人员信息数据", _dbContext);
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
        public IActionResult HomeAddressImport(IFormFile excelfile)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                DateTime beginTime = DateTime.Now;

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = " 涉侨人员信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                            var entity = new HaikanSmartTownCockpit.Api.Entities.ConcerQiao();
                            entity.ConcerningQiaoUuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString()))
                            {
                                entity.RealName = dt.Rows[i]["姓名"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["证件类型"].ToString()))
                            {
                                entity.UserIdCardType = dt.Rows[i]["证件类型"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["证件号码"].ToString()))
                            {
                                entity.UserIdCardNum = dt.Rows[i]["证件号码"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["手机号"].ToString()))
                            {
                                entity.Phone = dt.Rows[i]["手机号"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["邮箱"].ToString()))
                            {
                                entity.Email = dt.Rows[i]["邮箱"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["性别"].ToString()))
                            {
                                entity.Sex = dt.Rows[i]["性别"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["国外所在地"].ToString()))
                            {
                                entity.Gwaddress = dt.Rows[i]["国外所在地"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["中国居住地"].ToString()))
                            {
                                entity.Cnaddress = dt.Rows[i]["中国居住地"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["现居地址"].ToString()))
                            {
                                entity.Xjaddress = dt.Rows[i]["现居地址"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["涉侨类型"].ToString()))
                            {
                                entity.ConcerningType = dt.Rows[i]["涉侨类型"].ToString();
                            }
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            //entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            entity.IsDeleted = 0;
                            _dbContext.ConcerQiao.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:涉侨人员信息数据", _dbContext);
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
                    var query1 = _dbContext.ConcerQiao.Where(x => x.IsDeleted != 1 && parameters.Contains(x.ConcerningQiaoUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.ConcerQiao
                    {
                        Id = x.Id,
                        RealName = x.RealName,          //姓名
                        UserIdCardType = x.UserIdCardType,     //证件类型
                        UserIdCardNum = x.UserIdCardNum,      //证件号码
                        Phone = x.Phone,         //手机号
                        Email = x.Email,         //邮箱
                        Sex = x.Sex,         //性别
                        Gwaddress = x.Gwaddress,       //国外所在地
                        Cnaddress = x.Cnaddress,        //中国居住地
                        Xjaddress = x.Xjaddress,        //现居地址
                        ConcerningType = x.ConcerningType, //涉侨类型 
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "涉侨人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:涉侨人员信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.ConcerQiao.Where(x => x.IsDeleted != 1).Select(x => new HaikanSmartTownCockpit.Api.Entities.ConcerQiao
                    {
                        Id = x.Id,
                        RealName = x.RealName,          //姓名
                        UserIdCardType = x.UserIdCardType,     //证件类型
                        UserIdCardNum = x.UserIdCardNum,      //证件号码
                        Phone = x.Phone,         //手机号
                        Email = x.Email,         //邮箱
                        Sex = x.Sex,         //性别
                        Gwaddress = x.Gwaddress,       //国外所在地
                        Cnaddress = x.Cnaddress,        //中国居住地
                        Xjaddress = x.Xjaddress,        //现居地址
                        ConcerningType = x.ConcerningType, //涉侨类型 
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "涉侨人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:涉侨人员信息数据", _dbContext);
                    return Ok(response);
                }


            }

        }





        public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.ConcerQiao> query, string filename)
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

        private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.ConcerQiao> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "姓名","证件类型","证件号码","手机号","邮箱","性别","国外所在地","中国居住地","现居地址","涉侨类型"
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
                dataRow.CreateCell(0).SetCellValue(row.RealName);
                dataRow.CreateCell(1).SetCellValue(row.UserIdCardType);
                dataRow.CreateCell(2).SetCellValue(row.UserIdCardNum);
                dataRow.CreateCell(3).SetCellValue(row.Phone);
                dataRow.CreateCell(4).SetCellValue(row.Email);
                dataRow.CreateCell(5).SetCellValue(row.Sex);
                dataRow.CreateCell(6).SetCellValue(row.Gwaddress);
                dataRow.CreateCell(7).SetCellValue(row.Cnaddress);
                dataRow.CreateCell(8).SetCellValue(row.Xjaddress);
                dataRow.CreateCell(9).SetCellValue(row.ConcerningType);
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
