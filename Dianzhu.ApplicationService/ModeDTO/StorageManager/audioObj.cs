using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class audioObj
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
        /// 语音长度
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
        /// 文件大小
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
