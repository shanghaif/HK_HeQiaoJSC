using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task.app
{
    public class ZWDingHelp
    {
        //public static string MyIp = "153.34.54.40";
        //public static string MyMac = "00-E0-4C-79-03-95";

        public static string Dingdomainname = "https://openplatform.dg-work.cn";
        public static string Appkey = "haikan_rwgj-OW9KAUudSJDE3YJRVz";
        public static string Appsecret = "4uM7oVCH6wVD8paDzHy11Ks9r4Y40E62PVJ4ic2m";
        public static string AgentId = "39633";
        public static string WebUrl = "";
        public static string WorkMsgHead = "";
        public static string tenantId = "39633";
        //public static List<string> Departments = new List<string>();

        private static DateTime Access_tokenStartTime;//AccessToken的获取事件
        private static ZWDingAccessToken AccessTokenNow;//当前的AccessToken
        private static bool IsFirst = true;//是否第一次

        private static readonly long GetDepartmentLimit = 100;//部门人员分页数
        private static readonly long GetDepartmentUserLimit = 100;//部门人员分页数

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        public static ZWDingAccessToken GetAccess_token()
        {
            ZWDingAccessToken model;
            if (IsFirst)
            {
                //第一次重新获取
                model = T_GetAccessToken();
            }
            else
            {
                TimeSpan span = DateTime.Now.Subtract(Access_tokenStartTime);
                if (span.TotalSeconds > 3600)
                {
                    //重新获取
                    model = T_GetAccessToken();
                }
                else
                {
                    //返回缓存
                    return AccessTokenNow;
                }
            }
            return model;
        }

        /// <summary>
        /// 获取根组织信息
        /// </summary>
        /// <returns></returns>
        public static ZWDingRootOrganization GetRootOrganization()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("tenantId", AgentId);
            ZWDingRootOrganization res = DingGovRequest<ZWDingRootOrganization>(HttpMethod.Post, "/mozi/organization/getRootOrganization", keyValuePairs);
            return res;
        }

        /// <summary>
        /// 获得下级组织部门
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<string> GetSubOrganizationCodes(string code)
        {
            List<string> departcodeslist = new List<string>();
            int pageNo = 1;
            while (true)
            {
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("organizationCode", code);
                keyValuePairs.Add("pageNo", pageNo.ToString());
                keyValuePairs.Add("pageSize", GetDepartmentLimit.ToString());
                keyValuePairs.Add("returnTotalSize", "true");
                keyValuePairs.Add("status", "A");
                keyValuePairs.Add("tenantId", AgentId);
                keyValuePairs = keyValuePairs.OrderBy(m => m.Key).ToDictionary(m => m.Key, p => p.Value);
                ZWDingJuniorDepartment res = DingGovRequest<ZWDingJuniorDepartment>
                    (HttpMethod.Post, "/mozi/organization/pageSubOrganizationCodes", keyValuePairs);

                if (res.Success)
                {
                    if (res.Content.Success == true && res.Content.Data != null)
                    {
                        if (res.Content.Data.Count <= 0)
                        {
                            //没有数据跳出循环
                            break;
                        }

                        //判断是否需要跳出循环
                        if ((pageNo - 1) * GetDepartmentLimit + res.Content.Data.Count >= res.Content.TotalSize)
                        {
                            //数据获取全了也跳出循环
                            departcodeslist.AddRange(res.Content.Data);
                            break;
                        }
                        else
                        {
                            departcodeslist.AddRange(res.Content.Data);
                            pageNo = pageNo + 1;
                        }
                    }
                    else
                    {
                        //错误跳出循环
                        break;
                    }
                }
                else
                {
                    //错误跳出循环
                    break;
                }
            }
            return departcodeslist;
        }

        /// <summary>
        /// 查询通讯录权限
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAuthOrganization()
        {
            List<string> departcodeslist = new List<string>();
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("tenantId", AgentId);
            ZWDingAuthOrganization res = DingGovRequest<ZWDingAuthOrganization>(HttpMethod.Post, "/auth/scopesV2", keyValuePairs);
            if (res.Success)
            {
                departcodeslist = res.Content.DeptVisibleScopes;
            }
            return departcodeslist;
        }

        /// <summary>
        /// 依据部门获取人员
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<ZWDingGetUserModel> GetUsersByOrganizationCode(string code)
        {
            List<ZWDingGetUserModel> userslist = new List<ZWDingGetUserModel>();
            int pageNo = 1;
            while (true)
            {
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("employeeStatus", "A");
                keyValuePairs.Add("organizationCode", code);
                keyValuePairs.Add("pageNo", pageNo.ToString());
                keyValuePairs.Add("pageSize", GetDepartmentUserLimit.ToString());
                keyValuePairs.Add("returnTotalSize", "true");
                keyValuePairs.Add("tenantId", AgentId);
                keyValuePairs = keyValuePairs.OrderBy(m => m.Key).ToDictionary(m => m.Key, p => p.Value);
                ZWDingGetUsers res = DingGovRequest<ZWDingGetUsers>
                    (HttpMethod.Post, "/mozi/organization/pageOrganizationEmployeePositions", keyValuePairs);

                if (res.Success)
                {
                    if (res.Content.Success)
                    {
                        if (res.Content.Data == null)
                        {
                            //没有数据跳出循环
                            break;
                        }
                        if (res.Content.Data.Count <= 0)
                        {
                            //没有数据跳出循环
                            break;
                        }

                        //判断是否需要跳出循环
                        if ((pageNo - 1) * GetDepartmentUserLimit + res.Content.Data.Count >= res.Content.TotalSize)
                        {
                            //数据获取全了也跳出循环
                            userslist.AddRange(res.Content.Data);
                            break;
                        }
                        else
                        {
                            userslist.AddRange(res.Content.Data);
                            pageNo = pageNo + 1;
                        }
                    }
                    else
                    {
                        //错误跳出循环
                        break;
                    }
                }
                else
                {
                    //错误跳出循环
                    break;
                }
            }
            return userslist;
        }

        #region 获取部门信息
        /// <summary>
        /// 依据部门Id列表获取部门信息
        /// </summary>
        /// <param name="OrganizationCodes"></param>
        /// <returns></returns>
        public static List<ZWDingGetOrganizationsModel> GetOrganizationByCodes(List<string> OrganizationCodes)
        {
            List<ZWDingGetOrganizationsModel> list = new List<ZWDingGetOrganizationsModel>();
            if (OrganizationCodes != null && OrganizationCodes.Count > 0)
            {
                OrganizationCodes.Sort((x, y) => x.CompareTo(y)); //顺序
            }
            else
            {
                return new List<ZWDingGetOrganizationsModel>();
            }

            List<string> pagecodes = new List<string>();
            int pcount = 0;
            foreach (var codeitem in OrganizationCodes)
            {
                pcount = pcount + 1;
                pagecodes.Add(codeitem);
                if (pcount == GetDepartmentLimit)
                {
                    pcount = 0;
                    ZWDingGetOrganizations res = GetPageOrganizationByCodes(pagecodes);
                    if (res.Success && res.Content.Success)
                    {
                        list.AddRange(res.Content.Data);
                    }
                    pagecodes = new List<string>();
                }
            }
            if (pagecodes.Count > 0)
            {
                ZWDingGetOrganizations res = GetPageOrganizationByCodes(pagecodes);
                if (res.Success && res.Content.Success)
                {
                    list.AddRange(res.Content.Data);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取一页部门信息
        /// </summary>
        /// <param name="OrganizationCodes"></param>
        /// <returns></returns>
        private static ZWDingGetOrganizations GetPageOrganizationByCodes(List<string> OrganizationCodes)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>(new RepeatDictionaryComparer());
            foreach (var item in OrganizationCodes)
            {
                keyValuePairs.Add("organizationCodes", item);
            }
            keyValuePairs.Add("tenantId", AgentId);
            ZWDingGetOrganizations res = DingGovRequest<ZWDingGetOrganizations>
                    (HttpMethod.Post, "/mozi/organization/listOrganizationsByCodes", keyValuePairs);
            return res;
        }
        #endregion

        /// <summary>
        /// 获取免登用户
        /// </summary>
        /// <returns></returns>
        public static ZWDingGetLoginUser GetFreeLogin(string code)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("access_token", GetAccess_token().Content.Data.AccessToken);
            keyValuePairs.Add("auth_code", code);
            keyValuePairs = keyValuePairs.OrderBy(m => m.Key).ToDictionary(m => m.Key, p => p.Value);
            ZWDingGetLoginUser res = DingGovRequest<ZWDingGetLoginUser>(HttpMethod.Post, "/rpc/oauth2/dingtalk_app_user.json", keyValuePairs);
            return res;
        }

        /// <summary>
        /// 获取通讯录权限范围
        /// </summary>
        /// <returns></returns>
        public static RootscopesV2 GetscopesV2()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("access_token", GetAccess_token().Content.Data.AccessToken);
            keyValuePairs.Add("tenantId", tenantId);
            keyValuePairs = keyValuePairs.OrderBy(m => m.Key).ToDictionary(m => m.Key, p => p.Value);
            RootscopesV2 res = DingGovRequest<RootscopesV2>(HttpMethod.Post, "/auth/scopesV2", keyValuePairs);
            return res;
        }

        /// <summary>
        /// 根据组织 Code 查询详情
        /// </summary>
        /// <returns></returns>
        public static RootgetOrganizationByCode GetgetOrganizationByCode(string code)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("access_token", GetAccess_token().Content.Data.AccessToken);
            keyValuePairs.Add("organizationCode", code);
            keyValuePairs.Add("tenantId", tenantId);
            keyValuePairs = keyValuePairs.OrderBy(m => m.Key).ToDictionary(m => m.Key, p => p.Value);
            RootgetOrganizationByCode res = DingGovRequest<RootgetOrganizationByCode>(HttpMethod.Post, "/mozi/organization/getOrganizationByCode", keyValuePairs);
            return res;
        }

        /// <summary>
        /// 查询组织下人员详情，包含个人信息和任职信息
        /// </summary>
        /// <returns></returns>
        public static RootEmployeePositions GetpageOrganizationEmployeePositions(string code)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("access_token", GetAccess_token().Content.Data.AccessToken);
            keyValuePairs.Add("organizationCode", code);
            keyValuePairs.Add("tenantId", tenantId);
            keyValuePairs = keyValuePairs.OrderBy(m => m.Key).ToDictionary(m => m.Key, p => p.Value);
            RootEmployeePositions res = DingGovRequest<RootEmployeePositions>(HttpMethod.Post, "/mozi/organization/pageOrganizationEmployeePositions", keyValuePairs);
            return res;
        }


        #region 获取人员accoutid
        /// <summary>
        /// 获取人员accoutid
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static List<ZWDingGetEmployeeAccountIdsModel> GetAccountIds(List<string> codes)
        {
            List<ZWDingGetEmployeeAccountIdsModel> list = new List<ZWDingGetEmployeeAccountIdsModel>();
            if (codes != null && codes.Count > 0)
            {
                codes.Sort((x, y) => x.CompareTo(y)); //顺序
            }
            else
            {
                return new List<ZWDingGetEmployeeAccountIdsModel>();
            }

            List<string> pagecodes = new List<string>();
            int pcount = 0;
            foreach (var codeitem in codes)
            {
                pcount = pcount + 1;
                pagecodes.Add(codeitem);
                if (pcount == GetDepartmentLimit)
                {
                    pcount = 0;
                    ZWDingGetEmployeeAccountIds res = GetPageAccountIds(pagecodes);
                    if (res.Success && res.Content.Success)
                    {
                        list.AddRange(res.Content.Data);
                    }
                    pagecodes = new List<string>();
                }
            }
            if (pagecodes.Count > 0)
            {
                ZWDingGetEmployeeAccountIds res = GetPageAccountIds(pagecodes);
                if (res.Success && res.Content.Success)
                {
                    list.AddRange(res.Content.Data);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取一页人员accoutid
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        private static ZWDingGetEmployeeAccountIds GetPageAccountIds(List<string> codes)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>(new RepeatDictionaryComparer());
            foreach (var item in codes)
            {
                keyValuePairs.Add("employeeCodes", item);
            }
            keyValuePairs.Add("tenantId", AgentId);
            ZWDingGetEmployeeAccountIds res = DingGovRequest<ZWDingGetEmployeeAccountIds>
                    (HttpMethod.Post, "/mozi/employee/listEmployeeAccountIds", keyValuePairs);
            return res;
        }

        #endregion

        /// <summary>
        /// 手机号获取人员Code
        /// </summary>
        /// <returns></returns>
        public static string GetByMobileModel(string mobile)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("areaCode", "86");
            keyValuePairs.Add("tenantId", AgentId);
            keyValuePairs.Add("namespace", "local");
            keyValuePairs.Add("mobile", mobile);
            keyValuePairs = keyValuePairs.OrderBy(m => m.Key).ToDictionary(m => m.Key, p => p.Value);
            ZWDingGetByMobile res = DingGovRequest<ZWDingGetByMobile>(HttpMethod.Post, "/mozi/employee/get_by_mobile", keyValuePairs);
            return res.Content.Data.EmployeeCode;
        }

        /// <summary>
        /// 发送推送
        /// </summary>
        /// <param name="ApplyId"></param>
        /// <param name="Page"></param>
        /// <param name="Title"></param>
        /// <param name="Content"></param>
        /// <param name="DingUserIds"></param>
        /// <param name="OrganizationCodes"></param>
        /// <returns></returns>
        public static WorkMsgModel SendUseCarCodeMsg(int ApplyId, string Page, string Title, string Content, string DingUserIds, string OrganizationCodes)
        {
            //string param = JsonConvert.SerializeObject(new WorkMsgId { id = ApplyId });
            //string url = WebUrl + $"/#/{Page}?params=" + System.Web.HttpUtility.UrlEncode(param, System.Text.Encoding.UTF8);

            string param = JsonConvert.SerializeObject(new WorkMsgId { id = ApplyId, page = Page });
            string url = WebUrl + $"/#/pages/loginForwarding/index?params=" + System.Web.HttpUtility.UrlEncode(param, Encoding.UTF8);
            string url2 = WorkMsgHead + "://dingtalkclient/page/link?url=" + System.Web.HttpUtility.UrlEncode(url + "?ddtab=true", Encoding.UTF8);
            var msg = new
            {
                msgtype = "action_card",
                action_card = new
                {
                    title = Title,
                    markdown = Content,
                    single_title = "用车详情",
                    single_url = url,
                    single_pc_url = ""
                }
            };

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            //keyValuePairs.Add("bizMsgId", "");
            keyValuePairs.Add("msg", JsonConvert.SerializeObject(msg));
            keyValuePairs.Add("receiverIds", DingUserIds);
            keyValuePairs.Add("tenantId", AgentId);
            keyValuePairs = keyValuePairs.OrderBy(m => m.Key).ToDictionary(m => m.Key, p => p.Value);
            WorkMsgModel res = DingGovRequest
                <WorkMsgModel>(HttpMethod.Post, "/message/workNotification", keyValuePairs);
            return res;

        }

        public class RepeatDictionaryComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return x != y;
            }
            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }
        #region 通用方法

        /// <summary>
        /// 通用请求方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="canonicalURI"></param>
        /// <param name="requestParas"></param>
        /// <returns></returns>
        private static T DingGovRequest<T>(HttpMethod method, string canonicalURI, Dictionary<string, string> requestParas)
        {
            var domain = Dingdomainname;
            var accessKey = Appkey;
            var secretKey = Appsecret;
            var timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            var nonce = T_ConvertDateTimeToInt(DateTime.Now) + "1234";
            var requestUrl = domain + canonicalURI;
            try
            {
                var bytesToSign = $"{method}\n{timestamp}\n{nonce}\n{canonicalURI}";
                if (requestParas?.Count() > 0)
                {
                    //参数参与签名
                    bytesToSign += '\n' + DicionartyToUrlParameters(requestParas);
                }
                var message = new HttpRequestMessage
                {
                    Method = method,
                };
                #region 请求头
                //message.Headers.Add("X-Hmac-Auth-IP", "1.1.1.1");
                //message.Headers.Add("X-Hmac-Auth-MAC", "MAC");
                message.Headers.Add("X-Hmac-Auth-Timestamp", timestamp);
                message.Headers.Add("X-Hmac-Auth-Version", "1.0");
                message.Headers.Add("X-Hmac-Auth-Nonce", nonce);
                message.Headers.Add("apiKey", accessKey);
                message.Headers.Add("X-Hmac-Auth-Signature", T_GetSignature(bytesToSign, secretKey));
                #endregion
                if (method == HttpMethod.Post)
                {
                    var paras = new Dictionary<string, string>(new RepeatDictionaryComparer());
                    if (requestParas?.Count() > 0)
                    {

                        foreach (var dic in requestParas)
                        {
                            if (canonicalURI == "/message/workNotification" && dic.Key == "msg")
                            {
                                paras.Add(dic.Key, dic.Value);
                            }
                            else
                            {
                                paras.Add(dic.Key, Uri.UnescapeDataString(dic.Value));
                            }

                        }
                        message.Content = new FormUrlEncodedContent(paras);
                        message.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    }
                }
                else if (method == HttpMethod.Get)
                {

                    requestUrl += $"?{DicionartyToUrlParameters(requestParas)}";
                }

                message.RequestUri = new Uri(requestUrl);
                using (var client = new HttpClient())
                {
                    var request = client.SendAsync(message);
                    var reponseContent = request.Result.Content.ReadAsStringAsync();

                    //记录日志

                    if (!request.Result.IsSuccessStatusCode)
                    {
                        throw new Exception(reponseContent.Result);
                    }

                    return JsonConvert.DeserializeObject<T>(reponseContent.Result);
                }


            }
            catch (Exception ex)
            {
                //记录日志
                return (T)default;
            }
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        private static ZWDingAccessToken T_GetAccessToken()
        {
            //try
            //{             
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("appkey", Appkey);
            keyValuePairs.Add("appsecret", Appsecret);
            ZWDingAccessToken res = DingGovRequest<ZWDingAccessToken>(HttpMethod.Get, "/gettoken.json", keyValuePairs);
            if (res.Success)
            {
                //返回正确结果,设置缓存
                Access_tokenStartTime = DateTime.Now;
                IsFirst = false;
                AccessTokenNow = res;
            }
            return res;

            //}
            //catch (WebException ex)
            //{
            //    var res = (HttpWebResponse)ex.Response;
            //    StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            //    var ret = sr.ReadToEnd();
            //    return ret;
            //}

        }

        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        private static long T_ConvertDateTimeToInt(DateTime time)
        {
            DateTime timeStamp = new DateTime(1970, 1, 1); //得到1970年的时间戳
            long t = (time.Ticks - timeStamp.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="message">将bytesToSign作为消息</param>
        /// <param name="secret">将SecretKey作为秘钥</param>
        /// <returns></returns>
        private static string T_GetSignature(string message, string secret)
        {
            secret = secret ?? "";
            //var encoding = new System.Text.ASCIIEncoding();
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        /// <summary>
        /// 参数拼接
        /// </summary>
        /// <param name="requestParas"></param>
        /// <returns></returns>
        private static string DicionartyToUrlParameters(Dictionary<string, string> requestParas)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in requestParas)
            {
                string header = "";
                if (sb.Length != 0)
                {
                    header = "&";
                }
                sb.Append(header + item.Key + "=" + item.Value);
            }
            string HttpRequestParams = sb.ToString();
            return HttpRequestParams;

            //var HttpRequestParams = string.Empty;
            //if (requestParas != null)
            //{
            //    foreach (var item in requestParas)
            //    {
            //        if (!string.IsNullOrEmpty(HttpRequestParams))
            //            HttpRequestParams += "&";
            //        HttpRequestParams = string.Format("{0}{1}={2}", HttpRequestParams, item.Key, item.Value);
            //    }
            //}
            //return HttpRequestParams;
        }

        /// <summary>
        /// base64解密
        /// </summary>
        /// <param name="result">加密源字符串</param>
        /// <returns></returns>
        public static string DecodeBase64(string result)
        {
            Encoding encode = System.Text.Encoding.UTF8;
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string EncodeBase64(string source)
        {
            Encoding encode = System.Text.Encoding.UTF8;
            string res;
            byte[] bytes = encode.GetBytes(source);
            try
            {
                res = Convert.ToBase64String(bytes);
            }
            catch
            {
                res = source;
            }
            return res;
        }

        #endregion
    }
}
