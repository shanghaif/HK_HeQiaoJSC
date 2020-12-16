using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.Models.Party
{
    public class TimePerson
    {


        /// <summary>
        /// 
        /// </summary>
        public int errno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cost { get; set; }

        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public UserTags userTags { get; set; }
        }

        public class UserTags
        {
            /// <summary>
            /// 
            /// </summary>
            public double age_45 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double age_18_24 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double age_25_34 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double age_35_44 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double age_other { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double gender_female { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double gender_male { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double consumption_middle { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double consumption_low { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double consumption_high { get; set; }
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(PropertyName= "age_45+")]
            public double age_45s { get; set; }
        }
    }
}
