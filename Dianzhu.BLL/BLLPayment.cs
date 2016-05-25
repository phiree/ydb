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
        BLLClaims bllClaims = null;
        DAL.DALPayment dal;
        string errMsg = string.Empty;
        public BLLPayment(DAL.DALPayment dal,BLLClaims bllClaims)
        {
            this.dal = dal;
            this.bllClaims = bllClaims;
        }
        public BLLPayment():this(new DAL.DALPayment(), new BLLClaims())
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
                //todo:如果 支付成功 
                if (payment.Status == enum_PaymentStatus.Trade_Success)
                {
                    errMsg = "该项已经支付完成";
                    log.Error(errMsg);
                    throw new Exception(errMsg);
                }

                //该支付项已经创建,验证其金额是否有变化
                var payAmount = GetPayAmount(order, payTarget);
                if (payAmount != payment.Amount)
                {
                    errMsg =string.Format( "本次申请金额和上次不一样. 本次:{0},上次:{1}",payment.Amount,payAmount);
                    log.Warn(errMsg);
                    //申请金额和之前的不一致, 需要警告
                }
                payment.Amount = payAmount;                
                Update(payment);
                

            }
            else if (paymentCount == 0)
            {
                payment = new Payment { Amount=GetPayAmount(order, payTarget), Order=order, PayTarget= payTarget};
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
            return Config.Config.GetAppSetting("PaySite") + "Pay/?paymentid=" + paymentId;
        }
        public Payment GetOne(Guid id)
        {
            return dal.GetOne(id);
        }
        public void SaveOrUpdate(Payment payment)
        {
            payment.LastUpdateTime = DateTime.Now;
            dal.SaveOrUpdate(payment);
        }
        public void Update(Payment payment)
        {
            payment.LastUpdateTime = DateTime.Now;
            dal.Update(payment);
        }

        public Payment GetPaymentForWaitPay(ServiceOrder order)
        {
            return dal.GetPaymentForWaitPay(order);
        }
        /// <summary>
        /// 查询订单支付的订金
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Payment GetPayedForDeposit(ServiceOrder order)
        {
            return dal.GetPayedByTarget(order, enum_PayTarget.Deposit);
        }
        /// <summary>
        /// 查询订单支付的尾款
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Payment GetPayedForFinal(ServiceOrder order)
        {
            return dal.GetPayedByTarget(order, enum_PayTarget.FinalPayment);
        }

        /// <summary>
        /// 获取支付总额
        /// </summary>
        /// <param name="payTarget">支付类型</param>
        /// <returns></returns>
        public decimal GetPayAmount(ServiceOrder order, enum_PayTarget payTarget)
        {
            if (payTarget == enum_PayTarget.Deposit)
            {
                return order.DepositAmount;
            }
            else if (payTarget == enum_PayTarget.FinalPayment)
            {
                return order.NegotiateAmount - order.DepositAmount;
            }
            else if (payTarget == enum_PayTarget.Compensation)
            {
                log.Debug("查询订单的理赔");
                Claims claims = bllClaims.GetOneByOrder(order);
                if (claims == null)
                {
                    log.Error("订单没有对应的理赔");
                    throw new Exception("订单没有对应的理赔");
                }

                log.Debug("查询理赔详情");
                IList<ClaimsDetails> cdList = claims.ClaimsDatailsList.OrderByDescending(x => x.LastUpdateTime).Where(x => x.Target == enum_ChatTarget.store).ToList();
                ClaimsDetails claimsDetails;
                if (cdList.Count > 0)
                {
                    claimsDetails = cdList[0];
                }
                else
                {
                    log.Error("该订单没有理赔");
                    throw new Exception("该订单没有理赔");
                }

                return claimsDetails.Amount;
            }
            else
            {
                throw new Exception("没有计算公式");
            }
        }
    }
}
