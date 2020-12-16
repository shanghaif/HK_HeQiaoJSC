using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.FireFighting
{
    [Route("api/v1/FireFighting/[controller]/[action]")]
    [ApiController]
    public class FireFightingController : Controller
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public FireFightingController(haikanHeQiaoContext dbContext, IMapper mapper,ILogger<FireFightingController> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        //[HttpPost]
        //public IActionResult GetAction(FireFightingViewModels model)
        //{
        //    var response = ResponseModelFactory.CreateResultInstance;
        //    SystemLog systemLogmodel = new SystemLog();
        //    try
        //    {
        //        var jsonStr = JsonConvert.SerializeObject(model);
        //        systemLogmodel.SystemLogUuid = Guid.NewGuid();
        //        systemLogmodel.OperationTime = DateTime.Now;
        //        systemLogmodel.Type = "2";
        //        systemLogmodel.IsDelete = 0;
        //        systemLogmodel.AddTime = DateTime.Now;
        //        systemLogmodel.OperationContent = jsonStr;
        //        _dbContext.SystemLog.Add(systemLogmodel);
        //        _dbContext.SaveChanges();
        //        _logger.LogDebug("报警信息订阅：" + jsonStr);
        //    }catch(Exception ex)
        //    {
        //        _logger.LogDebug("报警信息订阅错误：" + ex.Message);
        //    }
            
        //    return Ok(response);
        //}

        [HttpPost]
        public async Task<string> GetAction()
        {
            string keyWord = "";
            string str = "";
            XiaoFang tempdata = new XiaoFang();
            Stream reqStream = Request.Body;
            string text = "";
            try
            {
                using (StreamReader reader = new StreamReader(reqStream))
                {
                    text = reader.ReadToEndAsync().Result;  ///这里的 text 就是 json字符串，然后在 后台反序列化 成 对象 就可以了
                    string filePath = "./logs/jiekou/";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string postPath = filePath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                    byte[] bytes = null;

                    bytes = Encoding.UTF8.GetBytes(text);//Obj为json数据

                    FileStream fs = new FileStream(postPath, FileMode.Create);

                    fs.Write(bytes, 0, bytes.Length);

                    fs.Close();
                    
                    //SendEmailHelp.SendEmailByQQ("zyp33966@163.com", "" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "推送内容", "" + text + "");
                    tempdata = JsonConvert.DeserializeObject<XiaoFang>(text); //反序列化为 JSonfor 的对象
                }
            }
            catch (Exception ex)
            {
                string filePath = "./logs/jiekou/";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string postPath = filePath + DateTime.Now.ToString("yyyyMMddHHmmss") + "Message.txt";
                byte[] bytes = null;

                bytes = Encoding.UTF8.GetBytes(ex.Message);//Obj为json数据

                FileStream fs = new FileStream(postPath, FileMode.Create);

                fs.Write(bytes, 0, bytes.Length);

                fs.Close();
                str = "{code = 999,msg = '" + ex.Message + "'}";
                return JsonConvert.SerializeObject(str);
            }
            if (tempdata != null)
            {
                if (tempdata.data != null)
                {
                    for (int i = 0; i < tempdata.data.Count; i++)
                    {
                        using (_dbContext)
                        {
                            Alarmdata entity = new Alarmdata();
                            entity.AddTime = DateTime.Now.ToString();
                            entity.AlarmUuid = Guid.NewGuid();
                            entity.AlarmTypeId = tempdata.data[i].alarmTypeId;
                            entity.AnalogValue = tempdata.data[i].analogValue;
                            entity.AnalogValueTypeId = tempdata.data[i].analogValueTypeId;
                            entity.AudioUrl = tempdata.data[i].audioUrl;
                            entity.AutoStatus = tempdata.data[i].autoStatus.ToString();
                            entity.BuildingId = tempdata.data[i].buildingId;
                            entity.ChargingUserName = tempdata.data[i].chargingUserName;
                            entity.ChargingUserTel = tempdata.data[i].chargingUserTel;
                            entity.ConfirmUserId = tempdata.data[i].confirmUserId;
                            entity.DetectorId = tempdata.data[i].detectorId;
                            entity.EndTime = tempdata.data[i].endTime;
                            entity.EventAddress = tempdata.data[i].eventAddress;
                            entity.EventContent = tempdata.data[i].eventContent;
                            entity.EventStatus = tempdata.data[i].eventStatus;
                            entity.EventTypeId = tempdata.data[i].eventTypeId;
                            entity.HandingTime = tempdata.data[i].handingTime;
                            entity.HandlingSuggestion = tempdata.data[i].handlingSuggestion;
                            entity.ImageUrl = tempdata.data[i].imageUrl;
                            entity.IsHandled = tempdata.data[i].isHandled.ToString();
                            entity.MainframeId = tempdata.data[i].mainframeId;
                            entity.OrgId = tempdata.data[i].orgId;
                            entity.PlatformCode = tempdata.data[i].platformCode;
                            entity.RecordCode = tempdata.data[i].recordCode;
                            entity.Remarks = tempdata.data[i].remarks;
                            entity.ReportUserId = tempdata.data[i].reportUserId;
                            entity.ResetStatus = tempdata.data[i].resetStatus.ToString();
                            entity.ResetTime = tempdata.data[i].resetTime;
                            entity.StartTime = tempdata.data[i].startTime.ToString();
                            entity.VideoUrl = tempdata.data[i].videoUrl;
                            _dbContext.Alarmdata.Add(entity);
                            _dbContext.SaveChanges();
                        }
                    }
                    str = "{code = 200,msg = '推送成功'}";

                }
                else
                {
                    str = "{code = 300,msg = '推送失败：data值为空'}";
                }
            }
            else
            {
                str = "{code = -1,msg = '推送失败：内容为空！'}";
            }
            string result = JsonConvert.SerializeObject(str);
            return result;
        }
    }
}
