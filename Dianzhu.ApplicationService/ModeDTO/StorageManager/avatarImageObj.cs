using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class avatarImageObj
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

        string _lowUrl = "";
        /// <summary>
        /// 低分辨率头像的完整路径
        /// </summary>
        /// <type>string</type>
        public string lowUrl
        {
            get
            {
                return _lowUrl;
            }
            set
            {
                _lowUrl = value;
            }
        }

        string _hdUrl = "";
        /// <summary>
        /// 高分辨率的头像完整路径
        /// </summary>
        /// <type>string</type>
        public string hdUrl
        {
            get
            {
                return _hdUrl;
            }
            set
            {
                _hdUrl = value;
            }
        }

        string _size = "0";
        /// <summary>
        /// 文件大小 KB
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
    }
}
