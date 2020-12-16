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
    public class ListOfKeyPetController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;

        public ListOfKeyPetController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
            var query = from c in _dbContext.ListOfKeyPetitioners
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.ListOfKeyPetitionersUuid,
                            c.Name,
                            c.OwnedNetwork,
                            c.ReasonForPetition,
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
            ToLog.AddLog("查询", "成功:查询:重点上访人员信息数据", _dbContext);
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
                var query1 = _dbContext.ListOfKeyPetitioners.Where(x => x.ListOfKeyPetitionersUuid == guid);
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
        public IActionResult GetCreate(ListOfKeyPetViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.ListOfKeyPetitioners();
                entity.ListOfKeyPetitionersUuid = Guid.NewGuid();
                entity.Name = model.Name;
                entity.Attention = model.Attention;
                entity.IdCard = model.IdCard;
                entity.OwnedNetwork = model.OwnedNetwork;
                entity.ReasonForPetition = model.ReasonForPetition;
                entity.Sex = model.Sex;
                entity.DateOfBirth = DateTime.Parse(model.DateOfBirth).ToString("yyyy-MM-dd");
                entity.ResidenceAddress = model.ResidenceAddress;
                entity.RegisteredAddress = model.RegisteredAddress;
                entity.HouseholdRegistrationPoliceStation = model.HouseholdRegistrationPoliceStation;
                entity.NumberOfTheHouse = model.NumberOfTheHouse;
                entity.CurrentAddress = model.CurrentAddress;
                entity.NoRoomReason = model.NoRoomReason;
                entity.OtherAddress = model.OtherAddress;
                entity.FormerName = model.FormerName;
                entity.Employer = model.Employer;
                entity.ContactPhone = model.ContactPhone;
                entity.ContactNumber = model.ContactNumber;
                entity.Email = model.Email;
                entity.Nation = model.Nation;
                entity.PoliticalStatus = model.PoliticalStatus;
                entity.Education = model.Education;
                entity.Occupation = model.Occupation;
                entity.MaritalStatus = model.MaritalStatus;
                entity.BloodType = model.BloodType;
                entity.ReligiousBelief = model.ReligiousBelief;
                entity.Height = model.Height;
                entity.PetitionStatus = model.PetitionStatus;
                entity.PetitionType = model.PetitionType;
                entity.ServiceMembers = model.ServiceMembers;
                entity.LatestServiceHours = model.LatestServiceHours;
                entity.Remarks = model.Remarks;
                entity.IsDeleted = 0;
                _dbContext.ListOfKeyPetitioners.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:重点上访人员信息一条数据", _dbContext);
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
        public IActionResult GetEdit(ListOfKeyPetViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.ListOfKeyPetitioners.FirstOrDefault(x => x.ListOfKeyPetitionersUuid == model.ListOfKeyPetitionersUuid);
                entity.Name = model.Name;
                entity.IdCard = model.IdCard;
                entity.Attention = model.Attention;
                entity.OwnedNetwork = model.OwnedNetwork;
                entity.Sex = model.Sex;
                entity.DateOfBirth = DateTime.Parse(model.DateOfBirth).ToString("yyyy-MM-dd");
                entity.ResidenceAddress = model.ResidenceAddress;
                entity.RegisteredAddress = model.RegisteredAddress;
                entity.HouseholdRegistrationPoliceStation = model.HouseholdRegistrationPoliceStation;
                entity.NumberOfTheHouse = model.NumberOfTheHouse;
                entity.CurrentAddress = model.CurrentAddress;
                entity.NoRoomReason = model.NoRoomReason;
                entity.OtherAddress = model.OtherAddress;
                entity.FormerName = model.FormerName;
                entity.Employer = model.Employer;
                entity.ContactPhone = model.ContactPhone;
                entity.ContactNumber = model.ContactNumber;
                entity.Email = model.Email;
                entity.Nation = model.Nation;
                entity.PoliticalStatus = model.PoliticalStatus;
                entity.Education = model.Education;
                entity.Occupation = model.Occupation;
                entity.MaritalStatus = model.MaritalStatus;
                entity.BloodType = model.BloodType;
                entity.ReligiousBelief = model.ReligiousBelief;
                entity.Height = model.Height;
                entity.PetitionStatus = model.PetitionStatus;
                entity.PetitionType = model.PetitionType;
                entity.ServiceMembers = model.ServiceMembers;
                entity.LatestServiceHours = model.LatestServiceHours;
                entity.Remarks = model.Remarks;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:重点上访人员信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE ListOfKeyPetitioners SET IsDeleted=@isDeleted WHERE ListOfKeyPetitionersUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:重点上访人员信息一条数据", _dbContext);
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
                string uploadtitle = "重点上访人员清单导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                            var entity = new HaikanSmartTownCockpit.Api.Entities.ListOfKeyPetitioners();
                            entity.ListOfKeyPetitionersUuid = Guid.NewGuid();
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
                                            var d1 = ddd.Substring(6, 4) + "-" + ddd.Substring(10, 2) + "-" + ddd.Substring(12, 2);
                                            sex = ddd.Substring(14, 3);
                                            entity.DateOfBirth = DateTime.Parse(d1).ToString("yyyy-MM-dd");
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
                                            entity.DateOfBirth = DateTime.Parse(d1).ToString("yyyy-MM-dd");
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
                            if (!string.IsNullOrEmpty(dt.Rows[i]["上访原因"].ToString()))
                            {
                                entity.ReasonForPetition = dt.Rows[i]["上访原因"].ToString();
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
                                entity.HouseholdRegistrationPoliceStation = dt.Rows[i]["户籍派出所"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["房屋编号"].ToString()))
                            {
                                entity.NumberOfTheHouse = dt.Rows[i]["房屋编号"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["现住地址"].ToString()))
                            {
                                entity.CurrentAddress = dt.Rows[i]["现住地址"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["无房原因"].ToString()))
                            {
                                entity.NoRoomReason = dt.Rows[i]["无房原因"].ToString();
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
                                entity.ContactNumber = dt.Rows[i]["联系电话"].ToString();
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
                                entity.ReligiousBelief = dt.Rows[i]["宗教信仰"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["身高"].ToString()))
                            {
                                entity.Height = dt.Rows[i]["身高"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["上访状态"].ToString()))
                            {
                                entity.PetitionStatus = dt.Rows[i]["上访状态"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["上访类型"].ToString()))
                            {
                                entity.PetitionType = dt.Rows[i]["上访类型"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["有无服务成员"].ToString()))
                            {
                                entity.ServiceMembers = dt.Rows[i]["有无服务成员"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["最新服务时间"].ToString()))
                            {
                                entity.LatestServiceHours = dt.Rows[i]["最新服务时间"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["备注"].ToString()))
                            {
                                entity.Remarks = dt.Rows[i]["备注"].ToString();
                            }
                            entity.IsDeleted = 0;
                            _dbContext.ListOfKeyPetitioners.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;
                    ToLog.AddLog("导入", "成功:导入:重点上访人员信息数据", _dbContext);
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
                    var query1 = _dbContext.ListOfKeyPetitioners.Where(x => x.IsDeleted != 1 && parameters.Contains(x.ListOfKeyPetitionersUuid.ToString())).Select(x => new ListOfKeyData
                    {
                        Id = x.Id,
                        Name = x.Name,
                        OwnedNetwork = x.OwnedNetwork,
                        Sex = x.Sex,
                        DateOfBirth = x.DateOfBirth,
                        IdCard = x.IdCard,
                        Attention = x.Attention,
                        ResidenceAddress = x.ResidenceAddress,
                        RegisteredAddress = x.RegisteredAddress,
                        HouseholdRegistrationPoliceStation = x.HouseholdRegistrationPoliceStation,
                        NumberOfTheHouse = x.NumberOfTheHouse,
                        CurrentAddress = x.CurrentAddress,
                        NoRoomReason = x.NoRoomReason,
                        OtherAddress = x.OtherAddress,
                        FormerName = x.FormerName,
                        Employer = x.Employer,
                        ContactNumber = x.ContactNumber,
                        ContactPhone = x.ContactPhone,
                        Email = x.Email,
                        Nation = x.Nation,
                        PoliticalStatus = x.PoliticalStatus,
                        Education = x.Education,
                        Occupation = x.Occupation,
                        MaritalStatus = x.MaritalStatus,
                        BloodType = x.BloodType,
                        ReligiousBelief = x.ReligiousBelief,
                        Height = x.Height,
                        PetitionStatus = x.PetitionStatus,
                        PetitionType = x.PetitionType,
                        ServiceMembers = x.ServiceMembers,
                        LatestServiceHours = x.LatestServiceHours,
                        Remarks = x.Remarks,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\KeyPointExcel\\";
                    string uploadtitle = "重点上访人员清单导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\KeyPointExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:重点上访人员信息数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.ListOfKeyPetitioners.Where(x => x.IsDeleted != 1).Select(x => new ListOfKeyData
                    {
                        Id = x.Id,
                        Name = x.Name,
                        OwnedNetwork = x.OwnedNetwork,
                        Sex = x.Sex,
                        DateOfBirth = x.DateOfBirth,
                        IdCard = x.IdCard,
                        Attention = x.Attention,
                        ResidenceAddress = x.ResidenceAddress,
                        RegisteredAddress = x.RegisteredAddress,
                        HouseholdRegistrationPoliceStation = x.HouseholdRegistrationPoliceStation,
                        NumberOfTheHouse = x.NumberOfTheHouse,
                        CurrentAddress = x.CurrentAddress,
                        NoRoomReason = x.NoRoomReason,
                        OtherAddress = x.OtherAddress,
                        FormerName = x.FormerName,
                        Employer = x.Employer,
                        ContactPhone = x.ContactPhone,
                        ContactNumber = x.ContactNumber,
                        Email = x.Email,
                        Nation = x.Nation,
                        PoliticalStatus = x.PoliticalStatus,
                        Education = x.Education,
                        Occupation = x.Occupation,
                        MaritalStatus = x.MaritalStatus,
                        BloodType = x.BloodType,
                        ReligiousBelief = x.ReligiousBelief,
                        Height = x.Height,
                        PetitionStatus = x.PetitionStatus,
                        PetitionType = x.PetitionType,
                        ServiceMembers = x.ServiceMembers,
                        LatestServiceHours = x.LatestServiceHours,
                        Remarks = x.Remarks,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\KeyPointExcel\\";
                    string uploadtitle = "重点上访人员清单导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\KeyPointExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:重点上访人员信息数据", _dbContext);
                    return Ok(response);
                }


            }

        }





        public static bool TablesToExcel(List<ListOfKeyData> query, string filename)
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

        private static void CreatSheet(ISheet sheet, List<ListOfKeyData> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
  "姓名","所属网格","性别","出生日期","身份证","上访原因","关注程度","户籍地址","户籍地详址","户籍派出所","房屋编号","现住地址","无房原因","其他地址","曾用名","工作单位","联系电话","联系手机","电子邮件","民族","政治面貌","文化程度","职业","婚姻状况","血型","宗教信仰","身高","上访状态","上访类型","有无服务成员","最新服务时间","备注"
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
                dataRow.CreateCell(1).SetCellValue(row.OwnedNetwork);
                dataRow.CreateCell(2).SetCellValue(row.Sex);
                dataRow.CreateCell(3).SetCellValue(row.DateOfBirth);
                dataRow.CreateCell(4).SetCellValue(row.IdCard);
                dataRow.CreateCell(5).SetCellValue(row.ReasonForPetition);
                dataRow.CreateCell(6).SetCellValue(row.Attention);
                dataRow.CreateCell(7).SetCellValue(row.ResidenceAddress);
                dataRow.CreateCell(8).SetCellValue(row.RegisteredAddress);
                dataRow.CreateCell(9).SetCellValue(row.HouseholdRegistrationPoliceStation);
                dataRow.CreateCell(10).SetCellValue(row.NumberOfTheHouse);
                dataRow.CreateCell(11).SetCellValue(row.CurrentAddress);
                dataRow.CreateCell(12).SetCellValue(row.NoRoomReason);
                dataRow.CreateCell(13).SetCellValue(row.OtherAddress);
                dataRow.CreateCell(14).SetCellValue(row.FormerName);
                dataRow.CreateCell(15).SetCellValue(row.Employer);
                dataRow.CreateCell(16).SetCellValue(row.ContactNumber);
                dataRow.CreateCell(17).SetCellValue(row.ContactPhone); 
                dataRow.CreateCell(18).SetCellValue(row.Email);
                dataRow.CreateCell(19).SetCellValue(row.Nation);
                dataRow.CreateCell(20).SetCellValue(row.PoliticalStatus);
                dataRow.CreateCell(21).SetCellValue(row.Education);
                dataRow.CreateCell(22).SetCellValue(row.Occupation);
                dataRow.CreateCell(23).SetCellValue(row.MaritalStatus);
                dataRow.CreateCell(24).SetCellValue(row.BloodType);
                dataRow.CreateCell(25).SetCellValue(row.ReligiousBelief);
                dataRow.CreateCell(26).SetCellValue(row.Height);
                dataRow.CreateCell(27).SetCellValue(row.PetitionStatus);
                dataRow.CreateCell(28).SetCellValue(row.PetitionType);
                dataRow.CreateCell(29).SetCellValue(row.ServiceMembers);
                dataRow.CreateCell(30).SetCellValue(row.LatestServiceHours);
                dataRow.CreateCell(31).SetCellValue(row.Remarks);
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
    public class ListOfKeyData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnedNetwork { get; set; }
        public string Sex { get; set; }
        public string DateOfBirth { get; set; }
        public string IdCard { get; set; }
        public string ReasonForPetition { get; set; }
        
        public string Attention { get; set; }
        public string ResidenceAddress { get; set; }
        public string RegisteredAddress { get; set; }
        public string HouseholdRegistrationPoliceStation { get; set; }
        public string NumberOfTheHouse { get; set; }
        public string CurrentAddress { get; set; }
        public string NoRoomReason { get; set; }
        public string ContactNumber { get; set; }
        
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
        public string ReligiousBelief { get; set; }
        public string Height { get; set; }
        public string PetitionStatus { get; set; }
        public string PetitionType { get; set; }
        public string ServiceMembers { get; set; }
        public string LatestServiceHours { get; set; }
        public string Remarks { get; set; }

    }
}
