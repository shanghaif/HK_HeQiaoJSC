using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using HaikanTeachingProcess;
using log4net;
using WindowsService_Tcp_fluviometer.Data;

namespace WindowsService_Tcp_fluviometer
{
    public partial class Service1 : ServiceBase
    {

        private static ILog log = LogManager.GetLogger(typeof(Service1));
        private HQContext context1;
        private HQContext context2;
        private TcpListener listener1;
        private TcpListener listener2;
        private DateTime lastSentTime1 = DateTime.Now;
        private long count1 = 0;
        private double lastNum1 = 0;
        private DateTime lastSentTime2 = DateTime.Now;
        private long count2 = 0;
        private double lastNum2 = 0;

        public Service1()
        {
            InitializeComponent();
        }
        //public void OnStart()
        protected override void OnStart(string[] args)
        {


            ThreadStart threadStart = new ThreadStart(TCP1);//通过ThreadStart委托告诉子线程执行什么方法　　
            Thread thread = new Thread(threadStart);
            thread.Start();//启动新线程
            log.Info("tcp1启动");

            ThreadStart threadStart2 = new ThreadStart(TCP2);//通过ThreadStart委托告诉子线程执行什么方法　　
            Thread thread2 = new Thread(threadStart2);
            thread2.Start();//启动新线程
            log.Info("tcp2启动");


        }

        protected override void OnStop()
        {

            context1.Dispose();
            context2.Dispose();
            listener1.Stop();
            listener2.Stop();
            log.Info("服务关闭");
            //try
            //{
            //    System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName("WindowsService_Tcp_fluviometer");
            //    foreach (System.Diagnostics.Process p in ps)
            //    {
            //        log.Info("对进程进行关闭" + p.ProcessName + "/" + p.Id);
            //        p.Kill();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    log.Info("关闭进程出差：" + ex.Message);
            //}

        }

        public void TCP1()
        {
            context1 = new HQContext();
            try
            {

                string message = "当前水位：";
                int port = 33300;
                IPAddress localAddr = IPAddress.Parse("10.172.33.4");
                listener1 = new TcpListener(localAddr, port);
                listener1.Start();
                log.Info("监听1开启");
                byte[] bytes = new byte[500];
                string data = null;
                while (true)
                {
                    var date = DateTime.Now;
                    Console.WriteLine(date);
                    TcpClient client = listener1.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    log.Info("TCP1Connected!");
                    data = null;
                    NetworkStream stream = client.GetStream();

                    int i;
                    if (stream.CanRead)
                    {
                        log.Info("stream1可读");
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            log.Info("接受报文1");
                            Console.WriteLine("字节数:" + i);
                            var str = "";
                            Console.WriteLine(ToHex(bytes, 0, 1));
                            Console.WriteLine(ToHex(bytes, 2, 2));
                            Console.WriteLine(ToHex(bytes, 3, 7));
                            Console.WriteLine(ToHex(bytes, 8, 9));
                            Console.WriteLine(ToHex(bytes, 10, 10));
                            Console.WriteLine(ToHex(bytes, 11, 12));
                            Console.WriteLine("----------------------------");
                            Console.WriteLine(ToHex(bytes, 13, 13));
                            Console.WriteLine("******************************");
                            Console.WriteLine(ToHex(bytes, 14, 15));  //1
                            Console.WriteLine(ToBCD(bytes, 16, 21));  //2


                            if (i > 25)
                            {
                                Console.WriteLine(ToHex(bytes, 22, 23));  //3
                                Console.WriteLine(ToHex(bytes, 24, 28));  //4
                                Console.WriteLine(ToHex(bytes, 29, 29));  //5
                                Console.WriteLine(ToHex(bytes, 30, 31));  //6
                                Console.WriteLine(ToBCD(bytes, 32, 36));  //7
                                Console.WriteLine(ToHex(bytes, 37, 38));  //8
                                Console.WriteLine(ToHex(bytes, 39, 42));  //9
                                Console.WriteLine(ToHex(bytes, 43, 44));  //10
                                Console.WriteLine(ToHex(bytes, 45, 47));  //11
                                Console.WriteLine(ToHex(bytes, 48, 49));  //12
                                Console.WriteLine(ToHex(bytes, 50, 52));  //13
                                Console.WriteLine(ToHex(bytes, 53, 54));  //14
                                Console.WriteLine(ToHex(bytes, 55, 56));  //15


                                var dtbdc = (DateTime.Now.Year / 100).ToString() + ToBCD(bytes, 32, 36) + "00";
                                log.Info("监测时间1：" + dtbdc);

                                var dateTime = DateTime.Now;
                                var checkToTime = DateTime.TryParseExact(dtbdc, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out dateTime);
                                if (checkToTime)
                                {
                                    var sdatetime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                                    log.Info("监测时间1转换：" + sdatetime);

                                    var sw = ToHex(bytes, 39, 42);
                                    //var sw = "0000750";
                                    double num = 0;
                                    //if (sw[sw.Length - 1] == '0')
                                    //{
                                    //    num = double.Parse(sw);
                                    //    num = num / 100;
                                    //}
                                    //else
                                    //{
                                    num = double.Parse(sw);
                                    num = num / 1000;
                                    Console.WriteLine("水位：" + num);
                                    log.Info("水位1：" + num);
                                    var checkinfo = "";
                                    if (num == 9999.999)
                                    {
                                        checkinfo = "大于";
                                        num = 2.5;
                                    }
                                    else
                                    {
                                        checkinfo = "";
                                    }
                                    //}
                                    sw = num.ToString();
                                    var ljsw = (double)context1.SystemSetting.First(x => x.ID == 1).WaterLevelCriticalPoint;
                                    if (num >= ljsw)
                                    {
                                        log.Info("count1:" + count1 + "/" + "lastNum1:" + lastNum1 + "/" + "lastSentTime1:" + lastSentTime1.ToString("yyyy-MM-dd HH:mm:ss"));

                                        if (count1 >= 0 && lastNum1 < ljsw)
                                        {
                                            log.Info("<警告！中鑫村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            //YunSendMsg.SendMsg("15868894638", "警告！中鑫村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            YunSendMsg.SendMsg("16657110023", "警告！中鑫村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            YunSendMsg.SendMsg("18100196818", "警告！中鑫村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            count1++;
                                            lastNum1 = num;
                                            lastSentTime1 = DateTime.Now;
                                        }
                                        else if (count1 > 0 && lastNum1 > ljsw && (DateTime.Now - lastSentTime1).Minutes >= 29)
                                        {
                                            log.Info(">警告！中鑫村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            //YunSendMsg.SendMsg("15868894638", "警告！中鑫村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            YunSendMsg.SendMsg("16657110023", "警告！中鑫村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            YunSendMsg.SendMsg("18100196818", "警告！中鑫村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            count1++;
                                            lastNum1 = num;
                                            lastSentTime1 = DateTime.Now;
                                        }
                                        else
                                        {
                                            lastNum1 = num;
                                        }

                                    }
                                    lastNum1 = num;
                                    num = 0;
                                    Console.WriteLine(sw);

                                    //if(sw)
                                    var cp = ToHex(bytes, 45, 47);
                                    num = double.Parse(cp);
                                    num = num / 10;

                                    HeDaowater heDaowater = new HeDaowater()
                                    {
                                        HeDaowaterUuid = Guid.NewGuid(),
                                        HeDaowaterAddress = ToHex(bytes, 24, 28) + "/" + TelemetryStationType(ToHex(bytes, 29, 29)),
                                        HeDaowaterTime = sdatetime,
                                        HeDaoHeDaowaterSw = sw,
                                        IsDeleted = 0,
                                        AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                        AddPeople = "TCP",
                                        CurrentPrecipitation = (decimal?)num,

                                    };
                                    if (!context1.HeDaowater.Any(x => x.HeDaowaterAddress == heDaowater.HeDaowaterAddress && x.HeDaowaterTime == sdatetime && x.IsDeleted == 0))
                                    {
                                        context1.HeDaowater.Add(heDaowater);
                                        context1.SaveChanges();
                                    }
                                }





                            }





                            Console.WriteLine("******************************");
                            Console.WriteLine(ToHex(bytes, i - 3, i - 3));
                            Console.WriteLine("----------------------------");
                            //Console.WriteLine(Encoding.ASCII.GetString(bytes,13,1));
                            //Console.WriteLine(Encoding.ASCII.GetString(bytes,22,1));
                            Console.WriteLine(str);
                            if (i > 25)
                            {
                                data = ToHex(bytes, 0, 15) + ToBCD(bytes, 16, 21) + ToHex(bytes, 22, 31) + ToBCD(bytes, 32, 36) + ToHex(bytes, 37, i - 1);
                            }
                            else
                            {
                                data = ToHex(bytes, 0, 15) + ToBCD(bytes, 16, 21) + ToHex(bytes, 21, i - 1);
                            }

                            Console.WriteLine("Received:" + data);
                            log.Info("Received:" + data);
                            data = data.ToUpper();

                            //byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                            //// Send back a response.
                            //stream.Write(msg, 0, msg.Length);
                            //Console.WriteLine("Sent: {0}", data);
                        }
                    }
                    else
                    {
                        log.Info("stream1无法读,");
                        log.Info(listener1.Server.Connected);
                        log.Info(listener1.Server.Blocking);
                        //stream.Close();
                        //client.Close();
                        //client= listener1.AcceptTcpClient();
                        //stream = client.GetStream();
                    }

                    //client.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                log.Info("监听1错误：" + ex.Message);
                listener1.Stop();
            }
            finally
            {
                //listener1.Stop();
                listener1 = null;
                GC.Collect();
                log.Info("重启TCP1");
                TCP1();
            }
        }
        public void TCP2()
        {
            context2 = new HQContext();

            try
            {

                string message = "";
                int port = 33320;
                IPAddress localAddr = IPAddress.Parse("10.172.33.4");
                listener2 = new TcpListener(localAddr, port);
                listener2.Start();
                log.Info("监听2开启");

                byte[] bytes = new byte[500];
                string data = null;
                while (true)
                {
                    log.Info("while2(true)");
                    var date = DateTime.Now;
                    Console.WriteLine(date);
                    TcpClient client = listener2.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    log.Info("tcp2Connected!");

                    data = null;
                    NetworkStream stream = client.GetStream();

                    int i;
                    if (stream.CanRead)
                    {
                        log.Info("stream2可读");
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            log.Info("接受报文2");
                            Console.WriteLine("字节数:" + i);
                            var str = "";
                            Console.WriteLine(ToHex(bytes, 0, 1));
                            Console.WriteLine(ToHex(bytes, 2, 2));
                            Console.WriteLine(ToHex(bytes, 3, 7));
                            Console.WriteLine(ToHex(bytes, 8, 9));
                            Console.WriteLine(ToHex(bytes, 10, 10));
                            Console.WriteLine(ToHex(bytes, 11, 12));
                            Console.WriteLine("----------------------------");
                            Console.WriteLine(ToHex(bytes, 13, 13));
                            Console.WriteLine("******************************");
                            Console.WriteLine(ToHex(bytes, 14, 15));  //1
                            Console.WriteLine(ToBCD(bytes, 16, 21));  //2


                            if (i > 25)
                            {
                                Console.WriteLine(ToHex(bytes, 22, 23));  //3
                                Console.WriteLine(ToHex(bytes, 24, 28));  //4
                                Console.WriteLine(ToHex(bytes, 29, 29));  //5
                                Console.WriteLine(ToHex(bytes, 30, 31));  //6
                                Console.WriteLine(ToBCD(bytes, 32, 36));  //7
                                Console.WriteLine(ToHex(bytes, 37, 38));  //8
                                Console.WriteLine(ToHex(bytes, 39, 42));  //9
                                Console.WriteLine(ToHex(bytes, 43, 44));  //10
                                Console.WriteLine(ToHex(bytes, 45, 47));  //11
                                Console.WriteLine(ToHex(bytes, 48, 49));  //12
                                Console.WriteLine(ToHex(bytes, 50, 52));  //13
                                Console.WriteLine(ToHex(bytes, 53, 54));  //14
                                Console.WriteLine(ToHex(bytes, 55, 56));  //15


                                var dtbdc = (DateTime.Now.Year / 100).ToString() + ToBCD(bytes, 32, 36) + "00";
                                log.Info("监测时间2：" + dtbdc);

                                var dateTime = DateTime.Now;
                                var checkToTime = DateTime.TryParseExact(dtbdc, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out dateTime);
                                //var dateTime = DateTime.ParseExact(dtbdc, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss");
                                if (checkToTime)
                                {
                                    var sdatetime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                                    log.Info("监测时间2转换：" + sdatetime);

                                    var sw = ToHex(bytes, 39, 42);
                                    //var sw = "0000750";
                                    double num = 0;
                                    //if (sw[sw.Length - 1] == '0')
                                    //{
                                    //    num = double.Parse(sw);
                                    //    num = num / 100;
                                    //}
                                    //else
                                    //{
                                    num = double.Parse(sw);
                                    num = num / 1000;
                                    Console.WriteLine("水位：" + num);
                                    log.Info("水位2：" + num);
                                    var checkinfo = "";
                                    if (num == 9999.999)
                                    {
                                        checkinfo = "大于";
                                        num = 2.5;
                                    }
                                    else
                                    {
                                        checkinfo = "";
                                    }
                                    //}
                                    sw = num.ToString();
                                    var ljsw = (double)context2.SystemSetting.First(x => x.ID == 1).WaterLevelCriticalPoint;
                                    if (num >= ljsw)
                                    {
                                        log.Info("count2:" + count2 + "/" + "lastNum2:" + lastNum2 + "/" + "lastSentTime2:" + lastSentTime2.ToString("yyyy-MM-dd HH:mm:ss"));
                                        if (count2 >= 0 && lastNum2 < ljsw)
                                        {
                                            log.Info("<警告！秀溪村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            //YunSendMsg.SendMsg("15868894638", "警告！秀溪村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            YunSendMsg.SendMsg("16657110023", "警告！秀溪村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            YunSendMsg.SendMsg("18100196818", "警告！秀溪村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            count2++;
                                            lastNum2 = num;
                                            lastSentTime2 = DateTime.Now;
                                        }
                                        else if (count2 > 0 && lastNum2 > ljsw && (DateTime.Now - lastSentTime2).Minutes >= 29)
                                        {
                                            log.Info(">警告！秀溪村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            //YunSendMsg.SendMsg("15868894638", "警告！秀溪村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            YunSendMsg.SendMsg("16657110023", "警告！秀溪村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            YunSendMsg.SendMsg("18100196818", "警告！秀溪村当前水位：" + checkinfo + sw + " (m),已超过临界点");
                                            count2++;
                                            lastNum2 = num;
                                            lastSentTime2 = DateTime.Now;
                                        }
                                        else
                                        {
                                            lastNum2 = num;
                                        }
                                    }
                                    lastNum2 = num;
                                    num = 0;
                                    Console.WriteLine(sw);
                                    var cp = ToHex(bytes, 45, 47);
                                    num = double.Parse(cp);
                                    num = num / 10;

                                    HeDaowater heDaowater = new HeDaowater()
                                    {
                                        HeDaowaterUuid = Guid.NewGuid(),
                                        HeDaowaterAddress = ToHex(bytes, 24, 28) + "/" + TelemetryStationType(ToHex(bytes, 29, 29)),
                                        HeDaowaterTime = sdatetime,
                                        HeDaoHeDaowaterSw = sw,
                                        IsDeleted = 0,
                                        AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                        AddPeople = "TCP",
                                        CurrentPrecipitation = (decimal?)num,

                                    };
                                    if (!context2.HeDaowater.Any(x => x.HeDaowaterAddress == heDaowater.HeDaowaterAddress && x.HeDaowaterTime == sdatetime && x.IsDeleted == 0))
                                    {
                                        context2.HeDaowater.Add(heDaowater);
                                        context2.SaveChanges();
                                    }
                                }





                            }





                            Console.WriteLine("******************************");
                            Console.WriteLine(ToHex(bytes, i - 3, i - 3));
                            Console.WriteLine("----------------------------");
                            //Console.WriteLine(Encoding.ASCII.GetString(bytes,13,1));
                            //Console.WriteLine(Encoding.ASCII.GetString(bytes,22,1));
                            Console.WriteLine(str);
                            if (i > 25)
                            {
                                data = ToHex(bytes, 0, 15) + ToBCD(bytes, 16, 21) + ToHex(bytes, 22, 31) + ToBCD(bytes, 32, 36) + ToHex(bytes, 37, i - 1);
                            }
                            else
                            {
                                data = ToHex(bytes, 0, 15) + ToBCD(bytes, 16, 21) + ToHex(bytes, 21, i - 1);
                            }

                            Console.WriteLine("Received:" + data);
                            log.Info("Received:" + data);

                            data = data.ToUpper();

                            //byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                            //// Send back a response.
                            //stream.Write(msg, 0, msg.Length);
                            //Console.WriteLine("Sent: {0}", data);
                        }
                        //client.Close();

                    }
                    else
                    {
                        log.Info("stream2不可读");
                        log.Info(listener2.Server.Connected);
                        log.Info(listener2.Server.Blocking);
                        //stream.Close();
                        //client.Close();
                        //client = listener2.AcceptTcpClient();
                        //stream = client.GetStream();
                    }
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                log.Info("监听2错误：" + ex.Message);
                listener2.Stop();

            }
            finally
            {
                //listener2.Stop();
                listener2 = null;
                GC.Collect();
                log.Info("重启TCP2");
                TCP2();
            }
        }


        public static string ToHex(byte[] arr, int start, int end)
        {
            var str = "";
            for (int i = start; i <= end; i++)
            {
                var s = string.Format("{0:X}", arr[i]);
                if (s.Length == 1)
                {
                    s = "0" + s;
                }
                str += s;
            }
            return str;
        }


        public static string ToBCD(byte[] arr, int start, int end)
        {
            var str = "";
            for (int i = start; i <= end; i++)
            {
                var num = ConvertBCDToInt(arr[i]).ToString();
                if (num.Length == 1)
                {
                    num = "0" + num;
                }


                str += num;
            }

            return str;
        }

        public static int ToInt(byte[] arr, int start, int end)
        {
            int num = 0;
            byte[] data = new byte[end - start + 1];
            for (int i = start; i <= end; i++)
            {

                data[num] = arr[i];
                num++;
            }

            return BitConverter.ToInt32(data, 0);
        }


        public static byte ConvertBCDToInt(byte b)
        {
            //高四位  
            byte b1 = (byte)((b >> 4) & 0xF);
            //低四位  
            byte b2 = (byte)(b & 0xF);

            return (byte)(b1 * 10 + b2);


        }

        public static string TelemetryStationType(string num)
        {
            var str = "未知";
            if (num == "50")
            {
                str = "降水";
            }
            else if (num == "48")
            {
                str = "河道";
            }
            else if (num == "4B")
            {
                str = "水库(湖泊)";
            }
            else if (num == "5A")
            {
                str = "闸坝";
            }
            else if (num == "44")
            {
                str = "泵站";
            }
            else if (num == "54")
            {
                str = "潮汐";
            }
            return str;
        }
    }
}
