using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public class OrderShareParam
    {
        /// <summary>
        /// 关联对象的Id, 比如 订单, 提现单,充值单..等等.
        /// </summary>
        public virtual string RelatedObjectId { get; set; }

        /// <summary>
        /// 分账的总金额
        /// </summary>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// 分账用户
        /// </summary>
        public IList<BalanceUserParam> BalanceUser { get; set; }

        /// <summary>
        /// 服务类型ID
        /// </summary>
        public virtual string ServiceTypeID { get; set; }
    }
}
