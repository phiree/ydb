using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{

    public class BalanceFlowFilter
    {
        /// <summary>
        /// 用户账户ID
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// 分账时间的开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 分账时间的开始时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 关联对象的Id, 比如 订单, 提现单,充值单..等等.
        /// </summary>
        public string RelatedObjectId { get; set; }

        /// <summary>
        /// 关联对象的流水编号
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// 发生类型
        /// </summary>
        public string FlowType { get; set; }

        /// <summary>
        /// true为收入，false为支出
        /// </summary>
        public string Income { get; set; }

    }
}
