using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.ViewModels.Rescue;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Rescue
{
    [Route("api/v1/Rescue/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class RescueTeamController : ControllerBase
    {

        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        //private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public RescueTeamController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            //_hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult GetTeamInfo()
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var teamlist = _dbContext.RescueTeam.Where(x => x.IsDeleted == 0).Select(x => new RescueTeaminfoView { TeamName=x.TeamName, RescueTeamUuid=x.RescueTeamUuid, TeamCaptain=x.TeamCaptain, TeamRenshu= x.TeamRenshu, TeamPhone= x.TeamPhone, MemberNames = "" }).ToList();
                var plist = _dbContext.RescueMember.Where(x => x.IsDelete == 0).ToList();
                for(int i = 0; i < teamlist.Count; i++)
                {
                    var names = "";
                    var namels= plist.FindAll(x => x.RescueTeamUuid == teamlist[i].RescueTeamUuid).Select(x=>x.MemberName).ToList();
                    names = string.Join(",", namels);
                    teamlist[i].MemberNames = names;
                }

                response.SetData(teamlist);

                return Ok(response);
            }


            
           


        }



    }
}
