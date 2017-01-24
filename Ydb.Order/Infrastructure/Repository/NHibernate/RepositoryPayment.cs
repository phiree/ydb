using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
using NHibernate;
using System.Linq.Expressions;
using Ydb.Common.Repository;
using Ydb.Common;
namespace Ydb.Order.Infrastructure.Repository.NHibernate
{
   public class RepositoryPayment : NHRepositoryBase<Payment, Guid>, IRepositoryPayment
    {
        /// <summary>
        /// 获取一个订单已经创建的支付项.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual IList<Payment> GetPaymentsForOrder(ServiceOrder order)
        {
            return Find(x => x.Order.Id == order.Id);
        }

        public virtual Payment GetPaymentForWaitPay(ServiceOrder order)
        {
            return FindOne(x => x.Order.Id == order.Id && x.Status == enum_PaymentStatus.Wait_Buyer_Pay);
        }
        /// <summary>
        /// 查询订单支付的订金
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual Payment GetPayedByTarget(ServiceOrder order, enum_PayTarget payTarget)
        {
            return FindOne(x => x.Order.Id == order.Id && x.PayTarget == payTarget);
        }

    }
}
