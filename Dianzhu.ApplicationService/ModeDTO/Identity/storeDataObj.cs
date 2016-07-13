using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class storeDataObj
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        /// <type>string</type>
        public string storeID { get; set; }

        /// <summary>
        /// 正在处理的订单数
        /// </summary>
        /// <type>string</type>
        public string handleCount { get; set; }

        /// <summary>
        /// 完成的订单数
        /// </summary>
        /// <type>string</type>
        public string finishCount { get; set; }

        /// <summary>
        /// 总单数
        /// </summary>
        /// <type>string</type>
        public string allCount { get; set; }
    }
}
