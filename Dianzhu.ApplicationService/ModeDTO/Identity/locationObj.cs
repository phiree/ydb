using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class locationObj
    {
        /// <summary>
        ///经度（百度地图坐标系（BD-09））
        /// </summary>
        /// <type>string</type>
        public string longitude { get; set; }

        /// <summary>
        ///维度（百度地图坐标系（BD-09））
        /// </summary>
        /// <type>string</type>
        public string latitude { get; set; }

        /// <summary>
        ///地址字符串
        /// </summary>
        /// <type>string</type>
        public string address { get; set; }
    } 
}
