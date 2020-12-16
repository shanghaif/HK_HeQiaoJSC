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
    public class DrugInfoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;

        public DrugInfoController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
            var query = from c in _dbContext.Drug
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.DrugUuid,
                            c.Name,
                            c.Phone,
                            FirstTime = c.FirstTime != null ? c.FirstTime.Value.ToString("yyyy-MM-dd") : "",
                            c.Detoxification,
                            c.OwningGrid,
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
            ToLog.AddLog("查询", "成功:查询:吸毒人员信息数据", _dbContext);
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
                var query1 = _dbContext.Drug.Where(x => x.DrugUuid == guid);
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
        public IActionResult GetCreate(DrugViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.Drug();
                entity.DrugUuid = Guid.NewGuid();
                entity.Name = model.Name;
                entity.Phone = model.Phone;
                entity.IdCard = model.IdCard; 
                entity.OwningGrid = model.OwningGrid;
                entity.Sex = model.Sex;
                entity.DateBirth = model.DateBirth;
                entity.SourceDrugs = model.SourceDrugs;
                entity.Detoxification = model.Detoxification;
                entity.Attention = model.Attention;
                entity.ResidenceAddress = model.ResidenceAddress;
                entity.RegisteredAddress = model.RegisteredAddress;
                entity.PoliceStation = model.PoliceStation;
                entity.HousesNumber = model.HousesNumber;
                entity.CurrentAddress = model.CurrentAddress;
                entity.RoomReason = model.RoomReason;
                entity.OtherAddress = model.OtherAddress;
                entity.FormerName = model.FormerName;
                entity.Employer = model.Employer;
                entity.ContactPhone = model.ContactPhone;
                entity.Email = model.Email;
                entity.Nation = model.Nation;
                entity.PoliticalStatus = model.PoliticalStatus;
                entity.Education = model.Education;
                entity.Occupation = model.Occupation;
                entity.MaritalStatus = model.MaritalStatus;
                entity.BloodType = model.BloodType;
                entity.Religious = model.Religious;
                entity.Height = model.Height;
                entity.DrugStatus = model.DrugStatus;
                entity.Species = model.Species;
                entity.ControlStatus = model.ControlStatus;
                entity.Detoxificationes = model.Detoxificationes;
                entity.Seizeddate = model.Seizeddate;
                entity.FirstTime = model.FirstTime;
                entity.Waiter = model.Waiter;
                entity.ServiceHours = model.ServiceHours;
                entity.Remarks = model.Remarks;
                entity.IsDeleted = 0;
                _dbContext.Drug.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:吸毒人员信息一条数据", _dbContext);
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
        public IActionResult GetEdit(DrugViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.Drug.FirstOrDefault(x => x.DrugUuid == model.DrugUuid);
                entity.Name = model.Name;
                entity.Phone = model.Phone;
                entity.IdCard = model.IdCard;
                entity.OwningGrid = model.OwningGrid;
                entity.Sex = model.Sex;
                entity.DateBirth = model.DateBirth;
                entity.SourceDrugs = model.SourceDrugs;
                entity.Detoxification = model.Detoxification;
                entity.Attention = model.Attention;
                entity.ResidenceAddress = model.ResidenceAddress;
                entity.RegisteredAddress = model.RegisteredAddress;
                entity.PoliceStation = model.PoliceStation;
                entity.HousesNumber = model.HousesNumber;
                entity.CurrentAddress = model.CurrentAddress;
                entity.RoomReason = model.RoomReason;
                entity.OtherAddress = model.OtherAddress;
                entity.FormerName = model.FormerName;
                entity.Employer = model.Employer;
                entity.ContactPhone = model.ContactPhone;
                entity.Email = model.Email;
                entity.Nation = model.Nation;
                entity.PoliticalStatus = model.PoliticalStatus;
                entity.Education = model.Education;
                entity.Occupation = model.Occupation;
                entity.MaritalStatus = model.MaritalStatus;
                entity.BloodType = model.BloodType;
                entity.Religious = model.Religious;
                entity.Height = model.Height;
                entity.DrugStatus = model.DrugStatus;
                entity.Species = model.Species;
                entity.ControlStatus = model.ControlStatus;
                entity.Detoxificationes = model.Detoxificationes;
                entity.Seizeddate = model.Seizeddate;
                entity.FirstTime = model.FirstTime;
                entity.Waiter = model.Waiter;
                entity.ServiceHours = model.ServiceHours;
                entity.Remarks = model.Remarks;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:吸毒人员信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE Drug SET IsDeleted=@isDeleted WHERE DrugUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:吸毒人员信息一条数据", _dbContext);
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
                string uploadtitle = "吸毒人员信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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

                            var entity = new HaikanSmartTownCockpit.Api.Entities.Drug();
                            entity.DrugUuid = Guid.NewGuid();
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
                            if (!string.IsNullOrEmpty(dt.Rows[i]["所属网格"].ToString()))
                            {
                                entity.OwningGrid = dt.Rows[i]["所属网格"].ToString();
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
                                            var d1 = ddd.Substring(6, 4) + "-" + ddd.Substring(10, 2) + "-" + ddd.Substring(12, 2);
                                            sex = ddd.Substring(14, 3);
                                            entity.DateBirth = DateTime.Parse(d1);
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
                                            var d1 = "19" + ddd.Substring(6, 2) + "-" + ddd.Substring(8, 2) + "-" + ddd.Substring(10, 2);
                                            sex = ddd.Substring(12, 3);
                                            entity.DateBirth = DateTime.Parse(d1);
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

                            if (!string.IsNullOrEmpty(dt.Rows[i]["毒品来源"].ToString()))
                            {
                                entity.SourceDrugs = dt.Rows[i]["毒品来源"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["戒毒情况"].ToString()))
                            {
                                entity.Detoxification = dt.Rows[i]["戒毒情况"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["关注程度"].ToString()))
                            {
                                entity.Attention = dt.Rows[i]["关注程度"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["户籍地址"].ToString()))
                            {
                                entity.ResidenceAddress = dt.Rows[i]["户籍地址"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["户籍地详址"].ToString()))
                            {
                                entity.RegisteredAddress = dt.Rows[i]["户籍地详址"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["户籍派出所"].ToString()))
                            {
                                entity.PoliceStation = dt.Rows[i]["户籍派出所"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["房屋编号"].ToString()))
                            {
                                entity.HousesNumber = dt.Rows[i]["房屋编号"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["现住地址"].ToString()))
                            {
                                entity.CurrentAddress = dt.Rows[i]["现住地址"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["无房原因"].ToString()))
                            {
                                entity.RoomReason = dt.Rows[i]["无房原因"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["其他地址"].ToString()))
                            {
                                entity.OtherAddress = dt.Rows[i]["其他地址"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["曾用名"].ToString()))
                            {
                                entity.FormerName = dt.Rows[i]["曾用名"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["工作单位"].ToString()))
                            {
                                entity.Employer = dt.Rows[i]["工作单位"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["联系电话"].ToString()))
                            {
                                entity.Phone = dt.Rows[i]["联系电话"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["联系手机"].ToString()))
                            {
                                entity.ContactPhone = dt.Rows[i]["联系手机"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["电子邮件"].ToString()))
                            {
                                entity.Email = dt.Rows[i]["电子邮件"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["民族"].ToString()))
                            {
                                entity.Nation = dt.Rows[i]["民族"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["政治面貌"].ToString()))
                            {
                                entity.PoliticalStatus = dt.Rows[i]["政治面貌"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["文化程度"].ToString()))
                            {
                                entity.Education = dt.Rows[i]["文化程度"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["职业"].ToString()))
                            {
                                entity.Occupation = dt.Rows[i]["职业"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["婚姻状况"].ToString()))
                            {
                                entity.MaritalStatus = dt.Rows[i]["婚姻状况"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["血型"].ToString()))
                            {
                                entity.BloodType = dt.Rows[i]["血型"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["宗教信仰"].ToString()))
                            {
                                entity.Religious = dt.Rows[i]["宗教信仰"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["身高"].ToString()))
                            {
                                entity.Height = dt.Rows[i]["身高"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["吸毒状态"].ToString()))
                            {
                                entity.DrugStatus = dt.Rows[i]["吸毒状态"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["滥用毒品种类"].ToString()))
                            {
                                entity.Species = dt.Rows[i]["滥用毒品种类"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["管控现状"].ToString()))
                            {
                                entity.ControlStatus = dt.Rows[i]["管控现状"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["是否服用美沙酮戒毒"].ToString()))
                            {
                                entity.Detoxificationes = dt.Rows[i]["是否服用美沙酮戒毒"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["查获日期"].ToString()))
                            {
                                entity.Seizeddate = dt.Rows[i]["查获日期"].ToString();
                            }
                            Regex regbr = new Regex("^((((1[6-9]|[2-9]\\d)\\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})-0?2-(0?[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
                            if (regbr.IsMatch(dt.Rows[i]["首吸时间"].ToString()))
                            {
                                entity.FirstTime = DateTime.Parse(dt.Rows[i]["首吸时间"].ToString());
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行首吸时间不正确,例:1999-09-09" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["有无服务成员"].ToString()))
                            {
                                entity.Waiter = dt.Rows[i]["有无服务成员"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["最新服务时间"].ToString()))
                            {
                                entity.ServiceHours = dt.Rows[i]["最新服务时间"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["备注"].ToString()))
                            {
                                entity.Remarks = dt.Rows[i]["备注"].ToString();
                            }
                            entity.IsDeleted = 0;
                            _dbContext.Drug.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;
                    ToLog.AddLog("导入", "成功:导入:吸毒人员信息数据", _dbContext);
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
                    var query1 = _dbContext.Drug.Where(x => x.IsDeleted != 1 && parameters.Contains(x.DrugUuid.ToString())).Select(x => new DrugData
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Phone = x.Phone,
                        OwningGrid = x.OwningGrid,
                        Sex = x.Sex,
                        DateBirth = x.DateBirth != null ? x.DateBirth.Value.ToString("yyyy-MM-dd") : "",
                        IdCard = x.IdCard,
                        SourceDrugs = x.SourceDrugs,
                        Detoxification = x.Detoxification,
                        Attention = x.Attention,
                        ResidenceAddress = x.ResidenceAddress,
                        RegisteredAddress = x.RegisteredAddress,
                        PoliceStation = x.PoliceStation,
                        HousesNumber = x.HousesNumber,
                        CurrentAddress = x.CurrentAddress,
                        RoomReason = x.RoomReason,
                        OtherAddress = x.OtherAddress,
                        FormerName = x.FormerName,
                        Employer = x.Employer,
                        ContactPhone = x.ContactPhone,
                        Email = x.Email,
                        Nation = x.Nation,
                        PoliticalStatus = x.PoliticalStatus,
                        Education = x.Education,
                        Occupation = x.Occupation,
                        MaritalStatus = x.MaritalStatus,
                        BloodType = x.BloodType,
                        Religious = x.Religious,
                        Height = x.Height,
                        DrugStatus = x.DrugStatus,
                        Species = x.Species,
                        ControlStatus = x.ControlStatus,
                        Detoxificationes = x.Detoxificationes,
                        Seizeddate = x.Seizeddate,
                        FirstTime = x.FirstTime != null ? x.FirstTime.Value.ToString("yyyy-MM-dd") : "",
                        Waiter = x.Waiter,
                        ServiceHours = x.ServiceHours,
                        Remarks = x.Remarks
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\KeyPointExcel\\";
                    string uploadtitle = "吸毒人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\KeyPointExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:吸毒人员信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.Drug.Where(x => x.IsDeleted != 1).Select(x => new DrugData
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Phone = x.Phone,
                        OwningGrid = x.OwningGrid,
                        Sex = x.Sex,
                        DateBirth = x.DateBirth != null ? x.DateBirth.Value.ToString("yyyy-MM-dd") : "",
                        IdCard = x.IdCard,
                        SourceDrugs = x.SourceDrugs,
                        Detoxification = x.Detoxification,
                        Attention = x.Attention,
                        ResidenceAddress = x.ResidenceAddress,
                        RegisteredAddress = x.RegisteredAddress,
                        PoliceStation = x.PoliceStation,
                        HousesNumber = x.HousesNumber,
                        CurrentAddress = x.CurrentAddress,
                        RoomReason = x.RoomReason,
                        OtherAddress = x.OtherAddress,
                        FormerName = x.FormerName,
                        Employer = x.Employer,
                        ContactPhone = x.ContactPhone,
                        Email = x.Email,
                        Nation = x.Nation,
                        PoliticalStatus = x.PoliticalStatus,
                        Education = x.Education,
                        Occupation = x.Occupation,
                        MaritalStatus = x.MaritalStatus,
                        BloodType = x.BloodType,
                        Religious = x.Religious,
                        Height = x.Height,
                        DrugStatus = x.DrugStatus,
                        Species = x.Species,
                        ControlStatus = x.ControlStatus,
                        Detoxificationes = x.Detoxificationes,
                        Seizeddate = x.Seizeddate,
                        FirstTime = x.FirstTime != null ? x.FirstTime.Value.ToString("yyyy-MM-dd") : "",
                        Waiter = x.Waiter,
                        ServiceHours = x.ServiceHours,
                        Remarks = x.Remarks
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\KeyPointExcel\\";
                    string uploadtitle = "吸毒人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\KeyPointExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:吸毒人员信息数据", _dbContext);
                    return Ok(response);
                }


            }

        }





        public static bool TablesToExcel(List<DrugData> query, string filename)
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

        private static void CreatSheet(ISheet sheet, List<DrugData> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "姓名","所属网格","性别","出生日期","身份证","毒品来源","戒毒情况","关注程度","户籍地址","户籍地详址","户籍派出所","房屋编号","现住地址","无房原因","其他地址","曾用名","工作单位","联系电话","联系手机","电子邮件","民族","政治面貌","文化程度","职业","婚姻状况","血型","宗教信仰","身高","吸毒状态","滥用毒品种类","管控现状","是否服用美沙酮戒毒","查获日期","首吸时间","有无服务成员","最新服务时间","备注"
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
                dataRow.CreateCell(1).SetCellValue(row.OwningGrid);
                dataRow.CreateCell(2).SetCellValue(row.Sex);
                dataRow.CreateCell(3).SetCellValue(row.DateBirth);
                dataRow.CreateCell(4).SetCellValue(row.IdCard);
                dataRow.CreateCell(5).SetCellValue(row.SourceDrugs);
                dataRow.CreateCell(6).SetCellValue(row.Detoxification);
                dataRow.CreateCell(7).SetCellValue(row.Attention);
                dataRow.CreateCell(8).SetCellValue(row.ResidenceAddress);
                dataRow.CreateCell(9).SetCellValue(row.RegisteredAddress);
                dataRow.CreateCell(10).SetCellValue(row.PoliceStation);
                dataRow.CreateCell(11).SetCellValue(row.HousesNumber);
                dataRow.CreateCell(12).SetCellValue(row.CurrentAddress);
                dataRow.CreateCell(13).SetCellValue(row.RoomReason);
                dataRow.CreateCell(14).SetCellValue(row.OtherAddress);
                dataRow.CreateCell(15).SetCellValue(row.FormerName);
                dataRow.CreateCell(16).SetCellValue(row.Employer);
                dataRow.CreateCell(17).SetCellValue(row.Phone);
                dataRow.CreateCell(18).SetCellValue(row.ContactPhone);
                dataRow.CreateCell(19).SetCellValue(row.Email);
                dataRow.CreateCell(20).SetCellValue(row.Nation);
                dataRow.CreateCell(21).SetCellValue(row.PoliticalStatus);
                dataRow.CreateCell(22).SetCellValue(row.Education);
                dataRow.CreateCell(23).SetCellValue(row.Occupation);
                dataRow.CreateCell(24).SetCellValue(row.MaritalStatus);
                dataRow.CreateCell(25).SetCellValue(row.BloodType);
                dataRow.CreateCell(26).SetCellValue(row.Religious);
                dataRow.CreateCell(27).SetCellValue(row.Height);
                dataRow.CreateCell(28).SetCellValue(row.DrugStatus);
                dataRow.CreateCell(29).SetCellValue(row.Species);
                dataRow.CreateCell(30).SetCellValue(row.ControlStatus);
                dataRow.CreateCell(31).SetCellValue(row.Detoxificationes);
                dataRow.CreateCell(32).SetCellValue(row.Seizeddate);
                dataRow.CreateCell(33).SetCellValue(row.FirstTime);
                dataRow.CreateCell(34).SetCellValue(row.Waiter);
                dataRow.CreateCell(35).SetCellValue(row.ServiceHours);
                dataRow.CreateCell(36).SetCellValue(row.Remarks);
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

    public class DrugData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string OwningGrid { get; set; }
        public string Sex { get; set; }
        public string DateBirth { get; set; }
        public string IdCard { get; set; }
        public string SourceDrugs { get; set; }
        public string Detoxification { get; set; }
        public string Attention { get; set; }
        public string ResidenceAddress { get; set; }
        public string RegisteredAddress { get; set; }
        public string PoliceStation { get; set; }
        public string HousesNumber { get; set; }
        public string CurrentAddress { get; set; }
        public string RoomReason { get; set; }
        public string OtherAddress { get; set; }
        public string FormerName { get; set; }
        public string Employer { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public string Nation { get; set; }
        public string PoliticalStatus { get; set; }
        public string Education { get; set; }
        public string Occupation { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodType { get; set; }
        public string Religious { get; set; }
        public string Height { get; set; }
        public string DrugStatus { get; set; }
        public string Species { get; set; }
        public string ControlStatus { get; set; }
        public string Detoxificationes { get; set; }
        public string Seizeddate { get; set; }
        public string FirstTime { get; set; }
        public string Waiter { get; set; }
        public string ServiceHours { get; set; }
        public string Remarks { get; set; }

    }
}
