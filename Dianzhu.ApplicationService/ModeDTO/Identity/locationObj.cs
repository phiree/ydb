using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class locationObj
    {
        string _longitude = "";
        /// <summary>
        ///经度（百度地图坐标系（BD-09））
        /// </summary>
        /// <type>string</type>
        public string longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
            }
        }

        string _latitude = "";
        /// <summary>
        ///维度（百度地图坐标系（BD-09））
        /// </summary>
        /// <type>string</type>
        public string latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
            }
        }

        string _address = "";
        /// <summary>
        ///地址字符串
        /// </summary>
        /// <type>string</type>
        public string address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }
    } 
}
