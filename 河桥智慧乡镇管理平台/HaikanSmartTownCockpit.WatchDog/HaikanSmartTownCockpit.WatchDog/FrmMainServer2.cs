using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using com.hikvision.artemis.sdk;
using com.hikvision.artemis.sdk.config;
using Haikan2.DebugTools;
using HaikanSmartTownCockpit.WatchDog.Region;
using HaikanSmartTownCockpit.WatchDog.sqjz.model;
using HaikanSmartTownCockpit.WatchDog.sqjz.model2;
using HaikanSmartTownCockpit.WatchDog.sqjz.model3;
using HaiKanTaskHousekeeperCore.Data;
using Newtonsoft.Json;

namespace HaikanSmartTownCockpit.WatchDog
{
    /// <summary>
    /// 112服务器
    /// </summary>
    public partial class FrmMainServer2 : Form
    {
        #region 实例化
        List<HaikanSmartTownCockpit.WatchDog.sqjz.model2.List> parknodes = new List<HaikanSmartTownCockpit.WatchDog.sqjz.model2.List>();
        List<HaikanSmartTownCockpit.WatchDog.Camera.List> cameraList = new List<HaikanSmartTownCockpit.WatchDog.Camera.List>();
        string appkey = "xfw5a4704zd8wnd2190giygi25fpqx1g";
        string secret = "tugikc3e7435y8vsdd1w5a9x732dby38";

        //实例化Timer类，设置间隔时间为；  
        System.Timers.Timer t = new System.Timers.Timer(1000); // 同步地址库,同步监控
        //System.Timers.Timer t2 = new System.Timers.Timer(1000); // 社区矫正&危房清单[因为没有河桥的数据,暂时没有使用]
        System.Timers.Timer t3 = new System.Timers.Timer(1000); // 停车场同步
        System.Timers.Timer t4 = new System.Timers.Timer(5000); // 大气防治
        HaikanHeQiaoContext _dbContext1 = new HaikanHeQiaoContext();
        HaikanHeQiaoContext _dbContext2 = new HaikanHeQiaoContext();
        HaikanHeQiaoContext _dbContext3 = new HaikanHeQiaoContext();
        //区域基础表格
        big_data.BLL.RegionInfo regionInfobll = new big_data.BLL.RegionInfo();
        big_data.Model.RegionInfo regionInfomodel = new big_data.Model.RegionInfo();

        //地址库区域匹配
        big_data.BLL.RegionInfos regionInfosbll = new big_data.BLL.RegionInfos();
        big_data.Model.RegionInfos regionInfosmodel = new big_data.Model.RegionInfos();

        //地址库
        big_data.BLL.UnifiedAddressLibrary unifiedAddressLibrarybll = new big_data.BLL.UnifiedAddressLibrary();
        big_data.Model.UnifiedAddressLibrary unifiedAddressLibrarymodel = new big_data.Model.UnifiedAddressLibrary();

        //户籍信息
        big_data.BLL.Userinfoty userinfotybll = new big_data.BLL.Userinfoty();
        big_data.Model.Userinfoty userinfotymodel = new big_data.Model.Userinfoty();
        //地址库和户籍信息匹配表
        big_data.BLL.UnifiedAddressLibraryUserInfo unifiedAddressLibraryUserInfobll = new big_data.BLL.UnifiedAddressLibraryUserInfo();
        big_data.Model.UnifiedAddressLibraryUserInfo unifiedAddressLibraryUserInfomodel = new big_data.Model.UnifiedAddressLibraryUserInfo();
        //村信息
        big_data.BLL.Town townbll = new big_data.BLL.Town();
        big_data.Model.Town townmodel = new big_data.Model.Town();
        public string info = "";

        #endregion

        public FrmMainServer2()
        {
            ArtemisConfig.host = "10.172.33.2"; // 代理API网关nginx服务器ip端口
            ArtemisConfig.appKey = "27628571";  // 秘钥appkey
            ArtemisConfig.appSecret = "TJzjgMqgM41EezwD36aV";// 秘钥appSecret

            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            LogHelper.WriteLog(typeof(FrmMainServer), "看门狗程序启动！");

            //到达时间的时候执行事件； 
            t.Elapsed += timerDo_Tick; // 同步地址库,同步监控
            //t2.Elapsed += timerDo2_Tick; // 社区矫正&危房清单[因为没有河桥的数据,暂时没有使用]
            t3.Elapsed += timerDo3_Tick; //停车场同步
            t4.Elapsed += timerDo4_Tick;

            //设置是执行一次（false）还是一直执行(true)；   
            t.AutoReset = true;
            //t2.AutoReset = true;
            t3.AutoReset = true;
            t4.AutoReset = true;

            //是否执行System.Timers.Timer.Elapsed事件；  
            t.Enabled = true;
            //t2.Enabled = true;
            t3.Enabled = true;
            t4.Enabled = true;

            LogHelper.WriteLog(typeof(FrmMainServer), "看门狗程序启动完毕！");
        }

        #region 同步地址库,同步监控
        /// <summary>
        /// 同步地址库,同步监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDo_Tick(object sender, ElapsedEventArgs e)
        {
            t.Enabled = false;
            var date = DateTime.Now;
            Console.WriteLine(date.ToString("yyyy/MM/dd HH:mm:ss"));

            if (date.Second % 5 == 0)
            {
                LogHelper.WriteLog(typeof(string), "开始进行监控同步！");

                try
                {
                    Monitoring2();

                    LogHelper.WriteLog(typeof(string), "监控同步结束！");
                    t.Enabled = true;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(FrmMainServer), "同步监控遇到错误！" + ex.Message);
                }
                finally
                {
                    t.Enabled = true;
                }
            }
            else
            {
                t.Enabled = true;
            }

            LogHelper.WriteLog(typeof(FrmMainServer), "timerDo_Tick程序完毕！");
        }


        /// <summary>
        /// 全部监控的同步
        /// </summary>
        public void Monitoring2()
        {
            //_dbContext1.Dispose();
            //_dbContext1 = new HaikanHeQiaoContext();
            cameraList = new List<HaikanSmartTownCockpit.WatchDog.Camera.List>();
            GetCameras(1);
            for (int i = 0; i < cameraList.Count; i++)
            {
                var cid = cameraList[i].indexCode;
                if (Queryable.Any<XlProject>(_dbContext1.XlProject, x => x.XlShebeiId == cid && x.IsDeleted == 0))
                {
                    LogHelper.WriteLog(typeof(string), "监控信息修改:" + cid);

                    //Console.WriteLine("监控信息修改:" + cid);
                    var entity = Queryable.FirstOrDefault<XlProject>(_dbContext1.XlProject, x => x.XlShebeiId == cid && x.IsDeleted == 0);
                    entity.ShebeiAddress = cameraList[i].name;
                    entity.XlShebeiType = cameraList[i].regionName;
                    //entity.Lat = cameraList[i].latitude;
                    //entity.Lon = cameraList[i].longitude;
                    entity.ShebeiType = GetOnlineState(cid);
                    entity.UrlType = 1;
                }
                else
                {
                    LogHelper.WriteLog(typeof(string), "监控信息添加:" + cid);

                    //Console.WriteLine("监控信息添加:" + cid);
                    XlProject project = new XlProject()
                    {
                        XlProjectUuid = Guid.NewGuid(),
                        XlShebeiId = cid,
                        XlShebeiType = cameraList[i].regionName,
                        ShebeiAddress = cameraList[i].name,
                        Lon = cameraList[i].longitude,
                        Lat = cameraList[i].latitude,
                        IsDeleted = 0,
                        ShebeiType = GetOnlineState(cid),
                        AddPeople = "狗",
                        AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        UrlType = 1,
                    };
                    _dbContext1.XlProject.Add(project);
                    LogHelper.WriteLog(typeof(string), "添加" + JsonConvert.SerializeObject(project));
                    //Console.WriteLine("添加" + JsonConvert.SerializeObject(project));
                }
                var num = _dbContext1.SaveChanges();
                Console.WriteLine("改变：" + num);
                if (cameraList[i].regionName == "垃圾分类监控")
                {
                    if (Queryable.Any<Ygiene>(_dbContext1.Ygiene, x => x.YgieneMonitorId == cid && x.IsDeleted == 0))
                    {
                        LogHelper.WriteLog(typeof(string), "case9修改:" + cid);
                        //Console.WriteLine("case9修改:" + cid);
                        var entity = Queryable.FirstOrDefault<Ygiene>(_dbContext1.Ygiene, x => x.YgieneMonitorId == cid && x.IsDeleted == 0);
                        entity.YgieneType = cameraList[i].regionName;
                        entity.YgieneAddress = cameraList[i].name;
                        //entity.Lat = cameraList[i].latitude;
                        //entity.Lon = cameraList[i].longitude;
                        entity.YgieneStaues = GetOnlineState(cid);
                        entity.UrlType = 1;
                    }
                    else
                    {
                        LogHelper.WriteLog(typeof(string), "case9添加:" + cid);

                        //Console.WriteLine("case9添加:" + cid);
                        Ygiene ygiene = new Ygiene()
                        {
                            YgieneUuid = Guid.NewGuid(),
                            YgieneMonitorId = cid,
                            YgieneAddress = cameraList[i].name,
                            Lon = cameraList[i].longitude,
                            Lat = cameraList[i].latitude,
                            AddPeople = "狗",
                            AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            YgieneStaues = GetOnlineState(cid),
                            YgieneType = cameraList[i].regionName,
                            IsDeleted = 0,
                            UrlType = 1,
                        };
                        _dbContext1.Ygiene.Add(ygiene);
                        LogHelper.WriteLog(typeof(string), "添加卫生点" + JsonConvert.SerializeObject(ygiene));
                        //Console.WriteLine("添加卫生点" + JsonConvert.SerializeObject(ygiene));
                    }
                    _dbContext1.SaveChanges();
                }
            }


            cameraList = new List<HaikanSmartTownCockpit.WatchDog.Camera.List>();
            //GC.Collect();
        }

        /// <summary>
        /// 全部监控的获取
        /// </summary>
        /// <param name="page"></param>
        public void GetCameras(int page = 1)
        {
            string cameraP = "/artemis/api/resource/v2/camera/search";
            string body_c = "{\"pageNo\":" + page + ",\"pageSize\": 100,\"orderBy\": \"name\",\"orderType\": \"desc\"}";
            java.util.Map path_c = new java.util.HashMap();
            path_c.put("https://", cameraP);
            string result_c = ArtemisHttpUtil.doPostStringArtemis(path_c, body_c, null, "*/*", "application/json", null);
            var camera = JsonConvert.DeserializeObject<Camera.Camera>(result_c);
            LogHelper.WriteLog(typeof(string), "监控查询code:" + camera.code);

            //Console.WriteLine("监控查询code:" + camera.code);
            if (camera.data.total == 0)
            {
                return;
            }
            var clist = camera.data.list;
            cameraList.AddRange(clist);
            var camera_total = camera.data.total;
            if ((camera_total - page * 100) % 100 > 0)
            {
                LogHelper.WriteLog(typeof(string), "开始迭代");

                //Console.WriteLine("开始迭代");
                page++;
                GetCameras(page);
            }
        }

        /// <summary>
        /// 获取监控的在线状态
        /// </summary>
        /// <param name="indexCode"></param>
        /// <returns>1在线,0不在线</returns>
        public string GetOnlineState(string indexCode)
        {
            Console.WriteLine("获取监控状态");
            string onlineP = "/artemis/api/nms/v1/online/camera/get";
            string body_o = "{\"indexCodes\": [\"" + indexCode + "\"]}";
            java.util.Map path_o = new java.util.HashMap();
            path_o.put("https://", onlineP);
            string result_o = ArtemisHttpUtil.doPostStringArtemis(path_o, body_o, null, "*/*", "application/json", null);
            var online_d = JsonConvert.DeserializeObject<Online.Online>(result_o);
            //Console.WriteLine(indexCode + "在线状态查询code:" + online_d.code);
            LogHelper.WriteLog(typeof(string), indexCode + "在线状态查询code:" + online_d.code);

            if (online_d.data.list.Count > 0)
            {
                return online_d.data.list[0].online == 1 ? "在线" : "离线";
            }
            else
            {
                return "未知";
            }
        }
        #endregion

        #region 社区矫正&危房清单[因为没有河桥的数据,暂时没有使用]
        /// <summary>
        /// 社区矫正&危房清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDo2_Tick(object sender, ElapsedEventArgs e)
        {
            // t2.Enabled = false;
            // var date = DateTime.Now;
            // Console.WriteLine(date.ToString("yyyy/MM/dd HH:mm:ss"));
            // Console.WriteLine("社区矫正，危房清单同步开始");
            // try
            // {
            //     if (date.Second % 5 == 0)
            //     {
            //         Synchronize();
            //     }
            //     else
            //     {
            //         t2.Enabled = true;
            //         Console.WriteLine("非时段跳出");
            //         return;
            //     }
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine("同步社区矫正,危房清单错误:" + ex.Message);
            //     LogHelper.WriteLog(typeof(string), "同步社区矫正,危房清单错误:" + ex.Message);
            // }
            // finally
            // {
            //     t2.Enabled = true;
            // }
        }

        /// <summary>
        /// 社区矫正&危房清单
        /// </summary>
        public void Synchronize()
        {
            var timeStamp = GetTimeStamp();
            var sign = GetMD5Hash(appkey + secret + timeStamp);
            string getTokenUrl = "http://10.33.188.31:8101/interface/public/service/risen-inte/reTokenByKey.action?appKey=" + appkey + "&sign=" + sign + "&requestTime=" + timeStamp;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(getTokenUrl);
            myRequest.Method = "GET";
            //myRequest.ContentType = "application/json";
            //通过Web访问对象获取响应内容
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string returnJson = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
            var code = myResponse.StatusCode.ToString();
            Console.WriteLine(returnJson);
            Console.WriteLine(code);
            reader.Close();
            myResponse.Close();
            var tokenresult = JsonConvert.DeserializeObject<TokenResult>(returnJson);
            string requestSecret = "";
            if (tokenresult.code == "00")
            {
                requestSecret = tokenresult.datas.requestSecret;
                #region 社区矫正
                string getCorrectionUrl = "http://10.33.188.31:8101/interface/public/service/risen-api/8qU6E83Jb6x6q77bnew.action";
                var timeStamp2 = GetTimeStamp();
                var sign2 = GetMD5Hash(appkey + requestSecret + timeStamp2);
                HttpWebRequest myRequest2 = (HttpWebRequest)WebRequest.Create(getCorrectionUrl);
                myRequest2.Method = "POST";
                //myRequest2.ContentType = "application/json";
                //string body = "{\"sfzh\":\"\",\"appKey\":\"" + appkey + "\",\"sign\":\"" + sign2 + "\",\"requestTime\":\"" + timeStamp2 + "\",\"additional\":\"\"}";
                myRequest2.ContentType = "application/x-www-form-urlencoded";
                string body2 = "appKey=" + appkey + "&sign=" + sign2 + "&requestTime=" + timeStamp2;
                byte[] btbs2 = Encoding.UTF8.GetBytes(body2);
                myRequest2.ContentLength = btbs2.Length;
                myRequest2.GetRequestStream().Write(btbs2, 0, btbs2.Length);
                HttpWebResponse myResponse2 = (HttpWebResponse)myRequest2.GetResponse();
                StreamReader reader2 = new StreamReader(myResponse2.GetResponseStream(), Encoding.UTF8);
                string returnJson2 = reader2.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
                var code2 = myResponse2.StatusCode.ToString();
                Console.WriteLine(returnJson2);
                Console.WriteLine(code2);
                reader2.Close();
                myResponse2.Close();

                //TODO

                #endregion
                #region 危房清单

                string getDangerousUrl = "http://10.33.188.31:8101/interface/public/service/risen-api/5be9xaJ3tk84S6M2new.action";
                var timeStamp3 = GetTimeStamp();
                var sign3 = GetMD5Hash(appkey + requestSecret + timeStamp3);
                HttpWebRequest myRequest3 = (HttpWebRequest)WebRequest.Create(getDangerousUrl);
                myRequest3.Method = "POST";
                //myRequest2.ContentType = "application/json";
                //string body = "{\"sfzh\":\"\",\"appKey\":\"" + appkey + "\",\"sign\":\"" + sign2 + "\",\"requestTime\":\"" + timeStamp2 + "\",\"additional\":\"\"}";
                myRequest3.ContentType = "application/x-www-form-urlencoded";
                string body3 = "appKey=" + appkey + "&sign=" + sign3 + "&requestTime=" + timeStamp3;
                byte[] btbs3 = Encoding.UTF8.GetBytes(body3);
                myRequest3.ContentLength = btbs3.Length;
                myRequest3.GetRequestStream().Write(btbs3, 0, btbs3.Length);
                HttpWebResponse myResponse3 = (HttpWebResponse)myRequest3.GetResponse();
                StreamReader reader3 = new StreamReader(myResponse3.GetResponseStream(), Encoding.UTF8);
                string returnJson3 = reader3.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
                var code3 = myResponse3.StatusCode.ToString();
                Console.WriteLine(returnJson3);
                Console.WriteLine(code3);
                reader3.Close();
                myResponse3.Close();


                //TODO

                #endregion
            }
        }


        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 使用 MD5  对字符串  进行加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// 
        public static string GetMD5Hash(string str)
        {
            StringBuilder result = new StringBuilder();
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {

                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                int length = data.Length;
                for (int i = 0; i < length; i++)
                    result.Append(data[i].ToString("x2"));

            }
            return result.ToString();
        }
        #endregion

        #region 停车场同步
        /// <summary>
        /// 停车场同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDo3_Tick(object sender, ElapsedEventArgs e)
        {
            t3.Enabled = false;
            Console.WriteLine("开始同步停车场");
            LogHelper.WriteLog(typeof(string), "开始同步停车场");

            var date = DateTime.Now;
            Console.WriteLine(date.ToString("yyyy/MM/dd HH:mm:ss"));
            LogHelper.WriteLog(typeof(string), date.ToString("yyyy/MM/dd HH:mm:ss"));

            try
            {
                if (date.Second % 10 == 0)
                {
                    SyncParking();

                }
                else
                {
                    t3.Enabled = true;
                    Console.WriteLine("非时段跳出");
                    LogHelper.WriteLog(typeof(string), "非时段跳出");

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("同步停车场错误" + ex.Message);
                LogHelper.WriteLog(typeof(string), "同步停车场错误:" + ex.Message);
            }
            finally
            {
                t3.Enabled = true;
            }
        }

        /// <summary>
        /// 同步停车场信息
        /// </summary>
        public void SyncParking()
        {
            parknodes = new List<HaikanSmartTownCockpit.WatchDog.sqjz.model2.List>();
            Parking();
            //查找停车场
            var parking = parknodes.FindAll(x => x.parentIndexCode == "0");
            Console.WriteLine("停车场条数:" + parking.Count);
            if (parking.Count > 0)
            {
                for (int i = 0; i < parking.Count; i++)
                {
                    ParkingCarport(parking[i].indexCode, parking[i]);
                }

            }

            parknodes = new List<HaikanSmartTownCockpit.WatchDog.sqjz.model2.List>();
        }


        /// <summary>
        /// 停车场节点信息
        /// </summary>
        public void Parking(int page = 1)
        {

            //获取停车库节点信息
            string parknodesurl = "/artemis/api/resource/v1/park/search";
            string pnBody = "{\"pageSize\": 20,\"pageNo\": " + page + "}";
            java.util.Map path_pn = new java.util.HashMap();
            path_pn.put("https://", parknodesurl);
            string result_pn = ArtemisHttpUtil.doPostStringArtemis(path_pn, pnBody, null, "*/*", "application/json", null);
            var parknodes_d = JsonConvert.DeserializeObject<ParkNodes>(result_pn);

            Console.WriteLine("停车场节点查询code:" + parknodes_d.code);
            parknodes.AddRange(parknodes_d.data.list);
            var pn_total = parknodes_d.data.total;
            if ((pn_total - page * 20) % 20 > 0)
            {
                Console.WriteLine("开始迭代");
                page++;
                Parking(page);
            }
        }

        /// <summary>
        /// 停车场车位信息
        /// </summary>
        /// <param name="indexcode"></param>
        public void ParkingCarport(string indexcode, HaikanSmartTownCockpit.WatchDog.sqjz.model2.List data)
        {
            string carportUrl = "/artemis/api/pms/v1/park/remainSpaceNum";
            string cpbody = "{\"parkSyscode\": \"" + indexcode + "\"}";
            java.util.Map path_cp = new java.util.HashMap();
            path_cp.put("https://", carportUrl);
            string result_cp = ArtemisHttpUtil.doPostStringArtemis(path_cp, cpbody, null, "*/*", "application/json", null);
            var parkcarport_cp = JsonConvert.DeserializeObject<Carport>(result_cp);
            var carport = parkcarport_cp.data[0];
            if (_dbContext2.ParkingLot.Any(x => x.ParkingIndexCode == data.indexCode && x.IsDeleted == 0))
            {
                var entity = _dbContext2.ParkingLot.FirstOrDefault(x => x.ParkingIndexCode == data.indexCode && x.IsDeleted == 0);
                entity.Zchewei = carport.totalPlace.ToString();
                entity.Schewei = carport.leftPlace.ToString();
                entity.Ychewei = (carport.totalPlace - carport.leftPlace).ToString();
            }
            else
            {
                ParkingLot lot = new ParkingLot()
                {
                    ParkingLotUuid = Guid.NewGuid(),
                    ParkingLotName = data.name,
                    ParkingIndexCode = data.indexCode,
                    IsDeleted = 0,
                    Zchewei = carport.totalPlace.ToString(),
                    Schewei = carport.leftPlace.ToString(),
                    Ychewei = (carport.totalPlace - carport.leftPlace).ToString(),
                    AddTime = DateTime.Now.ToString("yyyy-MM-dd"),

                };
                _dbContext2.ParkingLot.Add(lot);

            }
            var num = _dbContext2.SaveChanges();
            Console.WriteLine("添加结果:" + num);

        }
        #endregion 

        #region 大气防治
        private void timerDo4_Tick(object sender, ElapsedEventArgs e)
        {
            t4.Enabled = false;
            var date = DateTime.Now;
            Console.WriteLine(date.ToString("yyyy/MM/dd HH:mm:ss"));
            LogHelper.WriteLog(typeof(FrmMainServer), "看门狗程序开始！");
            if (date.Hour >= 2)
            {
                try
                {
                    Console.WriteLine("开始同步PM2.5");
                    LogHelper.WriteLog(typeof(FrmMainServer), "开始同步PM2.5！");
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.help.bj.cn/apis/weather/?id=101210107");
                    request.Method = "GET";
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                    string retString = myStreamReader.ReadToEnd();
                    var respTqpm = JsonConvert.DeserializeObject<TqPm25>(retString);
                    Console.WriteLine(respTqpm);
                    myStreamReader.Close();
                    myResponseStream.Close();
                    var times = date.ToString("yyyy-MM-dd");
                    var query = _dbContext3.Barometric.FirstOrDefault(x => x.IsDeleted != 1 && x.BarTime == times);


                    if (query == null)
                    {
                        Barometric lot = new Barometric()
                        {
                            BarometricUuid = Guid.NewGuid(),
                            NowShuzhi = respTqpm.pm25,
                            IsDeleted = 0,
                            Linjie = "75",
                            BarTime = DateTime.Now.ToString("yyyy-MM-dd"),
                            AddTime = DateTime.Now.ToString("yyyy-MM-dd"),
                            AddPeople = "狗"
                        };
                        _dbContext3.Barometric.Add(lot);
                        _dbContext3.SaveChanges();
                    }
                    t4.Enabled = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("同步PM2.5遇到错误！" + ex.Message);
                    LogHelper.WriteLog(typeof(FrmMainServer), "同步PM2.5遇到错误！" + ex.Message);

                }
                finally
                {
                    t4.Enabled = true;
                }
            }



            LogHelper.WriteLog(typeof(FrmMainServer), "看门狗程序完毕！");

        }
        #endregion

        /// <summary>
        /// 地址库与监控关联
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            QuYuinfo();
            UnifiedAddressLibraryQuYuinfo();
            UnifiedAddressLibraryUserInfo();
        }
        /// <summary>
        /// 区域基础数据整理
        /// </summary>
        public void QuYuinfo()
        {
            DataSet dss = regionInfobll.GetList("1=1");
            if (dss.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
                {
                    string Ids = dss.Tables[0].Rows[i]["RegionID"].ToString();
                    string zbx = dss.Tables[0].Rows[i]["RegionX"].ToString();//经度
                    string zby = dss.Tables[0].Rows[i]["RegionY"].ToString();//纬度
                    DataSet dssbs = regionInfosbll.GetList("RegionID=" + Ids);
                    if (dssbs.Tables[0].Rows.Count > 0)
                    {
                        regionInfosmodel = regionInfosbll.GetModel(Convert.ToInt32(dssbs.Tables[0].Rows[0]["ID"]));
                        regionInfosmodel.RegionID = Convert.ToInt32(Ids);
                        if (regionInfosmodel.RegionXYInfo.ToString() != "")
                        {
                            regionInfosmodel.RegionXYInfo += "|" + zbx + "," + zby;
                        }
                        else
                        {
                            regionInfosmodel.RegionXYInfo = zbx + "," + zby;
                        }
                        regionInfosmodel.UnifiedAddressLibraryID = "";
                        regionInfosmodel.AddTime = DateTime.Now;
                        regionInfosbll.Update(regionInfosmodel);
                    }
                    else
                    {
                        regionInfosmodel.RegionID = Convert.ToInt32(Ids);
                        regionInfosmodel.RegionXYInfo = zbx + "," + zby;
                        regionInfosmodel.UnifiedAddressLibraryID = "";
                        regionInfosmodel.AddTime = DateTime.Now;
                        regionInfosbll.Add(regionInfosmodel);
                    }
                }
            }

            LogHelper.WriteLog(typeof(FrmMainServer), "数据更新完成！！！");
            //AlertAndStay("数据更新完成！！！");
        }
        /// <summary>
        /// 地址库数据区域数据匹配
        /// </summary>
        public void UnifiedAddressLibraryQuYuinfo()
        {
            DataSet dss = unifiedAddressLibrarybll.GetList("1=1");
            if (dss.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
                {
                    string UnifiedAddressLibraryID = dss.Tables[0].Rows[i]["ID"].ToString();
                    double xinfo = Convert.ToDouble(dss.Tables[0].Rows[i]["LON"].ToString());
                    double yinfo = Convert.ToDouble(dss.Tables[0].Rows[i]["LAT"].ToString());
                    DataSet dssab = regionInfosbll.GetList("1=1");
                    if (dssab.Tables[0].Rows.Count > 0)
                    {
                        for (int i1 = 0; i1 < dssab.Tables[0].Rows.Count; i1++)
                        {
                            string ids = dssab.Tables[0].Rows[i1]["ID"].ToString();
                            string xyinfolist = dssab.Tables[0].Rows[i1]["RegionXYInfo"].ToString();
                            if (xyinfolist != "")
                            {
                                string[] listitem = xyinfolist.Split('|');
                                if (listitem.Length > 0)
                                {

                                    Point point11 = new Point(xinfo, yinfo);
                                    Point[] ps = new Point[listitem.Length];
                                    for (int i2 = 0; i2 < listitem.Length; i2++)
                                    {
                                        string[] xylist = listitem[i2].Split(',');
                                        if (xylist.Length > 0)
                                        {
                                            ps[i2] = new Point(Convert.ToDouble(xylist[0]), Convert.ToDouble(xylist[1]));
                                        }
                                    }
                                    if (PointInFences(point11, ps))
                                    {
                                        regionInfosmodel = regionInfosbll.GetModel(Convert.ToInt32(ids));
                                        if (!("," + regionInfosmodel.UnifiedAddressLibraryID + ",").Contains("," + UnifiedAddressLibraryID + ","))
                                        {
                                            if (regionInfosmodel.UnifiedAddressLibraryID != "")
                                            {
                                                regionInfosmodel.UnifiedAddressLibraryID += "," + UnifiedAddressLibraryID;
                                            }
                                            else
                                            {
                                                regionInfosmodel.UnifiedAddressLibraryID = UnifiedAddressLibraryID;
                                            }
                                            regionInfosmodel.AddTime = DateTime.Now;
                                            regionInfosbll.Update(regionInfosmodel);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    unifiedAddressLibrarymodel = unifiedAddressLibrarybll.GetModel(Convert.ToInt32(UnifiedAddressLibraryID));
                    unifiedAddressLibrarymodel.REMARK = "1";
                    unifiedAddressLibrarymodel.AddTime = DateTime.Now.ToString();
                    unifiedAddressLibrarybll.Update(unifiedAddressLibrarymodel);
                }
            }
            //AlertAndStay("数据更新完成！！！");
            LogHelper.WriteLog(typeof(FrmMainServer), "数据更新完成！！！");
        }
        /// <summary>
        /// 匹配户籍信息和地址库
        /// </summary>
        public void UnifiedAddressLibraryUserInfo()
        {
            DataSet dss = userinfotybll.GetList("1=1");
            if (dss.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
                {
                    string mphhm = "";
                    string Issfpp = "0";
                    if (dss.Tables[0].Rows[i]["Domicile"].ToString() != "")
                    {
                        userinfotymodel = userinfotybll.GetModel(Convert.ToInt32(dss.Tables[0].Rows[i]["ID"].ToString()));
                        if (dss.Tables[0].Rows[i]["Domicile"].ToString().Length >= 12)
                        {
                            string[] strlist = dss.Tables[0].Rows[i]["Domicile"].ToString().Split('村');
                            if (strlist.Length > 1)
                            {
                                mphhm = ToDBC(strlist[1]);//地址
                            }
                        }
                        else
                        {
                            mphhm = ToDBC(dss.Tables[0].Rows[i]["Domicile"].ToString());//地址
                        }
                    }
                    else
                    {
                        mphhm = "户籍信息中没有地址信息";
                    }
                    string username = dss.Tables[0].Rows[i]["RealName"].ToString();//姓名
                    DataSet dssa = unifiedAddressLibrarybll.GetList("SOURCEADDRESS like '%" + mphhm + "%'");
                    if (dssa.Tables[0].Rows.Count > 0)
                    {
                        for (int i1 = 0; i1 < dssa.Tables[0].Rows.Count; i1++)
                        {
                            DataSet dssb = unifiedAddressLibraryUserInfobll.GetList("UnifiedAddressLibraryID=" + dssa.Tables[0].Rows[i1]["ID"]);
                            if (dssb.Tables[0].Rows.Count > 0)
                            {
                                unifiedAddressLibraryUserInfomodel = unifiedAddressLibraryUserInfobll.GetModel(Convert.ToInt32(dssb.Tables[0].Rows[0]["ID"]));
                                if (!("," + unifiedAddressLibraryUserInfomodel.UserIDList + ",").Contains("," + dss.Tables[0].Rows[i]["ID"] + ","))
                                {
                                    unifiedAddressLibraryUserInfomodel.UserIDList += "," + dss.Tables[0].Rows[i]["ID"].ToString();
                                }
                                unifiedAddressLibraryUserInfomodel.AddTime = DateTime.Now;
                                unifiedAddressLibraryUserInfobll.Update(unifiedAddressLibraryUserInfomodel);
                            }
                            else
                            {
                                unifiedAddressLibraryUserInfomodel.UnifiedAddressLibraryID = Convert.ToInt32(dssa.Tables[0].Rows[i1]["ID"]);
                                unifiedAddressLibraryUserInfomodel.UserIDList = dss.Tables[0].Rows[i]["ID"].ToString();
                                unifiedAddressLibraryUserInfomodel.AddTime = DateTime.Now;
                                unifiedAddressLibraryUserInfobll.Add(unifiedAddressLibraryUserInfomodel);
                            }
                        }
                        Issfpp = "1";
                    }
                    else
                    {
                        DataSet dssab = unifiedAddressLibrarybll.GetList("SOURCEADDRESS like '%" + username + "%'");
                        if (dssab.Tables[0].Rows.Count > 0)
                        {
                            for (int i1 = 0; i1 < dssab.Tables[0].Rows.Count; i1++)
                            {
                                DataSet dssabb = unifiedAddressLibraryUserInfobll.GetList("UnifiedAddressLibraryID=" + dssab.Tables[0].Rows[i1]["ID"]);
                                if (dssabb.Tables[0].Rows.Count > 0)
                                {
                                    unifiedAddressLibraryUserInfomodel = unifiedAddressLibraryUserInfobll.GetModel(Convert.ToInt32(dssabb.Tables[0].Rows[0]["ID"]));
                                    if (!("," + unifiedAddressLibraryUserInfomodel.UserIDList + ",").Contains("," + dss.Tables[0].Rows[i]["ID"] + ","))
                                    {
                                        unifiedAddressLibraryUserInfomodel.UserIDList += "," + dss.Tables[0].Rows[i]["ID"].ToString();
                                    }
                                    unifiedAddressLibraryUserInfomodel.AddTime = DateTime.Now;
                                    unifiedAddressLibraryUserInfobll.Update(unifiedAddressLibraryUserInfomodel);
                                }
                                else
                                {
                                    unifiedAddressLibraryUserInfomodel.UnifiedAddressLibraryID = Convert.ToInt32(dssab.Tables[0].Rows[i1]["ID"]);
                                    unifiedAddressLibraryUserInfomodel.UserIDList = dss.Tables[0].Rows[i]["ID"].ToString();
                                    unifiedAddressLibraryUserInfomodel.AddTime = DateTime.Now;
                                    unifiedAddressLibraryUserInfobll.Add(unifiedAddressLibraryUserInfomodel);
                                }
                            }
                            Issfpp = "1";
                        }
                    }
                    userinfotymodel.Work = Issfpp;
                    userinfotymodel.TownUuid = townbll.GetModel(1).TownUuid;
                    userinfotybll.Update(userinfotymodel);
                }
            }
            //AlertAndStay("数据更新完成！！！");
            LogHelper.WriteLog(typeof(FrmMainServer), "数据更新完成！！！");
        }
        /**/
        // /
        // / 转半角的函数(DBC case)
        // /
        // /任意字符串
        // /半角字符串
        // /
        // /全角空格为12288，半角空格为32
        // /其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        // /
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }
        public class Point
        {
            public double X;
            public double Y;
            public Point(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }
        }
        public double isLeft(Point P0, Point P1, Point P2)
        {
            double abc = ((P1.X - P0.X) * (P2.Y - P0.Y) - (P2.X - P0.X) * (P1.Y - P0.Y));
            return abc;
        }

        private bool PointInFences(Point pnt1, Point[] fencePnts)
        {
            int wn = 0, j = 0; //wn 计数器 j第二个点指针
            for (int i = 0; i < fencePnts.Length; i++)
            {//开始循环
                if (i == fencePnts.Length - 1)
                    j = 0;//如果 循环到最后一点 第二个指针指向第一点
                else
                    j = j + 1; //如果不是 ，则找下一点


                if (fencePnts[i].Y <= pnt1.Y) // 如果多边形的点 小于等于 选定点的 Y 坐标
                {
                    if (fencePnts[j].Y > pnt1.Y) // 如果多边形的下一点 大于于 选定点的 Y 坐标
                    {
                        if (isLeft(fencePnts[i], fencePnts[j], pnt1) > 0)
                        {
                            wn++;
                        }
                    }
                }
                else
                {
                    if (fencePnts[j].Y <= pnt1.Y)
                    {
                        if (isLeft(fencePnts[i], fencePnts[j], pnt1) < 0)
                        {
                            wn--;
                        }
                    }
                }
            }
            if (wn == 0)
                return false;
            else
                return true;
        }
    }
}
