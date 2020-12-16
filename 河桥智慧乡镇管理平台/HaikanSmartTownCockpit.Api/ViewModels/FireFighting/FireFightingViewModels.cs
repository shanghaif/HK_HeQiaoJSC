using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.FireFighting
{
    public class FireFightingViewModels
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DataItem> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string timeStamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }

        public class DataItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string alarmTypeId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string analogValue { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string analogValueTypeId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string audioUrl { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int autoStatus { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string buildingId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string chargingUserName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string chargingUserTel { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string confirmUserId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string detectorId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string endTime { get; set; }
            /// <summary>
            /// 电气火灾
            /// </summary>
            public string eventAddress { get; set; }
            /// <summary>
            /// 火警
            /// </summary>
            public string eventContent { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string eventStatus { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string eventTypeId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string handingTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string handlingSuggestion { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string imageUrl { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int isHandled { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mainframeId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string orgId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string platformCode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string recordCode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string remarks { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string reportUserId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int resetStatus { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string resetTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string startTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string videoUrl { get; set; }
        }
    }
}
