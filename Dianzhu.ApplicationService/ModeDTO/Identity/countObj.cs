using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class countObj
    {
        string _count = "";
        /// <summary>
        /// 图片Url
        /// </summary>
        /// <type>string</type>
        public string count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }
    }
}
