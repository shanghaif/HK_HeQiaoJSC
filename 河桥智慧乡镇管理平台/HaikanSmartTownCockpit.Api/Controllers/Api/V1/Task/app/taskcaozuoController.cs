using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.ViewModels.Task;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task.app
{
    [Route("api/v1/task/app/[controller]/[action]")]
    [ApiController]
    public class taskcaozuoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;


        public taskcaozuoController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;

            _mapper = mapper;
        }

        //按条件查询日志
        [HttpGet]
        public IActionResult caozuorizhi(string uuid)//任务uuid
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var participant = _dbContext.Mission.FirstOrDefault(x => x.MissionUuid == Guid.Parse(uuid));//参与人id
                if (participant == null) {
                    response.SetFailed();
                    return Ok(response);
                }
                var participantids = participant.Participant;//参与人id



                var query = from m in _dbContext.MissionJournal
                            join u in _dbContext.SystemUser
                            on m.EstablishName equals u.SystemUserUuid.ToString()
                            where m.MissionUuid == Guid.Parse(uuid)
                            orderby m.Id ascending//排序
                            select new
                            {
                                content = m.Content,//内容
                                img = m.Accessory,//图片
                                establishTime = m.EstablishTime,//创建时间
                                id = m.Id,//id
                                establishName = m.EstablishName,//创建人uuid
                                chuanjianname = u.RealName,//创建人姓名
                                read = m.Read,//已读人员id
                                //weiducount= selectweidu(uuid,m.MissionJournalUuid.ToString(),"weidu"),//未读人数
                            };

                //var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query.ToList());
                return Ok(response);

            }
        }


        /// <summary>
        /// 添加任务操作日志
        /// </summary>
        /// <param name="model">需要保存的数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult appcreaterizhi(taskcaozuoCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                var entity = new MissionJournal();
                entity.MissionJournalUuid = Guid.NewGuid();
                entity.MissionUuid = Guid.Parse(model.MissionUUID.ToString());//所属任务
                entity.Content = model.Content;//日志内容
                                               //if (model.Accessory != "" && model.Accessory != null)
                                               //{
                                               //    entity.Accessory = model.Accessory.Split('/')[3];//日志图片               
                                               //}
                                               //else
                                               //{
                entity.Accessory = model.Accessory;
                //}

                entity.EstablishName = model.EstablishName;//创建人
                entity.EstablishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//创建时间
                entity.Read = "";//已读人员
                _dbContext.MissionJournal.Add(entity);//添加
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }

        [HttpGet("{uid}")]
        public IActionResult updatestatus(string uid)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                Mission entity = _dbContext.Mission.Where(x => x.MissionUuid == Guid.Parse(uid)).FirstOrDefault();
                entity.Accomplish = "1";
                entity.AuditStatus = "2";
                //_dbContext.Mission.Update(entity);//添加
                _dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }



        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PicResultData> RegistPicture([FromServices] IHostingEnvironment environment)
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
                        //strpath1 = files[0].FileName;

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
        /// 查询已读，未读人数(暂时没用)
        /// </summary>
        /// <returns></returns>

        public string selectweidu(string renwuuuid,string caozuouuid,string type) {
            using (_dbContext)
            {
            var participantids = _dbContext.Mission.Where(x => x.MissionUuid == Guid.Parse(renwuuuid)).ToList()[0].Participant;//参与人id
            var caozuoredid = _dbContext.MissionJournal.Where(x => x.MissionJournalUuid == Guid.Parse(caozuouuid)).ToList()[0].Read;//已读人员uuid

            var yiducount = "0";//已读人数
            var weiducount = "0";//未读人数

            if (participantids != "" && participantids != null)
            {
                var patcount = participantids.Split(',');
                var caozuocont = caozuoredid.Split(',');
                var weidurenids = "";//未读人员
                var yidurenids = "";//已读人员
                for (int i = 0; i < patcount.Length; i++)
                {
                    for (int j = 0; j < caozuocont.Length; j++)
                    {
                        if (patcount[i] == caozuocont[j])
                        {
                            yidurenids += patcount[i] + ",";
                        }
                        else {
                            weidurenids += patcount[i] + "";
                        }
                    }
                }
                if (yidurenids != "")
                {
                yiducount = yidurenids.Trim(',').Split(",").Length.ToString();//已读人数
                }
                if (weidurenids != "")
                {
               weiducount = weidurenids.Trim(',').Split(",").Length.ToString();//未读人数
                }
            }

            if (type == "yidu")
            {
                return yiducount;
            }
            else
            {
                return weiducount;
            }
            }

        }


    }
}