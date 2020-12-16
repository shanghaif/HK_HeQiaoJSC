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
    public class XiaoQuAnFangController : Controller
    {
        private readonly haikanHeQiaoContext _dbContext;
        public XiaoQuAnFangController(haikanHeQiaoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<string> GetAction()
        {
            
            string keyWord = "";
            string str = "";
            XiaoQuFang tempdata = new XiaoQuFang();
            Stream reqStream = Request.Body;
            
            string text = "";
            try
            {
                using (StreamReader reader = new StreamReader(reqStream))
                {
                    text = reader.ReadToEndAsync().Result;  ///这里的 text 就是 json字符串，然后在 后台反序列化 成 对象 就可以了
                    string filePath = "./logs/jiekou2/";
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
                    tempdata = JsonConvert.DeserializeObject<XiaoQuFang>(text); //反序列化为 JSonfor 的对象
                }
            }
            catch (Exception ex)
            {
                string filePath = "./logs/jiekou2/";
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
                            if (tempdata.@params.ability == "event_face")
                            {
                                XiaoQuAnFang entity = new XiaoQuAnFang();
                                if (tempdata.@params.events[i].data.captureLibResult.Count == 1)
                                {
                                    if (tempdata.@params.events[i].data.captureLibResult[0].faces.Count == 1)
                                    {
                                        entity.Guid = Guid.NewGuid();
                                        entity.Ability = tempdata.@params.ability;
                                        entity.SendTime = tempdata.@params.sendTime;
                                        //entity.SendTime = tempdata.data[i].analogValue;
                                        entity.EventId = tempdata.@params.events[i].eventId;
                                        entity.EventType = tempdata.@params.events[i].eventType.ToString();
                                        entity.HappenTime = tempdata.@params.events[i].happenTime;
                                        entity.SrcIndex = tempdata.@params.events[i].srcIndex;
                                        entity.SrcType = tempdata.@params.events[i].srcType;
                                        entity.Status = tempdata.@params.events[i].status.ToString();
                                        entity.DataType = tempdata.@params.events[i].data.dataType;
                                        entity.IpAddress = tempdata.@params.events[i].data.ipAddress;
                                        entity.PortNo = tempdata.@params.events[i].data.portNo.ToString();
                                        entity.BkgUrl = tempdata.@params.events[i].data.captureLibResult[0].targetAttrs.bkgUrl;
                                        entity.CameraIndexCode = tempdata.@params.events[i].data.captureLibResult[0].targetAttrs.cameraIndexCode;
                                        entity.DeviceIndexCode = tempdata.@params.events[i].data.captureLibResult[0].targetAttrs.cameraIndexCode;
                                        entity.FaceTime = tempdata.@params.events[i].data.captureLibResult[0].targetAttrs.faceTime;
                                        entity.PicServerIndexCode = tempdata.@params.events[i].data.captureLibResult[0].targetAttrs.picServerIndexCode;
                                        entity.Url = tempdata.@params.events[i].data.captureLibResult[0].faces[0].URL;
                                        entity.Type = tempdata.@params.events[i].data.captureLibResult[0].faces[0].faceScore.type.ToString();

                                        _dbContext.XiaoQuAnFang.Add(entity);
                                        _dbContext.SaveChanges();
                                    }
                                    else
                                    {
                                        for (int b = 0; b < tempdata.@params.events[i].data.captureLibResult[0].faces.Count; b++)
                                        {
                                            entity.Guid = Guid.NewGuid();
                                            entity.Ability = tempdata.@params.ability;
                                            entity.SendTime = tempdata.@params.sendTime;
                                            //entity.SendTime = tempdata.data[i].analogValue;
                                            entity.EventId = tempdata.@params.events[i].eventId;
                                            entity.EventType = tempdata.@params.events[i].eventType.ToString();
                                            entity.HappenTime = tempdata.@params.events[i].happenTime;
                                            entity.SrcIndex = tempdata.@params.events[i].srcIndex;
                                            entity.SrcType = tempdata.@params.events[i].srcType;
                                            entity.Status = tempdata.@params.events[i].status.ToString();
                                            entity.DataType = tempdata.@params.events[i].data.dataType;
                                            entity.IpAddress = tempdata.@params.events[i].data.ipAddress;
                                            entity.PortNo = tempdata.@params.events[i].data.portNo.ToString();
                                            entity.BkgUrl = tempdata.@params.events[i].data.captureLibResult[0].targetAttrs.bkgUrl;
                                            entity.CameraIndexCode = tempdata.@params.events[i].data.captureLibResult[0].targetAttrs.cameraIndexCode;
                                            entity.DeviceIndexCode = tempdata.@params.events[i].data.captureLibResult[0].targetAttrs.cameraIndexCode;
                                            entity.FaceTime = tempdata.@params.events[i].data.captureLibResult[0].targetAttrs.faceTime;
                                            entity.PicServerIndexCode = tempdata.@params.events[i].data.captureLibResult[0].targetAttrs.picServerIndexCode;
                                            entity.Url = tempdata.@params.events[i].data.captureLibResult[0].faces[b].URL;
                                            entity.Type = tempdata.@params.events[i].data.captureLibResult[0].faces[b].faceScore.type.ToString();

                                            _dbContext.XiaoQuAnFang.Add(entity);
                                            _dbContext.SaveChanges();
                                        }
                                    }
                                }
                                else
                                {
                                    for (int a = 0; a < tempdata.@params.events[i].data.captureLibResult.Count; a++)
                                    {
                                        if (tempdata.@params.events[i].data.captureLibResult[a].faces.Count == 1)
                                        {
                                            entity.Guid = Guid.NewGuid();
                                            entity.Ability = tempdata.@params.ability;
                                            entity.SendTime = tempdata.@params.sendTime;
                                            //entity.SendTime = tempdata.data[i].analogValue;
                                            entity.EventId = tempdata.@params.events[i].eventId;
                                            entity.EventType = tempdata.@params.events[i].eventType.ToString();
                                            entity.HappenTime = tempdata.@params.events[i].happenTime;
                                            entity.SrcIndex = tempdata.@params.events[i].srcIndex;
                                            entity.SrcType = tempdata.@params.events[i].srcType;
                                            entity.Status = tempdata.@params.events[i].status.ToString();
                                            entity.DataType = tempdata.@params.events[i].data.dataType;
                                            entity.IpAddress = tempdata.@params.events[i].data.ipAddress;
                                            entity.PortNo = tempdata.@params.events[i].data.portNo.ToString();
                                            entity.BkgUrl = tempdata.@params.events[i].data.captureLibResult[a].targetAttrs.bkgUrl;
                                            entity.CameraIndexCode = tempdata.@params.events[i].data.captureLibResult[a].targetAttrs.cameraIndexCode;
                                            entity.DeviceIndexCode = tempdata.@params.events[i].data.captureLibResult[a].targetAttrs.cameraIndexCode;
                                            entity.FaceTime = tempdata.@params.events[i].data.captureLibResult[a].targetAttrs.faceTime;
                                            entity.PicServerIndexCode = tempdata.@params.events[i].data.captureLibResult[a].targetAttrs.picServerIndexCode;
                                            entity.Url = tempdata.@params.events[i].data.captureLibResult[a].faces[0].URL;
                                            entity.Type = tempdata.@params.events[i].data.captureLibResult[a].faces[0].faceScore.type.ToString();
                                            _dbContext.XiaoQuAnFang.Add(entity);
                                            _dbContext.SaveChanges();
                                        }
                                        else
                                        {
                                            for (int c = 0; c < tempdata.@params.events[i].data.captureLibResult[a].faces.Count; c++)
                                            {
                                                entity.Guid = Guid.NewGuid();
                                                entity.Ability = tempdata.@params.ability;
                                                entity.SendTime = tempdata.@params.sendTime;
                                                //entity.SendTime = tempdata.data[i].analogValue;
                                                entity.EventId = tempdata.@params.events[i].eventId;
                                                entity.EventType = tempdata.@params.events[i].eventType.ToString();
                                                entity.HappenTime = tempdata.@params.events[i].happenTime;
                                                entity.SrcIndex = tempdata.@params.events[i].srcIndex;
                                                entity.SrcType = tempdata.@params.events[i].srcType;
                                                entity.Status = tempdata.@params.events[i].status.ToString();
                                                entity.DataType = tempdata.@params.events[i].data.dataType;
                                                entity.IpAddress = tempdata.@params.events[i].data.ipAddress;
                                                entity.PortNo = tempdata.@params.events[i].data.portNo.ToString();
                                                entity.BkgUrl = tempdata.@params.events[i].data.captureLibResult[a].targetAttrs.bkgUrl;
                                                entity.CameraIndexCode = tempdata.@params.events[i].data.captureLibResult[a].targetAttrs.cameraIndexCode;
                                                entity.DeviceIndexCode = tempdata.@params.events[i].data.captureLibResult[a].targetAttrs.cameraIndexCode;
                                                entity.FaceTime = tempdata.@params.events[i].data.captureLibResult[a].targetAttrs.faceTime;
                                                entity.PicServerIndexCode = tempdata.@params.events[i].data.captureLibResult[a].targetAttrs.picServerIndexCode;
                                                entity.Url = tempdata.@params.events[i].data.captureLibResult[a].faces[c].URL;
                                                entity.Type = tempdata.@params.events[i].data.captureLibResult[a].faces[c].faceScore.type.ToString();
                                                _dbContext.XiaoQuAnFang.Add(entity);
                                                _dbContext.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                            
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
