using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 支付项模块 业务业务层
    /// </summary>
    public class BLLPayment
    {
        DAL.DALPayment dal;
        string errMsg = string.Empty;
        public BLLPayment(DAL.DALPayment dal)
        {
            this.dal = dal;
        }
        public BLLPayment():this(new DAL.DALPayment())
        {
        }
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL");
        /// <summary>
        /// 订单申请支付,
        ///<returns>支付链接</returns>
        /// <param name="order">申请支付的订单</param>
        /// <param name="payTarget">支付目标</param>
        public Payment ApplyPay(ServiceOrder order, enum_PayTarget payTarget)
        {
            //验证请求类型是否有效
            bool applyIsValid = false;
            
            switch (payTarget)
            {
                
                case enum_PayTarget.Deposit:
                    errMsg = "只有 刚创建的订单 才能申请 订金支付";
                    applyIsValid = order.OrderStatus == enum_OrderStatus.Created;
                    break;
                case enum_PayTarget.FinalPayment:
                    //只有 已经服务完成的 订单 才能申请 支付尾款 
                    errMsg = "只有 已经服务完成的 订单 才能申请 支付尾款 ";
                    applyIsValid = order.OrderStatus == enum_OrderStatus.Ended;
                    break;
                case enum_PayTarget.Compensation:
                    errMsg = "只有已经完成的订单 才能申请赔偿.";
                    applyIsValid = order.OrderStatus == enum_OrderStatus.Finished||
                          order.OrderStatus == enum_OrderStatus.Appraised;
                    break;
            }
            if (!applyIsValid)
            {
                throw new Exception(errMsg);
            }
            //获取该订单已经申请过的项目.
            IList<Payment> payments = dal.GetPaymentsForOrder(order);
            var paymentList = payments.Where(x => x.PayTarget == payTarget).ToList();
            //验证该支付申请是否有效. 
            //无效: 同类型的支付申请已经创建, 直接返回该支付链接. 当前支付金额
            var paymentCount= paymentList.Count();
            Payment payment = null;
            if (paymentCount == 1)
            {
                  payment = paymentList[0];

                //验证该支付项的状态
                if (payment.Status == enum_PaymentStatus.Success)
                {
                    errMsg = "该项已经支付完成";
                    log.Error(errMsg);
                    throw new Exception(errMsg);
                }

                //该支付项已经创建,验证其金额是否有变化
                var payAmount = order.GetAmount(payTarget);
                if (payAmount != payment.Amount)
                {
                    errMsg =string.Format( "本次申请金额和上次不一样. 本次:{0},上次:{1}",payment.Amount,payAmount);
                    log.Warn(errMsg);
                    //申请金额和之前的不一致, 需要警告
                }
                payment.Amount = payAmount;
                payment.LastUpdateTime = DateTime.Now;
                dal.Update(payment);
                

            }
            else if (paymentCount == 0)
            {
                payment = new Payment { Amount=order.GetAmount(payTarget), Order=order, PayTarget= payTarget};
                dal.Save(payment);
            }
            else //已经存在多项
            {
                errMsg = string.Format("该订单已经存在多项同类型的支付项", order.Id);
                log.Fatal(errMsg);
                throw new Exception(errMsg);
            }
            return payment;
        }

        public string BuildPayLink(Guid  paymentId)
        {
            return Config.Config.GetAppSetting("PayServerUrl") + "?paymentid=" + paymentId;
        }
        public Payment GetOne(Guid id)
        {
            return dal.GetOne(id);
        }
        public void SaveOrUpdate(Payment payment)
        {
            dal.SaveOrUpdate(payment);
        }
        public void Update(Payment payment)
        {
            dal.Update(payment);
        }
    }
}
