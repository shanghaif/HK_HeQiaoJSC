using HaikanYixueyuanDog.DataClass;
using Newtonsoft.Json;
using org.omg.CORBA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils.Menjin
{
    public static class MenjinRecord
    {
        #region 门组查询
        public class Group
        {
            /// <summary>
            /// 
            /// </summary>
            public Data data { get; set; }
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
            public string hasChildChannel { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string memo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
        }
        public class Data
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
        /// <summary>
        /// 门组查询
        /// </summary>
        /// <param name="address"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ResponseData doorGroup(string address, string token)
        {
            
            ResponseData response = new ResponseData();

            string url_doorGroup = address + "/CardSolution/card/accessControl/doorGroup/bycondition/combined";
            try
            {
                var postdata = new
                {
                    token= token,
                    pageNum = 1,
                    pageSize=20,
                    singleCondition=""
                };
                var jsonstr = JsonConvert.SerializeObject(postdata);
                Group groupdata = JsonConvert.DeserializeObject<Group>(PostGetRequest.HttpPost(url_doorGroup, jsonstr));
                if(groupdata.success=="true")
                {
                    if(groupdata.data.pageData.Count>0)
                    {
                        response.code = "200";
                        response.msg = groupdata.data.pageData[0].id.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                response.code = "400";
                response.msg = e.Message;
            }
            return response;
        }
        #endregion
        #region 当天条数
        class RecordCount
        {
            /// <summary>
            /// 
            /// </summary>
            public int data { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string errMsg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string success { get; set; }
        }
        /// <summary>
        /// 刷卡记录条数统计
        /// </summary>
        /// <param name="address"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ResponseData CardRecordCount(string address, string token)
        {
            //当天0时0分0秒：
            DateTime start = Convert.ToDateTime(DateTime.Now.ToString("D").ToString());

            //当天23时59分59秒：
            DateTime end = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("D").ToString()).AddSeconds(-1);

            ResponseData response = new ResponseData();

            string url_CardRecordCount = address + "/CardSolution/card/accessControl/swingCardRecord/bycondition/combinedCount";
            try
            {
                var postdata = new
                {
                    token= token,
                    //pageNum = 1,
                    //pageSize = 20,
                    startSwingTime = start.ToString(),
                    endSwingTime= end.ToString()
                };
                var jsonstr = JsonConvert.SerializeObject(postdata);
                RecordCount countdata = JsonConvert.DeserializeObject<RecordCount>(PostGetRequest.HttpPost(url_CardRecordCount, jsonstr));
                if (countdata.success == "true")
                {
                    response.code = "200";
                    response.msg = countdata.data.ToString();
                }
            }
            catch (Exception e)
            {
                response.code = "400";
                response.msg = e.Message;
            }
            return response;
        }
        #endregion


        #region 当天刷卡记录

        /// <summary>
        /// 当天刷卡记录
        /// </summary>
        /// <param name="address"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ResponseData CardRecordCombined(string address, string token)
        {
            //当天0时0分0秒：
            DateTime start = Convert.ToDateTime(DateTime.Now.ToString("D").ToString());

            //当天23时59分59秒：
            DateTime end = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("D").ToString()).AddSeconds(-1);

            ResponseData response = new ResponseData();

            string url_CardRecordCount = address + "/CardSolution/card/accessControl/swingCardRecord/bycondition/combinedCount";
            try
            {
                var postdata = new
                {
                    token = token,
                    //pageNum = 1,
                    //pageSize = 20,
                    startSwingTime = start.ToString(),
                    endSwingTime = end.ToString()
                };
                var jsonstr = JsonConvert.SerializeObject(postdata);
                CardRecord8900 recorddata = JsonConvert.DeserializeObject<CardRecord8900>(PostGetRequest.HttpPost(url_CardRecordCount, jsonstr));
                if (recorddata.success == "true")
                {
                    response.code = "200";
                    response.msg = "sucess";
                    response.data = recorddata.data.pageData;
                }
            }
            catch (Exception e)
            {
                response.code = "400";
                response.msg = e.Message;
            }
            return response;
        }
        #endregion
    }
}
