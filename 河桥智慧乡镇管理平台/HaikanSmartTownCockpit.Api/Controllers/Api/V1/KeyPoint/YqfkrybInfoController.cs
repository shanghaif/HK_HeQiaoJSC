using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.logs.TolLog;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.UserInfo;
using HaikanSmartTownCockpit.Api.ViewModels.KeyPoint;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.KeyPoint
{
    [Route("api/v1/KeyPoint/[controller]/[action]")]
    [ApiController]

    public class YqfkrybInfoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;

        public YqfkrybInfoController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
            var query = from c in _dbContext.Yqfkryb
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.Yqfkrybuuid,
                            c.Name,
                            c.OwnedNetwork,
                            c.ReasonForConcern,
                            c.Attention,
                            c.IdCard,
                        };
            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.Name.Contains(payload.Kw.Trim()));
            }
            query = query.OrderByDescending(x => x.Id);
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            ToLog.AddLog("查询", "成功:查询:疫情防控人员信息数据", _dbContext);
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
                var query1 = _dbContext.Yqfkryb.Where(x => x.Yqfkrybuuid == guid);
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
        public IActionResult GetCreate(YqfkrybViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.Yqfkryb();
                entity.Yqfkrybuuid = Guid.NewGuid();
                entity.Name = model.Name;
                entity.Attention = model.Attention;
                entity.IdCard = model.IdCard;
                entity.OwnedNetwork = model.OwnedNetwork;
                entity.ReasonForConcern = model.ReasonForConcern;
                entity.Sex = model.Sex;
                entity.EpidemicPreventionAndControlStatus = model.EpidemicPreventionAndControlStatus;
                entity.ContactPhone = model.ContactPhone;
                entity.ServicePremises = model.ServicePremises;
                entity.IsolationTime = model.IsolationTime;
                entity.CurrentAddress = model.CurrentAddress;
                entity.Origin = model.Origin;
                entity.WhereToGo = model.WhereToGo;
                entity.ToAddress = model.ToAddress;
                entity.FamilyMemberContactInformation = model.FamilyMemberContactInformation;
                entity.GoToAddressContact = model.GoToAddressContact;
                entity.ContactPhoneNumber = model.ContactPhoneNumber;
                entity.ReturnOrEstimatedReturnTime = model.ReturnOrEstimatedReturnTime;
                entity.Transportation = model.Transportation;
                entity.YesNoSuspectedFever = model.YesNoSuspectedFever;
                entity.CheckMethod = model.CheckMethod;
                entity.NameOfResponsibleDoctor = model.NameOfResponsibleDoctor;
                entity.PhoneNumberOfResponsibleDoctor = model.PhoneNumberOfResponsibleDoctor;
                entity.ServiceMemberInformation = model.ServiceMemberInformation;
                entity.GuardianInformation = model.GuardianInformation;
                entity.Remarks = model.Remarks;
                entity.IsDeleted = 0;
                _dbContext.Yqfkryb.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:疫情防控人员信息一条数据", _dbContext);
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
        public IActionResult GetEdit(YqfkrybViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.Yqfkryb.FirstOrDefault(x => x.Yqfkrybuuid == model.Yqfkrybuuid);
                entity.Name = model.Name;
                entity.Attention = model.Attention;
                entity.IdCard = model.IdCard;
                entity.OwnedNetwork = model.OwnedNetwork;
                entity.ReasonForConcern = model.ReasonForConcern;
                entity.Sex = model.Sex;
                entity.EpidemicPreventionAndControlStatus = model.EpidemicPreventionAndControlStatus;
                entity.ContactPhone = model.ContactPhone;
                entity.ServicePremises = model.ServicePremises;
                entity.IsolationTime = model.IsolationTime;
                entity.CurrentAddress = model.CurrentAddress;
                entity.Origin = model.Origin;
                entity.WhereToGo = model.WhereToGo;
                entity.ToAddress = model.ToAddress;
                entity.FamilyMemberContactInformation = model.FamilyMemberContactInformation;
                entity.GoToAddressContact = model.GoToAddressContact;
                entity.ContactPhoneNumber = model.ContactPhoneNumber;
                entity.ReturnOrEstimatedReturnTime = model.ReturnOrEstimatedReturnTime;
                entity.Transportation = model.Transportation;
                entity.YesNoSuspectedFever = model.YesNoSuspectedFever;
                entity.CheckMethod = model.CheckMethod;
                entity.NameOfResponsibleDoctor = model.NameOfResponsibleDoctor;
                entity.PhoneNumberOfResponsibleDoctor = model.PhoneNumberOfResponsibleDoctor;
                entity.ServiceMemberInformation = model.ServiceMemberInformation;
                entity.GuardianInformation = model.GuardianInformation;
                entity.Remarks = model.Remarks;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:疫情防控人员信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE Yqfkryb SET IsDeleted=@isDeleted WHERE Yqfkrybuuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:疫情防控人员信息一条数据", _dbContext);
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

                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\KeyPointExcel";


                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = "疫情防控人员清单导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var entity = new HaikanSmartTownCockpit.Api.Entities.Yqfkryb();
                            entity.Yqfkrybuuid = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString()))
                            {
                                entity.Name = dt.Rows[i]["姓名"].ToString();
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名为空" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["疫情防控状态"].ToString()))
                            {
                                entity.EpidemicPreventionAndControlStatus = dt.Rows[i]["疫情防控状态"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["关注原因"].ToString()))
                            {
                                entity.ReasonForConcern = dt.Rows[i]["关注原因"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["关注程度"].ToString()))
                            {
                                entity.Attention = dt.Rows[i]["关注程度"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["所属网格"].ToString()))
                            {
                                entity.OwnedNetwork = dt.Rows[i]["所属网格"].ToString();
                            }
                            Regex regsfz = new Regex("^(^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$)|(^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[Xx])$)$");
                            if (regsfz.IsMatch(dt.Rows[i]["身份证"].ToString()))
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i]["身份证"].ToString()))
                                {
                                    string ddd = dt.Rows[i]["身份证"].ToString();
                                    entity.IdCard = dt.Rows[i]["身份证"].ToString();
                                    if (ddd != "" && ddd != null)
                                    {
                                        string sex = "";
                                        //判断身份证是否18位
                                        if (ddd.Length == 18)
                                        {
                                            sex = ddd.Substring(14, 3);
                                            if (sex != "" && sex != null)
                                            {
                                                if (int.Parse(sex) % 2 == 0)
                                                {
                                                    entity.Sex = "女";
                                                }
                                                else
                                                {
                                                    entity.Sex = "男";
                                                }
                                            }
                                        }
                                        //判断身份证是否15位
                                        if (ddd.Length == 15)
                                        {
                                            sex = ddd.Substring(12, 3);
                                            if (sex != "" && sex != null)
                                            {
                                                if (int.Parse(sex) % 2 == 0)
                                                {
                                                    entity.Sex = "女";
                                                }
                                                else
                                                {
                                                    entity.Sex = "男";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行身份证格式不正确" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["联系手机"].ToString()))
                            {
                                entity.ContactPhone = dt.Rows[i]["联系手机"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["现住地址"].ToString()))
                            {
                                entity.CurrentAddress = dt.Rows[i]["现住地址"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["服务住所"].ToString()))
                            {
                                entity.ServicePremises = dt.Rows[i]["服务住所"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["隔离时间"].ToString()))
                            {
                                entity.IsolationTime = dt.Rows[i]["隔离时间"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["来源地"].ToString()))
                            {
                                entity.Origin = dt.Rows[i]["来源地"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["去往地点"].ToString()))
                            {
                                entity.WhereToGo = dt.Rows[i]["去往地点"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["去往地址"].ToString()))
                            {
                                entity.ToAddress = dt.Rows[i]["去往地址"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["家庭成员联系方式"].ToString()))
                            {
                                entity.FamilyMemberContactInformation = dt.Rows[i]["家庭成员联系方式"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["去往地址联络人"].ToString()))
                            {
                                entity.GoToAddressContact = dt.Rows[i]["去往地址联络人"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["联络人手机号码"].ToString()))
                            {
                                entity.ContactPhoneNumber = dt.Rows[i]["联络人手机号码"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["返回或预计返回时间"].ToString()))
                            {
                                entity.ReturnOrEstimatedReturnTime = dt.Rows[i]["返回或预计返回时间"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["交通方式"].ToString()))
                            {
                                entity.Transportation = dt.Rows[i]["交通方式"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["有无疑似发热"].ToString()))
                            {
                                entity.YesNoSuspectedFever = dt.Rows[i]["有无疑似发热"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["排查方式"].ToString()))
                            {
                                entity.CheckMethod = dt.Rows[i]["排查方式"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["责任医生姓名"].ToString()))
                            {
                                entity.NameOfResponsibleDoctor = dt.Rows[i]["责任医生姓名"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["责任医生电话"].ToString()))
                            {
                                entity.PhoneNumberOfResponsibleDoctor = dt.Rows[i]["责任医生电话"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["服务成员信息"].ToString()))
                            {
                                entity.ServiceMemberInformation = dt.Rows[i]["服务成员信息"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["监护人员信息"].ToString()))
                            {
                                entity.GuardianInformation = dt.Rows[i]["监护人员信息"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["备注"].ToString()))
                            {
                                entity.Remarks = dt.Rows[i]["备注"].ToString();
                            }
                            entity.IsDeleted = 0;
                            _dbContext.Yqfkryb.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;
                    ToLog.AddLog("导入", "成功:导入:疫情防控人员信息数据", _dbContext);
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
                    var query1 = _dbContext.Yqfkryb.Where(x => x.IsDeleted != 1 && parameters.Contains(x.Yqfkrybuuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.Yqfkryb
                    {
                        Id = x.Id,
                        Name = x.Name,
                        OwnedNetwork = x.OwnedNetwork,
                        Sex = x.Sex,
                        IdCard = x.IdCard,
                        Attention = x.Attention,
                        EpidemicPreventionAndControlStatus = x.EpidemicPreventionAndControlStatus,
                        ContactPhone = x.ContactPhone,
                        ServicePremises = x.ServicePremises,
                        IsolationTime = x.IsolationTime,
                        CurrentAddress = x.CurrentAddress,
                        Origin = x.Origin,
                        WhereToGo = x.WhereToGo,
                        ReasonForConcern = x.ReasonForConcern,
                        ToAddress = x.ToAddress,
                        FamilyMemberContactInformation = x.FamilyMemberContactInformation,
                        GoToAddressContact = x.GoToAddressContact,
                        ContactPhoneNumber = x.ContactPhoneNumber,
                        ReturnOrEstimatedReturnTime = x.ReturnOrEstimatedReturnTime,
                        Transportation = x.Transportation,
                        YesNoSuspectedFever = x.YesNoSuspectedFever,
                        CheckMethod = x.CheckMethod,
                        NameOfResponsibleDoctor = x.NameOfResponsibleDoctor,
                        PhoneNumberOfResponsibleDoctor = x.PhoneNumberOfResponsibleDoctor,
                        ServiceMemberInformation = x.ServiceMemberInformation,
                        GuardianInformation = x.GuardianInformation,
                        Remarks = x.Remarks,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\KeyPointExcel\\";
                    string uploadtitle = "疫情防控人员清单导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\KeyPointExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:疫情防控人员信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.Yqfkryb.Where(x => x.IsDeleted != 1).Select(x => new HaikanSmartTownCockpit.Api.Entities.Yqfkryb
                    {
                        Id = x.Id,
                        Name = x.Name,
                        OwnedNetwork = x.OwnedNetwork,
                        Sex = x.Sex,
                        IdCard = x.IdCard,
                        Attention = x.Attention,
                        EpidemicPreventionAndControlStatus = x.EpidemicPreventionAndControlStatus,
                        ContactPhone = x.ContactPhone,
                        ServicePremises = x.ServicePremises,
                        IsolationTime = x.IsolationTime,
                        CurrentAddress = x.CurrentAddress,
                        Origin = x.Origin,
                        WhereToGo = x.WhereToGo,
                        ReasonForConcern = x.ReasonForConcern,
                        ToAddress = x.ToAddress,
                        FamilyMemberContactInformation = x.FamilyMemberContactInformation,
                        GoToAddressContact = x.GoToAddressContact,
                        ContactPhoneNumber = x.ContactPhoneNumber,
                        ReturnOrEstimatedReturnTime = x.ReturnOrEstimatedReturnTime,
                        Transportation = x.Transportation,
                        YesNoSuspectedFever = x.YesNoSuspectedFever,
                        CheckMethod = x.CheckMethod,
                        NameOfResponsibleDoctor = x.NameOfResponsibleDoctor,
                        PhoneNumberOfResponsibleDoctor = x.PhoneNumberOfResponsibleDoctor,
                        ServiceMemberInformation = x.ServiceMemberInformation,
                        GuardianInformation = x.GuardianInformation,
                        Remarks = x.Remarks,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\KeyPointExcel\\";
                    string uploadtitle = "疫情防控人员清单导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\KeyPointExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:疫情防控人员信息数据", _dbContext);
                    return Ok(response);
                }


            }

        }





        public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.Yqfkryb> query, string filename)
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

        private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.Yqfkryb> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
  "姓名","性别","疫情防控状态","关注程度","关注原因","所属网格","身份证","联系手机","现住地址","服务住所","隔离时间","来源地","去往地点","去往地址","家庭成员联系方式","去往地址联络人","联络人手机号码","返回或预计返回时间","交通方式","有无疑似发热","排查方式","责任医生姓名","责任医生电话","服务成员信息","监护人员信息","备注"
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
                dataRow.CreateCell(2).SetCellValue(row.EpidemicPreventionAndControlStatus);
                dataRow.CreateCell(3).SetCellValue(row.Attention);
                dataRow.CreateCell(4).SetCellValue(row.ReasonForConcern);
                dataRow.CreateCell(5).SetCellValue(row.OwnedNetwork);
                dataRow.CreateCell(6).SetCellValue(row.IdCard);
                dataRow.CreateCell(7).SetCellValue(row.ContactPhone);
                dataRow.CreateCell(8).SetCellValue(row.CurrentAddress);
                dataRow.CreateCell(9).SetCellValue(row.ServicePremises);
                dataRow.CreateCell(10).SetCellValue(row.IsolationTime);
                dataRow.CreateCell(11).SetCellValue(row.Origin);
                dataRow.CreateCell(12).SetCellValue(row.WhereToGo);
                dataRow.CreateCell(13).SetCellValue(row.ToAddress);
                dataRow.CreateCell(14).SetCellValue(row.FamilyMemberContactInformation);
                dataRow.CreateCell(15).SetCellValue(row.GoToAddressContact);
                dataRow.CreateCell(16).SetCellValue(row.ContactPhoneNumber);
                dataRow.CreateCell(17).SetCellValue(row.ReturnOrEstimatedReturnTime);
                dataRow.CreateCell(18).SetCellValue(row.Transportation);
                dataRow.CreateCell(19).SetCellValue(row.YesNoSuspectedFever);
                dataRow.CreateCell(20).SetCellValue(row.CheckMethod);
                dataRow.CreateCell(21).SetCellValue(row.NameOfResponsibleDoctor);
                dataRow.CreateCell(22).SetCellValue(row.PhoneNumberOfResponsibleDoctor);
                dataRow.CreateCell(23).SetCellValue(row.ServiceMemberInformation);
                dataRow.CreateCell(24).SetCellValue(row.GuardianInformation);
                dataRow.CreateCell(25).SetCellValue(row.Remarks);
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
