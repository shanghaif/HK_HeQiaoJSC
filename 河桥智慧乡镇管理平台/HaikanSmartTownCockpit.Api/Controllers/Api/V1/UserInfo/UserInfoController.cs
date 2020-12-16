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
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaiKanStudentDiningManagementSystem.Api.Controllers.Api.V1.Cuisine
{
    [Route("api/v1/UserInfo/[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class UserInfoController : ControllerBase
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
        public UserInfoController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
            var query = from c in _dbContext.Userinfoty
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.RealName,         //姓名
                            c.Address,          //地址
                            c.Phone,            //手机
                            c.Email,            //邮箱
                            c.Sex,              //性别
                            c.Birth,            //生日
                            c.IdentityCard,     //身份证
                            c.Residence,        //居住地
                            c.Domicile,         //户籍地
                            c.Nation,           //民族
                            c.Education,        //学历
                            c.QianYiEtime,         //迁移地
                            c.QianYiStime,       //迁移时间
                            c.Occupation,       //职业
                            c.DyStaues,         //是否党员
                            c.Politics,         //政治面貌
                            c.Position,         //职位
                            c.Evaluate,         //考评级别
                            c.JoinArmy,         //参军意愿
                            c.ArmyTime,         //参军时间
                            c.Age,
                            c.UserInfoUuid,
                            c.Relation,
                            c.HouseholderName,
                            c.Household,

                        };

            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.RealName.Contains(payload.Kw.Trim()));
            }
            if (!string.IsNullOrEmpty(payload.Kw1))
            {
                query = query.Where(x => x.IdentityCard.Contains(payload.Kw1.Trim()));
            }
            if (!string.IsNullOrEmpty(payload.Kw2))
            {
                query = query.Where(x => x.Education.Contains(payload.Kw2.Trim()));
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
            ToLog.AddLog("查询", "成功:查询:统一人员信息信息数据", _dbContext);
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
                var entity = from c in _dbContext.Userinfoty
                             where c.UserInfoUuid == guid
                             select new
                             {
                                 c.Id,
                                 c.RealName,         //姓名
                                 c.Address,          //地址
                                 c.Phone,            //手机
                                 c.Email,            //邮箱
                                 c.Sex,              //性别
                                 c.Birth,            //生日
                                 c.IdentityCard,     //身份证
                                 c.Residence,        //居住地
                                 c.Domicile,         //户籍地
                                 c.Nation,           //民族
                                 c.Education,        //学历
                                 c.QianYiEtime,         //迁移地
                                 c.QianYiStime,       //迁移时间
                                 c.Occupation,       //职业
                                 c.DyStaues,         //是否党员
                                 c.Politics,         //政治面貌
                                 c.Position,         //职位
                                 c.Evaluate,         //考评级别
                                 c.JoinArmy,         //参军意愿
                                 c.ArmyTime,         //参军时间
                                 c.Age,
                                 c.UserInfoUuid,
                                 c.Relation,
                                 c.HouseholderName,
                                 c.Household,
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
                var entity = new HaikanSmartTownCockpit.Api.Entities.Userinfoty();
                entity.UserInfoUuid = Guid.NewGuid();
                entity.RealName = model.RealName;
                entity.Sex = model.Sex;
                entity.Education = model.Education;
                entity.Politics = model.Politics;
                entity.Nation = model.Nation;
                entity.IdentityCard = model.IdentityCard;
                entity.Occupation = model.Occupation;
                entity.Residence = model.Residence;
                entity.Domicile = model.Domicile;
                
                entity.Household = model.Household; 
                entity.Relation = model.Relation;
                entity.HouseholderName = model.HouseholderName;
                entity.Phone = model.Phone;
                if (model.Birth != null && model.Birth != "")
                {
                    entity.Birth = DateTime.Parse(model.Birth).ToString("yyyy-MM-dd");
                    DateTime now = DateTime.Now;
                    DateTime birth = Convert.ToDateTime(model.Birth);
                    int age = now.Year - birth.Year;
                    if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
                    {
                        age--;
                    }
                    entity.Age = age < 0 ? 0 : age;
                }
                else
                {
                    entity.Birth = "";
                }
                if (model.QianYiEtime != null && model.QianYiEtime != "")
                {
                    entity.QianYiEtime = DateTime.Parse(model.QianYiEtime).ToString("yyyy-MM-dd");
                }
                else
                {
                    entity.QianYiEtime = "";
                }
                if (model.QianYiStime != null && model.QianYiStime != "")
                {
                    entity.QianYiStime = DateTime.Parse(model.QianYiStime).ToString("yyyy-MM-dd");
                }
                else
                {
                    entity.QianYiStime = "";
                }
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                //entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                //entity.AddPeople = model.addPeople;
                entity.IsDeleted = 0;
                _dbContext.Userinfoty.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:统一人员信息一条数据", _dbContext);
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
            string guid = model.UserInfoUuid;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.Userinfoty.FirstOrDefault(x => x.UserInfoUuid == Guid.Parse(guid));
                entity.RealName = model.RealName;
                entity.Sex = model.Sex;
                entity.Education = model.Education;
                entity.Politics = model.Politics;
                entity.Nation = model.Nation;
                entity.Household = model.Household;
                entity.Relation = model.Relation;
                entity.HouseholderName = model.HouseholderName;
                if (model.Birth != null && model.Birth != "")
                {
                    entity.Birth = DateTime.Parse(model.Birth).ToString("yyyy-MM-dd");
                    DateTime now = DateTime.Now;
                    DateTime birth = Convert.ToDateTime(model.Birth);
                    int age = now.Year - birth.Year;
                    if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
                    {
                        age--;
                    }
                    entity.Age = age < 0 ? 0 : age;
                }
                else 
                {
                    entity.Birth = "";
                }
                if (model.QianYiEtime != null && model.QianYiEtime != "")
                {
                    entity.QianYiEtime = DateTime.Parse(model.QianYiEtime).ToString("yyyy-MM-dd");
                }
                else
                {
                    entity.QianYiEtime = "";
                }
                if (model.QianYiStime != null && model.QianYiStime != "")
                {
                    entity.QianYiStime = DateTime.Parse(model.QianYiStime).ToString("yyyy-MM-dd");
                }
                else
                {
                    entity.QianYiStime = "";
                }
                entity.IdentityCard = model.IdentityCard;
                entity.Occupation = model.Occupation;
                entity.Residence = model.Residence;
                entity.Domicile = model.Domicile;
                
                entity.Phone = model.Phone;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:统一人员信息一条数据", _dbContext);
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
                var sql = string.Format("UPDATE Userinfoty SET IsDeleted=@isDeleted WHERE UserInfoUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:统一人员信息一条数据", _dbContext);
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
                string uploadtitle = " 人员信息导入" + DateTime.Now.ToString("yyyyMMddHHmmss");
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

                            var entity = new HaikanSmartTownCockpit.Api.Entities.Userinfoty();
                            entity.UserInfoUuid = Guid.NewGuid();
                            //Regex regxm = new Regex("^([\\u4e00-\\u9fa5]){2,7}$");
                            //if (regxm.IsMatch(dt.Rows[i]["姓名"].ToString()))
                            //{
                            if (string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString().Trim())){
                                continue;
                            }
                            else
                            {
                                entity.RealName = dt.Rows[i]["姓名"].ToString();
                            }


                            //}
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行姓名格式不正确" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            //Regex regxb = new Regex("^(男|女)$");
                            //if (regxb.IsMatch(dt.Rows[i]["性别"].ToString()))
                            //{
                            //entity.Sex = dt.Rows[i]["性别"].ToString();
                            //}
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行性别不正确" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            //Regex regbr = new Regex("^((((1[6-9]|[2-9]\\d)\\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})-0?2-(0?[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");

                            //if (regbr.IsMatch(dt.Rows[i]["出生日期"].ToString()))
                            //{
                            //entity.Birth = dt.Rows[i]["出生日期"].ToString();
                            //DateTime now = DateTime.Now;
                            //DateTime birth = Convert.ToDateTime(entity.Birth);
                            //int age = now.Year - birth.Year;
                            //if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
                            //{
                            //    age--;
                            //}
                            //entity.Age = age < 0 ? 0 : age;
                            //}
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行出生日期不正确,例:1999-09-09" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}

                            Regex regsfz = new Regex("^(^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$)|(^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[Xx])$)$");
                            if (regsfz.IsMatch(dt.Rows[i]["身份证号"].ToString()))
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i]["身份证号"].ToString()))
                                {
                                    string ddd = dt.Rows[i]["身份证号"].ToString();
                                    entity.IdentityCard = dt.Rows[i]["身份证号"].ToString();
                                    if (ddd != "" && ddd != null)
                                    {
                                        string sex = "";
                                        //判断身份证是否18位
                                        if (ddd.Length == 18)
                                        {
                                            entity.Birth = ddd.Substring(6, 4) + "-" + ddd.Substring(10, 2) + "-" + ddd.Substring(12, 2);
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
                                            entity.Birth = DateTime.Parse(d1).ToString("yyyy-MM-dd");
                                            DateTime now = DateTime.Now;
                                            DateTime birth = Convert.ToDateTime(d1);
                                            int age = now.Year - birth.Year;
                                            if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
                                            {
                                                age--;
                                            }
                                            entity.Age = age < 0 ? 0 : age;
                                        }
                                        //判断身份证是否15位
                                        if (ddd.Length == 15)
                                        {
                                            entity.Birth = "19" + ddd.Substring(6, 2) + "-" + ddd.Substring(8, 2) + "-" + ddd.Substring(10, 2);
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
                                            entity.Birth = DateTime.Parse(d1).ToString("yyyy-MM-dd");
                                            DateTime now = DateTime.Now;
                                            DateTime birth = Convert.ToDateTime(d1);
                                            int age = now.Year - birth.Year;
                                            if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
                                            {
                                                age--;
                                            }
                                            entity.Age = age < 0 ? 0 : age;
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
                            if (dt.Rows[i]["户籍地"]!=null)
                            {
                                entity.Residence = dt.Rows[i]["户籍地"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["地址"].ToString()))
                            {
                                entity.Domicile = dt.Rows[i]["地址"].ToString();
                            }
                            //Regex reg = new Regex("^(汉族|蒙古族|回族|藏族|维吾尔族|苗族|彝族|壮族|布依族|朝鲜族|满族|侗族|瑶族|白族|土家族|哈尼族|哈萨克族|傣族|黎族|僳僳族|佤族|畲族|高山族|拉祜族|水族|东乡族|纳西族|景颇族|柯尔克孜族|土族|达斡尔族|仫佬族|羌族|布朗族|撒拉族|毛南族|仡佬族|锡伯族|阿昌族|普米族|塔吉克族|怒族|乌孜别克族|俄罗斯族|鄂温克族|德昂族|保安族|京族|独龙族|鄂伦春族|赫哲族|裕固族|门巴族|珞巴族|基诺族)$");
                            //if (reg.IsMatch(dt.Rows[i]["民族"].ToString()))
                            //{
                                //entity.Nation = dt.Rows[i]["民族"].ToString();
                            //}
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行民族不正确" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            //Regex reg1 = new Regex("^(小学|初中|中专|大专|本科|硕士|博士)$");
                            //if (reg1.IsMatch(dt.Rows[i]["学历"].ToString()))
                            //{
                                //entity.Education = dt.Rows[i]["学历"].ToString();
                            //}
                            //else
                            //{
                            //    responsemsgdefault += "<p style='color:red'>" + "第" + (i + 2) + "行学历类别不正确" + "</p></br>";
                            //    defaultcount++;
                            //    continue;
                            //}
                            if (!string.IsNullOrEmpty(dt.Rows[i]["户别"]?.ToString()))
                            {
                                entity.Household = dt.Rows[i]["户别"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["电话号码"]?.ToString()))
                            {
                                entity.Phone = dt.Rows[i]["电话号码"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["与户主的关系"]?.ToString()))
                            {
                                entity.Relation = dt.Rows[i]["与户主的关系"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["户主姓名"]?.ToString()))
                            {
                                entity.HouseholderName = dt.Rows[i]["户主姓名"].ToString();
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[i]["迁入时间"].ToString()))
                            {
                                entity.QianYiStime = dt.Rows[i]["迁入时间"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["迁出时间"].ToString()))
                            {
                                entity.QianYiEtime = dt.Rows[i]["迁出时间"].ToString();
                            }
                            //if (!string.IsNullOrEmpty(dt.Rows[i]["职业"].ToString()))
                            //{
                            //    entity.Occupation = dt.Rows[i]["职业"].ToString();
                            //}
                            if (!string.IsNullOrEmpty(dt.Rows[i]["政治面貌"].ToString()))
                            {
                                entity.Politics = dt.Rows[i]["政治面貌"].ToString();
                                if (dt.Rows[i]["政治面貌"].ToString().Contains("党")) {
                                    entity.DyStaues = "1";
                                }
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
                            _dbContext.Userinfoty.Add(entity);
                            _dbContext.SaveChanges();
                            successcount++;
                        }
                    }
                    responsemsgsuccess = "<p style='color:green'>导入成功:" + successcount + "条</p></br>" + responsemsgsuccess;
                    responsemsgrepeat = "<p style='color:orange'>重复需手动修改数据:" + repeatcount + "条</p></br>" + responsemsgrepeat;
                    responsemsgdefault = "<p style='color:red'>导入失败:" + defaultcount + "条</p></br>" + responsemsgdefault;

                    ToLog.AddLog("导入", "成功:导入:统一人员信息数据", _dbContext);
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
                    var query1 = _dbContext.Userinfoty.Where(x => x.IsDeleted != 1 && parameters.Contains(x.UserInfoUuid.ToString())).Select(x => new HaikanSmartTownCockpit.Api.Entities.Userinfoty
                    {
                        Id = x.Id,
                        RealName = x.RealName,
                        Sex = x.Sex,
                        Birth = x.Birth,
                        IdentityCard = x.IdentityCard,
                        Residence = x.Residence,
                        Domicile = x.Domicile,
                        Nation = x.Nation,
                        Education = x.Education,
                        QianYiEtime = x.QianYiEtime,
                        QianYiStime = x.QianYiStime,
                        Occupation = x.Occupation,
                        Politics = x.Politics,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:统一人员信息数据", _dbContext);
                    return Ok(response);
                }
                else {
                    var query1 = _dbContext.Userinfoty.Where(x => x.IsDeleted != 1 ).Select(x => new HaikanSmartTownCockpit.Api.Entities.Userinfoty
                    {
                        Id = x.Id,
                        RealName = x.RealName,
                        Sex = x.Sex,
                        Birth = x.Birth,
                        IdentityCard = x.IdentityCard,
                        Residence = x.Residence,
                        Domicile = x.Domicile,
                        Nation = x.Nation,
                        Education = x.Education,
                        QianYiStime = x.QianYiStime,
                        QianYiEtime = x.QianYiEtime,
                        Occupation = x.Occupation,
                        Politics = x.Politics,
                    });
                    var query = query1.OrderByDescending(x => x.Id).ToList();
                    string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\UploadFiles\\ImportUserInfoExcel\\";
                    string uploadtitle = "人员信息导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sFileName = $"{sWebRootFolder + uploadtitle}.xlsx";
                    //CuisineViewModel model = new CuisineViewModel();
                    //model.Demos = query;
                    TablesToExcel(query, sFileName);
                    response.SetData("\\UploadFiles\\ImportUserInfoExcel\\" + uploadtitle + ".xlsx");
                    ToLog.AddLog("导出", "成功:导出:统一人员信息数据", _dbContext);
                    return Ok(response);
                }
 
 
            }

        }





        public static bool TablesToExcel(List<HaikanSmartTownCockpit.Api.Entities.Userinfoty> query, string filename)
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

        private static void CreatSheet(ISheet sheet, List<HaikanSmartTownCockpit.Api.Entities.Userinfoty> query)
        {
            IRow headerRow = sheet.CreateRow(0);
            List<string> list = new List<string>() {
                "姓名","性别","出生日期","身份证号","居住地","户籍地","民族","学历","迁入时间","迁出时间","职业","政治面貌"
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
                dataRow.CreateCell(1).SetCellValue(row.Sex);
                dataRow.CreateCell(2).SetCellValue(row.Birth);
                dataRow.CreateCell(3).SetCellValue(row.IdentityCard);
                dataRow.CreateCell(4).SetCellValue(row.Residence);
                dataRow.CreateCell(5).SetCellValue(row.Domicile);
                dataRow.CreateCell(6).SetCellValue(row.Nation);
                dataRow.CreateCell(7).SetCellValue(row.Education);
                dataRow.CreateCell(8).SetCellValue(row.QianYiStime);
                dataRow.CreateCell(9).SetCellValue(row.QianYiEtime);
                dataRow.CreateCell(10).SetCellValue(row.Occupation);
                dataRow.CreateCell(11).SetCellValue(row.Politics);
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
