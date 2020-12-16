using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaikanSmartTownCockpit.Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.FireFighting
{
    [Route("api/v1/FireFighting/[controller]/[action]")]
    [ApiController]
    public class AnFangDataController : Controller
    {
        private readonly haikanHeQiaoContext _dbContext;
        public AnFangDataController(haikanHeQiaoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public string List(string id)
        {
            var list = (from a in _dbContext.XiaoQuAnFang
                        where a.CameraIndexCode == id
                        select new
                        {
                            HappenTime=Convert.ToDateTime(a.HappenTime).ToString("yyyy-MM-dd HH:mm:ss"),
                            Ability= Gettype1(a.Ability),
                            Types= GetType(a.Type),
                            time=Convert.ToDateTime(a.HappenTime),
                            a.Url,                            
                        }).ToList();
            list = list.OrderByDescending(x => x.time).ToList();
            string result = JsonConvert.SerializeObject(list);
            return result;
        }
        [HttpGet]
        public string List2(string id)
        {
            var list = (from a in _dbContext.CarAnFang
                        where a.MonitorName == id
                        select new
                        {
                            HappenTime = Convert.ToDateTime(a.HappenTime).ToString("yyyy-MM-dd HH:mm:ss"),
                            Ability = Gettype2(a.Ability),
                            a.PlateTypeName,
                            time = Convert.ToDateTime(a.HappenTime),
                            a.MonitorName,
                            a.VehiclePicUrl,
                            a.PlatePicUrl,
                            a.PhoneNo
                        }).ToList();
            list = list.OrderByDescending(x => x.time).ToList();
            string result = JsonConvert.SerializeObject(list);
            return result;
        }

        public static string GetType(string id)
        {
            if (id=="0")
            {
                return "全部类型";
            }
            if (id == "1")
            {
                return "人脸抓拍图片过滤";
            }
            if (id == "2")
            {
                return "黑名单库";
            }
            if (id == "3")
            {
                return "白名单库";
            }
            return "";
        }
        public static string Gettype1(string id)
        {
            if (id== "event_face")
            {
                return "人脸抓拍";
            }
            return "";
        }
        public static string Gettype2(string id)
        {
            if (id == "event_mpc")
            {
                return "园区卡口事件";
            }
            return "";
        }
    }
}
