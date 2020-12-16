using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.Extensions;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using HaikanSmartTownCockpit.Api.RequestPayload.Intelligenttravel;
using HaikanSmartTownCockpit.Api.ViewModels.Tourist;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Intelligenttravel
{
    [Route("api/v1/intelligenttravel/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class TouristController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public TouristController(haikanHeQiaoContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        #region 后台管理列表
        /// <summary>
        /// 显示所有信息
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(TouristRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var scentc = _dbContext.Scenic.Where(x => x.IsDeleted == 0).Select(x => new { x.ScenicUuid, x.ScenicName });
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    scentc = scentc.Where(x => x.ScenicName.Contains(payload.Kw.Trim()));
                }
                var scentcList = scentc.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = scentc.Count();

                var suuid = scentcList.Select(x => x.ScenicUuid).ToList();

                if (payload.SType == 1)
                {
                    List<TouristYearData> touristYearDatas = new List<TouristYearData>();

                    var listTOY = _dbContext.TouristOfYear.Where(x => suuid.Contains(x.ScenicUuid.Value)).OrderByDescending(x => x.Year).ToList();
                    var maxYear = _dbContext.TouristOfYear.Max(x => x.Year);
                    if (maxYear == null)
                    {
                        maxYear = DateTime.Now.Year;
                    }
                    for (int i = 0; i < scentcList.Count; i++)
                    {
                        var toys = listTOY.FindAll(x => x.ScenicUuid == scentcList[i].ScenicUuid).Take(10).ToList();
                        TouristYearData touristYearData = new TouristYearData
                        {
                            ScenicUuid = scentcList[i].ScenicUuid,
                            ScenicName = scentcList[i].ScenicName,
                            First = ReturnYearData(toys, 0),
                            Second = ReturnYearData(toys, 1),
                            Third = ReturnYearData(toys, 2),
                            Fourth = ReturnYearData(toys, 3),
                            Fifth = ReturnYearData(toys, 4),
                            Sixth = ReturnYearData(toys, 5),
                            Seventh = ReturnYearData(toys, 6),
                            Eighth = ReturnYearData(toys, 7),
                            Ninth = ReturnYearData(toys, 8),
                            Tenth = ReturnYearData(toys, 9),
                            Sum = toys.Sum(x => (long)x.Num.Value),
                        };

                        touristYearDatas.Add(touristYearData);
                    }
                    response.SetData(new { maxYear, touristYearDatas, totalCount });
                    return Ok(response);




                }
                else if (payload.SType == 2)
                {
                    List<TouristMonthData> touristMonthDatas = new List<TouristMonthData>();
                    var listTOM = _dbContext.TouristOfMonth.Where(x => x.Year.Value == payload.year && suuid.Contains(x.ScenicUuid.Value)).OrderBy(x => x.Month).ToList();

                    for (int i = 0; i < scentcList.Count; i++)
                    {
                        var toms = listTOM.FindAll(x => x.ScenicUuid == scentcList[i].ScenicUuid);
                        TouristMonthData touristMonthData = new TouristMonthData()
                        {
                            ScenicUuid = scentcList[i].ScenicUuid,
                            ScenicName = scentcList[i].ScenicName,
                            
                            Jan = ReturnMonthData(toms, 1),
                            Feb = ReturnMonthData(toms, 2),
                            Mar = ReturnMonthData(toms, 3),
                            Apr = ReturnMonthData(toms, 4),
                            May = ReturnMonthData(toms, 5),
                            Jun = ReturnMonthData(toms, 6),
                            Jul = ReturnMonthData(toms, 7),
                            Aug = ReturnMonthData(toms, 8),
                            Sep = ReturnMonthData(toms, 9),
                            Oct = ReturnMonthData(toms, 10),
                            Nov = ReturnMonthData(toms, 11),
                            Dec = ReturnMonthData(toms, 12),
                            Sum = toms.Sum(x=>(long)x.Num),
                        };
                        touristMonthDatas.Add(touristMonthData);

                    }
                    response.SetData(new { touristMonthDatas, totalCount });
                    return Ok(response);
                }
                else if (payload.SType == 3)
                {
                    GregorianCalendar gc = new GregorianCalendar();
                    //本月开始时间
                    DateTime sDate = new DateTime(payload.year, payload.Month, 1);
                    //本月结束时间
                    DateTime eDate = sDate.AddMonths(1).AddDays(-1);
                    //开始时间所在周数
                    int sWeekOfYear = gc.GetWeekOfYear(sDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                    //结束时间所在周数
                    int eWeekOfYear = gc.GetWeekOfYear(eDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                    //获取开始时间的本周周一时间
                    DateTime startWeek = DateTime.Now;
                    if (sDate.DayOfWeek == 0)
                    {
                        startWeek = sDate.AddDays(-6);
                    }
                    else
                    {
                        startWeek = sDate.AddDays(1-(int)sDate.DayOfWeek);
                    }

                    //通过周一时间获取开始周数到结束周数的各个周区间,作为显示列名
                    List<string> columns = new List<string>();
                    for (int i= 0; i <= eWeekOfYear-sWeekOfYear; i++)
                    {
                        var s= startWeek.AddDays(i * 7);
                        var e = s.AddDays(6);
                        columns.Add(s.ToString("MM/dd") + "-" + e.ToString("MM/dd"));
                    }

                    List<TouristWeekData> touristWeekDatas = new List<TouristWeekData>();
                    var listTOW = _dbContext.TouristOfWeek.Where(x => x.Year.Value == payload.year &&x.Weekend>=sWeekOfYear&&x.Weekend<=eWeekOfYear&& suuid.Contains(x.ScenicUuid.Value)).OrderBy(x => x.Weekend).ToList();
                    for(int i = 0; i < scentcList.Count; i++)
                    {
                        var tows = listTOW.FindAll(x => x.ScenicUuid == scentcList[i].ScenicUuid);
                        TouristWeekData touristWeekData = new TouristWeekData()
                        {
                            ScenicUuid = scentcList[i].ScenicUuid,
                            ScenicName = scentcList[i].ScenicName,
                            First= ReturnWeekData(tows, sWeekOfYear),
                            Second = ReturnWeekData(tows, sWeekOfYear+1),
                            Third = ReturnWeekData(tows, sWeekOfYear+2),
                            Fourth = ReturnWeekData(tows, sWeekOfYear+3),
                            Sum=tows.Sum(x=>(long)x.Num),
                        };
                        if (eWeekOfYear - sWeekOfYear >= 4)
                        {
                            touristWeekData.Fifth = ReturnWeekData(tows, sWeekOfYear + 4);
                        }
                        else
                        {
                            touristWeekData.Fifth = null;
                        }
                        if (eWeekOfYear - sWeekOfYear == 5)
                        {
                            touristWeekData.Sixth = ReturnWeekData(tows, sWeekOfYear + 5);
                        }
                        else
                        {
                            touristWeekData.Sixth = null;
                        }

                        touristWeekDatas.Add(touristWeekData);

                    }

                    response.SetData(new { eWeekOfYear,sWeekOfYear, touristWeekDatas, columns, totalCount });

                    return Ok(response);
                }
                else if (payload.SType == 4)
                {
                    int days=DateTime.DaysInMonth(payload.year, payload.Month);
                    List<TouristDayData> touristDayDatas = new List<TouristDayData>();
                    var listTOD = _dbContext.TouristOfDay.Where(x => x.Year.Value == payload.year &&x.Month==payload.Month&& suuid.Contains(x.ScenicUuid.Value)).OrderBy(x => x.Day).ToList();

                    for(int i = 0; i < scentcList.Count; i++)
                    {
                        var tods = listTOD.FindAll(x => x.ScenicUuid == scentcList[i].ScenicUuid);
                        TouristDayData touristDayData = new TouristDayData()
                        {
                            ScenicUuid = scentcList[i].ScenicUuid,
                            ScenicName = scentcList[i].ScenicName,
                            First= ReturnDayData(tods,1),
                            Second = ReturnDayData(tods, 2),
                            Third = ReturnDayData(tods, 3),
                            Fourth = ReturnDayData(tods, 4),
                            Fifth = ReturnDayData(tods, 5),
                            Sixth = ReturnDayData(tods, 6),
                            Seventh = ReturnDayData(tods, 7),
                            Eighth = ReturnDayData(tods, 8),
                            Ninth = ReturnDayData(tods, 9),
                            Tenth = ReturnDayData(tods, 10),
                            Eleventh = ReturnDayData(tods, 11),
                            Twelfth = ReturnDayData(tods, 12),
                            Thirteenth = ReturnDayData(tods, 13),
                            Fourteenth = ReturnDayData(tods, 14),
                            Fifteenth = ReturnDayData(tods, 15),
                            Sixteenth = ReturnDayData(tods, 16),
                            Seventeenth = ReturnDayData(tods, 17),
                            Eighteenth = ReturnDayData(tods, 18),
                            Nineteenth = ReturnDayData(tods, 19),
                            Twentieth = ReturnDayData(tods, 20),
                            Twenty_first = ReturnDayData(tods, 21),
                            Twenty_second = ReturnDayData(tods, 22),
                            Twenty_third = ReturnDayData(tods, 23),
                            Twenty_fourth = ReturnDayData(tods, 24),
                            Twenty_fifth = ReturnDayData(tods, 25),
                            Twenty_sixth = ReturnDayData(tods, 26),
                            Twenty_seventh = ReturnDayData(tods, 27),
                            Twenty_eighth = ReturnDayData(tods, 28),
                            Twenty_ninth = ReturnDayData(tods, 29),
                            Thirtieth = ReturnDayData(tods, 30),
                            Thirty_first = ReturnDayData(tods, 31),
                            Sum = listTOD.Sum(x => (long)x.Num),
                        };
                        touristDayDatas.Add(touristDayData);

                    }
                    response.SetData(new { days, touristDayDatas, totalCount });


                    return Ok(response);
                }
                else
                {
                    return Ok(null);
                }

                    //string year = DateTime.Now.Year.ToString();

                    //if (!string.IsNullOrEmpty(payload.year))
                    //    year = payload.year;
                    //var query = from p in _dbContext.Tourist
                    //            where p.TouristTime.Contains(year)
                    //            group p by new
                    //            {
                    //                p.ScenicName
                    //            }
                    //            into g
                    //            select new
                    //            {
                    //                ScenicName = g.Key.ScenicName,
                    //                Count = g.Count(),
                    //            };
                    //var query2 = from p in query
                    //             select new
                    //             {
                    //                 ScenicName = p.ScenicName,
                    //                 Count = p.Count,
                    //                 Jan = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-01")),
                    //                 Feb = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-02")),
                    //                 Mar = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-03")),
                    //                 Apr = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-04")),
                    //                 May = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-05")),
                    //                 Jun = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-06")),
                    //                 Jul = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-07")),
                    //                 Aug = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-08")),
                    //                 Sep = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-09")),
                    //                 Oct = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-10")),
                    //                 Nov = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-11")),
                    //                 Dec = _dbContext.Tourist.Count(x => x.ScenicName == p.ScenicName && x.TouristTime.Contains(year + "-12")),
                    //             };


                    //if (!string.IsNullOrEmpty(payload.Kw))
                    //{
                    //    query2 = query2.Where(x => x.ScenicName.Contains(payload.Kw.Trim()));
                    //}


                    //var list = query2.Paged(payload.CurrentPage, payload.PageSize).ToList();

                    //totalCount = query2.Count();

                    //response.SetData(list, totalCount);
                    //return Ok(response);
                
            }
        }


        private static int ReturnYearData(List<TouristOfYear> ls, int index)
        {
            if (index >= ls.Count)
            {
                return 0;
            }
            else
            {
                return ls[index].Num.Value;
            }
        }


        private static int ReturnMonthData(List<TouristOfMonth> ls, int month)
        {
            var data = ls.Find(x => x.Month == month);
            if (data != null && data.Num != null)
            {
                return data.Num.Value;
            }
            else
            {
                return 0;
            }
        }


        private static int ReturnWeekData(List<TouristOfWeek> ls, int week)
        {
            var data = ls.Find(x => x.Weekend == week);
            if (data != null && data.Num != null)
            {
                return data.Num.Value;
            }
            else
            {
                return 0;
            }
        }

        private static int ReturnDayData(List<TouristOfDay> ls, int day)
        {
            var data = ls.Find(x => x.Day == day);
            if (data != null && data.Num != null)
            {
                return data.Num.Value;
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
