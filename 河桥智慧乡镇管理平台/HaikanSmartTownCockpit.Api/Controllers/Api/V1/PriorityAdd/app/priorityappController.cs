using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.ViewModels.Priority;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Priority.app
{
    [Route("api/v1/priorityAdd/app/[controller]/[action]")]
    [ApiController]
    public class priorityappController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        public priorityappController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        //添加重点工作
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(priorityCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {

                var entity = new HaikanSmartTownCockpit.Api.Entities.Priority();


                entity.PriorityUuid = Guid.NewGuid();
                entity.PriorityHeadline = model.PriorityHeadline;
                entity.PriorityDescribe = model.PriorityDescribe;
                entity.Principal = model.Principal;
                entity.Participant = "," + model.Participant + ",";
                entity.Sortord = model.Sortord;
                entity.Recycle = "0";
                entity.Accomplish = "0";
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//创建时间
                entity.EndTime = Convert.ToDateTime(model.EndTime).ToString("yyyy-MM-dd HH:mm:ss");
                entity.EstablishName = model.EstablishName;//创建人uuid
                _dbContext.Priority.Add(entity);
                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }

        //用guid查负责人
        [HttpGet("{type}")]
        public IActionResult prioritybyguid(string type)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.Priority
                            join u in _dbContext.SystemUser
                            on p.Principal equals u.SystemUserUuid.ToString()
                            select new
                            {
                                fuzerenuuid = p.Principal,//负责人uuid
                                fuzerename = u.RealName,
                            };
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query.OrderBy(a => a.fuzerename).ToList());
                return Ok(response);
            }
        }
        /// <summary>
        /// 查询出所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("{type}")]
        public IActionResult gealluser(string type)
        {
            using (_dbContext)
            {
                if (type == "duo")
                {
                    var query = from s in _dbContext.SystemUser
                                where s.IsDeleted == 0
                                select new
                                {
                                    systemUserUUID = s.SystemUserUuid,
                                    value = s.Id,
                                    loginName = s.LoginName,//登录名
                                    name = s.RealName,//真实姓名
                                    isDeleted = s.IsDeleted,
                                    checkeds = false,

                                };
                    var response = ResponseModelFactory.CreateResultInstance;
                    response.SetData(query.ToList());
                    return Ok(response);
                }
                else
                {
                    var query = from s in _dbContext.SystemUser
                                where s.IsDeleted == 0
                                select new
                                {
                                    value = s.SystemUserUuid,
                                    loginName = s.LoginName,//登录名
                                    name = s.RealName,//真实姓名
                                    isDeleted = s.IsDeleted,
                                    checkeds = false,

                                };
                    var response = ResponseModelFactory.CreateResultInstance;
                    response.SetData(query.OrderBy(a => a.name).ToList());
                    return Ok(response);
                }

            }
        }
    }
}