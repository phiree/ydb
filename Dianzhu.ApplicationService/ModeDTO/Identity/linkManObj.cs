using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class linkManObj
    {
        string _linkManID = "";
        /// <summary>
        /// 联系人ID
        /// </summary>
        /// <type>string</type>
        public string linkManID
        {
            get
            {
                return _linkManID;
            }
            set
            {
                _linkManID = value;
            }
        }
    }
}
