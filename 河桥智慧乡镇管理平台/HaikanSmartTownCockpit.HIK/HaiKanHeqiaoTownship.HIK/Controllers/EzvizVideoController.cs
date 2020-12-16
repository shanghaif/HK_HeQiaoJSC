using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using WebApplication2.Models.Ezviz.AccessToken;
using WebApplication2.Models.Ezviz.LiveVideo;
using WebApplication2.Models.Ezviz.PTZ;
using WebApplication2.Util;

namespace WebApplication2.Controllers
{
    /// <summary>
    /// ezviz监控操作接口
    /// </summary>
    public class EzvizVideoController : ApiController
    {
        /// <summary>
        /// 请求的token
        /// </summary> 
        public static string _ezvizAccessToken;
        private string _ezvizAppKey = "5ee9c68b4983424b9d2efa43d25244c7";
        private string _ezvizSecret = "8175be5f10f7f94eb8053430e52a9499";
        //private List<Models.Ezviz.LiveVideo.Data> _liveList = new List<Models.Ezviz.LiveVideo.Data>();
        private string _deviceSerial;

        public EzvizVideoController()
        {
            _ezvizAccessToken = WebConfigurationManager.AppSettings.Get("token");
        }

        /// <summary>
        /// 获取污水监控播放路径
        /// </summary>
        /// <param name="deviceSerial">监控编号</param>
        /// <returns></returns>
        [HttpGet]
        public object GetVideoUrl(string deviceSerial)
        {
            _deviceSerial = deviceSerial;
            var video = GetEzvizLiveList(0);
            return Ok(new { video, deviceSerial });
        }

        /// <summary>
        /// 云台控制接口
        /// </summary>
        /// <param name="deviceSerial"></param>
        /// <param name="channelNo"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        [HttpGet]
        public object ToVideoAction(string deviceSerial,int channelNo,int direction)
        {
            return ToEzvizPTZ(deviceSerial, channelNo, direction);
        }

        /// <summary>
        /// 停止云台操作
        /// </summary>
        /// <param name="deviceSerial"></param>
        /// <param name="channelNo"></param>
        /// <returns></returns>
        [HttpGet]
        public object StopVideoAction(string deviceSerial, int channelNo)
        {
            return StopEzvizPTZ(deviceSerial, channelNo);
        }


        /// <summary>
        /// ezviz平台获取token
        /// </summary>
        private void GetEzvizAccessToken()
        {
            var postData = "appKey=" + _ezvizAppKey + "&appSecret=" + _ezvizSecret;
            var token = ToHttp.ToPost<AccessToken>("https://open.ys7.com/api/lapp/token/get", postData);
            if (token.code == "200")
            {
                _ezvizAccessToken = token.data.accessToken;
                WebConfigurationManager.AppSettings.Set("token", token.data.accessToken);
                
            }
            else
            {
                throw new Exception("获取token错误:" + token.code + "|" + token.msg);
            }
        }




        /// <summary>
        /// ezviz 获取监控直播列表
        /// </summary>
        /// <param name="pageIndex">页数</param>
        private object GetEzvizLiveList(int pageIndex)
        {
            //var hls = "";
            var postData = "accessToken=" + _ezvizAccessToken + "&pageStart=" + pageIndex + "&pageSize=50";
            var liveVideo = ToHttp.ToPost<LiveVideo>("https://open.ys7.com/api/lapp/live/video/list", postData);
            if (liveVideo.code == "200")
            {
                var index = liveVideo.data.FindIndex(x => x.deviceSerial == _deviceSerial);
                if (index != -1)
                {
                    return liveVideo.data[index];
                }
                else
                {
                    if ((liveVideo.page.total - (pageIndex + 1) * 50) % 50 > 0)
                    {
                        pageIndex++;
                        return GetEzvizLiveList(pageIndex);
                    }
                    else
                    {
                        return null;
                    }
                }
                //_liveList.AddRange(liveVideo.data);


            }
            else if (liveVideo.code == "10002")
            {
                GetEzvizAccessToken();
                return GetEzvizLiveList(0);
            }
            else
            {
                return null;

            }


            

        }


        /// <summary>
        /// 云台控制
        /// </summary>
        /// <param name="deviceSerial"></param>
        /// <param name="channelNo"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private object ToEzvizPTZ(string deviceSerial, int channelNo, int direction)
        {
            
            var postData = "accessToken=" + _ezvizAccessToken + "&deviceSerial=" + deviceSerial + "&channelNo="+ channelNo+ "&direction="+direction+"&speed=1";
            var ptzResult = ToHttp.ToPost<PTZResult>("https://open.ys7.com/api/lapp/device/ptz/start", postData);
            
            if (ptzResult.code == "10002")
            {
                GetEzvizAccessToken();
                ToEzvizPTZ(deviceSerial,channelNo,direction);
            }
           


            return ptzResult;

        }

        /// <summary>
        /// 停止控制
        /// </summary>
        /// <param name="deviceSerial"></param>
        /// <param name="channelNo"></param>
        /// <returns></returns>
        private object StopEzvizPTZ(string deviceSerial,int channelNo)
        {
            var postData = "accessToken=" + _ezvizAccessToken + "&deviceSerial=" + deviceSerial + "&channelNo=" + channelNo;
            var ptzResult = ToHttp.ToPost<PTZResult>("https://open.ys7.com/api/lapp/device/ptz/stop", postData);

            if(ptzResult.code== "10002")
            {
                GetEzvizAccessToken();
                StopEzvizPTZ(deviceSerial, channelNo);
            }

            return ptzResult;

        }

    }
}
