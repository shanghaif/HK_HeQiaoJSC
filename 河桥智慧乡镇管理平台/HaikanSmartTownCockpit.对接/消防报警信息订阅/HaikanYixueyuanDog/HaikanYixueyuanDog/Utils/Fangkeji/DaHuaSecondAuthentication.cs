using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils.Fangkeji
{
    public static class DaHuaSecondAuthentication
    {
        public static  string AttendanceData(string id, string randomKey, string realm)
        {
            string one = Haikan.DEncrypt.DesEncrypt.GetMd5String("Hzsf@123");
            //Hzsf@123
            string two = Haikan.DEncrypt.DesEncrypt.GetMd5String("system" + Test(one));
            string three = Haikan.DEncrypt.DesEncrypt.GetMd5String(Test(two));
            string four = Haikan.DEncrypt.DesEncrypt.GetMd5String("system:"+ realm+":" + Test(three));
            string five = Haikan.DEncrypt.DesEncrypt.GetMd5String(Test(four) + ":" + randomKey);

            string strurl2 = "http://172.20.184.6/admin/API/accounts/authorize";
            ////参数
            string parmjosn2 = "{\"clientType\": \"WINPC\",\"encryptType\": \"MD5\",\"ipAddress\": \"\",\"mac\": \"\",\"randomKey\": \"" + randomKey + "\",\"signature\": \"" + Test(five) + "\",\"userName\": \"system\"}";  ////查询出结果json
            string datajson2 = PostGetRequest.HttpPostJson(strurl2, parmjosn2);
            DaHuaTwoAuthenticationModelRoot dm = null;
            try
            {
                //rbtcc = JsonConvert.DeserializeObject<DaHuaTwoAuthenticationModel>(datajson2);
                 dm = JsonConvert.DeserializeObject<DaHuaTwoAuthenticationModelRoot>(datajson2);
                string tokens = dm.token;
                //令牌
                DaHuaUpdateToken.AttendanceData(id, tokens, Test(five));
                return tokens;
            }
            catch (Exception e)
            {
                //AddSystemLog(e.ToString(), "第二次鉴权");
                dm = null;
                return "";
            }

        }
        //转小写
        public static string Test(string str)
        {
            string newStr = string.Empty;    //用于存放新字符串
            foreach (char item in str)
            {
                if (item >= 'A' && item <= 'Z')
                {
                    //大写字母转小写
                    newStr += item.ToString().ToLower();
                }
                else if (item >= '0' && item <= '9')
                {
                    //数字不变
                    newStr += item.ToString();
                }
            }
            return newStr;
        }
    }
}
