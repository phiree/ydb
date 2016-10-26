using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public interface IOrderShareService
    {
        /// <summary>
        /// 订单分成操作
        /// </summary>
        /// <param name="order" type="BalanceParam">分账的订单及用户信息</param>
        void ShareOrder(BalanceParam order);
    }
}
