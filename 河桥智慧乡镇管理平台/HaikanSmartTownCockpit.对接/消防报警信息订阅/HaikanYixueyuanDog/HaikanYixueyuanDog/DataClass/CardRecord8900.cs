using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.DataClass
{
    public class CardRecord8900
    {
        /// <summary>
        /// 
        /// </summary>
        public RecordData data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string success { get; set; }
    }
    public class PageDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string cardNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cardStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cardType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string channelCode { get; set; }
        /// <summary>
        /// 130考勤_通道1
        /// </summary>
        public string channelName { get; set; }
        /// <summary>
        /// 导入导出测试部门
        /// </summary>
        public string deptName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceCode { get; set; }
        /// <summary>
        /// 130考勤
        /// </summary>
        public string deviceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int enterOrExit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int openResult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int openType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paperNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string personCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int personId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string personName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recordImage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string swingTime { get; set; }
    }

    public class RecordData
    {
        /// <summary>
        /// 
        /// </summary>
        public int currentPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PageDataItem> pageData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalRows { get; set; }
    }
}
