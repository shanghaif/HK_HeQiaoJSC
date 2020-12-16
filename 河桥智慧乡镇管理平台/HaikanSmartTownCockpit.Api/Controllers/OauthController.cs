using System;
using System.Linq;
using System.Security.Claims;
using HaikanSmartTownCockpit.Api.Auth;
using HaikanSmartTownCockpit.Api.Configurations;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.User;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.SystemUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Haikan3.Utils;
using HaikanSmartTownCockpit.Api.logs.TolLog;

namespace HaikanSmartTownCockpit.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        private readonly AppAuthenticationSettings _appSettings;
        private readonly haikanHeQiaoContext _dbContext;
        private readonly MdDesEncrypt MdDesEncrypt;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        public OauthController(IOptions<AppAuthenticationSettings> appSettings, haikanHeQiaoContext dbContext, IOptions<MdDesEncrypt> mdDesEncrypt)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
            MdDesEncrypt = mdDesEncrypt.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Auth(UserData userdata)
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
                string s = Haikan3.Utils.DesEncrypt.Encrypt(userdata.password.Trim(), MdDesEncrypt.SecretKey);
                if (user.PassWord != Haikan3.Utils.DesEncrypt.Encrypt(userdata.password.Trim(), MdDesEncrypt.SecretKey))
                {
                    response.SetFailed("密码不正确");
                    return Ok(response);
                }
                //if (user.IsLocked == CommonEnum.IsLocked.Locked)
                //{
                //    response.SetFailed("账号已被锁定");
                //    return Ok(response);
                //}
                //if (user.Status == UserStatus.Forbidden)
                //{
                //    response.SetFailed("账号已被禁用");
                //    return Ok(response);
                //}

                //获取权限名
                string[] roleid = user.SystemRoleUuid.TrimEnd(',').Split(",");
                string rolename = "";
                for (int i = 0; i < roleid.Length; i++)
                {
                    if (!string.IsNullOrEmpty(roleid[i]))
                        rolename += _dbContext.SystemRole.FirstOrDefault(x => x.SystemRoleUuid == Guid.Parse(roleid[i])).RoleName + ",";
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

        /// <summary>
        /// 微信端--手机号与密码登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult WXAuth2(UserData userdata)
        {
            var response = ResponseModelFactory.CreateInstance;
            SystemUser user;
            using (_dbContext)
            {
                user = _dbContext.SystemUser.FirstOrDefault(x => x.Phone == userdata.username.Trim());
                if (user == null || user.IsDeleted == 1)
                {
                    response.SetFailed("用户不存在");
                    return Ok(response);
                }
                if (user.PassWord != Haikan3.Utils.DesEncrypt.Encrypt(userdata.password.Trim(), MdDesEncrypt.SecretKey))
                {
                    response.SetFailed("密码不正确");
                    return Ok(response);
                }
                //获取权限名
                string[] roleid = user.SystemRoleUuid.TrimEnd(',').Split(",");
                string rolename = "";
                for (int i = 0; i < roleid.Length; i++)
                {
                    if (!string.IsNullOrEmpty(roleid[i]))
                        rolename += _dbContext.SystemRole.FirstOrDefault(x => x.SystemRoleUuid == Guid.Parse(roleid[i])).RoleName + ",";
                }
                string zyz = "";
                string yh = "";
                string ddy = "";
                string sj = "";

                var temp1 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("志愿者")).Select(x => new { x.SystemRoleUuid }).ToList();
                if (temp1.Count > 0)
                    zyz = temp1[0].SystemRoleUuid.ToString();
                var temp2 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("用户")).Select(x => new { x.SystemRoleUuid }).ToList();
                if (temp2.Count > 0)
                    yh = temp2[0].SystemRoleUuid.ToString();
                var temp3 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("督导员")).Select(x => new { x.SystemRoleUuid }).ToList();
                if (temp3.Count > 0)
                    ddy = temp3[0].SystemRoleUuid.ToString();
                var temp4 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("商")).Select(x => new { x.SystemRoleUuid }).ToList();
                if (temp4.Count > 0)
                    sj = temp4[0].SystemRoleUuid.ToString();

                var claimsIdentity = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userdata.username),
                    new Claim("guid",user.SystemUserUuid.ToString()),
                    new Claim("avatar",""),
                    new Claim("displayName",user.RealName),
                    new Claim("loginName",user.LoginName),
                    new Claim("emailAddress",""),
                    //new Claim("guid",user.SystemUserUuid.ToString()),
                    new Claim("userType",((int)user.UserType).ToString()),
                    new Claim("roleid",(user.SystemRoleUuid.TrimEnd(','))),
                    new Claim("roleName",(rolename.TrimEnd(','))),
                    new Claim("ZYZ",(zyz)),
                    new Claim("YH",(yh)),
                    new Claim("DDY",(ddy)),
                    new Claim("SJ",(sj))
                    });
                var token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);

                response.SetData(token);
                return Ok(response);
            }
        }

        /// <summary>
        /// 微信授权登录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult WXAuth(WXUserInfo info)
        {
            var response = ResponseModelFactory.CreateInstance;
            SystemUser user = new SystemUser();
            //string result= EWM.AES_decrypt(info.EncryptedData,info.Session_key,info.Iv);
            //return Ok(response);
            using (_dbContext)
            {
                var entity = _dbContext.SystemUser.FirstOrDefault(x=>x.Wechat == info.Openid);
                if (entity==null)
                {
                    user.SystemUserUuid = Guid.NewGuid();
                    user.LoginName = info.NickName;
                    user.RealName = "";
                    user.Wechat = info.Openid;
                    //授权登录的家庭用户
                    user.UserType = 5;
                    user.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                    if (info.Sex == 0)
                    {
                        user.Sex = "未知";
                    }
                    if (info.Sex == 1)
                    {
                        user.Sex = "男";
                    }
                    if (info.Sex == 2)
                    {
                        user.Sex = "女";
                    }
                    user.Phone = info.Phone;
                    user.IsDeleted = 0;
                    user.SystemRoleUuid = _dbContext.SystemRole.FirstOrDefault(x => x.RoleName=="家庭用户").SystemRoleUuid.ToString();
                    _dbContext.SystemUser.Add(user);
                }
                else
                {
                    entity.LoginName = info.NickName;
                    //if (info.Sex == 0)
                    //{
                    //    entity.Sex = "未知";
                    //}
                    //if (info.Sex == 1)
                    //{
                    //    entity.Sex = "男";
                    //}
                    //if (info.Sex == 2)
                    //{
                    //    entity.Sex = "女";
                    //}
                    entity.IsDeleted = 0;
                }

                _dbContext.SaveChanges();

                response.SetSuccess("授权成功");
                return Ok(response);
            }
        }

        [HttpPost]
        public IActionResult WXPhone(WXUserInfo info)
        {
            var response = ResponseModelFactory.CreateInstance;
            string result = EWM.AES_decrypt(info.EncryptedData, info.Session_key, info.Iv);
            UserPhoneInfoModel model = JsonConvert.DeserializeObject<UserPhoneInfoModel>(result);

            if (model == null)
            {
                response.SetFailed();
            }
            else
            {
                response.SetData(model.purePhoneNumber);
            }
            return Ok(response);
        }


        /// <summary>
        /// 微信端--手机号与密码登录
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult WXOpenAuth(string openid)
        {
            var response = ResponseModelFactory.CreateInstance;
            SystemUser user;
            using (_dbContext)
            {
                user = _dbContext.SystemUser.FirstOrDefault(x => x.Wechat == openid);
                if (user == null)
                {
                    response.SetFailed("需要微信授权登录！");
                    return Ok(response);
                }
                else
                {
                    //获取权限名
                    string[] roleid = user.SystemRoleUuid.TrimEnd(',').Split(",");
                    string rolename = "";
                    for (int i = 0; i < roleid.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(roleid[i]))
                            rolename += _dbContext.SystemRole.FirstOrDefault(x => x.SystemRoleUuid == Guid.Parse(roleid[i])).RoleName + ",";
                    }
                    string zyz = "";
                    string yh = "";
                    string ddy = "";
                    string sj = "";

                    var temp1 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("志愿者")).Select(x => new { x.SystemRoleUuid }).ToList();
                    if (temp1.Count > 0)
                        zyz = temp1[0].SystemRoleUuid.ToString();
                    var temp2 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("用户")).Select(x => new { x.SystemRoleUuid }).ToList();
                    if (temp2.Count > 0)
                        yh = temp2[0].SystemRoleUuid.ToString();
                    var temp3 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("督导员")).Select(x => new { x.SystemRoleUuid }).ToList();
                    if (temp3.Count > 0)
                        ddy = temp3[0].SystemRoleUuid.ToString();
                    var temp4 = _dbContext.SystemRole.Where(x => x.RoleName.Contains("商")).Select(x => new { x.SystemRoleUuid }).ToList();
                    if (temp4.Count > 0)
                        sj = temp4[0].SystemRoleUuid.ToString();

                    var claimsIdentity = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, user.LoginName),
                    new Claim("guid",user.SystemUserUuid.ToString()),
                    new Claim("avatar",""),
                    new Claim("displayName",user.RealName),
                    new Claim("loginName",user.LoginName),
                    new Claim("emailAddress",""),
                    //new Claim("guid",user.SystemUserUuid.ToString()),
                    new Claim("userType",((int)user.UserType).ToString()),
                    new Claim("roleid",(user.SystemRoleUuid.TrimEnd(','))),
                    new Claim("roleName",(rolename.TrimEnd(','))),
                    new Claim("ZYZ",(zyz)),
                    new Claim("YH",(yh)),
                    new Claim("DDY",(ddy)),
                    new Claim("SJ",(sj))
                        });
                    var token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);
                    //查询当前登录用户拥有的权限集合(非超级管理员)
//                    var sqlPermission = @"SELECT P.SystemPermissionUUID AS PermissionCode,P.ActionCode AS PermissionActionCode,P.Name AS PermissionName,P.Type AS PermissionType,M.Name AS MenuName,M.SystemMenuUUID AS MenuGuid,M.Alias AS MenuAlias,M.IsDefaultRouter FROM SystemRolePermissionMapping AS RPM 
//LEFT JOIN SystemPermission AS P ON P.SystemPermissionUUID = RPM.SystemPermissionUUID
//INNER JOIN SystemMenu AS M ON M.SystemMenuUUID = P.SystemMenuUUID
//WHERE P.IsDeleted=0 AND P.Status=1 AND EXISTS (SELECT 1 FROM SystemUserRoleMapping AS URM WHERE URM.SystemUserUUID={0} AND URM.SystemRoleUUID=RPM.SystemRoleUUID)";
//                    if (user.UserType == 0)
//                    {
//                        //如果是超级管理员
//                        sqlPermission = @"SELECT P.SystemPermissionUUID AS PermissionCode,P.ActionCode AS PermissionActionCode,P.Name AS PermissionName,P.Type AS PermissionType,M.Name AS MenuName,M.SystemMenuUUID AS MenuGuid,M.Alias AS MenuAlias,M.IsDefaultRouter FROM SystemPermission AS P 
//INNER JOIN SystemMenu AS M ON M.SystemMenuUUID = P.SystemMenuUUID
//WHERE P.IsDeleted=0 AND P.Status=1";
//                    }
//                    var permissions = _dbContext.SystemPermissionWithMenu.FromSql(sqlPermission, user.SystemUserUuid.ToString()).ToList();

//                    var pagePermissions = permissions.GroupBy(x => x.MenuAlias).ToDictionary(g => g.Key, g => g.Select(x => x.PermissionActionCode).Distinct());
                    response.SetData(new
                    {
                        access = new string[] { },
                        user_guid = user.SystemUserUuid,
                        user_name = user.RealName,
                        user_type = user.UserType,
                        permissions = "null",
                        roleName = GetroleName(user.SystemRoleUuid),
                        address = user.Address,
                        tokens = token,
                        phone=user.Phone,
                        //shop_guid = user.ShopUuid,
                        //HomeAddressUUID = user.HomeAddressUuid,
                        openid,
                        idCard=user.UserIdCard,
                    });
                }
                return Ok(response);
            }
        }
        private string GetroleName(string ids)
        {
            string s = "";
            string[] temp = ids.TrimEnd(',').Split(',');
            using (haikanHeQiaoContext cc = new haikanHeQiaoContext())
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    if (!string.IsNullOrEmpty(temp[i]))
                    {
                        var qu = cc.SystemRole.Where(x => x.SystemRoleUuid == Guid.Parse(temp[i])).Select(x => new { x.RoleName }).ToList();
                        if (!string.IsNullOrEmpty(qu[0].RoleName))
                            s += qu[0].RoleName + ",";
                    }
                }
                return s.TrimEnd(',');
            }

        }
    }
}