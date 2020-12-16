using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Haikan3.Utils;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
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

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.KeyPoint
{
    [Route("api/v1/KeyPoint/[controller]/[action]")]
    [ApiController]
    public class DangerousController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;

        public DangerousController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(UserInfoRequestpayload payload)
        {
            var query = _dbContext.DangerousGoods.Where(x => x.IsDeleted == 0);
            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.Name.Contains(payload.Kw.Trim()));
            }
            query = query.OrderByDescending(x => x.Id);
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            ToLog.AddLog("查询", "成功:查询:危险品从业人口信息数据", _dbContext);
            return Ok(response);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(DangerousViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new DangerousGoods();
                entity.MentallyUuid = Guid.NewGuid();
                entity.Name = model.Name;
                entity.OwningGrid = model.OwningGrid;
                entity.Sex = model.Sex;
                entity.DateBirth = model.DateBirth;
                entity.IdCard = model.IdCard;
                entity.Danger = model.Danger;
                entity.Employer = model.Employer;
                entity.Attention = model.Attention;
                entity.LegalPerson = model.LegalPerson;
                entity.LegalPersonPhone = model.LegalPersonPhone;
                entity.CorporatePhone = model.CorporatePhone;
                entity.ResidenceAddress = model.ResidenceAddress;
                entity.RegisteredAddress = model.RegisteredAddress;
                entity.PoliceStation = model.PoliceStation;
                entity.HousesNumber = model.HousesNumber;
                entity.CurrentAddress = model.CurrentAddress;
                entity.RoomReason = model.RoomReason;
                entity.OtherAddress = model.OtherAddress;
                entity.FormerName = model.FormerName;
                entity.Phone = model.Phone;
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
                entity.Work = model.Work;
                entity.WorkNumber = model.WorkNumber;
                entity.EffectiveTime = model.EffectiveTime;
                entity.EndTime = model.EndTime;
                entity.Waiter = model.Waiter;
                entity.ServiceHours = model.ServiceHours;
                entity.Remarks = model.Remarks;
                entity.IsDeleted = 0;
                _dbContext.DangerousGoods.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:危险品从业人口信息一条数据", _dbContext);
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
        public IActionResult Edit(DangerousViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.DangerousGoods.FirstOrDefault(x => x.MentallyUuid == model.MentallyUuid);
                entity.Name = model.Name;
                entity.OwningGrid = model.OwningGrid;
                entity.Sex = model.Sex;
                entity.DateBirth = model.DateBirth;
                entity.IdCard = model.IdCard;
                entity.Danger = model.Danger;
                entity.Employer = model.Employer;
                entity.Attention = model.Attention;
                entity.LegalPerson = model.LegalPerson;
                entity.LegalPersonPhone = model.LegalPersonPhone;
                entity.CorporatePhone = model.CorporatePhone;
                entity.ResidenceAddress = model.ResidenceAddress;
                entity.RegisteredAddress = model.RegisteredAddress;
                entity.PoliceStation = model.PoliceStation;
                entity.HousesNumber = model.HousesNumber;
                entity.CurrentAddress = model.CurrentAddress;
                entity.RoomReason = model.RoomReason;
                entity.OtherAddress = model.OtherAddress;
                entity.FormerName = model.FormerName;
                entity.Phone = model.Phone;
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
                entity.Work = model.Work;
                entity.WorkNumber = model.WorkNumber;
                entity.EffectiveTime = model.EffectiveTime;
                entity.EndTime = model.EndTime;
                entity.Waiter = model.Waiter;
                entity.ServiceHours = model.ServiceHours;
                entity.Remarks = model.Remarks;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:危险品从业人口信息一条数据", _dbContext);
                }
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
                var entity = _dbContext.DangerousGoods.FirstOrDefault(x => x.MentallyUuid == guid);
                response.SetData(entity);
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除单个
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
        /// 删除
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
                var sql = string.Format("UPDATE DangerousGoods SET IsDeleted=@IsDeleted WHERE MentallyUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:危险品从业人口信息一条数据", _dbContext);
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
                string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\KeyPointExcel";
                //var schoolinfo = _dbContext.SchoolInforManagement.AsQueryable();
                string uploadtitle = "危险品从业人口信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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

                            var entity = new HaikanSmartTownCockpit.Api.Entities.DangerousGoods();
                            entity.MentallyUuid = Guid.NewGuid();
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
                                            entity.DateBirth =Convert.ToDateTime(d1);
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
                                            entity.DateBirth = Convert.ToDateTime(d1);
                                        }


                                    }
                                }
                            }
                            else
                            {
                                responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行身份证号格式不正确" + "</p></br>";
                                defaultcount++;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["所属网路"].ToString()))
                            {
                                entity.OwningGrid = dt.Rows[i]["所属网路"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["危险从业类别"].ToString()))
                            {
                                entity.Danger = dt.Rows[i]["危险从业类别"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["关注程度"].ToString()))
                            {
                                entity.Employer = dt.Rows[i]["关注程度"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["工作单位"].ToString()))
                            {
                                entity.Attention = dt.Rows[i]["工作单位"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["企业法人代表"].ToString()))
                            {
                                entity.LegalPerson = dt.Rows[i]["企业法人代表"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["企业法人联系手机"].ToString()))
                            {
                                entity.LegalPersonPhone = dt.Rows[i]["企业法人联系手机"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["企业法人联系电话"].ToString()))
                            {
                                entity.CorporatePhone = dt.Rows[i]["企业法人联系电话"].ToString();
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
                            if (!string.IsNullOrEmpty(dt.Rows[i]["从业资格证书"].ToString()))
                            {
                                entity.Work = dt.Rows[i]["从业资格证书"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["从业资格证书号"].ToString()))
                            {
                                entity.WorkNumber = dt.Rows[i]["从业资格证书号"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["有效期开始日期"].ToString()))
                            {
                                entity.EffectiveTime =Convert.ToDateTime(dt.Rows[i]["有效期开始日期"].ToString());
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["有效期结束日期"].ToString()))
                            {
                                entity.EndTime = Convert.ToDateTime(dt.Rows[i]["有效期结束日期"].ToString());
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
                            _dbContext.DangerousGoods.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;
                    ToLog.AddLog("导入", "成功:导入:危险品从业人口信息一条数据", _dbContext);
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
                List<string> headers = new List<string>()
                    {
                        "姓名","所属网路","性别","出生日期","身份证","危险从业类别","关注程度","工作单位","企业法人代表","企业法人联系手机","企业法人联系电话","户籍地址","户籍地详址","户籍派出所","房屋编号","现住地址","无房原因","其他地址","曾用名","联系电话","联系手机","电子邮件","民族","政治面貌","文化程度","职业","婚姻状况","血型","宗教信仰","身高","从业资格证书","从业资格证书号","有效期开始日期","有效期结束日期","有无服务成员","最新服务时间","备注"
                    };
                List<string> rows = new List<string>()
                    {
                        "Name","OwningGrid","Sex","DateBirth","IdCard","Danger","Employer","Attention","LegalPerson","LegalPersonPhone","CorporatePhone","ResidenceAddress","RegisteredAddress","PoliceStation","HousesNumber","CurrentAddress","RoomReason","OtherAddress","FormerName","Phone","ContactPhone","Email","Nation","PoliticalStatus","Education","Occupation","MaritalStatus","BloodType","Religious","Height","Work","WorkNumber","EffectiveTime","EndTime","Waiter","ServiceHours","Remarks"
                    };
                if (ids != null)
                {
                    var parameters = ids.Split(",");
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = parameters[i].ToUpper();
                    }
                    var query1 = _dbContext.DangerousGoods.Where(x => x.IsDeleted != 1 && parameters.Contains(x.MentallyUuid.ToString())).OrderByDescending(x => x.Id).Select(x => new DangerousGoods
                    {

                        Name = x.Name,
                        OwningGrid = x.OwningGrid,
                        Sex = x.Sex,
                        DateBirth = x.DateBirth,
                        IdCard = x.IdCard,
                        Danger = x.Danger,
                        Employer = x.Employer,
                        Attention = x.Attention,
                        LegalPerson = x.LegalPerson,
                        LegalPersonPhone = x.LegalPersonPhone,
                        CorporatePhone = x.CorporatePhone,
                        ResidenceAddress = x.ResidenceAddress,
                        RegisteredAddress = x.RegisteredAddress,
                        PoliceStation = x.PoliceStation,
                        HousesNumber = x.HousesNumber,
                        CurrentAddress = x.CurrentAddress,
                        RoomReason = x.RoomReason,
                        OtherAddress = x.OtherAddress,
                        FormerName = x.FormerName,
                        Phone = x.Phone,
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
                        Work = x.Work,
                        WorkNumber = x.WorkNumber,
                        EffectiveTime = x.EffectiveTime,
                        EndTime = x.EndTime,
                        Waiter = x.Waiter,
                        ServiceHours = x.ServiceHours,
                        Remarks = x.Remarks
                    });
                    var query = query1.ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\KeyPointExcel\\";
                    string uploadtitle = "危险品从业人口信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";

                    DataToExcel.TablesToExcel<DangerousGoods>(query, sFileName, headers, rows);
                    response.SetData("\\UploadFiles\\KeyPointExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:危险品从业人口信息一条数据", _dbContext);
                    return Ok(response);
                }
                else
                {
                    var query1 = _dbContext.DangerousGoods.Where(x => x.IsDeleted != 1).OrderByDescending(x => x.Id).Select(x => new DangerousGoods
                    {
                        Name = x.Name,
                        OwningGrid = x.OwningGrid,
                        Sex = x.Sex,
                        DateBirth = x.DateBirth,
                        IdCard = x.IdCard,
                        Danger = x.Danger,
                        Employer = x.Employer,
                        Attention = x.Attention,
                        LegalPerson = x.LegalPerson,
                        LegalPersonPhone = x.LegalPersonPhone,
                        CorporatePhone = x.CorporatePhone,
                        ResidenceAddress = x.ResidenceAddress,
                        RegisteredAddress = x.RegisteredAddress,
                        PoliceStation = x.PoliceStation,
                        HousesNumber = x.HousesNumber,
                        CurrentAddress = x.CurrentAddress,
                        RoomReason = x.RoomReason,
                        OtherAddress = x.OtherAddress,
                        FormerName = x.FormerName,
                        Phone = x.Phone,
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
                        Work = x.Work,
                        WorkNumber = x.WorkNumber,
                        EffectiveTime = x.EffectiveTime,
                        EndTime = x.EndTime,
                        Waiter = x.Waiter,
                        ServiceHours = x.ServiceHours,
                        Remarks = x.Remarks
                    });
                    var query = query1.ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\KeyPointExcel\\";
                    string uploadtitle = "危险品从业人口信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    DataToExcel.TablesToExcel<DangerousGoods>(query, sFileName, headers, rows);
                    response.SetData("\\UploadFiles\\KeyPointExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:危险品从业人口信息一条数据", _dbContext);
                    return Ok(response);
                }


            }

        }
    }
}
