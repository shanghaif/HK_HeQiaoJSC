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
    public class DangOrganizationController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public DangOrganizationController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult List(UserInfoRequestpayload payload)
        {

            var query = from t in _dbContext.DangOrganization
                        where t.IsDeleted != 1
                        select new
                        {
                            t.Id,
                            t.DangOrganizationUuid,
                            t.DangOrganizationName,
                            t.DangClerk,
                            t.DangType,
                            OrganizationPeople = organpeople(t.DangOrganizationName)
                        };
            if (!string.IsNullOrEmpty(payload.Kw))
            {
                query = query.Where(x => x.DangOrganizationName.Contains(payload.Kw));
            }
            query = query.OrderByDescending(x => x.Id);
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            ToLog.AddLog("查询", "成功:查询:党组织信息数据", _dbContext);
            return Ok(response);
        }


        public static int organpeople(string DangOrganizationName)
        {
            using (haikanHeQiaoContext cc = new haikanHeQiaoContext())
            {
                int num = cc.CpcmanInfo.Where(x => x.DangOrganizationName == DangOrganizationName && x.IsDeleted != 1).Count();
                var query = cc.DangOrganization.FirstOrDefault(x => x.DangOrganizationName == DangOrganizationName);
                query.DangOrganizationPeople = num.ToString();
                cc.SaveChanges();
                return num;
            }
        }

        
        /// <summary>
        /// 列表显示
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult xiangList(UserInfoRequestpayload payload)
        {

            var query = from c in _dbContext.CpcmanInfo
                        where c.IsDeleted == 0
                        select new
                        {
                            c.Id,
                            c.DangOrganizationName,//党组织
                            c.RealName,         //姓名
                            c.Sex,              //性别
                            Birth = c.Birth.Value.ToString("yyyy-MM-dd"),            //生日
                            c.Education,        //学历
                            c.Politics,         //政治面貌
                            c.Age,
                            c.CpcmanUuid,
                        };
            if (!string.IsNullOrEmpty(payload.Kw1))
            {
                query = query.Where(x => x.DangOrganizationName.Contains(payload.Kw1));
            }
            query = query.OrderByDescending(x => x.Id);
            var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            var totalCount = query.Count();
            var response = ResponseModelFactory.CreateResultInstance;
            response.SetData(list, totalCount);
            ToLog.AddLog("查询", "成功:查询:党组织信息数据", _dbContext);
            return Ok(response);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(DangJibenInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                string name = model.DangOrganizationName;
                var query = _dbContext.DangOrganization.FirstOrDefault(x => x.DangOrganizationName == name);
                if (query != null)
                {
                    response.SetFailed("该组织已存在");
                    return Ok(response);
                }
                var entity = new HaikanSmartTownCockpit.Api.Entities.DangOrganization();
                entity.DangOrganizationUuid = Guid.NewGuid();
                entity.DangOrganizationName = model.DangOrganizationName;
                entity.DangType = model.DangType;
                entity.DangClerk = model.DangClerk;
                entity.ChuangliTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.IsDeleted = 0;
                _dbContext.DangOrganization.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:党组织信息一条数据", _dbContext);
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
        public IActionResult Edit(DangJibenInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            Guid guid = (Guid)model.DangOrganizationUuid;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                string name = model.DangOrganizationName;
                var query = _dbContext.DangOrganization.FirstOrDefault(x => x.DangOrganizationName == name);
                var entity = _dbContext.DangOrganization.FirstOrDefault(x => x.DangOrganizationUuid == guid);
                if (query != null && entity.DangOrganizationName != name)
                {
                    response.SetFailed("该组织已存在");
                    return Ok(response);
                }
                entity.DangOrganizationName = model.DangOrganizationName;
                entity.DangType = model.DangType;
                entity.DangClerk = model.DangClerk;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:党组织信息一条数据", _dbContext);
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
                var entity = _dbContext.DangOrganization.FirstOrDefault(x => x.DangOrganizationUuid == guid);
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
                var sql = string.Format("UPDATE DangOrganization SET IsDeleted=@isDeleted WHERE DangOrganizationUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:党组织信息一条数据", _dbContext);
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
    }
}
