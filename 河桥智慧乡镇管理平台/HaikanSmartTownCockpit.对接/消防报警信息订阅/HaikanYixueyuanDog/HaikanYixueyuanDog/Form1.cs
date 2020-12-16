using Apache.NMS;
using ClassLibrary1;
using com.sun.org.apache.xerces.@internal.impl.dv.util;
using Haikan.DBHelper;
using Haikan.DebugTools;
using HaikanYixueyuanDog.DataClass;
using HaikanYixueyuanDog.DataClass.Subscribe8900;
using HaikanYixueyuanDog.DataClass.Xiaofang;
using HaikanYixueyuanDog.Utils;
using HaikanYixueyuanDog.Utils.Fangkeji;
using HaikanYixueyuanDog.Utils.Menjin;
using HaikanYixueyuanDog.Utils.Subscribe8900;
using HaikanYixueyuanDog.Utils.Xiaofang;
using java.security;
using java.security.interfaces;
using java.security.spec;
using javax.crypto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using sun.security.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using DataItem = HaikanYixueyuanDog.DataClass.Xiaofang.DataItem;

namespace HaikanYixueyuanDog
{
    public partial class Form1 : Form
    {
        #region 实例化对象
        //实例化Timer类，设置间隔时间为；  
        System.Timers.Timer t = new System.Timers.Timer(1000);
        // yixueyuanEntities _dbContext = new yixueyuanEntities();

        //消防建筑信息
        readonly ClassLibrary1.BLL.xf_Building _BuildingBLL = new ClassLibrary1.BLL.xf_Building();
        ClassLibrary1.Model.xf_Building _BuildingModel = new ClassLibrary1.Model.xf_Building();
        //探测器信息
        readonly ClassLibrary1.BLL.TcqInfo _TcqInfoBLL = new ClassLibrary1.BLL.TcqInfo();
        ClassLibrary1.Model.TcqInfo _TcqInfoModel = new ClassLibrary1.Model.TcqInfo();
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
            //MessageBox.Show("录入信息");
            //获取与数据库的连接对象
            // MessageBox.Show("开始连接");
            //绑定连接字符串
            //conn.ConnectionString = "D6288142E0987E77B63C8D025DA42486BDE464AC7842A5EDA42DA58D2FE1E7386E8B77199620BCECB1BA2C7B6A9ACFFB1EC4D68031FFAD888B4939AAA48A9694EC4E07A8C1CCBE717CBDBC747876D2FB3F6004ADD359B075A812E62D4F3B625DA9E118CEB42841D128FF2811E6E1BE8A15981408FE1A62623A33FE9437A2FA108A08F90048897329625D787C98B2E9F9878774A0DA9E654964E9774491E94D2AF561F3A19F8CD58B5AE22F985C05233043C0160E8F1C891D";
            //开启连接
            // conn.Open();
            //  MessageBox.Show("连接成功！");
            LogHelper.WriteLog(typeof(Form1), "看门狗程序启动！");
            //到达时间的时候执行事件； 
            t.Elapsed += timerDo_Tick;


            //设置是执行一次（false）还是一直执行(true)；   
            t.AutoReset = true;


            //是否执行System.Timers.Timer.Elapsed事件；  
            t.Enabled = true;

            LogHelper.WriteLog(typeof(Form1), "看门狗程序启动完毕！");

        }
        /// <summary>
        /// 查询相应的表，执行相应的硬件控制方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDo_Tick(object sender, ElapsedEventArgs e)
        {
            var date = DateTime.Now;
            Console.WriteLine(date.ToString("yyyy/MM/dd HH:mm:ss"));
            LogHelper.WriteLog(typeof(Form1), "看门狗程序开始！");
            if (date.Hour == 9 && date.Minute == 14 && date.Second == 30)
            {
                t.Enabled = false;
                try
                {
                    t.Enabled = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    LogHelper.WriteLog(typeof(Form1), "遇到错误！" + ex.Message);

                }
                finally
                {
                    t.Enabled = true;
                }
            }
            LogHelper.WriteLog(typeof(Form1), "看门狗程序完毕！");
        }
        #region 消防报警信息订阅
        /// <summary>
        /// 消防报警信息订阅
        /// </summary>
        private void XiaofanData()
        {
            var tokendata = DahuaXiaofang.GetToken("tuisong", "Ts123456");
            if(tokendata.code!="200")
            {
                LogHelper.WriteLog(typeof(Form1), "遇到错误！" + tokendata.msg);
                MessageBox.Show(tokendata.msg);
                return;
            }
            string token = tokendata.data;
            try
            {
                    //报警订阅信息
                    string url_AlarmEvent = "http://112.17.112.170:28100/IFCSI/public/setAlarmEventURL";
                    var alarmpost = new
                    {
                        token= token,
                        alarmSendUrl= "http://zaxy.hmc.edu.cn/api/v1/publicinterface/alarm/getAction",
                        timeStamp= Helper.ConvertDateTimeToInt(DateTime.Now).ToString()
                    };
                    var json_alarm = PostGetRequest.HttpPostJson(url_AlarmEvent, JsonConvert.SerializeObject(alarmpost));
                    alsrm_dyRoot ady = JsonConvert.DeserializeObject<alsrm_dyRoot>(json_alarm);
                    if(ady.ret_code!=0)
                    {
                        MessageBox.Show(ady.ret_msg);
                    }
                    else
                    {
                        MessageBox.Show("订阅成功");
                    }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Form1), "遇到错误！" + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 消防建筑物信息
        /// <summary>
        /// 建筑物信息
        /// </summary>
        private void Building()
        {
            BuildingData bd = null;
            int pageNum = 1;
            var tokendata = DahuaXiaofang.GetToken("duijie", "123456");
            if (tokendata.code != "200")
            {
                LogHelper.WriteLog(typeof(Form1), "遇到错误！" + tokendata.msg);
                MessageBox.Show(tokendata.msg);
                return;
            }
            string token = tokendata.data;
            try
            {
                //建筑物信息
                string url_AlarmEvent = "http://112.17.112.170:28100/IFCSI/acquire/building";
                var alarmpost = new
                {
                    token = token,
                    timeStamp = Helper.ConvertDateTimeToInt(DateTime.Now).ToString(),
                    pageNum = "1"
                };
                var json_alarm = PostGetRequest.HttpPostJson(url_AlarmEvent, JsonConvert.SerializeObject(alarmpost));

                
                Building ady = JsonConvert.DeserializeObject<Building>(json_alarm);
                
                if (ady.ret_Code != 0)
                {
                    MessageBox.Show(ady.ret_msg);
                }
                else
                {
                    List<BuildingData> bdata = JsonConvert.DeserializeObject<List<BuildingData>>(ady.data);
                    int num = 0;
                    for (int a = 1; a < Math.Ceiling(ady.total / 50.0)+1; a++)
                    {
                        var post1 = new
                        {
                            token = token,
                            timeStamp = Helper.ConvertDateTimeToInt(DateTime.Now).ToString(),
                            pageNum = a.ToString()
                        };
                        var json_build = PostGetRequest.HttpPostJson(url_AlarmEvent, JsonConvert.SerializeObject(post1));


                        Building build = JsonConvert.DeserializeObject<Building>(json_build);
                        bdata = JsonConvert.DeserializeObject<List<BuildingData>>(build.data);
                        if (bdata != null)
                        {

                            
                            for (int i = 0; i < bdata.Count; i++)
                            {
                                DataSet ds = _BuildingBLL.GetList("recordCode='" + bdata[i].recordCode + "'");
                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    _BuildingModel.BuildingUuid = Guid.NewGuid();
                                    _BuildingModel.buildingName = bdata[i].buildingName;
                                    _BuildingModel.buildingType = bdata[i].buildingType;
                                    _BuildingModel.buildingUseNature = bdata[i].buildingUseNature;
                                    _BuildingModel.fireDanger = bdata[i].fireDanger;
                                    _BuildingModel.fireResistantLevel = bdata[i].fireResistantLevel;
                                    _BuildingModel.structureType = bdata[i].structureType;
                                    _BuildingModel.buildingHeight = Convert.ToDecimal(bdata[i].buildingHeight);
                                    _BuildingModel.regionCode = bdata[i].regionCode;
                                    _BuildingModel.gpsX3d = Convert.ToDecimal(bdata[i].gpsX3d);
                                    _BuildingModel.gpsY3d = Convert.ToDecimal(bdata[i].gpsY3d);
                                    _BuildingModel.gpsZ3d = Convert.ToDecimal(bdata[i].gpsZ3d);
                                    _BuildingModel.recordCode = bdata[i].recordCode;
                                    _BuildingModel.orgAbutSituation = bdata[i].orgAbutSituation;
                                    _BuildingModel.belongOrgId = bdata[i].belongOrgId;
                                    _BuildingModel.enterOrgNum = Convert.ToInt32(bdata[i].enterOrgNum);
                                    _BuildingModel.managerOrgId = bdata[i].managerOrgId;
                                    _BuildingModel.recordCode = bdata[i].recordCode;
                                    _BuildingBLL.Add(_BuildingModel);
                                    num++;
                                }
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("建筑物信息为空");
                        }
                    }
                    MessageBox.Show("获取建筑信息" + num);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Form1), "遇到错误！" + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 探测器信息
        public void TcqTongBu()
        {
            TcqModel bd = null;
            int pageNum = 1;
            var tokendata = DahuaXiaofang.GetToken("duijie", "123456");
            if (tokendata.code != "200")
            {
                LogHelper.WriteLog(typeof(Form1), "遇到错误！" + tokendata.msg);
                MessageBox.Show(tokendata.msg);
                return;
            }
            string token = tokendata.data;
            try
            {
                //建筑物信息
                string url_AlarmEvent = "http://112.17.112.170:28100/IFCSI/acquire/detector";
                var alarmpost = new
                {
                    token = token,
                    timeStamp = Helper.ConvertDateTimeToInt(DateTime.Now).ToString(),
                    pageNum = "1",
                    pageSize="100",
                };
                var json_alarm = PostGetRequest.HttpPostJson(url_AlarmEvent, JsonConvert.SerializeObject(alarmpost));


                TcqModel ady = JsonConvert.DeserializeObject<TcqModel>(json_alarm);

                if (ady.ret_Code != 0)
                {
                    MessageBox.Show(ady.ret_msg);
                }
                else
                {
                    List<DataItem> bdata = JsonConvert.DeserializeObject<List<DataItem>>(ady.data);
                    int num = 0;
                    for (int a = 1; a < Math.Ceiling(ady.total / 100.0) + 1; a++)
                    {
                        var post1 = new
                        {
                            token = token,
                            timeStamp = Helper.ConvertDateTimeToInt(DateTime.Now).ToString(),
                            pageNum = a.ToString()
                        };
                        var json_build = PostGetRequest.HttpPostJson(url_AlarmEvent, JsonConvert.SerializeObject(post1));


                        TcqModel build = JsonConvert.DeserializeObject<TcqModel>(json_build);
                        bdata = JsonConvert.DeserializeObject<List<DataItem>>(build.data);
                        if (bdata != null)
                        {


                            for (int i = 0; i < bdata.Count; i++)
                            {
                                DataSet ds = _TcqInfoBLL.GetList("recordCode='" + bdata[i].recordCode + "'");
                                if (ds.Tables[0].Rows.Count == 0)
                                {

                                    _TcqInfoModel.Guid = Guid.NewGuid();
                                    _TcqInfoModel.detectorCode = bdata[i].detectorCode;
                                    _TcqInfoModel.detectorName = bdata[i].detectorName;
                                    _TcqInfoModel.mainframeId = bdata[i].mainframeId;
                                    _TcqInfoModel.buildingId = bdata[i].buildingId;
                                    _TcqInfoModel.detectorAddr = bdata[i].detectorAddr;
                                    _TcqInfoModel.orgId = bdata[i].orgId;
                                    _TcqInfoModel.detectorType = bdata[i].detectorType;
                                    _TcqInfoModel.ifcsSystemType = bdata[i].ifcsSystemType;
                                    _TcqInfoModel.partunitloopCode = bdata[i].partunitloopCode;
                                    _TcqInfoModel.channelNo = bdata[i].channelNo;
                                    _TcqInfoModel.hardwareVersion = bdata[i].hardwareVersion;
                                    _TcqInfoModel.softwareVersion = bdata[i].softwareVersion;
                                    _TcqInfoModel.personId = bdata[i].personId;
                                    _TcqInfoModel.deviceRange = bdata[i].deviceRange;
                                    _TcqInfoModel.registerTime = bdata[i].registerTime;
                                    _TcqInfoModel.registerStatus = bdata[i].registerStatus;
                                    _TcqInfoModel.recordCode = bdata[i].recordCode;
                                    _TcqInfoModel.communicationId = bdata[i].communicationId.ToString();
                                    _TcqInfoBLL.Add(_TcqInfoModel);
                                    num++;
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("探测器信息为空");
                        }
                    }
                    MessageBox.Show("获取探测器信息" + num);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Form1), "遇到错误！" + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            XiaofanData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Building();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TcqTongBu();
        }
    }
}
