using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class orderHypermediaObj
    {
        string _href = "";
        /// <summary>
        /// MD5加密结果
        /// </summary>
        /// <type>string</type>
        public string href
        {
            get
            {
                return _href;
            }
            set
            {
                _href = value;
            }
        }
    }
}
