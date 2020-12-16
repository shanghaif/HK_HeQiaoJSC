using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.FireFighting
{
    [Route("api/v1/FireFighting/[controller]/[action]")]
    [ApiController]
    public class CarXiaoQuAnFangController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        public CarXiaoQuAnFangController(haikanHeQiaoContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<string> GetAction()
        {

            string keyWord = "";
            string str = "";
            Models.CarAnFang tempdata = new Models.CarAnFang();
            Stream reqStream = Request.Body;

            string text = "";
            try
            {
                using (StreamReader reader = new StreamReader(reqStream))
                {
                    text = reader.ReadToEndAsync().Result;  ///这里的 text 就是 json字符串，然后在 后台反序列化 成 对象 就可以了
                    string filePath = "./logs/jiekou3/";
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
                    tempdata = JsonConvert.DeserializeObject<Models.CarAnFang>(text); //反序列化为 JSonfor 的对象
                }
            }
            catch (Exception ex)
            {
                string filePath = "./logs/jiekou3/";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string postPath = filePath + DateTime.Now.ToString("yyyyMMddHHmmss") + "Message.txt";
                byte[] bytes = null;

                bytes = Encoding.UTF8.GetBytes(ex.Message.ToString());//Obj为json数据

                FileStream fs = new FileStream(postPath, FileMode.Create);

                fs.Write(bytes, 0, bytes.Length);

                fs.Close();
                str = "{code = 999,msg = '" + ex.Message + "'}";
                return JsonConvert.SerializeObject(str);
            }
            if (tempdata != null)
            {
                if (tempdata.@params.events != null)
                {
                    for (int i = 0; i < tempdata.@params.events.Count; i++)
                    {
                        using (_dbContext)
                        {
                           Entities.CarAnFang entity = new Entities.CarAnFang();
                            entity.Guid = Guid.NewGuid();
                            entity.Ability = tempdata.@params.ability;
                            entity.PlateTypeName = tempdata.@params.events[i].data.plateTypeName;
                            entity.CrossTime= tempdata.@params.events[i].data.crossTime;
                            entity.AlarmTypeName= tempdata.@params.events[i].data.alarmTypeName;
                            entity.MonitorName = tempdata.@params.events[i].data.monitorName;
                            entity.MixedName = tempdata.@params.events[i].data.mixedName;
                            entity.PlatePicUrl = tempdata.@params.events[i].data.picUrl.platePicUrl;
                            entity.VehiclePicUrl = tempdata.@params.events[i].data.picUrl.vehiclePicUrl;
                            entity.AlarmTypeName = tempdata.@params.events[i].data.alarmTypeName;
                            entity.ImageIndexCode = tempdata.@params.events[i].data.imageIndexCode;
                            entity.PersonName = tempdata.@params.events[i].data.person.personName;
                            entity.PhoneNo = tempdata.@params.events[i].data.plateNo;
                            entity.HappenTime = tempdata.@params.events[i].happenTime;
                            _dbContext.CarAnFang.Add(entity);
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
