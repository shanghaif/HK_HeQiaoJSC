using com.sun.org.apache.xpath.@internal;
using com.sun.tools.@internal.xjc.reader.gbind;
using HaikanYixueyuanDog.DataClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils
{
    //鉴权8900
    public static class Authentication8900
    {
        class publicKey8900
        {
            public string nonce { get; set; }
            public string publicKey { get; set; }
            public string success { get; set; }
        }
        class Token8900
        {
            /// <summary>
            /// 
            /// </summary>
            public string cmsIp { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string cmsPort { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string loginName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string orgCode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string publicKey { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string success { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string token { get; set; }
        }
        /// <summary>
        /// 获取token
        /// </summary>
        public static ResponseData getToken(string loginName,string password,string address)
        {
            ResponseData response = new ResponseData();

            string url_PublicKey = address+"/WPMS/getPublicKey";
            try
            {
                var postdata = new
                {
                    loginName = loginName
                };
                var jsonstr = JsonConvert.SerializeObject(postdata);
                publicKey8900 keydata = JsonConvert.DeserializeObject<publicKey8900>(PostGetRequest.HttpPost(url_PublicKey,jsonstr));
                if(keydata.success=="true")
                {
                    var publicKey = keydata.publicKey;
                    //rsa加密
                    var secretkey = RSACryption.RSAEncrypt(publicKey, password);
                    var postdata1 = new
                    {
                        loginName = loginName,
                        loginPass = secretkey
                    };
                    var jsonstr1 = JsonConvert.SerializeObject(postdata1);
                    string url_Token = address + "/WPMS/login";

                    Token8900 tokendata = JsonConvert.DeserializeObject<Token8900>(PostGetRequest.HttpPost(url_Token, jsonstr1));

                    if (tokendata.success == "true")
                    {
                        response.code = "200";
                        response.msg = "sucess";
                        response.data = tokendata.token;
                    }
                    else
                    {
                        response.code = "500";
                        response.msg = "获取token失败";
                    }
                }
                else
                {
                    response.code = "500";
                    response.msg = "获取publicKey失败";
                }
                
            }
            catch (Exception e)
            {
                response.code = "400";
                response.msg = e.Message;
            }
            return response;
         }
    }
}
