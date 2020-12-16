using HaikanYixueyuanDog.DataClass;
using HaikanYixueyuanDog.DataClass.Subscribe8900;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils.Subscribe8900
{
    public static class Subscribe
    {
        #region 接入方注册
        public static ResponseData Register(string address)
        {
            ResponseData rd = new ResponseData();
            SubRegister sr = new SubRegister();
            sr.name = "";
            sr.callbackAddress = "";
            sr.hasExpir = 0;
            string registerurl = address+"/register";
            try
            {
                var register = PostGetRequest.HttpPost(registerurl, JsonConvert.SerializeObject(sr));
                SubRegisterReturn srr = JsonConvert.DeserializeObject<SubRegisterReturn>(register);
                if(srr.code==200)
                {
                    rd.code = "200";
                    rd.msg = "成功 "+ srr.errMsg;
                    rd.data = srr.data;
                }
                else
                {
                    rd.code = "500";
                    rd.msg = srr.errMsg;
                }
            }
            catch(Exception e)
            {
                rd.code = "404";
                rd.msg = e.Message;
            }
            return rd;
        }
        #endregion
        #region 事件订阅
        public static ResponseData InterfaceList(string address,string code,string interfaceCode)
        {
            ResponseData rd = new ResponseData();
            string interfaceListurl = address + "/register/interfaceList/"+code+"/"+ interfaceCode;
            try
            {
                var register = PostGetRequest.HttpGet(interfaceListurl,"utf-8");
                InterfaceListReturn ifl = JsonConvert.DeserializeObject<InterfaceListReturn>(register);
                if (ifl.code == 200)
                {
                    rd.code = "200";
                    rd.msg = "成功 "+ ifl.errMsg;
                }
                else
                {
                    rd.code = "500";
                    rd.msg = ifl.errMsg;
                }
            }
            catch (Exception e)
            {
                rd.code = "404";
                rd.msg = e.Message;
            }
            return rd;
        }
        #endregion
        #region 取消事件订阅
        public static ResponseData Cancellation(string address, string code,string interfaceCode)
        {
            ResponseData rd = new ResponseData();
            string interfaceListurl = address + "/register/cancellation/" + code + "/" + interfaceCode;
            try
            {
                var register = PostGetRequest.HttpGet(interfaceListurl, "utf-8");
                CancellationReturn clr = JsonConvert.DeserializeObject<CancellationReturn>(register);
                if (clr.code == 200)
                {
                    rd.code = "200";
                    rd.msg = "成功 " + clr.errMsg;
                }
                else
                {
                    rd.code = "500";
                    rd.msg = clr.errMsg;
                }
            }
            catch (Exception e)
            {
                rd.code = "404";
                rd.msg = e.Message;
            }
            return rd;
        }
        #endregion
    }
}
