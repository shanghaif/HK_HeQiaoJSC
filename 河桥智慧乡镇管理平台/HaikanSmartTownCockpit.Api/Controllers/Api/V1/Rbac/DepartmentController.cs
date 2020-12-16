using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.Rbac.Department;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.Department;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Rbac
{
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class DepartmentController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public DepartmentController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 查看数据列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(DepartmentRequestPaload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.Department.AsQueryable();
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.Name.Contains(payload.Kw.Trim()));
                }
                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => (CommonEnum.IsDeleted)x.IsDeleted == payload.IsDeleted);
                }
                query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "ASC");
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                //var data = list.Select(_mapper.Map<Department, DepartmentJsonViewModel>);

                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:部门信息数据", _dbContext);
                return Ok(response);
            }


            //var response = ResponseModelFactory.CreateResultInstance;
            //using (_dbContext)
            //{
            //    var query = from a in _dbContext.Department
            //                where a.IsDeleted == 0
            //                select new
            //                {
            //                    DepartmentUuid = a.DepartmentUuid,
            //                    Id = a.Id,
            //                    Name = a.Name,
            //                    Remark = a.Remark,
            //                    EstablishName = a.EstablishName,
            //                    EstablishTime = a.EstablishTime,
            //                    IsDeleted = a.IsDeleted,
            //                };
            //    if (!string.IsNullOrEmpty(payload.Kw))
            //    {
            //        query = query.Where(x => x.Name.Contains(payload.Kw.Trim()));
            //    }
            //    if (payload.FirstSort != null)
            //    {
            //        query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
            //    }
            //    var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
            //    var totalCount = query.Count();
            //    response.SetData(list, totalCount);
            //    return Ok(response);
            //}

        }

        /// <summary>
        /// 创建科室
        /// </summary>
        /// <param name="model">科室视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(DepartmentCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (model.Name.Trim().Length <= 0)
            {
                response.SetFailed("请输入科室名称");
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.Department.Count(x => x.Name == model.Name) > 0)
                {
                    response.SetFailed("科室名已存在");
                    return Ok(response);
                }
                var entity = new Department();
                entity.DepartmentUuid = Guid.NewGuid();
                entity.Name = model.Name;//部门名称
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd");//添加时间
                entity.EstablishName = AuthContextService.CurrentUser.DisplayName;//添加人
                entity.IsDeleted = 0;
                _dbContext.Department.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:部门信息一条数据", _dbContext);
                }
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑科室
        /// </summary>
        /// <param name="guid">角色惟一编码</param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.Department.FirstOrDefault(x => x.DepartmentUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<Department, DepartmentCreateViewModel>(entity));
                return Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的科室信息
        /// </summary>
        /// <param name="model">科室视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(DepartmentCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.Department.Count(x => x.Name == model.Name && x.DepartmentUuid != model.DepartmentUuid) > 0)
                {
                    response.SetFailed("科室已存在");
                    return Ok(response);
                }

                var entity = _dbContext.Department.FirstOrDefault(x => x.DepartmentUuid == model.DepartmentUuid);
                entity.Name = model.Name;

                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:部门信息一条数据", _dbContext);
                }
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult DepartmentList()
        {
            using (_dbContext)
            {
                var entity = _dbContext.Department.ToList();
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(entity);
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除科室
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
        /// 恢复科室
        /// </summary>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Recover(string ids)
        {
            var response = UpdateIsDelete(CommonEnum.IsDeleted.No, ids);
            return Ok(response);
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">科室ID,多个以逗号分隔</param>
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
                //case "forbidden":
                //    if (ConfigurationManager.AppSettings.IsTrialVersion)
                //    {
                //        response.SetIsTrial();
                //        return Ok(response);
                //    }
                //    response = UpdateStatus(UserStatus.Forbidden, ids);
                //    break;
                //case "normal":
                //    response = UpdateStatus(UserStatus.Normal, ids);
                //    break;
                default:
                    break;
            }
            return Ok(response);
        }
        #region 私有方法

        /// <summary>
        /// 删除科室
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">科室ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE Department SET IsDeleted=@IsDeleted WHERE DepartmentUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:部门信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
    }
}