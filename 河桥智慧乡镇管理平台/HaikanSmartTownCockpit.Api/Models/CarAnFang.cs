using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.Models
{
    public class CarAnFang
    {
        /// <summary>
        /// 
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Params3 @params { get; set; }
}
    public class PicUrl
    {
        /// <summary>
        /// 
        /// </summary>
        public string platePicUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vehiclePicUrl { get; set; }
    }

    public class Person
    {
        /// <summary>
        /// 张三
        /// </summary>
        public string personName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phoneNo { get; set; }
    }

    public class Data3
    {
        /// <summary>
        /// 
        /// </summary>
        public string eventIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string plateNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string plateType { get; set; }
        /// <summary>
        /// 02式个性化车
        /// </summary>
        public string plateTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vehicleType { get; set; }
        /// <summary>
        /// 未知
        /// </summary>
        public string vehicleTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string crossTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int speed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alarmType { get; set; }
        /// <summary>
        /// 交通违法车
        /// </summary>
        public string alarmTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorId { get; set; }
        /// <summary>
        /// 6061_卡口1
        /// </summary>
        public string monitorName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int illegalType { get; set; }
        /// <summary>
        /// 点位6061
        /// </summary>
        public string mixedName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int mixedType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mixedId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PicUrl picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imageIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string speedLimit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Person person { get; set; }
    }

    public class EventsItem3
    {
        /// <summary>
        /// 
        /// </summary>
        public Data3 data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string eventId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int eventType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string happenTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcParentIdex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int timeout { get; set; }
    }

    public class Params3
    {
        /// <summary>
        /// 
        /// </summary>
        public string ability { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<EventsItem3> events { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sendTime { get; set; }
    }
}
