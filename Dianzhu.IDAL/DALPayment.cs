using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Ydb.Common;

namespace Dianzhu.IDAL
{
    public interface IDALPayment : IRepository<Payment, Guid>
    {
        IList<Payment> GetPaymentsForOrder(ServiceOrder order);

        Payment GetPaymentForWaitPay(ServiceOrder order);        

        /// <summary>
        /// 查询订单支付的订金
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Payment GetPayedByTarget(ServiceOrder order,  enum_PayTarget payTarget);
    }
}
