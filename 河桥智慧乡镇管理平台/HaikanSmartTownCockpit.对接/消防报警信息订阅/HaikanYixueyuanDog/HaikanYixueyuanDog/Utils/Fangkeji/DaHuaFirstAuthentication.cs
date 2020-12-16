using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils.Fangkeji
{
    public static class DaHuaFirstAuthentication
    {
        public static string AttendanceData(string id)
        {

            string strurl2 = "http://172.20.184.6/admin/API/accounts/authorize";
            ////参数
            string parmjosn2 = "{\"userName\": \"system\",\"clientType\": \"WINPC\",\"ipAddress\": \"\"}";  ////查询出结果json
            string datajson2 = PostGetRequest.HttpPostJson(strurl2, parmjosn2);
            DaHuaOneAuthenticationModelRoot dm = null;
            try
            {
                dm = JsonConvert.DeserializeObject<DaHuaOneAuthenticationModelRoot>(datajson2);
                string randomKey = dm.randomKey;
                string tokenss = DaHuaSecondAuthentication.AttendanceData(id, randomKey, dm.realm);//第二次鉴权
                return tokenss;
            }
            catch (Exception e)
            {
                //AddSystemLog(e.ToString(), "第一次鉴权");
                dm = null;
                return "";
            }
        }
    }
}
