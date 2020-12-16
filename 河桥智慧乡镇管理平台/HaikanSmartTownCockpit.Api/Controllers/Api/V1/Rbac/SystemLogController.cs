using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using HaikanSmartTownCockpit.Api.RequestPayload.Rbac.SystemLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Rbac
{
    /// <summary>
    /// 系统日志
    /// </summary>
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class SystemLogController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public SystemLogController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 操作日志列表
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult LogList(SystemLogRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from x in _dbContext.SystemLog
                            //join u in _dbContext.SystemUser
                            //on Guid.Parse(x.UserId) equals u.SystemUserUuid
                            select new
                            {
                                x.Id,
                                x.SystemLogUuid,
                                x.UserId,
                                x.UserName,
                                x.OperationTime,
                                x.OperationContent,
                                x.Ipaddress,
                                AddTime=x.AddTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                x.Type,
                                //u.LoginName,
                            };

                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.Type.Contains(payload.Kw.Trim()));
                }
                if (!string.IsNullOrEmpty(payload.Kw1)&&DateTime.TryParse(payload.Kw1,out var date))
                {
                    var endDate = date.AddDays(1);
                    query = query.Where(x => x.OperationTime.Value>=date&&x.OperationTime.Value<endDate);
                }
                var query2 = query.Select(x => new
                {
                    x.Id,
                    x.SystemLogUuid,
                    x.UserId,
                    x.UserName,
                    OperationTime = x.OperationTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    x.OperationContent,
                    x.Ipaddress,
                    x.AddTime,
                    x.Type,
                });
                query2 = query2.OrderByDescending(x => x.Id);
                var list = query2.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query2.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                return Ok(response);
            }
        }
    }
}
