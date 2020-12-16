using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaikanSmartTownCockpit.Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.FireFighting
{
    [Route("api/v1/FireFighting/[controller]/[action]")]
    [ApiController]
    public class XiaoFangDataController : ControllerBase
    {
        private readonly haikanHeQiaoContext _dbContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "dbContext" ></ param >
        /// < param name="mapper"></param>
        public XiaoFangDataController(haikanHeQiaoContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet]
        public string List()
        {
            var list = (from a in _dbContext.Alarmdata
                        select new
                        {
                            EventTypeId = GetSj(a.EventTypeId),
                            EventStatus = GetSjStust(a.EventStatus),
                            AlarmTypeId = GetBj(a.AlarmTypeId),
                            name = _dbContext.XfBuilding.Where(x => x.BelongOrgId == a.OrgId).FirstOrDefault().BuildingName,
                            time=Convert.ToDateTime(a.StartTime),
                            a.StartTime,
                            a.EndTime,
                            a.Remarks,
                            chuli = GetChuli(a.IsHandled),
                            fuwei = GetFuwei(a.ResetStatus)
                        }).ToList();
            list = list.OrderByDescending(x => x.time).ToList();
            string result = JsonConvert.SerializeObject(list);
            return result;
        }
        
        public static string GetSj(string id)
        {
            if (id=="1")
            {
                return "火灾报警";
            }
            if (id == "2")
            {
                return "火灾误报";
            }
            if (id == "3")
            {
                return "测试";
            }
            if (id == "4")
            {
                return "演习";
            }
            if (id == "5")
            {
                return "其他";
            }
            return "";
        }
        public static string GetSjStust(string id)
        {
            if (id == "1")
            {
                return "事件上报";
            }
            if (id == "2")
            {
                return "事件确认";
            }
            if (id == "3")
            {
                return "事件处理";
            }
            if (id == "4")
            {
                return "事件归档";
            }
            if (id == "5")
            {
                return "其他";
            }
            return "";
        }
        public static string GetBj(string id)
        {
            if (id == "-1")
            {
                return "告警";
            }
            if (id == "1")
            {
                return "火警";
            }
            if (id == "2")
            {
                return "电流过高";
            }
            if (id == "3")
            {
                return "电流过低";
            }
            if (id == "4")
            {
                return "阀门被打开";
            }
            if (id == "5")
            {
                return "倾倒";
            }
            if (id == "6")
            {
                return "温度过高";
            }
            if (id == "7")
            {
                return "温度过低";
            }
            if (id == "8")
            {
                return "电压过高";
            }
            if (id == "9")
            {
                return "电压过低";
            }
            if (id == "10")
            {
                return "视频火警";
            }
            if (id == "11")
            {
                return "物品遗留";
            }
            if (id == "12")
            {
                return "烟雾报警";
            }
            if (id == "13")
            {
                return "区域入侵";

            }
            if (id == "14")
            {
                return "视频丢失";
            }
            if (id == "15")
            {
                return "水压过高";
            }
            if (id == "16")
            {
                return "水压过低";
            }
            if (id == "17")
            {
                return "正常";
            }
            if (id == "18")
            {
                return "液位过高";
            }
            if (id == "19")
            {
                return "液位过低";
            }
            if (id == "20")
            {
                return "燃气";
            }
            if (id == "21")
            {
                return "启动";
            }
            if (id == "22")
            {
                return "反馈";
            }
            if (id == "23")
            {
                return "手动";
            }
            if (id == "303")
            {
                return "区域入侵";
            }
            if (id == "305")
            {
                return "物品遗留";
            }
            if (id == "312")
            {
                return "视频火警";
            }
            if (id == "313")
            {
                return "烟雾报警";
            }
            if (id == "577")
            {
                return "离岗监测";
            }
            if (id == "1111")
            {
                return "IPC断网";
            }
            if (id == "2001")
            {
                return "智能充电桩防盗报警";
            }
            if (id == "2002")
            {
                return "智能充电桩烟雾报警";
            }
            if (id == "2003")
            {
                return "智能充电桩充电线被拔报警";
            }
            if (id == "2004")
            {
                return "智能充电桩桩点故障";
            }
            if (id == "2005")
            {
                return "智能充电桩充电桩故障";
            }
            if (id == "2006")
            {
                return "智能充电桩功率过载";
            }
            if (id == "2007")
            {
                return "智能充电桩温度报警";
            }
            if (id == "2008")
            {
                return "智能充电桩离线卡异常";
            }
            if (id == "2009")
            {
                return "智能充电桩充电订单退款";
            }
            if (id == "2010")
            {
                return "智能充电桩充电器故障";
            }
            if (id == "2011")
            {
                return "智能充电桩电池故障";
            }
            if (id == "2012")
            {
                return "电路常开";
            }
            if (id == "2013")
            {
                return "电流不稳";
            }
            if (id == "2014")
            {
                return "充电口离线";
            }
            if (id == "2015")
            {
                return "充电口烧坏";
            }
            if (id == "2016")
            {
                return "过流";
            }
            if (id == "2017")
            {
                return "浮充";
            }

            return "";
        }
        public static string GetChuli(string id)
        {
            if (id=="0")
            {
                return "否";
            }
            if (id == "1")
            {
                return "是";
            }
            return "";
        }
        public static string GetFuwei(string id)
        {
            if (id == "0")
            {
                return "否";
            }
            if (id == "1")
            {
                return "是";
            }
            return "";
        }
    }
}
