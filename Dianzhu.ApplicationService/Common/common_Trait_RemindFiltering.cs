using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_RemindFiltering
    {
        /// <summary>
        /// 关联的订单ID
        /// </summary>
        /// <type>string</type>
        public string orderID { get; set; }

        /// <summary>
        /// 开始时间（yyyyMMdd）
        /// </summary>
        /// <type>string</type>
        public string startTime { get; set; }

        /// <summary>
        /// 结束时间（yyyyMMdd）
        /// </summary>
        /// <type>string</type>
        public string endTime { get; set; }
    }
}
