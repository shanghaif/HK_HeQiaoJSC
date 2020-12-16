using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils.Fangkeji
{
    public static class DaHuaUpdateToken
    {
        public static void AttendanceData(string id, string token, string sing)
        {
            //string one = Haikan.DEncrypt.DesEncrypt.GetMd5String("Hzsf@123");
            //string two = Haikan.DEncrypt.DesEncrypt.GetMd5String("system" + Test(one));
            //string three = Haikan.DEncrypt.DesEncrypt.GetMd5String(Test(two));
            //string four = Haikan.DEncrypt.DesEncrypt.GetMd5String("system:DSS:" + Test(three));
            //string five = Haikan.DEncrypt.DesEncrypt.GetMd5String(Test(four) + ":" + token);

            string strurl2 = "http://172.20.184.6/admin/API/accounts/updateToken";
            ////参数
            string parmjosn2 = "{\"signature\": \"" + sing + "\"}";  ////查询出结果json
            string datajson2 = PostGetRequest.HttpPostToken(strurl2, parmjosn2, token);
            DaHuaUpdateTokenModelRoot dm = null;
            try
            {
                 dm = JsonConvert.DeserializeObject<DaHuaUpdateTokenModelRoot>(datajson2);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
