using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Haikan3.Utils;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Models.Party;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Party
{
    [Route("api/v1/Party/[controller]/[action]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public PartyController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 文化程度
        /// </summary>
        [HttpGet]
        public IActionResult culture()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && x.Education == "专科" || x.Education == "本科" || x.Education == "硕士" || x.Education == "博士").Count();
                int bb = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && x.Education == "高中" || x.Education == "中专").Count();
                int cc = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && x.Education == "初中").Count();
                int dd = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && x.Education == "小学").Count();
                data.Add(new { aa, bb, cc, dd });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 男女比例
        /// </summary>
        [HttpGet]
        public IActionResult person()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int man = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && x.Sex == "男").Count();
                int woman = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && x.Sex == "女").Count();
                data.Add(new { man, woman });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 年龄分析
        /// </summary>
        [HttpGet]
        public IActionResult age()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && x.Age > 70).Count();
                int bb = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && (x.Age <= 70 && x.Age > 60)).Count();
                int cc = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && (x.Age <= 60 && x.Age > 50)).Count();
                int dd = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && (x.Age <= 50 && x.Age > 40)).Count();
                int ee = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && (x.Age <= 40 && x.Age > 30)).Count();
                data.Add(new { aa, bb, cc, dd, ee });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 党员情况
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult cpcinfo()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && x.Category == "正式党员").Count();
                int bb = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && x.Category == "预备党员").Count();
                int cc = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && x.Category == "发展对象").Count();
                int dd = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1" && x.Category == "积极分子").Count();
                data.Add(new { aa, bb, cc, dd });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 党组织活动
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Activitylist()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.DangActivity.Where(x => x.IsDeleted == 0).ToList();
                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 共青团
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult YLeague()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var sorganic = _dbContext.SthOrganic.Where(x => x.IsDelete == 0).Count();
                var yposition = _dbContext.YouthPosition.Where(x => x.IsDelete == 0).Count();
                var vteam = _dbContext.VolunteerTeam.Where(x => x.IsDelete == 0).Count();
                response.SetData(new { sorganic, yposition, vteam });
                return Ok(response);
            }
        }

        /// <summary>
        /// 团组织架构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Gettzzjg()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var list = _dbContext.SthOrganic.Where(x => x.IsDelete == 0).ToList();
                response.SetData(list);
                return Ok(response);
            }
        }

        /// <summary>
        /// 青年活动阵地
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Getqnhdzd()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var list = _dbContext.YouthPosition.Where(x => x.IsDelete == 0).ToList();
                response.SetData(list);
                return Ok(response);
            }
        }

        /// <summary>
        /// 志愿服务队伍
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Getzyfwdw()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var list = _dbContext.VolunteerTeam.Where(x => x.IsDelete == 0).ToList();
                response.SetData(list);
                return Ok(response);
            }
        }


        /// <summary>
        /// 宣传文化员名单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Getxcwhymd()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var list = _dbContext.PromoTeam.Where(x => x.IsDeleted == 0).ToList();
                response.SetData(list);
                return Ok(response);
            }
        }

        /// <summary>
        /// 宣传文化阵地
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Getxcwhzd()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var entity = _dbContext.PublicityType.Where(x => x.IsDeleted == 0);
                var zwhz = _dbContext.PublicityFronts.Where(x => x.IsDelete == 0 && x.PublicityTypeUuid == entity.FirstOrDefault(y => y.PublicityTypeName == "镇文化站").PublicityTypeUuid.ToString()).ToList();
                var xsdwmsjs = _dbContext.PublicityFronts.Where(x => x.IsDelete == 0 && x.PublicityTypeUuid == entity.FirstOrDefault(y => y.PublicityTypeName == "新时代文明实践所").PublicityTypeUuid.ToString()).ToList();
                var xsdwmsjz = _dbContext.PublicityFronts.Where(x => x.IsDelete == 0 && x.PublicityTypeUuid == entity.FirstOrDefault(y => y.PublicityTypeName == "新时代文明实践站").PublicityTypeUuid.ToString()).ToList();
                var njsw = _dbContext.PublicityFronts.Where(x => x.IsDelete == 0 && x.PublicityTypeUuid == entity.FirstOrDefault(y => y.PublicityTypeName == "农家书屋").PublicityTypeUuid.ToString()).ToList();
                var cjwhlt = _dbContext.PublicityFronts.Where(x => x.IsDelete == 0 && x.PublicityTypeUuid == entity.FirstOrDefault(y => y.PublicityTypeName == "村级文化礼堂").PublicityTypeUuid.ToString()).ToList();
                var djxchd = _dbContext.Promo.Where(x => x.IsDeleted == 0 ).ToList();
                response.SetData(new { zwhz, xsdwmsjs, xsdwmsjz, njsw, cjwhlt, djxchd });
                return Ok(response);
            }
        }

        /// <summary>
        /// 妇联
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Getwomen()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var womenzz = _dbContext.WomenFederation.Where(x => x.IsDeleted == 0).ToList();
                var womenhd = _dbContext.WomenActivities.Where(x => x.IsDeleted == 0).ToList();
                var womenfnzj = _dbContext.WomenHouse.Where(x => x.IsDeleted == 0).ToList();
                response.SetData(new { womenzz, womenhd, womenfnzj });
                return Ok(response);
            }
        }

        /// <summary>
        /// 工会活动
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Getghhd()
        {

            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var ghhd = _dbContext.TheUnion.Where(x => x.IsDeleted == 0).ToList();
                response.SetData(ghhd);
                return Ok(response);
            }
        }

        [HttpGet]
        public IActionResult Getssrk(int type)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            try
            {
                var timestamp = GetTimeStamp();
                string masterSecret = "917f2d05481695420cdb74d109e38874a009fbb01e5739772ccac0fa4fdf2ab0f703c2fa6ce68af9ac9bc02b25bfb0cfd2b9e97e9aea37b7a2ab0e3a420638ca";
                string appKey = "linanqu";
                string getCorrectionUrl = "https://webapi.getui.com/api/auth/creditAuth";
                var sign1 = GetMD5Hash(timestamp);
                var sign = GetSHA256Hash(appKey + sign1 + masterSecret);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(getCorrectionUrl);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";
                string body = "{\"appKey\":\"" + appKey + "\",\"sign\":\"" + sign + masterSecret + "\",\"timestamp\":" + Convert.ToInt32(timestamp) + ",\"version\":\"" + "0.0.1" + "\"}";
                byte[] btbs = Encoding.UTF8.GetBytes(body);
                myRequest.ContentLength = btbs.Length;
                myRequest.GetRequestStream().Write(btbs, 0, btbs.Length);
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string returnJson = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
                var code = myResponse.StatusCode.ToString();
                reader.Close();
                myResponse.Close();
                Timedata model = JsonConvert.DeserializeObject<Timedata>(returnJson);
                string token = model.data.accessToken;
                if (type == 1)
                {
                    string getCorrectionUrl1 = "https://webapi.getui.com/api/population/dashboard/getList";
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(getCorrectionUrl1);
                    myRequest1.Method = "POST";
                    //请求头参数设置
                    myRequest1.Headers.Add("Access-Token", token);
                    myRequest1.ContentType = "application/json";
                    myRequest1.UseDefaultCredentials = true;
                    string body1 = "{\"code\":\"" + "330112" + "\"}";
                    byte[] btbs1 = Encoding.UTF8.GetBytes(body1);
                    myRequest1.ContentLength = btbs1.Length;
                    myRequest1.GetRequestStream().Write(btbs1, 0, btbs1.Length);
                    HttpWebResponse myResponse1 = (HttpWebResponse)myRequest1.GetResponse();
                    StreamReader reader1 = new StreamReader(myResponse1.GetResponseStream(), Encoding.UTF8);
                    string returnJson1 = reader1.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
                    var code1 = myResponse1.StatusCode.ToString();
                    reader1.Close();
                    myResponse1.Close();
                    TimeStreet model1 = JsonConvert.DeserializeObject<TimeStreet>(returnJson1);
                    int max = 0;
                    int min = 0;
                    using (_dbContext)
                    {
                        Guid id = Guid.Parse("8678D1A0-1E2F-49B3-8F36-17CC1FD8F2CD");
                        MaxOrMin maxOrMin = _dbContext.MaxOrMin.Where(x => x.Guid == id).FirstOrDefault();
                        if (model1.data.children[11].count > Convert.ToInt32(maxOrMin.MaxCount))
                        {
                            maxOrMin.MaxCount = model1.data.children[11].count.ToString();
                            max = Convert.ToInt32(maxOrMin.MaxCount);
                        }
                        else
                        {
                            max = Convert.ToInt32(maxOrMin.MaxCount);
                        }
                        if (model1.data.children[11].count < Convert.ToInt32(maxOrMin.MinCount))
                        {
                            maxOrMin.MinCount = model1.data.children[11].count.ToString();
                            min = Convert.ToInt32(maxOrMin.MinCount);
                        }
                        else
                        {
                            min = Convert.ToInt32(maxOrMin.MinCount);
                        }
                        
                        _dbContext.MaxOrMin.Update(maxOrMin);
                        _dbContext.SaveChanges();
                    }
                    model1.data.children[11].maxcount = max;
                    model1.data.children[11].mincount = min;
                    response.SetData(model1.data.children[11]);
                }
                else if (type == 2)
                {
                    string getCorrectionUrl1 = "https://webapi.getui.com/api/population/dashboard/getUserTags";
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(getCorrectionUrl1);
                    myRequest1.Method = "POST";
                    //请求头参数设置
                    myRequest1.Headers.Add("Access-Token", token);
                    myRequest1.ContentType = "application/json";
                    myRequest1.UseDefaultCredentials = true;
                    string body1 = "{\"code\":\"" + "330112109000" + "\"}";
                    byte[] btbs1 = Encoding.UTF8.GetBytes(body1);
                    myRequest1.ContentLength = btbs1.Length;
                    myRequest1.GetRequestStream().Write(btbs1, 0, btbs1.Length);
                    HttpWebResponse myResponse1 = (HttpWebResponse)myRequest1.GetResponse();
                    StreamReader reader1 = new StreamReader(myResponse1.GetResponseStream(), Encoding.UTF8);
                    string returnJson1 = reader1.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
                    var code1 = myResponse1.StatusCode.ToString();
                    reader1.Close();
                    myResponse1.Close();
                    TimePerson model1 = JsonConvert.DeserializeObject<TimePerson>(returnJson1);
                    response.SetData(model1);
                }
                else if (type == 3)
                {
                    string getCorrectionUrl1 = "https://webapi.getui.com/api/population/dashboard/getOriginCity";
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(getCorrectionUrl1);
                    myRequest1.Method = "POST";
                    //请求头参数设置
                    myRequest1.Headers.Add("Access-Token", token);
                    myRequest1.ContentType = "application/json";
                    myRequest1.UseDefaultCredentials = true;
                    string body1 = "{\"code\":\"" + "330112109000" + "\"}";
                    byte[] btbs1 = Encoding.UTF8.GetBytes(body1);
                    myRequest1.ContentLength = btbs1.Length;
                    myRequest1.GetRequestStream().Write(btbs1, 0, btbs1.Length);
                    HttpWebResponse myResponse1 = (HttpWebResponse)myRequest1.GetResponse();
                    StreamReader reader1 = new StreamReader(myResponse1.GetResponseStream(), Encoding.UTF8);
                    string returnJson1 = reader1.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
                    var code1 = myResponse1.StatusCode.ToString();
                    reader1.Close();
                    myResponse1.Close();
                    TimeSource model1 = JsonConvert.DeserializeObject<TimeSource>(returnJson1);
                    response.SetData(model1);
                }
                else if (type == 4)
                {
                    string getCorrectionUrl1 = "https://webapi.getui.com/api/population/count/getWorkData";
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(getCorrectionUrl1);
                    myRequest1.Method = "POST";
                    //请求头参数设置
                    myRequest1.Headers.Add("Access-Token", token);
                    myRequest1.ContentType = "application/json";
                    myRequest1.UseDefaultCredentials = true;
                    string body1 = "{\"area_code\":\"" + "330112109000" + "\"}";
                    byte[] btbs1 = Encoding.UTF8.GetBytes(body1);
                    myRequest1.ContentLength = btbs1.Length;
                    myRequest1.GetRequestStream().Write(btbs1, 0, btbs1.Length);
                    HttpWebResponse myResponse1 = (HttpWebResponse)myRequest1.GetResponse();
                    StreamReader reader1 = new StreamReader(myResponse1.GetResponseStream(), Encoding.UTF8);
                    string returnJson1 = reader1.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
                    var code1 = myResponse1.StatusCode.ToString();
                    reader1.Close();
                    myResponse1.Close();
                    TimePostWork model1 = JsonConvert.DeserializeObject<TimePostWork>(returnJson1);
                    response.SetData(model1);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {

                response.SetFailed("获取失败");
                response.SetData(ex.Message);
                return Ok(response);
            }
        }

        /// <summary>
        /// 使用 MD5  对字符串  进行加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// 
        public static string GetMD5Hash(string str)
        {
            StringBuilder result = new StringBuilder();
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {

                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                int length = data.Length;
                for (int i = 0; i < length; i++)
                    result.Append(data[i].ToString("x2"));

            }
            return result.ToString();
        }

        /// <summary>
        /// 使用 SHA256  对字符串  进行加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// 
        public static string GetSHA256Hash(string str)
        {
            StringBuilder result = new StringBuilder();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] hash = SHA256Managed.Create().ComputeHash(bytes);
            for (int i = 0; i < hash.Length; i++)
                result.Append(hash[i].ToString("x2"));

            return result.ToString();
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 8, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds).ToString();
        }
    }
}
