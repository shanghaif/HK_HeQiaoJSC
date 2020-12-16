using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.dangyuanxinxi
{
    [Route("api/v1/dangyuanxinxi/[controller]/[action]")]
    [ApiController]
    public class dangyuanController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public dangyuanController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        ///  党员信息
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult dangyuan(string name)
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.Userinfoty
                            where c.IsDeleted == 0 && c.DyStaues == "1" && c.Residence == name
                            select new
                            {
                                RealName = c.RealName == null ? "" : c.RealName != null ? c.RealName : "",
                                Phone = c.Phone == null ? "" : c.Phone != null ? c.Phone : "",
                                Sex = c.Sex == null ? "" : c.Sex != null ? c.Sex : "",
                                IdentityCard = c.IdentityCard == null ? "" : c.IdentityCard != null ? c.IdentityCard : "",
                                Politics = c.Politics == null ? "" : c.Politics != null ? c.Politics : "",
                            };
            var query1 = query.ToList();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(query1);
            return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult qingnian(string name)
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.Youth
                            where c.IsDeleted == 0 && c.Census == name
                            select new
                            {
                                YouthName = c.YouthName == null ? "" : c.YouthName != null ? c.YouthName : "",
                                Phone = c.Phone == null ? "" : c.Phone != null ? c.Phone : "",
                                Sex = c.Sex == null ? "" : c.Sex != null ? c.Sex : "",
                                Occupation = c.Occupation == null ? "" : c.Occupation != null ? c.Occupation : "",
                                ArmyWill = c.ArmyWill == null ? "" : c.ArmyWill != null ? c.ArmyWill : "",
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult minbing(string name)
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.TownMilitia
                            where c.IsDeleted == 0 && c.Census == name
                            select new
                            {
                                TownMilitiaName = c.TownMilitiaName == null ? "" : c.TownMilitiaName != null ? c.TownMilitiaName : "",
                                Sex = c.Sex == null ? "" : c.Sex != null ? c.Sex : "",
                                Phone = c.Phone == null ? "" : c.Phone != null ? c.Phone : "",
                                Occupation = c.Occupation == null ? "" : c.Occupation != null ? c.Occupation : "",
                                ArmyTime = c.ArmyTime == null ? "" : c.ArmyTime != null ? c.ArmyTime : "",
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult dangyuankaoping(string name)
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.Userinfoty
                            where c.IsDeleted == 0 && c.DyStaues == "1" && c.Residence == name
                            select new
                            {
                                RealName = c.RealName == null ? "" : c.RealName != null ? c.RealName : "",
                                Sex = c.Sex == null ? "" : c.Sex != null ? c.Sex : "",
                                Politics = c.Politics == null ? "" : c.Politics != null ? c.Politics : "",
                                Occupation = c.Occupation == null ? "" : c.Occupation != null ? c.Occupation : "",
                                Evaluate = c.Evaluate == null ? "" : c.Evaluate != null ? c.Evaluate : "",
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult huodong(string name)
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.Activity
                            where c.IsDeleted == 0 && c.ActivityName == name
                            select new
                            {
                                ActivityName = c.ActivityName == null ? "" : c.ActivityName != null ? c.ActivityName : "",
                                ActivityTime = c.ActivityTime == null ? "" : c.ActivityTime != null ? c.ActivityTime : "",
                                ActivityWay = c.ActivityWay == null ? "" : c.ActivityWay != null ? c.ActivityWay : "",
                                Address = c.Address == null ? "" : c.Address != null ? c.Address : "",
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult zufang(string name)
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.RentoutRoom
                            where c.IsDeleted == 0 && c.RentoutInfo.Contains(name)
                            select new
                            {
                                RentoutInfo = c.RentoutInfo == null ? "" : c.RentoutInfo != null ? c.RentoutInfo : "",
                                RentoutYezhu = c.RentoutYezhu == null ? "" : c.RentoutYezhu != null ? c.RentoutYezhu : "",
                                RentoutZuhu = c.RentoutZuhu == null ? "" : c.RentoutZuhu != null ? c.RentoutZuhu : "",
                                RentoutMoney = c.RentoutMoney == null ? "" : c.RentoutMoney != null ? c.RentoutMoney : "",
                                c.RentoutStaues,
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult jiaozheng(string name)
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.RectifyInfo
                            where c.IsDeleted == 0 && c.RectifyInfoUnit == name
                            select new
                            {
                                RectifyInfoName = c.RectifyInfoName == null ? "" : c.RectifyInfoName != null ? c.RectifyInfoName : "",
                                RectifyInfoStaues = c.RectifyInfoStaues == null ? "" : c.RectifyInfoStaues != null ? c.RectifyInfoStaues : "",
                                DweiPhone = c.DweiPhone == null ? "" : c.DweiPhone != null ? c.DweiPhone : "",
                                ShangbanStaues = c.ShangbanStaues == null ? "" : c.ShangbanStaues != null ? c.ShangbanStaues : "",
                                RectifyTiem = c.RectifyTiem == null ? "" : c.RectifyTiem != null ? c.RectifyTiem : "",
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult zhifa(string name)
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.CityManagement
                            where c.IsDeleted == 0 && c.ZhifaAddress == name
                            select new
                            {
                                Incident = c.Incident == null ? "" : c.Incident != null ? c.Incident : "",
                                ZhifaTime = c.ZhifaTime == null ? "" : c.ZhifaTime != null ? c.ZhifaTime : "",
                                ZhifaAddress = c.ZhifaAddress == null ? "" : c.ZhifaAddress != null ? c.ZhifaAddress : "",
                                ChuliQingk = c.ChuliQingk == null ? "" : c.ChuliQingk != null ? c.ChuliQingk : "",
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult jiaoban(string name)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                if (name != "")
                {
                    var dep = _dbContext.Department.FirstOrDefault(x => x.Name == name && x.IsDeleted == 0);
                    if (dep == null)
                    {
                        var town = _dbContext.Town.FirstOrDefault(x => x.TownName == name && x.IsDeleted == 0);
                        var query = from e in _dbContext.Mission
                                    where e.Recycle == "0" && e.AdministrativeOffice.Contains(town.TownUuid.ToString())
                                    select new
                                    {
                                        MissionHeadline = e.MissionHeadline == null ? "" : e.MissionHeadline != null ? e.MissionHeadline : "",
                                        StartTime = e.StartTime == null ? "" : e.StartTime != null ? e.StartTime : "",
                                        FinishTime = e.FinishTime == null ? "" : e.FinishTime != null ? e.FinishTime : "",
                                        AuditStatus = e.AuditStatus == null ? "待下派" : e.AuditStatus == "0" ? "办理中" : e.AuditStatus == "1" ? "审核中" : e.AuditStatus == "2" ? "已完成" : "待下派",
                                    };
                        var query1 = query.ToList();
                        response.SetData(query1);
                        return Ok(response);
                    }
                    var query2 = from e in _dbContext.Mission
                                where e.Recycle == "0" && e.AdministrativeOffice.Contains(dep.DepartmentUuid.ToString())
                                 select new
                                 {
                                     MissionHeadline = e.MissionHeadline == null ? "" : e.MissionHeadline != null ? e.MissionHeadline : "",
                                     StartTime = e.StartTime == null ? "" : e.StartTime != null ? e.StartTime : "",
                                     FinishTime = e.FinishTime == null ? "" : e.FinishTime != null ? e.FinishTime : "",
                                     AuditStatus = e.AuditStatus == null ? "待下派" : e.AuditStatus == "0" ? "办理中" : e.AuditStatus == "1" ? "审核中" : e.AuditStatus == "2" ? "已完成" : "待下派",
                                 };
                    var query3 = query2.ToList();
                    response.SetData(query3);
                    return Ok(response);
                }

                response.SetData(response);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult renwu()
        {
            using (_dbContext)
            {
                var query = from e in _dbContext.Mission
                            where e.Recycle == "0" 
                            select new
                            {
                                MissionHeadline = e.MissionHeadline == null ? "" : e.MissionHeadline != null ? e.MissionHeadline : "",
                                StartTime = e.StartTime == null ? "" : e.StartTime != null ? e.StartTime : "",
                                FinishTime = e.FinishTime == null ? "" : e.FinishTime != null ? e.FinishTime : "",
                                AuditStatus = e.AuditStatus == null ? "待下派" : e.AuditStatus == "0" ? "办理中" : e.AuditStatus == "1" ? "审核中" : e.AuditStatus == "2" ? "已完成" : "待下派",
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }
    }


}
