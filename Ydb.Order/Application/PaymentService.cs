using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Common;
using Ydb.Order.DomainModel.Repository;
using Ydb.Common.Specification;

namespace Ydb.Order.Application
{
    //支付相关
  public  class PaymentService : IPaymentService
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.Order.Application.PaymentService");
        IRepositoryPayment repoPayment;
        IRepositoryServiceOrder repoOrder;
        IRepositoryClaims repoClaims;
        IRepositoryRefund repoRefund;
        //todo: applicationservice shouldn't reference other app_service?
        IServiceOrderService orderService;
        public PaymentService(IRepositoryPayment repoPayment, IRepositoryServiceOrder repoOrder, 
            IServiceOrderService orderService, IRepositoryClaims repoClaims
            ,IRepositoryRefund repoRefund)
        {
            this.repoPayment = repoPayment;
            this.repoOrder = repoOrder;
            
            this.orderService = orderService;
            this.repoClaims = repoClaims;
            this.repoRefund = repoRefund;
        }
        /// <summary>
        /// todo:　领域逻辑泄漏, 应该移到domain内部.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="payTarget"></param>
        /// <returns></returns>

        public Payment ApplyPay(string orderId, enum_PayTarget payTarget)
        {
            string errMsg = string.Empty;
            ServiceOrder order = repoOrder.FindById(new Guid(orderId));
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
                    applyIsValid = order.OrderStatus == enum_OrderStatus.Ended || order.OrderStatus == enum_OrderStatus.Finished;
                    break;
                case enum_PayTarget.Compensation:
                    errMsg = "只有已经完成的订单 才能申请赔偿.";
                    applyIsValid = order.OrderStatus == enum_OrderStatus.Finished ||
                          order.OrderStatus == enum_OrderStatus.Appraised;
                    break;
            }
            if (!applyIsValid)
            {
                throw new Exception(errMsg);
            }
            //获取该订单已经申请过的项目.
            IList<Payment> payments = repoPayment.GetPaymentsForOrder(order);
            var paymentList = payments.Where(x => x.PayTarget == payTarget).ToList();
            //验证该支付申请是否有效. 
            //无效: 同类型的支付申请已经创建, 直接返回该支付链接. 当前支付金额
            var paymentCount = paymentList.Count();
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
                    errMsg = string.Format("本次申请金额和上次不一样. 本次:{0},上次:{1}", payment.Amount, payAmount);
                    log.Warn(errMsg);
                    //申请金额和之前的不一致, 需要警告
                }
                payment.Amount = payAmount;
              


            }
            else if (paymentCount == 0)
            {
                payment = new Payment(GetPayAmount(order, payTarget), order, payTarget);

                repoPayment.Add(payment);
            }
            else //已经存在多项
            {
                errMsg = string.Format("该订单已经存在多项同类型的支付项", order.Id);
                log.Fatal(errMsg);
                throw new Exception(errMsg);
            }
            return payment;
        }
        public  decimal GetPayAmount(ServiceOrder order, enum_PayTarget payTarget)
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
                Claims claims = repoClaims.GetOneByOrder(order);
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

        /// <summary>
        /// 支付回调
        /// 只处理需要更新payment和order的回调.
        /// </summary>
        /// <param name="returnstr"></param>
        public void PayCallBack(enum_PayAPI payApi, string returnstr,string paymentId,string platformTradeNo)
        {
            //更新支付状态
            Payment payment = repoPayment.FindById(new Guid(paymentId));
        
             if (payment.Status == enum_PaymentStatus.Trade_Success) { log.Debug("当前支付项状态为：" + payment.Status + "，直接返回"); return; }

                payment.Status = enum_PaymentStatus.Trade_Success;
                payment.PayApi = payApi;
                payment.PayType = enum_PayType.Online;
                payment.PlatformTradeNo = platformTradeNo;
                
                //更新订单状态.

                ServiceOrder order = repoOrder.FindById(payment.Order.Id); //payment.Order;
                 switch (order.OrderStatus)
                {
                    case enum_OrderStatus.checkPayWithDeposit:
                    case enum_OrderStatus.Created:
                    //支付定金
                    orderService.OrderFlow_ConfirmDeposit(order);
                        break;
                    case enum_OrderStatus.WaitingDepositWithCanceled:
                        log.Debug("系统确认到帐，订单当前在等待退款，直接退款");
                        if (orderService.ApplyRefund(payment, payment.Amount, "取消订单退还订金"))
                        {
                            log.Debug("更新订单状态");

                        orderService.OrderFlow_EndCancel(order);
                        }
                        else
                        {
                            log.Error("退款失败，需联系系统管理员");
                        }
                        break;
                    case enum_OrderStatus.checkPayWithNegotiate:
                    case enum_OrderStatus.Ended:
                    orderService.OrderFlow_OrderFinished(order);
                        break;
                    case enum_OrderStatus.checkPayWithRefund:
                    case enum_OrderStatus.WaitingPayWithRefund:
                    orderService.OrderFlow_RefundSuccess(order);
                        break;
                    case enum_OrderStatus.checkPayWithIntervention:
                    case enum_OrderStatus.NeedPayWithIntervention:
                    orderService.OrderFlow_ConfirmInternention(order);
                        break;
                    default:
                        break;
                }
                log.Debug("TRADE_SUCCESS,订单最新状态为：" + order.OrderStatus.ToString());
          
        }
        
        public void RefundCallBack(enum_PayAPI payApi, string returnstr, string refundId, string platformTradeNo)
        {
             
            log.Debug("保存退款回调记录");
           //更新退款状态
            Refund refund =repoRefund.GetRefundByPlatformTradeNo(platformTradeNo);
            if (refund != null)
            {
                log.Debug("TRADE_SUCCESS,更新支付项,refundId为：" + refund.Id.ToString());
                refund.RefundStatus = enum_RefundStatus.Success;
            }
            else
            {
                log.Error("该退款没有支付记录");
                return;
            }
        }
        /// <summary>
        /// 条件读取支付项
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="payStatus"></param>
        /// <param name="payType"></param>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IList<Payment> GetPays(TraitFilter filter, string payStatus, string payType, Guid orderID, Guid userID)
        {
            var where = PredicateBuilder.True<Payment>();
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.Order.Id == orderID);
            }
            if (userID != Guid.Empty)
            {
                where = where.And(x => x.Order.CustomerId == userID.ToString());
            }
            if (!string.IsNullOrEmpty(payStatus))
            {
                enum_PaymentStatus ps;
                if (Enum.TryParse<enum_PaymentStatus>(payStatus, out ps))
                {
                    where = where.And(x => x.Status == ps);
                }
                else
                {
                    throw new Exception("查询条件错误：不存在支付状态" + payStatus);
                }
            }
            if (!string.IsNullOrEmpty(payType))
            {
                enum_PayTarget ps;
                if (Enum.TryParse<enum_PayTarget>(payType, out ps))
                {
                    where = where.And(x => x.PayTarget == ps);
                }
                else
                {
                    throw new Exception("查询条件错误：不存在支付类型" + payType);
                }
            }
            Payment baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = repoPayment.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? repoPayment.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() 
                : repoPayment.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;
        }
        /// <summary>
        /// 统计支付项的数量
        /// </summary>
        /// <param name="payStatus"></param>
        /// <param name="payType"></param>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public long GetPaysCount(string payStatus, string payType, Guid orderID, Guid userID)
        {
            var where = PredicateBuilder.True<Payment>();
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.Order.Id == orderID);
            }
            if (userID != Guid.Empty)
            {
                where = where.And(x => x.Order.CustomerId == userID.ToString());
            }
            if (payStatus != null && payStatus != "")
            {
                where = where.And(x => x.Status == (enum_PaymentStatus)Enum.Parse(typeof(enum_PaymentStatus), payStatus));
            }
            if (payType != null && payType != "")
            {
                where = where.And(x => x.PayTarget == (enum_PayTarget)Enum.Parse(typeof(enum_PayTarget), payType));
            }
            long count = repoPayment.GetRowCount(where);
            return count;
        }
        /// <summary>
        /// 读取支付项 根据ID
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Payment GetPay(Guid orderID, Guid payID, Guid userID)
        {
            var where = PredicateBuilder.True<Payment>();
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.Order.Id == orderID);
            }
            if (userID != Guid.Empty)
            {
                where = where.And(x => x.Order.CustomerId == userID.ToString());
            }
            if (payID != Guid.Empty)
            {
                where = where.And(x => x.Id == payID);
            }
            Payment payment = repoPayment.FindOne(where);
            return payment;
        }
        public void Update(Payment payment)
        {
            repoPayment.Update(payment);
        }
        public Payment GetOne(Guid id)
        {
            return repoPayment.FindById(id);
        }
       
    }
}
