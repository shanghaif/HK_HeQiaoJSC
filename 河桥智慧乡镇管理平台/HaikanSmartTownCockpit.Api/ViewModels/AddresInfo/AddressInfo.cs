using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.AddresInfo
{
    public class AddressInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
    }

    public class Loc
    {
        /// <summary>
        /// 
        /// </summary>
        public List<double> coordinates { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
    }

    public class AddrList
    {
        /// <summary>
        /// 
        /// </summary>
        public string door { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Loc loc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string town { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string squad { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string village { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string szone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resregion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string building_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public int address_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string belong_building { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double lon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string community { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string building { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string room { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public List<string> datasource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string addr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string floor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double lat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? room_floor { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public int hits { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AddrList> addrList { get; set; }
    }

}
