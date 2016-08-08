using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate.Criterion;
using Dianzhu.IDAL;

namespace Dianzhu.DAL
{


    public class DALPayment : NHRepositoryBase<Payment,Guid>,IDAL.IDALPayment
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
            return FindOne(x => x.Order.Id == order.Id && x.Status == Model.Enums.enum_PaymentStatus.Wait_Buyer_Pay);
        }
        /// <summary>
        /// 查询订单支付的订金
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual Payment GetPayedByTarget(ServiceOrder order, Model.Enums.enum_PayTarget payTarget)
        {
            return FindOne(x => x.Order.Id == order.Id && x.PayTarget == payTarget);
        }        
    }
}
