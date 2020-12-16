using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.ViewModels.Personal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Personal
{
    [Route("api/v1/personal/app/[controller]/[action]")]
    [ApiController]
    public class PersonalDiaryAppController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        public PersonalDiaryAppController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        //添加个人日志
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(personalViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.PersonalDiary();

                entity.PersonalDiaryUuid = Guid.NewGuid();
                entity.Headline = model.Headline;
                entity.Content = model.Content;
                entity.WriteTime = Convert.ToDateTime(model.WriteTime.ToString()).ToString("yyyy-MM-dd");
                entity.Accessory = model.Accessory;
                entity.IsDeleted = 0;//未删除
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd");//创建时间
                entity.EstablishName = model.EstablishName;//创建人uuid
                entity.MissionUuid = Guid.Parse(model.MissionUuid);
                entity.Type = model.Type;
                _dbContext.PersonalDiary.Add(entity);
                _dbContext.SaveChanges();
                response.SetSuccess();
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

                        strpath = Path.Combine("UploadFiles/PersonalDiary", strpath1);
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

        //添加任务完成日志
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create2(personalViewModel2 model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.MissionJournal();

                entity.MissionJournalUuid = Guid.NewGuid();
                entity.MissionUuid = model.MissionUuid;
                entity.Completed = model.Completed;
                entity.Coordination = model.Coordination;
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                entity.Accessory = model.Accessory;
                entity.IsDeleted = 0;//未删除
                entity.EstablishName = model.EstablishName;//创建人uuid
                _dbContext.MissionJournal.Add(entity);
                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create3(personalViewModel3 model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = new HaikanSmartTownCockpit.Api.Entities.PriorityJournal();

                entity.PriorityJournalUuid = Guid.NewGuid();
                entity.PriorityUuid = model.PriorityUuid;
                entity.Completed = model.Completed;
                entity.Coordination = model.Coordination;
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                entity.Accessory = model.Accessory;
                entity.IsDeleted = 0;//未删除
                entity.EstablishName = model.EstablishName;//创建人uuid
                _dbContext.PriorityJournal.Add(entity);
                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }
        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <param name="data">当前登陆人uuid</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult depdata()
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = from u in _dbContext.Department
                            where u.IsDeleted == 0
                            orderby u.Id ascending
                            select new
                            {
                                depUUID = u.DepartmentUuid,
                                name = u.Name,
                                count = _dbContext.SystemUser.Count(x => x.IsDeleted == 0 && x.DepartmentUuid.ToString().Contains(u.DepartmentUuid.ToString())),
                            };
                response.SetData(query.ToList());
            }
            return Ok(response);
        }

        /// <summary>
        /// 根据科室绑定人员
        /// </summary>
        /// <param name="uuid">科室uuid</param>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        public IActionResult userdatalist(string uuid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {

                if (uuid != null && uuid != "")
                {
                    var query = from u in _dbContext.SystemUser
                                where u.IsDeleted == 0
                                && u.DepartmentUuid.ToString().Contains(uuid)
                                orderby u.Id ascending
                                select new
                                {
                                    realname = u.RealName,
                                    uuid = u.SystemUserUuid,
                                };

                    response.SetData(query.ToList());
                }
            }
            return Ok(response);

        }
        /// <summary>
        /// 根据选择的人员查看日志
        /// </summary>
        /// <param name="uuid">人员uuid</param>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        public IActionResult personlist(string uuid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {

                if (uuid != null && uuid != "")
                {
                    var query = from u in _dbContext.PersonalDiary
                                where u.IsDeleted == 0
                                && u.EstablishName == uuid
                                orderby u.Id ascending
                                select new
                                {
                                    headline = u.Headline,
                                    time = u.WriteTime,
                                    adduser = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(uuid)).RealName,
                                    content = u.Content,
                                    establishName = u.EstablishName,
                                    accessory = u.Accessory,
                                    uid = u.PersonalDiaryUuid,
                                    u.Type
                                };

                    response.SetData(query.ToList());
                }

            }
            return Ok(response);

        }

        /// <summary>
        /// 根据日志uuid获取日志信息
        /// </summary>
        /// <param name="uuid">人员uuid</param>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        public IActionResult persondata(string uuid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {

                if (uuid != null && uuid != "")
                {
                    var query = (from u in _dbContext.PersonalDiary
                                 where u.IsDeleted == 0
                                 && u.PersonalDiaryUuid.ToString() == uuid
                                 orderby u.Id ascending
                                 select new
                                 {
                                     headline = u.Headline,
                                     time = u.WriteTime,
                                     adduser = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid.ToString().Contains(u.EstablishName)).RealName,
                                     content = u.Content,
                                     establishName = u.EstablishName,
                                     accessory = u.Accessory,
                                     uuid = u.PersonalDiaryUuid,
                                     u.Type
                                 }).FirstOrDefault();

                    response.SetData(query);
                }

            }
            return Ok(response);

        }



        /// <summary>
        /// 根据选择的人员查看日志
        /// </summary>
        /// <param name="data">前台参数</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getdatalist(datacx data)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {

                if (data != null)
                {
                    var query = from u in _dbContext.PersonalDiary
                                where u.IsDeleted == 0
                                && u.EstablishName == data.uname
                                orderby u.Id ascending
                                select new
                                {
                                    headline = u.Headline,
                                    time = u.WriteTime,
                                    adduser = _dbContext.SystemUser.FirstOrDefault(x => x.SystemUserUuid == Guid.Parse(data.uname)).RealName,
                                    content = u.Content,
                                    establishName = u.EstablishName,
                                    accessory = u.Accessory,
                                    uid=u.PersonalDiaryUuid,
                                };
                    if (data.cxname != "")
                    {
                        query = query.Where(x => x.headline.Trim().Contains(data.cxname));
                    }

                    response.SetData(query.ToList());
                }

            }
            return Ok(response);

        }
        public class datacx
        {
            public string cxname { get; set; }
            public string uname { get; set; }
        }


    }
}