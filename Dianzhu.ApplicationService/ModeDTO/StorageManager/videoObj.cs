using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class videoObj
    {
        string _id = "";
        /// <summary>
        /// 
        /// </summary>
        /// <type>string</type>
        public string id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        string _length = "0";
        /// <summary>
        /// 视频长度
        /// </summary>
        /// <type>string</type>
        public string length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
            }
        }

        string _url = "";
        /// <summary>
        /// 完整路径
        /// </summary>
        /// <type>string</type>
        public string url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }

        string _size = "0";
        /// <summary>
        /// 视频大小 KB
        /// </summary>
        /// <type>string</type>
        public string size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        string _height = "0";
        /// <summary>
        /// 高、长
        /// </summary>
        /// <type>string</type>
        public string height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        string _width = "0";
        /// <summary>
        /// 宽
        /// </summary>
        /// <type>string</type>
        public string width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
    }
}
