using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class snapshortsObj
    {
        /// <summary>
        /// 快照的日期（yyyyMMdd）
        /// </summary>
        /// <type>string</type>
        public string date { get; set; }

        /// <summary>
        /// 最大订单数
        /// </summary>
        /// <type>string</type>
        public string maxCountOrder { get; set; }

        /// <summary>
        /// 已生成服务的订单数量
        /// </summary>
        /// <type>string</type>
        public string havaCountOrder { get; set; }

        /// <summary>
        /// 该快照下包含的工作时间数组
        /// </summary>
        /// <type>array[workTimeObj]</type>
        public IList<workTimeObj> workTimeArray { get; set; }
    }
}
