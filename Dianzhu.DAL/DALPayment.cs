using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate.Criterion;
using Dianzhu.IDAL;

namespace Dianzhu.DAL
{
    public class DALPayment : NHRepositoryBase<Payment, Guid>, IDALPayment//: DALBase<Model.Payment>
    {

        public DALPayment()
        {

        }
        //注入依赖,供测试使用;
        //public DALPayment(string fortest) : base(fortest)
        //{

        //}

        /// <summary>
        /// 获取一个订单已经创建的支付项.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual IList<Payment> GetPaymentsForOrder(ServiceOrder order)
        {
            //20160621_longphui_modify
            //var list = GetList(Session.QueryOver<Payment>().Where(x => x.Order == order));
            IList<Payment> list = Find((x => x.Order == order));
            return list;
        }

        public virtual Payment GetPaymentForWaitPay(ServiceOrder order)
        {
            return Session.QueryOver<Payment>().Where(x => x.Order == order).And(x => x.Status == Model.Enums.enum_PaymentStatus.Wait_Buyer_Pay).SingleOrDefault();
        }
        /// <summary>
        /// 查询订单支付的订金
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual Payment GetPayedByTarget(ServiceOrder order, Model.Enums.enum_PayTarget payTarget)
        {
            return Session.QueryOver<Payment>().Where(x => x.Order == order).And(x => x.PayTarget == payTarget).And(x => x.Status == Model.Enums.enum_PaymentStatus.Trade_Success).SingleOrDefault();
        }

        /// <summary>
        /// 查询订单支付的订金
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual Payment GetPayedForDeposit(ServiceOrder order)
        {
            return new Payment();
        }
    }
}
