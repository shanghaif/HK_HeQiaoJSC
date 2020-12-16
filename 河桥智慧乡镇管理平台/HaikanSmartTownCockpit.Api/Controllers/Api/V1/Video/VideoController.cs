using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Video
{
    [Route("api/v1/Video/[controller]/[action]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        public static double pi = 3.14159265358979324;
        public static double a = 6378245.0;
        public static double ee = 0.00669342162296594323;
        public static double x_pi = 3.14159265358979324 * 3000.0 / 180.0;

        public VideoController(haikanHeQiaoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 雪亮工程视频点位信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult VideoPoint()
        {
            
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var query = _dbContext.XlProject.Where(x => x.IsDeleted == 0).Select(x => new
                {
                    point =(x.Lat!=null&&x.Lon!=null&&(!string.IsNullOrEmpty(x.Lon))&&(!string.IsNullOrEmpty(x.Lat)))?Wgs2gcj(double.Parse(x.Lon), double.Parse(x.Lat)):null,
                    x.AddPeople,
                    x.AddTime,
                    x.AdminInfo,
                    x.Id,
                    x.ShebeiAddress,
                    x.ShebeiType,
                    x.XlProjectUuid,
                    x.XlShebeiId,
                    x.XlShebeiType,

                }).OrderBy(x=>x.ShebeiAddress) ;

                var list = query.ToList();

                response.SetData(list);
                return Ok(response);
            }

        }


        /// <summary>
        /// 五水共治监控
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult WSGZVideo()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var query = _dbContext.XlProject.Where(x => x.IsDeleted == 0 && x.ShebeiType == "在线"&&x.XlShebeiType== "五水共治监控").Select(x => new
                {
                    //point = (x.Lat != null && x.Lon != null && (!string.IsNullOrEmpty(x.Lon)) && (!string.IsNullOrEmpty(x.Lat))) ? Wgs2gcj(double.Parse(x.Lon), double.Parse(x.Lat)) : null,
                    x.AddPeople,
                    x.AddTime,
                    x.AdminInfo,
                    x.Id,
                    x.ShebeiAddress,
                    x.ShebeiType,
                    x.XlProjectUuid,
                    x.XlShebeiId,
                    x.XlShebeiType,
                    

                }).OrderBy(x => x.ShebeiAddress);

                var list = query.ToList();

                response.SetData(list);
                return Ok(response);
            }

        }


        /// <summary>
        /// 五水共治监控
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult WSGZVideo2()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var query = _dbContext.XlProject.Where(x => x.IsDeleted == 0 && x.ShebeiType == "在线" && x.ShebeiAddress.Contains("蒲溪")&&x.XlShebeiType== "河道监控").Select(x => new
                {
                    //point = (x.Lat != null && x.Lon != null && (!string.IsNullOrEmpty(x.Lon)) && (!string.IsNullOrEmpty(x.Lat))) ? Wgs2gcj(double.Parse(x.Lon), double.Parse(x.Lat)) : null,
                    x.AddPeople,
                    x.AddTime,
                    x.AdminInfo,
                    x.Id,
                    x.ShebeiAddress,
                    x.ShebeiType,
                    x.XlProjectUuid,
                    x.XlShebeiId,
                    x.XlShebeiType,


                }).OrderBy(x => x.ShebeiAddress);

                var list = query.ToList();

                response.SetData(list);
                return Ok(response);
            }

        }


        /// <summary>
        /// 房多的视频
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FDVideo2()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var query = _dbContext.XlProject.Where(x => x.IsDeleted == 0 && x.ShebeiType == "在线" && x.ShebeiAddress.Contains("老街")).Select(x => new
                {
                    //point = (x.Lat != null && x.Lon != null && (!string.IsNullOrEmpty(x.Lon)) && (!string.IsNullOrEmpty(x.Lat))) ? Wgs2gcj(double.Parse(x.Lon), double.Parse(x.Lat)) : null,
                    x.AddPeople,
                    x.AddTime,
                    x.AdminInfo,
                    x.Id,
                    x.ShebeiAddress,
                    x.ShebeiType,
                    x.XlProjectUuid,
                    x.XlShebeiId,
                    x.XlShebeiType,


                }).OrderBy(x => x.ShebeiAddress);

                var list = query.ToList();

                response.SetData(list);
                return Ok(response);
            }

        }
        /// <summary>
        /// 房多的视频
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ZHDVideo2()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                List<string> videoname = new List<string>()
                {
                    "河桥镇-河桥村云月山庄旁",
                    "河桥镇-金燕村方家桥",
                    "河桥镇-七都村寺坞83号",
                    "蒲村-童婵斌住宅旁"
                };
                var query = _dbContext.XlProject.Where(x => x.IsDeleted == 0 &&videoname.Contains(x.ShebeiAddress)).Select(x => new
                {
                    //point = (x.Lat != null && x.Lon != null && (!string.IsNullOrEmpty(x.Lon)) && (!string.IsNullOrEmpty(x.Lat))) ? Wgs2gcj(double.Parse(x.Lon), double.Parse(x.Lat)) : null,
                    x.AddPeople,
                    x.AddTime,
                    x.AdminInfo,
                    x.Id,
                    x.ShebeiAddress,
                    x.ShebeiType,
                    x.XlProjectUuid,
                    x.XlShebeiId,
                    x.XlShebeiType,


                }).OrderBy(x => x.ShebeiAddress);

                var list = query.ToList();

                response.SetData(list);
                return Ok(response);
            }

        }


        /// <summary>
        /// 公厕的视频
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ZHDVideo3()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                List<string> videoname = new List<string>()
                {
                    "蒲溪旁3A公厕",
                    "新街停车场旁3A公厕"
                };
                var query = _dbContext.XlProject.Where(x => x.IsDeleted == 0 && videoname.Contains(x.ShebeiAddress)).Select(x => new
                {
                    //point = (x.Lat != null && x.Lon != null && (!string.IsNullOrEmpty(x.Lon)) && (!string.IsNullOrEmpty(x.Lat))) ? Wgs2gcj(double.Parse(x.Lon), double.Parse(x.Lat)) : null,
                    x.AddPeople,
                    x.AddTime,
                    x.AdminInfo,
                    x.Id,
                    x.ShebeiAddress,
                    x.ShebeiType,
                    x.XlProjectUuid,
                    x.XlShebeiId,
                    x.XlShebeiType,


                }).OrderBy(x => x.ShebeiAddress);

                var list = query.ToList();

                response.SetData(list);
                return Ok(response);
            }

        }



        /// <summary>
        /// 农民建房的视频
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NMJFVideo2()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                //&&x.ShebeiType=="在线"
                var query = _dbContext.XlProject.Where(x => x.IsDeleted == 0 && x.XlShebeiType== "农民房建设监控").Select(x => new
                {
                    //point = (x.Lat != null && x.Lon != null && (!string.IsNullOrEmpty(x.Lon)) && (!string.IsNullOrEmpty(x.Lat))) ? Wgs2gcj(double.Parse(x.Lon), double.Parse(x.Lat)) : null,
                    x.AddPeople,
                    x.AddTime,
                    x.AdminInfo,
                    x.Id,
                    x.ShebeiAddress,
                    x.ShebeiType,
                    x.XlProjectUuid,
                    x.XlShebeiId,
                    x.XlShebeiType,


                }).OrderBy(x => x.Id);

                var list = query.ToList();

                response.SetData(list);
                return Ok(response);
            }

        }


        /// <summary>
        /// 五水共治展示监控
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult WSGZSignVideo()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var query1 = _dbContext.XlProject.FirstOrDefault(x => x.IsDeleted == 0 && x.ShebeiType == "在线" && x.XlShebeiType == "五水共治污水监控");
                var query2 = _dbContext.XlProject.FirstOrDefault(x => x.IsDeleted == 0 && x.ShebeiType == "在线" && x.XlShebeiType == "五水共治防洪水监控");
                var query3 = _dbContext.XlProject.FirstOrDefault(x => x.IsDeleted == 0 && x.ShebeiType == "在线" && x.XlShebeiType == "五水共治排涝水监控");
                var query4 = _dbContext.XlProject.FirstOrDefault(x => x.IsDeleted == 0 && x.ShebeiType == "在线" && x.XlShebeiType == "五水共治保供水监控");
                var query5 = _dbContext.XlProject.FirstOrDefault(x => x.IsDeleted == 0 && x.ShebeiType == "在线" && x.XlShebeiType == "五水共治抓节水监控");


                

                response.SetData(new { query1,query2,query3,query4,query5});
                return Ok(response);
            }

        }

        /// <summary>
        /// 获取所有五水共治监控
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult WSGZAllVideo()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var query1 = _dbContext.XlProject.Where(x => x.IsDeleted == 0 && x.XlShebeiType.Contains("五水共治")).OrderBy(x=>x.ShebeiAddress);

                var list = query1.ToList();

                response.SetData(list);
                return Ok(response);
            }
        }

        /// <summary>
        /// 森林防火监控
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SLFHAllVideo()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var query1 = _dbContext.XlProject.Where(x => x.IsDeleted == 0 && x.XlShebeiType.Contains("森林防火")).OrderBy(x => x.ShebeiAddress);

                var list = query1.ToList();

                response.SetData(list);
                return Ok(response);
            }
        }




        /// <summary>
        ///  WGS坐标转换为GCJ坐标。
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        private static double[] Wgs2gcj(double lon, double lat)
        {

            double dLat = TransformLat(lon - 105.0, lat - 35.0);
            double dLon = TransformLon(lon - 105.0, lat - 35.0);
            double radLat = lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double SqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * SqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / SqrtMagic * Math.Cos(radLat) * pi);
            double mgLat = lat + dLat;
            double mgLon = lon + dLon;
            double[] loc = { mgLon, mgLat };
            return loc;

        }


        /// <summary>
        /// 转换方法，比较复杂，不必深究了。输入：横纵坐标，输出：转换后的横坐标（也就是 纬度）。
        /// </summary>
        /// <param name="lat">纬度（横坐标）</param>
        /// <param name="lon">经度（纵坐标）</param>
        /// <returns></returns>
        private static double TransformLat(double lon, double lat)
        {
            double ret = -100.0 + 2.0 * lon + 3.0 * lat + 0.2 * lat * lat + 0.1 * lat * lon + 0.2 * Math.Sqrt(Math.Abs(lon));
            ret += (20.0 * Math.Sin(6.0 * lon * pi) + 20.0 * Math.Sin(2.0 * lon * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lat * pi) + 40.0 * Math.Sin(lat / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(lat / 12.0 * pi) + 320 * Math.Sin(lat * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        /// <summary>
        /// 转换方法，比较复杂，不必深究了。输入：横纵坐标，输出：转换后的横坐标（也就是 经度）。
        /// </summary>
        /// <param name="lat">纬度（横坐标）</param>
        /// <param name="lon">经度（纵坐标）</param>
        /// <returns></returns>
        private static double TransformLon(double lon, double lat)
        {
            double ret = 300.0 + lon + 2.0 * lat + 0.1 * lon * lon + 0.1 * lat * lon + 0.1 * Math.Sqrt(Math.Abs(lon));
            ret += (20.0 * Math.Sin(6.0 * lon * pi) + 20.0 * Math.Sin(2.0 * lon * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lon * pi) + 40.0 * Math.Sin(lon / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(lon / 12.0 * pi) + 300.0 * Math.Sin(lon / 30.0 * pi)) * 2.0 / 3.0;
            return ret;
        }

    }
}
