using com.hikvision.artemis.sdk;
using com.hikvision.artemis.sdk.config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class SKVideoController : ApiController
    {
        public SKVideoController()
        {
            ArtemisConfig.host = "172.18.19.210"; // 代理API网关nginx服务器ip端口
            ArtemisConfig.appKey = "20979065";  // 秘钥appkey
            ArtemisConfig.appSecret = "AoGMDsl8X1r4OK2mhiFP";// 秘钥appSecret
        }
        /// <summary>
        /// 雪亮工程视频流
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public object VideoUrl(string indexCode)
        {
            //ArtemisConfig.host = "10.172.33.2"; // 代理API网关nginx服务器ip端口
            //ArtemisConfig.appKey = "27628571";  // 秘钥appkey
            //ArtemisConfig.appSecret = "TJzjgMqgM41EezwD36aV";// 秘钥appSecret
            //获取所有组织
            string videoP = "/artemis/api/video/v1/cameras/previewURLs";
            string body = "{\"cameraIndexCode\": \"" + indexCode + "\",\"streamType\": 0,\"protocol\": \"hls\",\"transmode\": 1,\"expand\": \"transcode=0\"}";
            java.util.Map path = new java.util.HashMap();
            path.put("https://", videoP);
            string result = ArtemisHttpUtil.doPostStringArtemis(path, body, null, "*/*", "application/json", null);
            var vdata = JsonConvert.DeserializeObject<SKVideo>(result);
            return Ok(new { vdata, indexCode });


        }


        /// <summary>
        /// 视频云台控制
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public object ToVideoAction(string cameraIndexCode, int number, string command)
        {
            if (string.IsNullOrEmpty(cameraIndexCode) || (number != 1 && number != 0) || string.IsNullOrEmpty(command))
            {
                return ("参数有误：cic=" + cameraIndexCode + ";num=" + number + ";comm=" + command);
            }
            string videoA = "/artemis/api/video/v1/ptz/controlling";
            string body = "{\"cameraIndexCode\": \"" + cameraIndexCode + "\",\"action\":" + number + ",\"command\": \"" + command + "\",\"speed\": 20}";
            java.util.Map path = new java.util.HashMap();
            path.put("https://", videoA);
            string result = ArtemisHttpUtil.doPostStringArtemis(path, body, null, "*/*", "application/json", null);
            var data = JsonConvert.DeserializeObject<SKVideoActionResult>(result);



            return Ok(new { data, cameraIndexCode, number, command, result });
        }
    }
}

