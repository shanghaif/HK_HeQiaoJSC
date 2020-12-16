using System;
using System.Collections.Generic;
using System.Configuration;
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
using Haikan.SmartTownCockpitCore;
using Haikan2.DebugTools;
using HaikanSmartTownCockpit.WatchDog.Ezviz.AccessToken;
using HaikanSmartTownCockpit.WatchDog.Ezviz.Device;
using HaikanSmartTownCockpit.WatchDog.Ezviz.Devices;
using HaikanSmartTownCockpit.WatchDog.Model2;
using HaikanSmartTownCockpit.WatchDog.Model3;
using HaikanSmartTownCockpit.WatchDog.Model3.JianKXQ;
using HaikanSmartTownCockpit.WatchDog.Region;
using HaikanSmartTownCockpit.WatchDog.sqjz.model2;
using HaikanSmartTownCockpit.WatchDog.sqjz.model3;
using HaiKanTaskHousekeeperCore.Data;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Data = HaikanSmartTownCockpit.WatchDog.Ezviz.Devices.Data;

namespace HaikanSmartTownCockpit.WatchDog
{
    /// <summary>
    /// 主要完成工作:
    /// 1.监控信息同步
    /// 2.地址库信息获取
    /// 172服务器
    /// 为了监控对接,我们开发了两个看门狗.这是其中一个
    /// </summary>
    public partial class FrmMainServer : Form
    {
        #region 实例化对象
       // string indexCode = null;

        List<Model3.List> jiankong = new List<Model3.List>();

        private List<Data> _sewage = new List<Data>();

        private string _ezvizAppKey = "5ee9c68b4983424b9d2efa43d25244c7";
        private string _ezvizSecret = "8175be5f10f7f94eb8053430e52a9499";
        private string _ezvizAccessToken = ConfigurationManager.AppSettings.Get("token");

        //实例化Timer类，设置间隔时间为；  
        private readonly System.Timers.Timer _t1 = new System.Timers.Timer(1000); // 同步地址库,同步监控
        //private readonly System.Timers.Timer _t2 = new System.Timers.Timer(5000); // 大气防治,pm2.5对接[因为服务器不能访问外网,放到server2]
        private readonly System.Timers.Timer _t3 = new System.Timers.Timer(1000); // 污水监控对接

        HaikanHeQiaoContext _dbContext = new HaikanHeQiaoContext();
        HaikanHeQiaoContext _dbContext1 = new HaikanHeQiaoContext();
        HaikanHeQiaoContext _dbContext2 = new HaikanHeQiaoContext();
        #endregion

        /// <summary>
        /// 构造
        /// </summary>
        public FrmMainServer()
        {
            ArtemisConfig.host = "172.18.19.210"; // 代理API网关nginx服务器ip端口
            ArtemisConfig.appKey = "20979065"; // 秘钥appkey
            ArtemisConfig.appSecret = "AoGMDsl8X1r4OK2mhiFP"; // 秘钥appSecret
            
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
        }

        /// <summary>
        /// 页面加载,初始化定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            LogHelper.WriteLog(typeof(FrmMainServer), "看门狗程序启动！");

            _t1.Elapsed += timerDo_Tick; // 同步地址库,同步监控
            //_t2.Elapsed += timerDo4_Tick; // 大气防治,pm2.5对接
            _t3.Elapsed += TimerDo5_Tick; // 污水监控对接

            _t1.AutoReset = true;
            //_t2.AutoReset = true;
            _t3.AutoReset = true;

            _t1.Enabled = true;
            //_t2.Enabled = true;
            _t3.Enabled = true;

            LogHelper.WriteLog(typeof(FrmMainServer), "看门狗程序启动完毕！");
        }

        #region t1:同步地址库,同步监控
        /// <summary>
        /// 同步地址库,同步监控,[每天的2点进行同步]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDo_Tick(object sender, ElapsedEventArgs e)
        {
            var date = DateTime.Now;

            LogHelper.WriteLog(typeof(FrmMainServer), date.ToString("yyyy/MM/dd HH:mm:ss"));

            if (date.Hour == 2 && date.Minute == 0 && date.Second == 0)
            {
                _t1.Enabled = false;
                try
                {
                    LogHelper.WriteLog(typeof(FrmMainServer), "开始同步地址库！");

                    //截断表HomeAddress,清空所有数据,遍历所有村庄,同步所有地址
                    backtoken();

                    _t1.Enabled = true;

                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(FrmMainServer), "同步地址库遇到错误！" + ex.Message);

                }
                finally
                {
                    _t1.Enabled = true;
                }
            }
            else if (date.Second % 5 == 0) // 每5秒同步所有的监控(雪亮工程)
            {
                _t1.Enabled = false;

                LogHelper.WriteLog(typeof(string), "开始进行监控同步！");

                try
                {
                    //同步所有的监控(雪亮工程)
                    Monitoring2();

                    LogHelper.WriteLog(typeof(string), "监控同步结束");
                    
                    _t1.Enabled = true;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(FrmMainServer), "同步监控遇到错误！" + ex.Message);
                }
                finally
                {
                    _t1.Enabled = true;
                }
            }

            LogHelper.WriteLog(typeof(FrmMainServer), "timerDo_Tick程序完毕！");

        }

        /// <summary>
        /// 截断表HomeAddress,清空所有数据,遍历所有村庄
        /// </summary>
        public void backtoken()
        {
            //var code = "0";
            try
            {
                string token  = "b34496fb28fd4ff3b1e4ad94bd4e08a9";

                LogHelper.WriteLog(typeof(string), "截断表HomeAddress,清空所有数据");
                var sql = string.Format("TRUNCATE TABLE HomeAddress");
                _dbContext.Database.ExecuteSqlCommand(sql);

                LogHelper.WriteLog(typeof(string), "遍历所有村庄");
                var vlist = _dbContext.Village.Where(x => x.IsDelete == 0).ToList();
                for (int n = 0; n < vlist.Count; n++)
                {
                    //同步所有地址
                    SyncAddress(vlist[n].VillageName, token);
                }
                
            }
            catch (Exception ex)
            {

                LogHelper.WriteLog(typeof(string), ex.Message);
                LogHelper.WriteLog(typeof(FrmMainServer), "遇到错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 同步所有地址
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="token"></param>
        /// <param name="page"></param>
        public void SyncAddress(string addr, string token, int page = 1)
        {
            LogHelper.WriteLog(typeof(string), "开始查询地址");
            List<HomeAddress> addresses = new List<HomeAddress>();
            string infoUrl = "http://10.33.188.56:8090/addrMatch/addrApi/searchAddr?token=" + token + "&addr=河桥镇" +
                             addr + "&page=" + page + "&limit=9999&fuzzy=false";
            LogHelper.WriteLog(typeof(string), infoUrl);

            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(infoUrl);
            myRequest1.ContentType = "application/json";
            HttpWebResponse myResponse1 = (HttpWebResponse)myRequest1.GetResponse();
            StreamReader reader1 = new StreamReader(myResponse1.GetResponseStream(), Encoding.UTF8);
            string returnJson1 = reader1.ReadToEnd(); //利用StreamReader就可以从响应内容从头读到尾
            var code = myResponse1.StatusCode.ToString();
            reader1.Close();
            myResponse1.Close();
            var data1 = JsonConvert.DeserializeObject<AddressInfo>(returnJson1);
            LogHelper.WriteLog(typeof(string), data1.data.count.ToString());
            LogHelper.WriteLog(typeof(string), "开始循环添加");

            for (int j = 0; j < data1.data.addrList.Count; j++)
            {
                HomeAddress entity = new HomeAddress();
                entity.Address = data1.data.addrList[j].addr;
                entity.Addresscode = data1.data.addrList[j].code;
                entity.town = data1.data.addrList[j].town;
                entity.ccmmunity = data1.data.addrList[j].community;
                entity.squad = data1.data.addrList[j].squad;
                entity.village = data1.data.addrList[j].village;
                entity.szone = data1.data.addrList[j].szone;
                entity.street = data1.data.addrList[j].street;
                entity.door = data1.data.addrList[j].door;
                entity.resregion = data1.data.addrList[j].resregion;
                entity.building = data1.data.addrList[j].building;
                entity.building_num = data1.data.addrList[j].building_num;
                entity.unit = data1.data.addrList[j].unit;
                entity.floor = data1.data.addrList[j].floor;
                entity.lon = new decimal(data1.data.addrList[j].lon);
                entity.lat = new decimal(data1.data.addrList[j].lat);
                entity.HomeAddressUUID = Guid.NewGuid();
                entity.IsDeleted = 0;
                addresses.Add(entity);
                LogHelper.WriteLog(typeof(string), addr + "第" + j + "个entity加入list");
                LogHelper.WriteLog(typeof(string), JsonConvert.SerializeObject(entity));
            }

            _dbContext.HomeAddress.AddRange(addresses);
            _dbContext.SaveChanges();
        }


        /// <summary>
        /// 同步所有的监控(雪亮工程),从程序运行目录下的vinfo.xlsx文件读取指定的监控
        /// </summary>
        public void Monitoring2()
        {
            jiankong = new List<Model3.List>();
            //LogHelper.WriteLog(typeof(string), "vinfo文件路径" + Application.ExecutablePath.ToString() + "/vinfo.xlsx");
            //var file = Application.ExecutablePath.ToString()+ "/vinfo.xlsx";
            var file = "D://vinfo.xlsx";

            DataTable dt = ExselData.ExcelToDataTable(file.ToString(), "Sheet1", true);

            //获取海康平台上所有的监控
            Jiankong();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LogHelper.WriteLog(typeof(string), "进入循环了code:" + dt.Rows[i]["name"]);
                var jkzy = jiankong.Find(x => x.name == dt.Rows[i]["name"].ToString());

                LogHelper.WriteLog(typeof(string), "jkzy的值" + jkzy);

                var cid = dt.Rows[i]["name"].ToString();
                var entity = _dbContext.XlProject.Any(x => x.ShebeiAddress == cid && x.IsDeleted == 0);
                LogHelper.WriteLog(typeof(string), "entity的值:" + entity);
                LogHelper.WriteLog(typeof(string), "开始判断是否有了此数据");

                // 添加
                if (jkzy != null && entity == false) 
                {
                    LogHelper.WriteLog(typeof(string), "开始录入");
                    XlProject xlProject = new XlProject();
                    xlProject.XlProjectUuid = Guid.NewGuid();
                    xlProject.XlShebeiType = dt.Rows[i]["type"].ToString();
                    xlProject.Lat = jkzy.latitude;
                    xlProject.Lon = jkzy.longitude;
                    xlProject.ShebeiAddress = jkzy.name;
                    xlProject.AddPeople = "狗";
                    xlProject.IsDeleted = 0;
                    xlProject.AddTime = DateTime.Now.ToString();
                    xlProject.XlShebeiId = jkzy.cameraIndexCode;
                    xlProject.ShebeiType = GetJianKState(jkzy.cameraIndexCode);
                    xlProject.UrlType = 2;
                    _dbContext.XlProject.Add(xlProject);
                    _dbContext.SaveChanges();
                    LogHelper.WriteLog(typeof(string), "录入一条code:" + jkzy.cameraIndexCode);

                }
                else if (jkzy != null && entity) // 修改
                {
                    var xlp = _dbContext.XlProject.FirstOrDefault(x => x.ShebeiAddress == cid && x.IsDeleted == 0);
                    xlp.XlShebeiId = jkzy.cameraIndexCode;
                    xlp.Lat = jkzy.latitude;
                    xlp.Lon = jkzy.longitude;
                    xlp.ShebeiType = GetJianKState(jkzy.cameraIndexCode);
                    _dbContext.SaveChanges();
                    LogHelper.WriteLog(typeof(string), "修改:" + jkzy.cameraIndexCode);

                }
                else
                {
                    LogHelper.WriteLog(typeof(string), "没有录入");
                }
            }
            jiankong = new List<Model3.List>();
        }

        /// <summary>
        /// 获取海康平台上所有的监控
        /// </summary>
        /// <param name="page"></param>
        public void Jiankong(int page = 1)
        {
            string regionP = "/artemis/api/resource/v1/cameras";
            string body_r = "{\"pageNo\":" + page + ",\"pageSize\": 2000}";
            java.util.Map path_r = new java.util.HashMap();
            path_r.put("https://", regionP);
            string result_r =
                ArtemisHttpUtil.doPostStringArtemis(path_r, body_r, null, "*/*", "application/json", null);
            var region_d = JsonConvert.DeserializeObject<JianKong>(result_r);
            LogHelper.WriteLog(typeof(string), "区域查询code:" + region_d.code);

            jiankong.AddRange(region_d.data.list);

            var region_total = region_d.data.total;
            if ((region_total - page * 2000) % 2000 > 0)
            {
                LogHelper.WriteLog(typeof(string), "开始迭代");
                page++;
                Jiankong(page);
            }
        }

        /// <summary>
        /// 通过海康平台api获取监控在线离线的状态
        /// </summary>
        /// <param name="indexCode"></param>
        /// <returns></returns>
        public string GetJianKState(string indexCode)
        {
            LogHelper.WriteLog(typeof(string), "获取监控状态");
            string onlineP = "/artemis/api/resource/v1/cameras/indexCode";
            string body_o = "{\"cameraIndexCode\": \"" + indexCode + "\"}";
            java.util.Map path_o = new java.util.HashMap();
            path_o.put("https://", onlineP);
            string result_o =
                ArtemisHttpUtil.doPostStringArtemis(path_o, body_o, null, "*/*", "application/json", null);
            var online_d = JsonConvert.DeserializeObject<JianKongXQ>(result_o);
            LogHelper.WriteLog(typeof(string), online_d.ToString());
            LogHelper.WriteLog(typeof(string), "在线状态查询code:" + online_d.code);
            if (online_d.data != null)
            {
                return online_d.data.status == 1 ? "在线" : "离线";
            }
            else
            {
                return "未知";
            }
        }
        #endregion

        #region t2:大气防治,pm2.5对接
        /// <summary>
        /// 大气防治,pm2.5对接,每天只保留第一条数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDo4_Tick(object sender, ElapsedEventArgs e)
        {
            _t1.Enabled = false;
            var date = DateTime.Now;
            LogHelper.WriteLog(typeof(string), date.ToString("yyyy/MM/dd HH:mm:ss"));
            LogHelper.WriteLog(typeof(FrmMainServer), "timerDo4_Tick程序开始！");
            if (date.Hour >= 2)
            {
                try
                {
                    var times = date.ToString("yyyy-MM-dd");
                    var query = _dbContext1.Barometric.FirstOrDefault(x => x.IsDeleted != 1 && x.BarTime == times);
                    if (query == null)
                    {
                        LogHelper.WriteLog(typeof(string), "开始同步PM2.5");
                        HttpWebRequest request =
                        (HttpWebRequest)WebRequest.Create("http://api.help.bj.cn/apis/weather/?id=101210107");
                        request.Method = "GET";
                        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream myResponseStream = response.GetResponseStream();
                        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                        string retString = myStreamReader.ReadToEnd();
                        var respTqpm = JsonConvert.DeserializeObject<TqPm25>(retString);
                        LogHelper.WriteLog(typeof(string), respTqpm.ToString());
                        myStreamReader.Close();
                        myResponseStream.Close();
                   
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
                        _dbContext1.Barometric.Add(lot);
                        _dbContext1.SaveChanges();
                    }

                    _t1.Enabled = true;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(string), "同步PM2.5遇到错误！" + ex.Message);
                    LogHelper.WriteLog(typeof(FrmMainServer), "同步PM2.5遇到错误！" + ex.Message);

                }
                finally
                {
                    _t1.Enabled = true;
                }
            }

            LogHelper.WriteLog(typeof(FrmMainServer), "看门狗程序完毕！");

        }
        #endregion

        #region t3:污水监控对接
        /// <summary>
        /// 污水监控对接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerDo5_Tick(object sender, ElapsedEventArgs e)
        {
            _t3.Enabled = false;
            var date = DateTime.Now;
            LogHelper.WriteLog(typeof(string), date.ToString("yyyy/MM/dd HH:mm:ss"));

            if (date.Minute % 3 == 0&&date.Second==0)
            {
                try
                {
                    LogHelper.WriteLog(typeof(string), "开始同步污水监控");
                    LogHelper.WriteLog(typeof(FrmMainServer), "开始同步污水监控！");
                    GetSewage();
                    _t3.Enabled = true;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(string), "同步污水监控遇到错误！" + ex.Message);
                    LogHelper.WriteLog(typeof(FrmMainServer), "同步污水监控遇到错误！" + ex.Message);

                }
                finally
                {
                    _t3.Enabled = true;
                }
            }
            else
            {
                _t3.Enabled = true;
            }

        }

        /// <summary>
        /// 获取污水监控
        /// </summary>
        public void GetSewage()
        {
            string[] deviceSerials = { "D80900074", "D80900042", "E39123677" };
            for (var i = 0; i < deviceSerials.Length; i++)
            {
                LogHelper.WriteLog(typeof(string), deviceSerials[i]);
                GetEzvizDevice(deviceSerials[i]);
            }
        }

        /// <summary>
        /// 获取单个设备信息
        /// </summary>
        /// <param name="deviceSerial"></param>
        public void GetEzvizDevice(string deviceSerial)
        {
            var postData = "accessToken=" + _ezvizAccessToken + "&deviceSerial=" + deviceSerial;
            var device = ToHttp.ToPost<Device>("https://open.ys7.com/api/lapp/device/info", postData);
            LogHelper.WriteLog(typeof(string), JsonConvert.SerializeObject(device));
            if (device.code == "200")
            {
                string status;
                if (device.data.status == 0)
                {
                    status = "离线";
                }
                else if (device.data.status == 1)
                {
                    status = "在线";
                }
                else
                {
                    status = "未知";
                }
                if (!_dbContext2.XlProject.Any(x => x.XlShebeiId == device.data.deviceSerial && x.IsDeleted == 0))
                {
                    XlProject xlProject = new XlProject();
                    xlProject.AddPeople = "狗";
                    xlProject.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    xlProject.XlProjectUuid = Guid.NewGuid();
                    xlProject.XlShebeiId = device.data.deviceSerial;
                    xlProject.XlShebeiType = "五水共治污水监控";
                    xlProject.IsDeleted = 0;
                    xlProject.ShebeiAddress = device.data.deviceName;
                    xlProject.ShebeiType = status;
                    xlProject.UrlType = 3;
                    _dbContext2.XlProject.Add(xlProject);
                    LogHelper.WriteLog(typeof(string), "添加" + device.data.deviceName);
                }
                else
                {
                    var xlp = _dbContext2.XlProject.FirstOrDefault(x => x.XlShebeiId == device.data.deviceSerial && x.IsDeleted == 0);
                    xlp.ShebeiAddress = device.data.deviceName;
                    xlp.ShebeiType = status;
                    xlp.UrlType = 3;
                    LogHelper.WriteLog(typeof(string), "修改" + device.data.deviceName);

                }
                _dbContext2.SaveChanges();

            }
            else if (device.code == "10002")
            {
                LogHelper.WriteLog(typeof(string), "token失效");
                GetEzvizAccessToken();
                GetEzvizDevice(deviceSerial);
            }
            else
            {
                LogHelper.WriteLog(typeof(string), device.code + "|" + device.msg);
            }
        }

        /// <summary>
        /// ezviz平台获取token
        /// </summary>
        public void GetEzvizAccessToken()
        {
            var postData = "appKey=" + _ezvizAppKey + "&appSecret=" + _ezvizSecret;
            var token = ToHttp.ToPost<AccessToken>("https://open.ys7.com/api/lapp/token/get", postData);
            if (token.code == "200")
            {
                _ezvizAccessToken = token.data.accessToken;
                ConfigurationManager.AppSettings.Set("token", token.data.accessToken);
            }
            else
            {
                LogHelper.WriteLog(typeof(string), token.code + "|" + token.msg);
                throw new Exception(token.code + " | " + token.msg);
            }
        }

        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <param name="pageIndex">页数</param>
        public void GetEzvizDevices(int pageIndex)
        {
            var postData = "accessToken=" + _ezvizAccessToken + "&pageStart=" + pageIndex + "&pageSize=50";
            var devices = ToHttp.ToPost<Devices>("https://open.ys7.com/api/lapp/device/list", postData);
            if (devices.code == "200")
            {
                _sewage.AddRange(devices.data);
                if ((devices.page.total - (pageIndex + 1) * 50) % 50 > 0)
                {
                    pageIndex++;
                    GetEzvizDevices(pageIndex);
                }

            }
            else if (devices.code == "10002")
            {
                GetEzvizAccessToken();
                GetEzvizDevices(0);
            }
            else
            {
                LogHelper.WriteLog(typeof(string), devices.code + "|" + devices.msg);
            }
        }
        #endregion

    }
}
