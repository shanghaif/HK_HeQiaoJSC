using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using HaikanSmartTownCockpit.Api.RequestPayload.SecurityCode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.OpenXmlFormats.Wordprocessing;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Safe
{
    [Route("api/v1/safe/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class SecurityCodeStatisticController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public SecurityCodeStatisticController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        #region 后台管理列表
        /// <summary>
        /// 显示所有信息
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(SecurityCodeStatisticRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.SecurityCode
                            where p.IsDeleted==0
                            group p by p.Address into g
                            select new
                            {
                                Address = g.Key,
                                Count = g.Count(),
                                wcount = _dbContext.SecurityCode.Count(x=>x.Address== g.Key&&x.State==0&& x.IsDeleted == 0),
                                ycount = _dbContext.SecurityCode.Count(x => x.Address == g.Key && x.State == 1&& x.IsDeleted == 0),
                            };


                
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.Address.Contains(payload.Kw.Trim()));
                }

                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                return Ok(response);
            }
        }

        /// <summary>
        /// 显示所有信息
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult NumberList(SecurityCodeNumberRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.SecurityCode
                            where p.Address == payload.Address
                            select new
                            {
                                p.SecurityCodeUuid,
                                p.Address,
                                p.Name,
                                p.State,
                                p.ChargeName,
                                p.ChargePhone,
                                p.IsDeleted,
                                p.ScannTime,
                                p.Id
                            };



                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.Address.Contains(payload.Kw.Trim()) || x.Name.Contains(payload.Kw.Trim()));
                }
                if (payload.state > -1)
                {
                    query = query.Where(x => x.State == payload.state);
                }
                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => x.IsDeleted == Convert.ToInt32(((CommonEnum.IsDeleted)payload.IsDeleted)));
                }

                query = query.OrderByDescending(x => x.Id);

                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                return Ok(response);
            }
        }
        #endregion
    }
}
