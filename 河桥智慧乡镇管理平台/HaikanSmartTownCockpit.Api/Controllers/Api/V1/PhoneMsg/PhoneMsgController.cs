using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.PhoneMsg
{
    [Route("api/v1/PhoneMsg/[controller]/[action]")]
    [ApiController]
    public class PhoneMsgController : ControllerBase
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
        public PhoneMsgController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        


    }
}
