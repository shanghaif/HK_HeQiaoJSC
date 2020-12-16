using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HaikanSmartTownCockpit.Api.Auth;
using HaikanSmartTownCockpit.Api.Configurations;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.User;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.QTLogin
{
    [Route("api/v1/QTLogin/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppAuthenticationSettings _appSettings;
        private readonly haikanHeQiaoContext _dbContext;
        private readonly MdDesEncrypt MdDesEncrypt;
        private readonly ILogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        public LoginController(IOptions<AppAuthenticationSettings> appSettings, haikanHeQiaoContext dbContext, IOptions<MdDesEncrypt> mdDesEncrypt, ILogger<TestController> logger)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
            MdDesEncrypt = mdDesEncrypt.Value;
            _logger = logger;
        }
        /// <summary>
        /// 权限
        /// </summary>
        [HttpPost]
        public IActionResult Profile(UserData2 userdata)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                _logger.LogError("1:" + userdata.jiaose);
                _logger.LogError("1.5:" + JsonConvert.SerializeObject(AuthContextService.CurrentUser));

                var guid = AuthContextService.CurrentUser.Guid;
                _logger.LogError("2:" + guid);
                var user = _dbContext.SystemUser.FirstOrDefaultAsync(x => x.SystemUserUuid == guid).Result;
                _logger.LogError("3:" + user);

                if (user.RealName != "超级管理员")
                {
                    var dd = user.SystemRoleUuid;
                    _logger.LogError("4:");

                    var ss = _dbContext.SystemRolePermissionMapping.Where(x => x.SystemRoleUuid.ToString() == dd).ToList();

                    _logger.LogError("5:" + ss.Count);

                    if (ss.Count > 0)
                    {
                        _logger.LogError("6:");

                        for (int i = 0; i < ss.Count; i++)
                        {
                            _logger.LogError("7:");

                            var gg = _dbContext.SystemPermission.Where(x => x.SystemPermissionUuid == ss[i].SystemPermissionUuid).ToList();
                            _logger.LogError("8:" + gg.Count);

                            if (gg.Count > 0)
                            {
                                _logger.LogError("9:");

                                for (int r = 0; r < gg.Count; r++)
                                {
                                    _logger.LogError("10:");

                                    if (gg[r].Name == "查看")
                                    {
                                        _logger.LogError("11:");

                                        var qq = _dbContext.SystemMenu.FirstOrDefault(x => x.SystemMenuUuid == gg[r].SystemMenuUuid);
                                        _logger.LogError("12:" + qq.Name);

                                        if (userdata.jiaose == qq.Name)
                                        {
                                            response.SetData("ok");
                                            return Ok(response);
                                        }
                                        else
                                        {
                                            //response.SetFailed("没有相关权限");
                                            //return Ok(response);
                                        }
                                    }
                                    else
                                    {
                                        //response.SetFailed("没有相关权限");
                                        //return Ok(response);
                                    }
                                }
                            }
                            else
                            {
                                //response.SetFailed("没有相关权限");
                                //return Ok(response);
                            }
                        }
                    }
                }
                else
                {
                    response.SetData("ok");
                    return Ok(response);
                }
            }
            response.SetFailed("没有相关权限");
            return Ok(response);
        }


        [HttpPost]
        public IActionResult receive(UserData userdata)
        {
            var response = ResponseModelFactory.CreateInstance;
            SystemUser user;
            using (_dbContext)
            {
                user = _dbContext.SystemUser.FirstOrDefault(x => x.LoginName == userdata.username.Trim());
                if (user == null || user.IsDeleted == 1)
                {
                    response.SetFailed("用户不存在");
                    return Ok(response);
                }
                else
                {
                    string s = Haikan3.Utils.DesEncrypt.Encrypt(userdata.password.Trim(), MdDesEncrypt.SecretKey);
                    if (user.PassWord != Haikan3.Utils.DesEncrypt.Encrypt(userdata.password.Trim(), MdDesEncrypt.SecretKey))
                    {
                        response.SetFailed("密码不正确");
                        return Ok(response);
                    }
                    else
                    {
                        if (user.RealName != "超级管理员")
                        {
                            var dd = user.SystemRoleUuid;
                            var ss = _dbContext.SystemRolePermissionMapping.Where(x => x.SystemRoleUuid.ToString() == dd).ToList();
                            if (ss.Count() > 0)
                            {
                                for (int i = 0; i < ss.Count(); i++)
                                {
                                    var gg = _dbContext.SystemPermission.Where(x => x.SystemPermissionUuid == ss[i].SystemPermissionUuid).ToList();
                                    if (gg.Count() > 0)
                                    {
                                        for (int r = 0; r < gg.Count(); r++)
                                        {
                                            if (gg[r].Name == "查看")
                                            {
                                                var qq = _dbContext.SystemMenu.FirstOrDefault(x => x.SystemMenuUuid == gg[r].SystemMenuUuid);
                                                if (userdata.jiaose == qq.Name)
                                                {
                                                    //获取权限名
                                                    string[] roleid = user.SystemRoleUuid.TrimEnd(',').Split(",");
                                                    string rolename = "";
                                                    for (int o = 0; o < roleid.Length; o++)
                                                    {
                                                        if (!string.IsNullOrEmpty(roleid[o]))
                                                            rolename += _dbContext.SystemRole.FirstOrDefault(x => x.SystemRoleUuid == Guid.Parse(roleid[o])).RoleName + ",";
                                                    }
                                                    string zyz = "";
                                                    string yh = "";
                                                    string ddy = "";
                                                    string sj = "";
                                                    //志愿者roleid
                                                    var temp1 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("志愿者")).Select(x => new { x.SystemRoleUuid }).ToList();
                                                    if (temp1.Count > 0)
                                                        zyz = temp1[0].SystemRoleUuid.ToString();

                                                    //普通用户roleid
                                                    var temp2 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("用户")).Select(x => new { x.SystemRoleUuid }).ToList();
                                                    if (temp2.Count > 0)
                                                        yh = temp2[0].SystemRoleUuid.ToString();

                                                    //督导员roleid
                                                    var temp3 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("督导员")).Select(x => new { x.SystemRoleUuid }).ToList();
                                                    if (temp3.Count > 0)
                                                        ddy = temp3[0].SystemRoleUuid.ToString();

                                                    //商户
                                                    var temp4 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("商")).Select(x => new { x.SystemRoleUuid }).ToList();
                                                    if (temp4.Count > 0)
                                                        sj = temp4[0].SystemRoleUuid.ToString();
                                                    string superAdmin = "";

                                                    //超管roleid
                                                    var temp5 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("超级")).Select(x => new { x.SystemRoleUuid }).ToList();
                                                    if (temp5.Count > 0)
                                                        superAdmin = temp5[0].SystemRoleUuid.ToString();
                                                    int usertype = 0;
                                                    if (!user.SystemRoleUuid.Contains(superAdmin))
                                                    {
                                                        usertype = 2;
                                                    }
                                                    var claimsIdentity = new ClaimsIdentity(new Claim[]
                                                    {
                                                new Claim(ClaimTypes.Name, userdata.username),
                                                new Claim("guid",user.SystemUserUuid.ToString()),
                                                new Claim("avatar",""),
                                                new Claim("displayName",user.RealName),
                                                new Claim("loginName",user.LoginName),
                                                new Claim("emailAddress",""),
                                                //new Claim("guid",user.SystemUserUuid.ToString()),
                                                //new Claim("userType",usertype.ToString()),
                                                new Claim("department",user.DepartmentUuid.ToString()),
                                                new Claim("userType",user.UserType.Value.ToString()),
                                                new Claim("roleid",user.SystemRoleUuid.TrimEnd(',')),
                                                new Claim("roleName",rolename.TrimEnd(',')),
                                                new Claim("ZYZ",zyz),
                                                new Claim("YH",yh),
                                                new Claim("DDY",ddy),
                                                new Claim("SJ",sj)
                                                    });
                                                    var token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);

                                                    response.SetData(token);
                                                    return Ok(response);
                                                }
                                                else
                                                {
                                                    //response.SetFailed("没有相关权限");
                                                    //return Ok(response);
                                                }
                                            }
                                            else
                                            {
                                                //response.SetFailed("没有相关权限");
                                                //return Ok(response);
                                            }

                                        }
                                    }
                                    else
                                    {
                                        //response.SetFailed("没有相关权限");
                                        //return Ok(response);
                                    }
                                }
                            }
                            else
                            {
                                response.SetFailed("没有相关权限");
                                return Ok(response);
                            }
                        }
                        else
                        {
                            //获取权限名
                            string[] roleid = user.SystemRoleUuid.TrimEnd(',').Split(",");
                            string rolename = "";
                            for (int o = 0; o < roleid.Length; o++)
                            {
                                if (!string.IsNullOrEmpty(roleid[o]))
                                    rolename += _dbContext.SystemRole.FirstOrDefault(x => x.SystemRoleUuid == Guid.Parse(roleid[o])).RoleName + ",";
                            }
                            string zyz = "";
                            string yh = "";
                            string ddy = "";
                            string sj = "";
                            //志愿者roleid
                            var temp1 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("志愿者")).Select(x => new { x.SystemRoleUuid }).ToList();
                            if (temp1.Count > 0)
                                zyz = temp1[0].SystemRoleUuid.ToString();

                            //普通用户roleid
                            var temp2 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("用户")).Select(x => new { x.SystemRoleUuid }).ToList();
                            if (temp2.Count > 0)
                                yh = temp2[0].SystemRoleUuid.ToString();

                            //督导员roleid
                            var temp3 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("督导员")).Select(x => new { x.SystemRoleUuid }).ToList();
                            if (temp3.Count > 0)
                                ddy = temp3[0].SystemRoleUuid.ToString();

                            //商户
                            var temp4 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("商")).Select(x => new { x.SystemRoleUuid }).ToList();
                            if (temp4.Count > 0)
                                sj = temp4[0].SystemRoleUuid.ToString();
                            string superAdmin = "";

                            //超管roleid
                            var temp5 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("超级")).Select(x => new { x.SystemRoleUuid }).ToList();
                            if (temp5.Count > 0)
                                superAdmin = temp5[0].SystemRoleUuid.ToString();
                            int usertype = 0;
                            if (!user.SystemRoleUuid.Contains(superAdmin))
                            {
                                usertype = 2;
                            }
                            var claimsIdentity = new ClaimsIdentity(new Claim[]
                            {
                                                new Claim(ClaimTypes.Name, userdata.username),
                                                new Claim("guid",user.SystemUserUuid.ToString()),
                                                new Claim("avatar",""),
                                                new Claim("displayName",user.RealName),
                                                new Claim("loginName",user.LoginName),
                                                new Claim("emailAddress",""),
                                                //new Claim("guid",user.SystemUserUuid.ToString()),
                                                //new Claim("userType",usertype.ToString()),
                                                new Claim("department",user.DepartmentUuid.ToString()),
                                                new Claim("userType",user.UserType.Value.ToString()),
                                                new Claim("roleid",user.SystemRoleUuid.TrimEnd(',')),
                                                new Claim("roleName",rolename.TrimEnd(',')),
                                                new Claim("ZYZ",zyz),
                                                new Claim("YH",yh),
                                                new Claim("DDY",ddy),
                                                new Claim("SJ",sj)
                            });
                            var token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);

                            response.SetData(token);
                            return Ok(response);
                        }
                    }

                    response.SetFailed("没有相关权限");
                    return Ok(response);
                }


            }
        }
    }
}
