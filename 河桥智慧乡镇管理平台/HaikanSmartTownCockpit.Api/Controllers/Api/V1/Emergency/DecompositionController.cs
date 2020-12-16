using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.logs.TolLog;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.Emergency;
using HaikanSmartTownCockpit.Api.ViewModels.Emergency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Emergency
{
    [Route("api/v1/emergency/[controller]/[action]")]
    [ApiController]
    
    public class DecompositionController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;


        public DecompositionController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult List(DecompRequestpayload payload)
        {
            using (_dbContext)
            {
                var query = _dbContext.TaskDecomposition.Where(x => x.IsDelete == 0);
                //if (!string.IsNullOrEmpty(payload.Kw))
                //{
                //    query = query.Where(x => x.DangOrganizationName.Contains(payload.Kw));
                //}
                query = query.OrderByDescending(x => x.Id);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:任务分解信息一条数据", _dbContext);
                return Ok(response);
            }
            
        }


        /// <summary>
        /// 获取当前条信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Load(Guid guid)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = _dbContext.TaskDecomposition.FirstOrDefault(x => x.TaskDecompositionUuid == guid);
                response.SetData(entity);
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(DecompViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {

                var entity = _dbContext.TaskDecomposition.FirstOrDefault(x => x.TaskDecompositionUuid == model.TaskDecompositionUuid);
                if (entity == null)
                {
                    response.SetFailed("该记录不存在");
                    return Ok(response);
                }

                entity.Commander = model.Commander;
                entity.Member = model.Member;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:任务分解信息一条数据", _dbContext);
                }
                response.SetSuccess("添加成功");
                return Ok(response);
            }
        }





        /// <summary>
        /// 分级任务详情列表
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DecompInfo(DecompInfoRequestpayload payload)
        {
            using (_dbContext)
            {
                var query = _dbContext.TaskDecompInfo.Where(x =>x.Tduuid==payload.Tduuid &&x.IsDelete == 0);
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.SpecialWorkTeam.Contains(payload.Kw));
                }
                query = query.OrderByDescending(x => x.Id);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                return Ok(response);
            }

        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InfoCreate(DecompInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                
                var td = _dbContext.TaskDecomposition.FirstOrDefault(x => x.TaskDecompositionUuid == model.Tduuid);
                if (td == null)
                {
                    response.SetFailed("该uuid不存在");
                    return Ok(response);
                }
                if (_dbContext.TaskDecompInfo.Any(x => x.SpecialWorkTeam == model.SpecialWorkTeam))
                {
                    response.SetFailed("该专班已存在");
                    return Ok(response);
                }
                var entity = new HaikanSmartTownCockpit.Api.Entities.TaskDecompInfo();
                entity.TaskDecompInfoUuid = Guid.NewGuid();
                entity.Tduuid = model.Tduuid;
                entity.SpecialWorkTeam = model.SpecialWorkTeam;
                entity.DedicatedTeamLeader = model.DedicatedTeamLeader;
                entity.RelatedCadres = model.RelatedCadres;
                entity.Principal = model.Principal;
                entity.Requirement = model.Requirement;
                entity.AddTime = DateTime.Now;
                entity.IsDelete = 0;
                _dbContext.TaskDecompInfo.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:任务分解信息一条数据", _dbContext);
                }
                response.SetSuccess("添加成功");
                return Ok(response);
            }

        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InfoEdit(DecompInfoViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {

                var tdinfo = _dbContext.TaskDecompInfo.FirstOrDefault(x => x.TaskDecompInfoUuid == model.TaskDecompInfoUuid);
                if (tdinfo == null)
                {
                    response.SetFailed("该记录不存在");
                    return Ok(response);
                }
                if (tdinfo.SpecialWorkTeam!=model.SpecialWorkTeam&& _dbContext.TaskDecompInfo.Any(x => x.SpecialWorkTeam == model.SpecialWorkTeam))
                {
                    response.SetFailed("该专班已存在");
                    return Ok(response);
                }

                tdinfo.SpecialWorkTeam = model.SpecialWorkTeam;
                tdinfo.DedicatedTeamLeader = model.DedicatedTeamLeader;
                tdinfo.RelatedCadres = model.RelatedCadres;
                tdinfo.Principal = model.Principal;
                tdinfo.Requirement = model.Requirement;

                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:任务分解信息详情一条数据", _dbContext);
                }
                response.SetSuccess("添加成功");
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取当前条信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult InfoLoad(Guid guid)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = _dbContext.TaskDecompInfo.FirstOrDefault(x => x.TaskDecompInfoUuid == guid);
                response.SetData(entity);
                return Ok(response);
            }
        }



        /// <summary>
        /// 删除单个角色
        /// </summary>
        /// <param name="ids">ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult InfoDelete(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            //if (ConfigurationManager.AppSettings.IsTrialVersion)
            //{
            //    response.SetIsTrial();
            //    return Ok(response);
            //}
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
                var sql = string.Format("UPDATE TaskDecompInfo SET IsDelete=@isDeleted WHERE TaskDecompInfoUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@isDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:任务分解信息一条数据", _dbContext);
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

        public IActionResult InfoBatch(string command, string ids)
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
        /// 获取任务分解表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ToShowInfo()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var entity = _dbContext.TaskDecomposition.FirstOrDefault(x => x.IsDelete == 0);
                var query = _dbContext.TaskDecompInfo.Where(x => x.Tduuid == entity.TaskDecompositionUuid && x.IsDelete == 0).Select(x=>new { x.TaskDecompInfoUuid,x.SpecialWorkTeam});
                
                response.SetData(new {entity, list=query.ToList() });
                return Ok(response);
            }
        }


        /// <summary>
        /// 获取任务分解表详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ToShowInfo2(Guid fjuid)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                
                var entity = _dbContext.TaskDecompInfo.FirstOrDefault(x => x.TaskDecompInfoUuid == fjuid);

                response.SetData(entity );
                return Ok(response);
            }
        }




    }
}
