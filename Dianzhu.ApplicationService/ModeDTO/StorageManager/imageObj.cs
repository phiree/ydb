using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class imageObj
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

        string _height = "";
        /// <summary>
        /// 
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

        string _width = "";
        /// <summary>
        /// 
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

        string _url = "";
        /// <summary>
        /// 
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
    }
}
