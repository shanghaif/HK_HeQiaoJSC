using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Haikan3.Utils;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.AppTongji
{
    [Route("api/v1/AppTongji/[controller]/[action]")]
    [ApiController]
    public class AppTongjiController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public AppTongjiController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }




        /// <summary>
        /// 河桥镇基本信息
        /// </summary>
        [HttpGet]
        public IActionResult jiben()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int renkou = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0).Count();
                int dangyuan = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && x.DyStaues == "1").Count();
                int wangge = _dbContext.Administrator.Where(x => x.IsDeleted == 0).Count();
                data.Add(new { renkou, dangyuan });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 人员年龄分析
        /// </summary>
        [HttpGet]
        public IActionResult age()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && (x.Age >= 60)).Count();         //老年
                int bb = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && (x.Age < 60 && x.Age >= 30)).Count();     //中年
                int cc = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && (x.Age < 30 && x.Age >= 18)).Count();     //青年
                int dd = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && (x.Age < 18 && x.Age >= 8)).Count();      //少年
                int ee = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0 && (x.Age < 8)).Count();         //幼年
                data.Add(new { aa, bb, cc, dd, ee });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 游客年龄分析
        /// </summary>
        [HttpGet]
        public IActionResult youkeage()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Sightseer.Where(x => x.IsDeleted == 0 && (Convert.ToInt32(x.Age) >= 60)).Count();         //老年
                int bb = _dbContext.Sightseer.Where(x => x.IsDeleted == 0 && (Convert.ToInt32(x.Age) < 60 && Convert.ToInt32(x.Age) >= 30)).Count();     //中年
                int cc = _dbContext.Sightseer.Where(x => x.IsDeleted == 0 && (Convert.ToInt32(x.Age) < 30 && Convert.ToInt32(x.Age) >= 18)).Count();     //青年
                int dd = _dbContext.Sightseer.Where(x => x.IsDeleted == 0 && (Convert.ToInt32(x.Age) < 18 && Convert.ToInt32(x.Age) >= 8)).Count();      //少年
                int ee = _dbContext.Sightseer.Where(x => x.IsDeleted == 0 && (Convert.ToInt32(x.Age) < 8)).Count();         //幼年
                data.Add(new { aa, bb, cc, dd, ee });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 游客来源
        /// </summary>
        [HttpGet]
        public IActionResult youeklaiyuan()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Sightseer.Where(x => x.IsDeleted == 0 && x.Shengneiwai == "是").Count();           //省内
                int bb = _dbContext.Sightseer.Where(x => x.IsDeleted == 0 && x.Shengneiwai == "否").Count();           //省外
                data.Add(new { aa, bb, });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 酒店民宿数量总数
        /// </summary>
        [HttpGet]
        public IActionResult jiudian()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Hotel.Where(x => x.IsDeleted == 0).Count();         //酒店民宿总数
                int bb = _dbContext.Hotel.Where(x => x.IsDeleted == 0 && x.HotelType == "正常营业").Count();     //正在营业
                data.Add(new { aa, bb, });
                response.SetData(data);
                return Ok(response);
            }
        }


        /// <summary>
        /// 酒店名宿详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Hotel(string village)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var allquery = _dbContext.Hotel.Where(x => x.Village == village && x.IsDeleted == 0);
                var normal = allquery.Where(x => x.HotelType == "正常营业").Count();
                var alllist = allquery.ToList();
                var all = alllist.Count;

                response.SetData(new { alllist, all, normal });
                return Ok(response);

            }
        }

        [HttpGet]
        public IActionResult HotelCount(string village)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var all = _dbContext.Hotel.Where(x => x.Village == village && x.IsDeleted == 0).Count();
                

                response.SetData(all);
                return Ok(response);

            }
        }


        /// <summary>
        /// 餐饮数量总数
        /// </summary>
        [HttpGet]
        public IActionResult canyishuliang()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Canyin.Where(x => x.IsDeleted == 0).Count();         //餐饮数量总数
                int bb = _dbContext.Canyin.Where(x => x.IsDeleted == 0 && x.Staues == "正常营业").Count();     //正在营业
                data.Add(new { aa, bb, });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 酒店民宿入驻情况
        /// </summary>
        [HttpGet]
        public IActionResult jiudianruzhu()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Hotel.Where(x => x.IsDeleted == 0 && x.HotelName.Contains("酒")).Count();           //酒店总数
                int bb = _dbContext.Hotel.Where(x => x.IsDeleted == 0 && x.HotelName.Contains("酒") == false).Count();           //民宿总数
                data.Add(new { aa, bb, });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 景点数量总数
        /// </summary>
        [HttpGet]
        public IActionResult jingdian()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Scenic.Where(x => x.IsDeleted == 0).Count();           //景点总数
                int bb = _dbContext.Scenic.Where(x => x.IsDeleted == 0 && x.ScenicType == "正常营业").Count();           //正在营业
                data.Add(new { aa, bb, });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 游客男女比例
        /// </summary>
        [HttpGet]
        public IActionResult youkenannv()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Sightseer.Where(x => x.IsDeleted == 0 && x.Sex == "男").Count();           //男
                int bb = _dbContext.Sightseer.Where(x => x.IsDeleted == 0 && x.Sex == "女").Count();           //女
                data.Add(new { aa, bb, });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 党员人数分析
        /// </summary>
        [HttpGet]
        public IActionResult dangyuanrenshu()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int hq = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.DangOrganizationName.Contains("河桥村")).Count();           //河桥村
                int jy = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.DangOrganizationName.Contains("金燕村")).Count();           //金燕村
                int ls = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.DangOrganizationName.Contains("罗山村")).Count();           //罗山村
                int nl = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.DangOrganizationName.Contains("泥骆村")).Count();           //泥骆村
                int pc = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.DangOrganizationName.Contains("蒲村村")).Count();             //蒲村
                int qd = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.DangOrganizationName.Contains("七都村")).Count();           //七都村
                int xx = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.DangOrganizationName.Contains("秀溪村")).Count();           //秀溪村
                int xc = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.DangOrganizationName.Contains("学川村")).Count();           //学川村
                int yl = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.DangOrganizationName.Contains("云浪村")).Count();           //云浪村
                int zx = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.DangOrganizationName.Contains("中鑫村")).Count();           //中鑫村
                int zc = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.DangOrganizationName.Contains("聚秀村")).Count();           //朱秀村
                data.Add(new { hq, jy, ls, nl, pc, qd, xx, xc, yl, zx, zc });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 河桥镇基本信息
        /// </summary>
        [HttpGet]
        public IActionResult heqiaojibeninfo()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int cz = _dbContext.Village.Where(x => x.IsDelete == 0).Count();           //村庄总数
                int rk = _dbContext.Userinfoty.Where(x => x.IsDeleted == 0).Count();           //人口
                int dy = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0).Count();           //党员人数
                int wg = _dbContext.Administrator.Where(x => x.IsDeleted == 0).Count();           //网格
                data.Add(new { cz, rk, dy, wg });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 企业数量
        /// </summary>
        [HttpGet]
        public IActionResult qiyeshuliang()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int yy = _dbContext.Qiye.Where(x => x.IsDeleted == 0 && x.QiyeType == "农业企业").Count();           //农业企业
                int zz = _dbContext.Qiye.Where(x => x.IsDeleted == 0 && x.QiyeType == "服务业企业").Count();         //服务业企业
                int qt = _dbContext.Qiye.Where(x => x.IsDeleted == 0 && x.QiyeType == "工业企业").Count();           //工业企业
                data.Add(new { yy, zz, qt });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 景点实时游客信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult jingdianssyouke()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = from d in _dbContext.Tourist
                            where d.IsDeleted == 0
                            group d by new
                            {
                                d.ScenicName
                            } into gp
                            select new
                            {
                                gp.Key,
                                count = gp.Count()
                            };
                response.SetData(query.ToList());
                return Ok(response);
            }
        }

        /// <summary>
        /// 旅游收入分析
        /// </summary>
        [HttpGet]
        public IActionResult lvyoushouru()
        {
            using (_dbContext)
            {
                var d5 = DateTime.Now.ToString("yyyy");
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                double sqs = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "1").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));          //一月
                float sq = ss((float)sqs);
                double sws = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "2").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));          //二月
                float sw = ss((float)sws);
                double ses = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "3").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));           //三月
                float se = ss((float)ses);
                double srs = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "4").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));           //四月
                float sr = ss((float)srs);
                double sts = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "5").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));           //五月
                float st = ss((float)sts);
                double sys = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "6").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));           //六月
                float sy = ss((float)sys);
                double sus = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "7").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));           //七月
                float su = ss((float)sus);
                double sis = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "8").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));           //八月
                float si = ss((float)sis);
                double sos = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "9").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));           //九月
                float so = ss((float)sos);
                double sps = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "10").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));          //十月
                float sp = ss((float)sps);
                double sds = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "11").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));          //十一月
                float sd = ss((float)sds);
                double zfs = _dbContext.Tourist.Where(x => x.IsDeleted != 1 && x.TjYear == d5 && x.TjMouth == "12").Select(x => new { x.TouristMoney }).Sum(x => Math.Round((double)x.TouristMoney * 0.0001, 2));          //十二月
                float zf = ss((float)zfs);
                data.Add(new { sq, sw, se, sr, st, sy, su, si, so, sp, sd, zf });
                response.SetData(data);
                return Ok(response);
            }
        }
        public static float ss(float xx)
        {
            int i = (int)(xx * 100);
            xx = (float)(i * 1.0) / 100;
            return xx;
        }


        /// <summary>
        /// 大气防治
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult daqifangzhi()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                List<dynamic> data1 = new List<dynamic>();
                string time = "";
                string times = "";
                double datas = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (i == 0)
                    {
                        time = DateTime.Now.ToString("dd") + "号";
                        times = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        time = DateTime.Now.AddDays(-i).ToString("dd") + "号";
                        times = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd");
                    }
                    var ss = _dbContext.Barometric.FirstOrDefault(x => x.IsDeleted == 0 && x.BarTime == times);
                    if (ss == null)
                    {
                        datas = 0;
                    }
                    else
                    {
                        var dd = ss.NowShuzhi;
                        datas = Convert.ToDouble(dd);
                    }
                    data.Add(datas);
                    data1.Add(time);
                }
                //data.Add(data);
                response.SetData(new { data, data1 });
                return Ok(response);
            }
        }

        /// <summary>
        /// 游客男女比例
        /// </summary>
        [HttpGet]
        public IActionResult zufanginfo()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.RentoutRoom.Where(x => x.IsDeleted == 0 && x.RentoutStaues == "已出租").Count();           //已出租
                int bb = _dbContext.RentoutRoom.Where(x => x.IsDeleted == 0 && x.RentoutStaues != "已出租").Count();           //未出租
                data.Add(new { aa, bb, });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 社会矫正
        /// </summary>
        [HttpGet]
        public IActionResult shehuijiaoz()
        {
            using (_dbContext)
            {
                string d1 = DateTime.Now.ToString("yyyy") + "-01";
                string d2 = DateTime.Now.ToString("yyyy") + "-02";
                string d3 = DateTime.Now.ToString("yyyy") + "-03";
                string d4 = DateTime.Now.ToString("yyyy") + "-04";
                string d5 = DateTime.Now.ToString("yyyy") + "-05";
                string d6 = DateTime.Now.ToString("yyyy") + "-06";
                string d7 = DateTime.Now.ToString("yyyy") + "-07";
                string d8 = DateTime.Now.ToString("yyyy") + "-08";
                string d9 = DateTime.Now.ToString("yyyy") + "-09";
                string d10 = DateTime.Now.ToString("yyyy") + "-10";
                string d11 = DateTime.Now.ToString("yyyy") + "-11";
                string d12 = DateTime.Now.ToString("yyyy") + "-12";
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int j1 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d1).Count();
                int j2 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d2).Count();
                int j3 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d3).Count();
                int j4 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d4).Count();
                int j5 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d5).Count();
                int j6 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d6).Count();
                int j7 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d7).Count();
                int j8 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d8).Count();
                int j9 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d9).Count();
                int j10 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d10).Count();
                int j11 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d11).Count();
                int j12 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && x.JiesuTime.Substring(0, 7) == d12).Count();
                data.Add(new { j1, j2, j3, j4, j5, j6, j7, j8, j9, j10, j11, j12 });
                response.SetData(data);
                return Ok(response);
            }
        }


        /// <summary>
        /// 城管执法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult chengguanzhifa()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                List<dynamic> data1 = new List<dynamic>();
                List<dynamic> data2 = new List<dynamic>();
                string time = "";
                string times = "";
                for (int i = 0; i < 7; i++)
                {
                    if (i == 0)
                    {
                        time = DateTime.Now.ToString("dd") + "号";
                        times = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        time = DateTime.Now.AddDays(-i).ToString("dd") + "号";
                        times = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd");
                    }
                    int ss = _dbContext.CityManagement.Where(x => x.IsDeleted == 0 && x.ZhifaTime.Substring(0, 10) == times && x.ChuliQingk == "已解决").Count();
                    int ww = _dbContext.CityManagement.Where(x => x.IsDeleted == 0 && x.ZhifaTime.Substring(0, 10) == times && x.ChuliQingk != "已解决").Count();
                    data.Add(time);
                    data1.Add(ss);
                    data2.Add(ww);
                }
                //data.Add(data);
                response.SetData(new { data, data1, data2 });
                return Ok(response);
            }
        }

        /// <summary>
        /// 交办数据
        /// </summary>
        [HttpGet]
        public IActionResult renwujiaoban()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Mission.Where(x => x.Recycle == "0" && x.Accomplish == "1").Count();         //已完成
                int bb = _dbContext.Mission.Where(x => x.Recycle == "0" && x.Accomplish != "1").Count();       //未完成
                data.Add(new { aa, bb, });
                response.SetData(data);
                return Ok(response);
            }
        }
        /// <summary>
        /// 交办数据统计
        /// </summary>
        [HttpGet]
        public IActionResult renwutongji()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Mission.Where(x => x.Recycle == "0" && x.AuditStatus == "0").Count();         //办理中
                int bb = _dbContext.Mission.Where(x => x.Recycle == "0" && x.AuditStatus == "1").Count();       //审核中
                int cc = _dbContext.Mission.Where(x => x.Recycle == "0" && x.AuditStatus == "2").Count();       //已完成
                data.Add(new { aa, bb, cc, });
                response.SetData(data);
                return Ok(response);
            }
        }
        /// <summary>
        /// 重点人员关系分析图片
        /// </summary>
        [HttpGet]
        public IActionResult zhongdianreny()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.Relationships.FirstOrDefault(x => x.Addpeople != "");
                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 党建-妇联
        /// </summary>
        [HttpGet]
        public IActionResult djFulian()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.WomenFederation.Where(x => x.IsDeleted == 0).Count();         //妇联组织总数
                int bb = _dbContext.WomenHouse.Where(x => x.IsDeleted == 0).Count();       //妇女之家总数
                int cc = _dbContext.WomenActivities.Where(x => x.IsDeleted == 0).Count();       //妇联活动次数
                data.Add(new { aa, bb, cc, });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 党建-工会
        /// </summary>
        [HttpGet]
        public IActionResult djGonghui()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.Organization.Where(x => x.IsDeleted == 0).Count();         //工会会员单位总数
                int bb = _dbContext.TheUnionZhengc.Where(x => x.IsDeleted == 0).Count();       //工会惠普政策总数
                int cc = _dbContext.TheUnion.Where(x => x.IsDeleted == 0).Count();       //工会活动次数
                data.Add(new { aa, bb, cc, });
                response.SetData(data);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult gethuiyuan()
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.Organization
                            where c.IsDeleted == 0
                            select new
                            {
                                OrganizationName = c.OrganizationName == null ? "" : c.OrganizationName != null ? c.OrganizationName : "",
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult gethpzc()
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.TheUnionZhengc
                            where c.IsDeleted == 0
                            select new
                            {
                                TheUnionZhengcName = c.TheUnionZhengcName == null ? "" : c.TheUnionZhengcName != null ? c.TheUnionZhengcName : "",
                                Neirong = c.Neirong == null ? "" : c.Neirong != null ? c.Neirong : "",

                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }

        /// <summary>
        /// 党建-党员信息
        /// </summary>
        [HttpGet]
        public IActionResult djdangyuanInfo()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.Politics == "正式党员").Count();         //正式党员
                int bb = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.Politics == "预备党员").Count();       //预备党员
                int cc = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.Politics == "入党积极分子").Count();       //积极分子
                int dd = _dbContext.CpcmanInfo.Where(x => x.IsDeleted == 0 && x.Politics == "入党申请人").Count();       //发展对象
                data.Add(new { aa, bb, cc, dd });
                response.SetData(data);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult getdjdangyuanInfo(string name)
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.CpcmanInfo
                            where c.IsDeleted == 0 && c.Politics.Contains(name)
                            select new
                            {
                                RealName = c.RealName == null ? "" : c.RealName != null ? c.RealName : "",
                                Sex = c.Sex == null ? "" : c.Sex != null ? c.Sex : "",
                                Birth = c.Birth == null ? "" : c.Birth != null ? c.Birth.Value.ToString("yyyy-MM-dd") : "",
                                Education = c.Education == null ? "" : c.Education != null ? c.Education : "",
                                Politics = c.Politics == null ? "" : c.Politics != null ? c.Politics : "",
                                DangOrganizationName = c.DangOrganizationName == null ? "" : c.DangOrganizationName != null ? c.DangOrganizationName : "",
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult gettydzkuInfo(string undzkid)
        {
            using (_dbContext)
            {
                var querys = _dbContext.RegionInfos.FirstOrDefault(x => x.RegionId == (Convert.ToInt32(undzkid) + 1));
                var query = from c in _dbContext.UnifiedAddressLibrary
                            where ("," + querys.UnifiedAddressLibraryId + ",").Contains("," + c.Id + ",")
                            select new
                            {
                                UnifiedAddressLibraryId = c.Id,
                                Sourceaddress = c.Sourceaddress,
                                issfysj = _dbContext.UnifiedAddressLibraryUserInfo.FirstOrDefault(x => x.UnifiedAddressLibraryId == c.Id) == null ? "0" : "1"
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult gettydzkuuserInfo(string untydzkid)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var querys = _dbContext.UnifiedAddressLibraryUserInfo.FirstOrDefault(x => x.UnifiedAddressLibraryId == Convert.ToInt32(untydzkid));
                if (querys != null)
                {
                    var query = from c in _dbContext.Userinfoty
                                where ("," + querys.UserIdlist + ",").Contains("," + c.Id + ",")
                                select new
                                {
                                    userid = c.Id,
                                    username = c.RealName,
                                    sex = c.Sex,
                                    idcard = c.IdentityCard
                                };
                    var query1 = query.ToList();                    
                    response.SetData(query1);
                }
                return Ok(response);
            }
        }

        /// <summary>
        /// 党建-党组织架构
        /// </summary>
        [HttpGet]
        public IActionResult getdangzuzhi(string name)
        {
            using (_dbContext)
            {
                var query = from c in _dbContext.DangOrganization
                            where c.IsDeleted == 0 && c.DangOrganizationName.Contains(name)
                            select new
                            {
                                dangOrganizationName = c.DangOrganizationName == null ? "" : c.DangOrganizationName != null ? c.DangOrganizationName : "",
                                dangOrganizationRemarks = c.DangOrganizationRemarks == null ? "" : c.DangOrganizationRemarks != null ? c.DangOrganizationRemarks : "",
                                dangClerk = c.DangClerk == null ? "" : c.DangClerk != null ? c.DangClerk : "",
                            };
                var query1 = query.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query1);
                return Ok(response);
            }
        }


        //重点人员
        [HttpGet]
        public IActionResult GetPersonnel()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                //刑满释放人员
                var xmsfUser = 0;
                //社区矫正人员
                var sqjzUser = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1).Count();
                //精神病人员
                var jsbUser = _dbContext.Mentally.Where(x => x.IsDeleted != 1).Count();
                //吸毒人员
                var xdUser = _dbContext.Drug.Where(x => x.IsDeleted != 1).Count();
                //重点青少年
                var zdqsnUser = _dbContext.KeyYouthList.Where(x => x.IsDeleted != 1).Count();
                //重点上访人员
                var zdsfUser = _dbContext.ListOfKeyPetitioners.Where(x => x.IsDeleted != 1).Count();
                //危险品从业人员
                var wxpcyUser = _dbContext.DangerousGoods.Where(x => x.IsDeleted != 1).Count();
                //其他人员
                var qtUser = 0;
                //传销人员
                var cxUser = 0;
                //特定人员
                var tdUser = 0;
                //疫情防控人员
                var yqfkUser = _dbContext.Yqfkryb.Where(x => x.IsDeleted != 1).Count();
                response.SetData(new { xmsfUser, sqjzUser, jsbUser, xdUser, zdqsnUser, zdsfUser, wxpcyUser, qtUser, cxUser, tdUser, yqfkUser });
                return Ok(response);
            }
        }

        /// <summary>
        /// 党建-党组织架构
        /// </summary>
        [HttpGet]
        public IActionResult djdangzuzhi()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int aa = _dbContext.DangOrganization.Where(x => x.IsDeleted == 0 && x.DangType == "乡镇").Count();         //乡镇
                int bb = _dbContext.DangOrganization.Where(x => x.IsDeleted == 0 && x.DangType == "行政村").Count();       //行政村
                int cc = _dbContext.DangOrganization.Where(x => x.IsDeleted == 0 && x.DangType == "机关").Count();       //机关
                int dd = _dbContext.DangOrganization.Where(x => x.IsDeleted == 0 && x.DangType == "两新").Count();       //两新
                int ee = _dbContext.DangOrganization.Where(x => x.IsDeleted == 0 && x.DangType == "国企").Count();       //国企
                data.Add(new { aa, bb, cc, dd, ee });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 水位监测统计图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult WaterLevelMonitoring()
        {
            using (_dbContext)
            {
                var dtS = DateTime.Now.ToString("yyyy-MM-dd");
                var query = from c in _dbContext.HeDaowater
                            where c.IsDeleted == 0 && c.HeDaowaterTime.Substring(0,10)==dtS && c.HeDaowaterAddress.Substring(0,10)=="0000000001"
                            orderby c.HeDaowaterTime
                            select new
                            {
                                HeDaowaterTime = c.HeDaowaterTime.Substring(11,5),
                                HeDaoHeDaowaterSw = c.HeDaoHeDaowaterSw ,
                                CurrentPrecipitation = c.CurrentPrecipitation,
                            };
                var query2 = from c in _dbContext.HeDaowater
                            where c.IsDeleted == 0 && c.HeDaowaterTime.Substring(0, 10) == dtS && c.HeDaowaterAddress.Substring(0, 10) == "0000000002"
                            orderby c.HeDaowaterTime
                            select new
                            {
                                HeDaowaterTime = c.HeDaowaterTime.Substring(11, 5),
                                HeDaoHeDaowaterSw = c.HeDaoHeDaowaterSw,
                                CurrentPrecipitation = c.CurrentPrecipitation,
                            };
                var list = query.ToList();
                var list2 = query2.ToList();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(new { list,list2});
                return Ok(response);
            }
        }

        /// <summary>
        /// 党建-党组织活动
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Activitylist()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                string time = DateTime.Now.ToString("yyyy");
                var query = _dbContext.DangActivity.Where(x => x.IsDeleted == 0 && x.ReleaseTime.Contains(time));
                int aa = query.Where(x => Convert.ToInt32(x.ReleaseTime.Substring(5, 2)) >= 1 && Convert.ToInt32(x.ReleaseTime.Substring(5, 2)) <= 3).Count();      //第一季度
                int bb = query.Where(x => Convert.ToInt32(x.ReleaseTime.Substring(5, 2)) >= 4 && Convert.ToInt32(x.ReleaseTime.Substring(5, 2)) <= 6).Count();      //第二季度
                int cc = query.Where(x => Convert.ToInt32(x.ReleaseTime.Substring(5, 2)) >= 7 && Convert.ToInt32(x.ReleaseTime.Substring(5, 2)) <= 9).Count();      //第三季度
                int dd = query.Where(x => Convert.ToInt32(x.ReleaseTime.Substring(5, 2)) >= 10 && Convert.ToInt32(x.ReleaseTime.Substring(5, 2)) <= 12).Count();    //第四季度
                data.Add(new { aa, bb, cc, dd });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 三维地图-雪亮工程-地图描点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult getThreemiaodian()
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.XlProject
                            where p.IsDeleted == 0 && p.Lon != null && p.Lon != "" && p.Lat != null && p.Lat != ""
                            select new Data
                            {
                                Id = p.XlProjectUuid,
                                Indexcode = p.XlShebeiId,
                                ShebeiAddress = p.ShebeiAddress,
                                Center = new Center()
                                {
                                    X = p.Lon == null ? 0 : p.Lon != null ? double.Parse(p.Lon) : 0,
                                    Y = p.Lat == null ? 0 : p.Lat != null ? double.Parse(p.Lat) : 0,
                                    Z = 100,
                                    H = 0,
                                    P = 0,
                                    R = 0,
                                },
                                Options = new Options()
                                {
                                    Center = new Center()
                                    {
                                        X = p.Lon == null ? 0 : p.Lon != null ? double.Parse(p.Lon) : 0,
                                        Y = p.Lat == null ? 0 : p.Lat != null ? double.Parse(p.Lat) : 0,
                                        Z = 100,
                                        H = 0,
                                        P = 0,
                                        R = 0,
                                    },
                                    Position = new Position()
                                    {
                                        X = p.Lon == null ? 0 : p.Lon != null ? double.Parse(p.Lon) : 0,
                                        Y = p.Lat == null ? 0 : p.Lat != null ? double.Parse(p.Lat) : 0,
                                        Z = 100,
                                        Heading = 0,
                                        Pitch = 0,
                                        Roll = 0,
                                    },
                                }
                            };
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query.ToList());
                return Ok(response);
            }
        }

        /// <summary>
        /// 社会矫正
        /// </summary>
        [HttpGet]
        public IActionResult shehuijiaoz2()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<dynamic> data = new List<dynamic>();
                int num16 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && Convert.ToInt32(x.KaishiTime.Substring(0, 4)) <= 2016 && Convert.ToInt32(x.JiesuTime.Substring(0, 4)) >= 2016).Count();
                int num17 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && Convert.ToInt32(x.KaishiTime.Substring(0, 4)) <= 2017 && Convert.ToInt32(x.JiesuTime.Substring(0, 4)) >= 2017).Count();
                int num18 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && Convert.ToInt32(x.KaishiTime.Substring(0, 4)) <= 2018 && Convert.ToInt32(x.JiesuTime.Substring(0, 4)) >= 2018).Count();
                int num19 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && Convert.ToInt32(x.KaishiTime.Substring(0, 4)) <= 2019 && Convert.ToInt32(x.JiesuTime.Substring(0, 4)) >= 2019).Count();
                int num20 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && Convert.ToInt32(x.KaishiTime.Substring(0, 4)) <= 2020 && Convert.ToInt32(x.JiesuTime.Substring(0, 4)) >= 2020).Count();
                int num21 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && Convert.ToInt32(x.KaishiTime.Substring(0, 4)) <= 2021 && Convert.ToInt32(x.JiesuTime.Substring(0, 4)) >= 2021).Count();
                int num22 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && Convert.ToInt32(x.KaishiTime.Substring(0, 4)) <= 2022 && Convert.ToInt32(x.JiesuTime.Substring(0, 4)) >= 2022).Count();
                int num23 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && Convert.ToInt32(x.KaishiTime.Substring(0, 4)) <= 2023 && Convert.ToInt32(x.JiesuTime.Substring(0, 4)) >= 2023).Count();
                int num24 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && Convert.ToInt32(x.KaishiTime.Substring(0, 4)) <= 2024 && Convert.ToInt32(x.JiesuTime.Substring(0, 4)) >= 2024).Count();
                int num25 = _dbContext.RectifyInfo.Where(x => x.IsDeleted != 1 && Convert.ToInt32(x.KaishiTime.Substring(0, 4)) <= 2025 && Convert.ToInt32(x.JiesuTime.Substring(0, 4)) >= 2025).Count();
                data.Add(new { num16, num17, num18, num19, num20, num21, num22, num23, num24, num25 });
                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 企业信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QiyeInfo()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.Qiye.Where(x => x.IsDeleted != 1);
                response.SetData(query.ToList());
                return Ok(response);
            }
        }

        /// <summary>
        /// 企业详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QiyeParticulars(string uuid)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.Qiye.FirstOrDefault(x => x.IsDeleted != 1 && x.QiyeUuid == Guid.Parse(uuid));

                response.SetData(query);
                return Ok(response);
            }
        }




        public class Data
        {
            public Center Center { get; set; }
            public Options Options { get; set; }
            public Guid Id { get; internal set; }
            public string Indexcode { get; internal set; }
            public string ShebeiAddress { get; set; }
        }
        public class Center
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public double H { get; set; }
            public double P { get; set; }
            public double R { get; set; }
        }
        public class Options
        {
            public Center Center { get; set; }
            public Position Position { get; set; }
        }
        public class Position
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public double Heading { get; set; }
            public double Pitch { get; set; }
            public double Roll { get; set; }

        }

    }
}

