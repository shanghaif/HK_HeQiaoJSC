using Apache.NMS;
using ClassLibrary1;
using ClassLibrary1.HData;
using com.sun.org.apache.xerces.@internal.impl.dv.util;
using Haikan.DebugTools;
using HaikanYixueyuanDog.DataClass;
using HaikanYixueyuanDog.Utils;
using HaikanYixueyuanDog.Utils.Fangkeji;
using java.security;
using java.security.interfaces;
using java.security.spec;
using javax.crypto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sun.security.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace HaikanYixueyuanDog
{
    public partial class Form1 : Form
    {
        #region 实例化对象
        //实例化Timer类，设置间隔时间为；  
        System.Timers.Timer t = new System.Timers.Timer(1000);
        HeQiaoDb _dbContext = new HeQiaoDb();

        private IMessageConsumer m_consumer;

        string url = "http://112.17.130.233:4474/api/v1/FireFighting/XiaoQuAnFang/GetAction";

        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region 安防小区人脸抓拍信息订阅
        /// <summary>
        /// 安防小区人脸抓拍信息订阅
        /// </summary>
        private void XiaoQuData()
        {
            HttpUtillib.SetPlatformInfo("27628571", "TJzjgMqgM41EezwD36aV", "10.172.33.2", 443, true);
            // 组装POST请求body          
            //string body = "{\"eventTypes\":[131614],\"eventDest\":\"" + url + "\",\"subType\":1,\"eventLvl\":[2]}";
            string body = "{\"eventTypes\":[131614,1157632001,1157632002,1157632003,1157632004,1157632005],\"eventDest\":\"http://112.17.130.233:4474/api/v1/FireFighting/XiaoQuAnFang/GetAction\",\"subType\":0,\"eventLvl\":[2]}";
            // 填充Url           
            string uri = "/artemis/api/eventService/v1/eventSubscriptionByEventTypes";

            // 发起POST请求，超时时间15秒，返回响应字节数组
            byte[] result = HttpUtillib.HttpPost(uri, body, 15);
            if (result != null)
            {
                string tmp = System.Text.Encoding.UTF8.GetString(result);
                Result ady = JsonConvert.DeserializeObject<Result>(tmp);
                if (ady.code == 0)
                {
                    MessageBox.Show("订阅成功！");
                }
            }
            else
            {
                MessageBox.Show("/api/eventService/v1/eventSubscriptionByEventTypes: POST fail");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XiaoQuData();
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public int code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string msg { get; set; }
        }

        #endregion
        /// <summary>
        /// 安防小区车辆抓拍信息订阅
        /// </summary>
        private void XiaoQuCarData()
        {
            HttpUtillib.SetPlatformInfo("27628571", "TJzjgMqgM41EezwD36aV", "10.172.33.2", 443, true);
            // 组装POST请求body          
            //string body = "{\"eventTypes\":[131614],\"eventDest\":\"" + url + "\",\"subType\":1,\"eventLvl\":[2]}";
            string body = "{\"eventTypes\":[1157632001,1157632002,1157632003,1157632004,1157632005],\"eventDest\":\"http://112.17.130.233:4474/api/v1/FireFighting/CarXiaoQuAnFang/GetAction\",\"subType\":0,\"eventLvl\":[2]}";
            // 填充Url           
            string uri = "/artemis/api/eventService/v1/eventSubscriptionByEventTypes";

            // 发起POST请求，超时时间15秒，返回响应字节数组
            byte[] result = HttpUtillib.HttpPost(uri, body, 15);
            if (result != null)
            {
                string tmp = System.Text.Encoding.UTF8.GetString(result);
                Result ady = JsonConvert.DeserializeObject<Result>(tmp);
                if (ady.code == 0)
                {
                    MessageBox.Show("订阅成功！");
                }
            }
            else
            {
                MessageBox.Show("/api/eventService/v1/eventSubscriptionByEventTypes: POST fail");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            XiaoQuCarData();
        }
    }
}
