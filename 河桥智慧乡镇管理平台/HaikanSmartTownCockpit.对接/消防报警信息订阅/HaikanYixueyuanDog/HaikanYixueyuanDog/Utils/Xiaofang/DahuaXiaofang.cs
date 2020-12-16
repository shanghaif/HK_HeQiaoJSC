using Haikan.DebugTools;
using HaikanYixueyuanDog.DataClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils.Xiaofang
{
    public static class DahuaXiaofang
    {
        public static ResponseData GetToken(string appId,string secret)
        {
            ResponseData rsp = new ResponseData();
            string charset = "UTF-8";
            string url_PublicKey = "http://112.17.112.170:28100/IFCSI/public/getPublicKey";
            try
            {
                var timeStamp = Helper.ConvertDateTimeToInt(DateTime.Now).ToString();

                PublickeyRoot keydata = JsonConvert.DeserializeObject<PublickeyRoot>(PostGetRequest.HttpGet(url_PublicKey, "utf-8"));
                var publicKey = keydata.data[0].publicKey;

                #region secret 加密处理
                var rsapublicKey = Helper.Base64Decode(publicKey, "UTF-8");
                appId = "duijie";
                var secretencrypt = RSACryption.RsaEncryptJava(publicKey, "123456");
                #endregion
                //获取token
                string url_token = "http://112.17.112.170:28100/IFCSI/public/token";
                var postdata = new
                {
                    appId = appId,
                    secret = secretencrypt,
                    publicKey = publicKey,
                    timeStamp = timeStamp
                };
                var jsonstr = JsonConvert.SerializeObject(postdata);
                var jsontoken = PostGetRequest.HttpPostJson(url_token, jsonstr);
                TokenDataRoot tokeninfo = JsonConvert.DeserializeObject<TokenDataRoot>(jsontoken);

                var token = tokeninfo.data.token;
                if (token != null)
                {
                    rsp.code = "200";
                    rsp.msg = "成功";
                    rsp.data = token;
                }
                else
                {
                    rsp.code = "404";
                    rsp.msg = tokeninfo.ret_msg;
                    LogHelper.WriteLog(typeof(Form1), "遇到错误！" + tokeninfo.ret_msg);
                }
            }
            catch (Exception ex)
            {
                rsp.code = "404";
                rsp.msg = ex.Message;
                LogHelper.WriteLog(typeof(Form1), "遇到错误！" + ex.Message);
            }
            return rsp;
        }
    }
}
