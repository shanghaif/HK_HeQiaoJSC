using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Entities.Enums;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Models.Response;
using HaikanSmartTownCockpit.Api.RequestPayload.Rbac.Role;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.PersonalDiary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using HaikanSmartTownCockpit.Api.Extensions.DataAccess;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Personal
{
    [Route("api/v1/Personal/[controller]/[action]")]
    [ApiController]
    public class PersonalDiaryController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PersonalDiaryController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(RoleRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = from a in _dbContext.PersonalDiary
                            where a.IsDeleted == 0
                            select new
                            {
                                PersonalDiaryUuid = a.PersonalDiaryUuid,
                                Id = a.Id,
                                Headline = a.Headline,
                                Content = a.Content,
                                WriteTime = a.WriteTime,
                                Accessory = a.Accessory,
                                EstablishName = a.EstablishName,
                                EstablishTime = a.EstablishTime,
                                IsDeleted = a.IsDeleted,
                            };
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.Headline.Contains(payload.Kw.Trim()) || x.EstablishName.Contains(payload.Kw.Trim()));
                }

                //时间段查询
                if (!string.IsNullOrEmpty(payload.kwstartime))
                {
                    query = query.Where(x => x.WriteTime.Contains(payload.kwstartime));
                }

                //if (!string.IsNullOrEmpty(payload.kwendtime))
                //{
                //    query = query.Where(x => Convert.ToDateTime(x.EstablishTime) <= Convert.ToDateTime(payload.kwendtime));
                //}

                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }
                if (AuthContextService.CurrentUser.DisplayName !="超级管理员")
                {
                    query= query.Where(x => x.EstablishName == AuthContextService.CurrentUser.DisplayName);
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                return Ok(response);
            }
        }

        /// <summary>
        /// 创建个人日志
        /// </summary>
        /// <param name="model">个人日志视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(PersonalCreateModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var personaldiary = new PersonalDiary();
                personaldiary.Headline = model.Headline;//标题
                personaldiary.Content = model.Content;//内容
                personaldiary.WriteTime = Convert.ToDateTime(model.WriteTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");//撰写时间
                personaldiary.Accessory = model.Accessory;//附件
                personaldiary.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//添加时间
                personaldiary.EstablishName = AuthContextService.CurrentUser.DisplayName;//添加人
                personaldiary.IsDeleted = 0;//是否删除
                _dbContext.PersonalDiary.Add(personaldiary);
                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑个人日志-显示数据
        /// </summary>
        /// <param name="guid">个人日志唯一编码</param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.PersonalDiary.FirstOrDefault(x => x.PersonalDiaryUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<PersonalDiary, PersonalCreateModel>(entity));
                return Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的个人日志信息
        /// </summary>
        /// <param name="model">个人日志视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(PersonalCreateModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = _dbContext.PersonalDiary.FirstOrDefault(x => x.PersonalDiaryUuid == model.PersonalDiaryUuid);
                entity.Headline = model.Headline;//标题
                entity.Content = model.Content;//内容
                entity.WriteTime = model.WriteTime;//撰写时间
                entity.Accessory = model.Accessory;//附件
                _dbContext.SaveChanges();
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除个人日志
        /// </summary>
        /// <param name="ids">个人日志ID,多个以逗号分隔</param>
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
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">个人日志ID,多个以逗号分隔</param>
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
                default:
                    break;
            }
            return Ok(response);
        }

        /// <summary>
        /// 获取指定用户的列表
        /// </summary>
        /// <param name="guid">用户GUID</param>
        /// <returns></returns>
        [HttpGet("/api/v1/rbac/role/find_list_by_user_guid/{guid}")]
        public IActionResult FindListByUserGuid(Guid guid)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var sql = @"SELECT R.* FROM PersonalDiary 
                WHERE PersonalDiaryUUID={0}";
                var query = _dbContext.PersonalDiary.FromSqlRaw(sql, guid).ToList();
                var assignedRoles = query.ToList().Select(x => x.PersonalDiaryUuid).ToList();
                var roles = _dbContext.PersonalDiary.Where(x => (CommonEnum.IsDeleted)x.IsDeleted == CommonEnum.IsDeleted.No).ToList().Select(x => new { label = x.Headline, key = x.PersonalDiaryUuid });
                response.SetData(new { roles, assignedRoles });
                return Ok(response);
            }
        }

        /// <summary>
        /// 返回文件结果
        /// </summary>
        public class PicResultData
        {
            public string Msg { get; set; }
            public string Path { get; set; }
            public string Code { get; set; }
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PicResultData> RegistPicture([FromServices]IHostingEnvironment environment)
        {
            var data = new PicResultData();
            string path = string.Empty;
            var files = Request.Form.Files;
            string strpath = "";
            string strpath1 = "";
            if (files == null || files.Count() <= 0)
            {
                data.Msg = "请选择上传的文件。";
                data.Code = "404";
                return data;
            }
            try
            {
                if (files.Sum(c => c.Length) <= 1024 * 1024 * 2)
                {
                    foreach (var file in files)
                    {
                        string[] sArray = file.FileName.Split(".");

                        //strpath1 = DateTime.Now.ToString("yyyyMMddHHmmss") + Guid.NewGuid().ToString().Replace("-", "") +
                        //          file.FileName.Substring(file.FileName.LastIndexOf("."), (file.FileName.Length - file.FileName.LastIndexOf(".")));
                        strpath1 = DateTime.Now.ToString("yyyyMMddHHmmss") + sArray[0] +
                              file.FileName.Substring(file.FileName.LastIndexOf("."), (file.FileName.Length - file.FileName.LastIndexOf(".")));

                        strpath = Path.Combine("UploadFiles/PersonalDiary",strpath1);
                        path = Path.Combine(environment.WebRootPath, strpath);
                        using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    var response = ResponseModelFactory.CreateInstance;
                    data.Code = "200";
                    data.Path = strpath1;
                    data.Msg = "上传成功";
                    return data;
                }
                else
                {
                    data.Code = "404";
                    data.Msg = "文件过大";
                    return data;
                }
            }
            catch (Exception)
            {
                data.Code = "404";
                data.Msg = "文件上传失败";
                return data;
            }
        }

        #region 私有方法

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
                var parameters = ids.Split(",").Select((id, index) => new Microsoft.Data.SqlClient.SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE PersonalDiary SET IsDeleted=@IsDel WHERE PersonalDiaryUUID IN ({0})", parameterNames);
                parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                var response = ResponseModelFactory.CreateInstance; 
                return response; 
            }
        }
        #endregion

    }


}