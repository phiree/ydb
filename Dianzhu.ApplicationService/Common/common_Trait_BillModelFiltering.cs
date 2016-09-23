using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_BillModelFiltering
    {
        /// <summary>
        /// 开始日期(yyyyMMdd)/月份(yyyyMM)
        /// </summary>
        /// <type>string</type>
        public string startTime { get; set; }

        /// <summary>
        /// 结束日期(yyyyMMdd)/月份(yyyyMM)
        /// </summary>
        /// <type>string</type>
        public string endTime { get; set; }


        string _serviceTypeLevel = "1";
        /// <summary>
        /// 按服务类型的层级的来返回明细内容，默认按level 2查询
        /// </summary>
        /// <type>string</type>
        public string serviceTypeLevel
        {
            get
            {
                return _serviceTypeLevel;
            }
            set
            {
                _serviceTypeLevel = value;
            }
        }

        /// <summary>
        /// 服务类型
        /// </summary>
        /// <type>string</type>
        public string billServiceType { get; set; }

        /// <summary>
        /// 支出状态
        /// </summary>
        /// <type>string</type>
        public string status { get; set; }

        /// <summary>
        /// 账单类型
        /// </summary>
        /// <type>string</type>
        public string billType { get; set; }

        
        /// <summary>
        /// 订单ID
        /// </summary>
        /// <type>string</type>
        public string orderId { get; set; }
    }
}
