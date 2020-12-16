using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Configurations;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.Rbac.User;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.SystemUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Rbac
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class UserController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private readonly MdDesEncrypt MdDesEncrypt;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public UserController(haikanHeQiaoContext dbContext, IMapper mapper, IOptions<MdDesEncrypt> mdDesEncrypt)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            MdDesEncrypt = mdDesEncrypt.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(UserRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from u in _dbContext.SystemUser
                            join d in _dbContext.Department
                            on u.DepartmentUuid equals d.DepartmentUuid
                            join ro in _dbContext.SystemRole
                            on u.SystemRoleUuid equals ro.SystemRoleUuid.ToString()
                            select new
                            {
                                SystemUserUuid = u.SystemUserUuid,
                                LoginName=u.LoginName,
                                RealName=u.RealName,
                                UserIdCard=u.UserIdCard,
                                DepartmentName = d.Name,
                                SystemRoleUuid=u.SystemRoleUuid,
                                SystemRoleName=ro.RoleName,
                                AddTime =u.AddTime,
                                AddPeople=u.AddPeople,
                                UserType =u.UserType,
                                IsDeleted =u.IsDeleted,
                                Id = u.Id
                            };
                    
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.LoginName.Contains(payload.Kw.Trim()) || x.RealName.Contains(payload.Kw.Trim()));
                }
                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => (CommonEnum.IsDeleted)x.IsDeleted == payload.IsDeleted);
                }
                //if (payload.Status > UserStatus.All)
                //{
                //    query = query.Where(x => x.Status == payload.Status);
                //}

                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                //var data = list.Select(_mapper.Map<SystemUser, UserJsonModel>);
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:系统用户信息数据", _dbContext);
                return Ok(response);
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="model">用户视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(UserCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (model.LoginName.Trim().Length <= 0)
            {
                response.SetFailed("请输入登录名称");
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.SystemUser.Count(x => x.LoginName == model.LoginName) > 0)
                {
                    response.SetFailed("登录名已存在");
                    return Ok(response);
                }
                if (_dbContext.SystemUser.Count(x => x.UserIdCard == model.UserIdCard) > 0)
                {
                    response.SetFailed("身份证号已存在");
                    return Ok(response);
                }
                var entity = _mapper.Map<UserCreateViewModel, SystemUser>(model);
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                entity.SystemUserUuid = Guid.NewGuid();
                var rolename = _dbContext.SystemRole.FirstOrDefault(x => x.SystemRoleUuid == model.SystemRoleUuid);
                entity.SystemRoleUuid = rolename.SystemRoleUuid.ToString();//用户角色uuid
                if(rolename.RoleName== "超级管理员")
                {
                    entity.UserType = 0;
                }
                else
                {
                    entity.UserType = 1;
                }
                
                entity.PassWord = Haikan3.Utils.DesEncrypt.Encrypt(model.PassWord.Trim(), MdDesEncrypt.SecretKey);
                _dbContext.SystemUser.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:系统用户信息一条数据", _dbContext);
                }

                _dbContext.Database.ExecuteSqlRaw("DELETE FROM SystemUserRoleMapping WHERE SystemUserUUID={0}", entity.SystemUserUuid);
                var roles = new SystemUserRoleMapping();
                roles.SystemUserUuid = entity.SystemUserUuid;
                roles.SystemRoleUuid = (Guid)model.SystemRoleUuid;
                roles.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                roles.AddPeople = AuthContextService.CurrentUser.DisplayName;
                var success = true;
                _dbContext.SystemUserRoleMapping.AddRange(roles);
                success = _dbContext.SaveChanges() > 0;
                if (success)
                {
                    response.SetSuccess();
                }
                else
                {
                    _dbContext.Database.ExecuteSqlRaw("DELETE FROM SystemUser WHERE SystemUserUUID={0}", entity.SystemUserUuid);
                    response.SetFailed("保存用户角色数据失败");
                }
                response.SetSuccess();
                return Ok(response);
            }
        }
        
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="guid">用户GUID</param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                var query = _mapper.Map<SystemUser, UserEditViewModel>(entity);
                //query.SystemRoleUuid = _dbContext.SystemUserRoleMapping.FirstOrDefault(x=>x.SystemUserUuid==entity.SystemUserUuid).SystemRoleUuid;
                query.PassWord = Haikan3.Utils.DesEncrypt.Decrypt(query.PassWord.Trim(), MdDesEncrypt.SecretKey);

                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的用户信息
        /// </summary>
        /// <param name="model">用户视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(UserEditViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == model.SystemUserUuid);
                if (entity == null)
                {
                    response.SetFailed("用户不存在");
                    return Ok(response);
                }
                if (_dbContext.SystemUser.Count(x => x.LoginName == model.LoginName && x.SystemUserUuid != model.SystemUserUuid) > 0)
                {
                    response.SetFailed("登录名已存在");
                    return Ok(response);
                }
                if (_dbContext.SystemUser.Count(x => x.UserIdCard == model.UserIdCard && x.SystemUserUuid != model.SystemUserUuid) > 0)
                {
                    response.SetFailed("身份证号已存在");
                    return Ok(response);
                }
                entity.LoginName = model.LoginName;
                entity.RealName = model.RealName;
                entity.UserIdCard = model.UserIdCard;
                entity.PassWord = Haikan3.Utils.DesEncrypt.Encrypt(model.PassWord.Trim(), MdDesEncrypt.SecretKey);
                entity.UserType = model.UserType;
                entity.SystemRoleUuid = model.SystemRoleUuid;
                entity.IsDeleted = model.IsDeleted;
                entity.DepartmentUuid = model.DepartmentUuid;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:系统用户信息一条数据", _dbContext);
                }

                _dbContext.Database.ExecuteSqlRaw("DELETE FROM SystemUserRoleMapping WHERE SystemUserUUID={0}", entity.SystemUserUuid);
                var roles = new SystemUserRoleMapping();
                roles.SystemUserUuid = entity.SystemUserUuid;
                roles.SystemRoleUuid = Guid.Parse(entity.SystemRoleUuid);
                roles.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                roles.AddPeople = AuthContextService.CurrentUser.DisplayName;
                var success = true;
                _dbContext.SystemUserRoleMapping.AddRange(roles);
                success = _dbContext.SaveChanges() > 0;
                if (success)
                {
                    response.SetSuccess();
                }
                else
                {
                    _dbContext.Database.ExecuteSqlRaw("DELETE FROM SystemUser WHERE SystemUserUUID={0}", entity.SystemUserUuid);
                    response.SetFailed("保存用户角色数据失败");
                }





                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔</param>
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
        /// 恢复用户
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔</param>
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
        /// <param name="ids">用户ID,多个以逗号分隔</param>
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

        #region 用户-角色
        /// <summary>
        /// 保存用户-角色的关系映射数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/api/v1/rbac/user/save_roles")]
        public IActionResult SaveRoles(SaveUserRolesViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            var roles = model.AssignedRoles.Select(x => new SystemUserRoleMapping
            {
                SystemUserUuid = model.UserGuid,
                AddTime = DateTime.Now.ToString(),
                SystemRoleUuid = new Guid(x)
            }).ToList();
            _dbContext.Database.ExecuteSqlRaw("DELETE FROM SystemUserRoleMapping WHERE SystemUserUUID={0}", model.UserGuid);
            var success = true;
            if (roles.Count > 0)
            {
                _dbContext.SystemUserRoleMapping.AddRange(roles);
                success = _dbContext.SaveChanges() > 0;
            }

            if (success)
            {
                response.SetSuccess();
            }
            else
            {
                response.SetFailed("保存用户角色数据失败");
            }
            return Ok(response);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">用户ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                //var idList = ids.Split(",").ToList();
                ////idList.ForEach(x => {
                ////  _dbContext.Database.ExecuteSqlRaw($"UPDATE DncUser SET IsDeleted=1 WHERE Id = {x}");
                ////});
                //_dbContext.Database.ExecuteSqlRaw($"UPDATE DncUser SET IsDeleted={(int)isDeleted} WHERE Id IN ({ids})");
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE SystemUser SET IsDeleted=@IsDeleted WHERE SystemUserUUID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:系统用户信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="status">用户状态</param>
        /// <param name="ids">用户ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        //private ResponseModel UpdateStatus(UserStatus status, string ids)
        //{
        //    using (_dbContext)
        //    {
        //        var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
        //        var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
        //        var sql = string.Format("UPDATE DncUser SET Status=@Status WHERE Guid IN ({0})", parameterNames);
        //        parameters.Add(new SqlParameter("@Status", (int)status));
        //        _dbContext.Database.ExecuteSqlRaw(sql, parameters);
        //        var response = ResponseModelFactory.CreateInstance;
        //        return response;
        //    }
        //}
        #endregion
    }
}