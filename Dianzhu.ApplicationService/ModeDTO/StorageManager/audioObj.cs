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

        string _length = "";
        /// <summary>
        /// 
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

        string _size = "";
        /// <summary>
        /// 
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
