using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Haikan3.Utils;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Emergency
{
    [Route("api/v1/emergency/[controller]/[action]")]
    [ApiController]
    public class EmergentController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        //private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public EmergentController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            //_hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult ToRes1(string vnames,string tnames,string content)
        {

            var response = ResponseModelFactory.CreateResultInstance;
            //if (string.IsNullOrEmpty(vnames))
            //{
            //    response.SetFailed("请选择村庄");
            //    return Ok(response);
            //}
            if (string.IsNullOrEmpty(tnames))
            {
                response.SetFailed("请选择村镇民兵");
                return Ok(response);
            }
            if (string.IsNullOrEmpty(content))
            {
                response.SetFailed("请输入短信内容");
                return Ok(response);
            }
            string[] tname = tnames.Split(',');
            var list = _dbContext.Cunzhenmb.Where(x => tname.Contains(x.Address)&&x.IsDeleted==0).ToList();
            for(int i = 0; i < list.Count; i++)
            {
                YunSendMsg.SendMsg(list[i].Phone, content);
            }
            //response.SetData(list, totalCount);
            return Ok(response);
        }


        [HttpGet]
        public IActionResult ToRes2(string level, string content)
        {

            var response = ResponseModelFactory.CreateResultInstance;
            
            if (string.IsNullOrEmpty(level))
            {
                response.SetFailed("请选择响应级别");
                return Ok(response);
            }
            if (string.IsNullOrEmpty(content))
            {
                response.SetFailed("请输入短信内容");
                return Ok(response);
            }
            
            if (level == "level4")
            {
                content = "四级响应:" + content;
            }
            if (level == "level3")
            {
                content = "三级响应:" + content;
            }
            if (level == "level2")
            {
                content = "二级响应:" + content;
            }
            if (level == "level1")
            {
                content = "一级响应:" + content;
            }


            var list = _dbContext.VillageMember.Where(x => x.IsDelete == 0).ToList();
            for(int i = 0; i < list.Count; i++)
            {

                YunSendMsg.SendMsg(list[i].MemberPhone, content);
            }

            //response.SetData(list, totalCount);
            return Ok(response);
        }
    }
}
