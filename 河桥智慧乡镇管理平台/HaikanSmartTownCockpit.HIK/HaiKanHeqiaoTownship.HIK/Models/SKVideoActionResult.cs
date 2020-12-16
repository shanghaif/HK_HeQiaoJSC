using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class SKVideoActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string cameraIndexCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int action { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string command { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int speed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int presetIndex { get; set; }
    }

}