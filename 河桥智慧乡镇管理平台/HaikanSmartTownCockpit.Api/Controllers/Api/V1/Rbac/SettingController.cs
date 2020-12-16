using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.logs.TolLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Rbac
{
    [Route("api/v1/Rbac/[controller]/[action]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        

        public SettingController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
           
        }

        /// <summary>
        /// 获取显示水位临界点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetWLCP()
        {
            using (_dbContext)
            {
                
                var response = ResponseModelFactory.CreateResultInstance;
                var entity = _dbContext.SystemSetting.FirstOrDefault(x => x.Id == 1);


                response.SetData(entity);
                return Ok(response);
            }
        }

        /// <summary>
        /// 设置水位临界点
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SetWLCP(double num)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            if (num<=0)
            {
                response.SetFailed("参数错误");
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.SystemSetting.FirstOrDefault(x => x.Id == 1);
                entity.WaterLevelCriticalPoint = (decimal?)num;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:水位临界点", _dbContext);
                }
                response.SetData(entity);
                return Ok(response);
            }

        }

    }
}
