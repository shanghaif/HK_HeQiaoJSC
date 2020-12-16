using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace WebApplication2.Util
{
    public class ToHttp
    {

        /// <summary>
        /// Post请求 application/x-www-form-urlencoded
        /// </summary>
        /// <param name="url">为请求地址</param>
        /// <param name="postData">请求内容例如："key1=value1&key2=value2&key3=value3"</param>
        /// <returns></returns>
        public static T ToPost<T>(string url, string postData)
        {
            T result = default;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Method = "POST";

                req.ContentType = "application/x-www-form-urlencoded";

                //req.Timeout = 800; //请求超时时间

                byte[] data = Encoding.UTF8.GetBytes(postData);

                req.ContentLength = data.Length;

                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);

                    reqStream.Close();
                }

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                Stream stream = resp.GetResponseStream();

                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var r = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<T>(r);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return result;
        }
    }
}