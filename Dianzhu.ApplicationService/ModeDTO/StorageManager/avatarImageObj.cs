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

        string _lowUrl = "";
        /// <summary>
        /// 
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
        /// 
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
    }
}
