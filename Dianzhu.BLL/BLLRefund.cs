using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.Pay;

namespace Dianzhu.BLL
{
    /// <summary>
    /// 退款模块 业务业务层
    /// </summary>
    public class BLLRefund
    {
        IDAL.IDALRefund dal;
        
        string errMsg = string.Empty;
        IDAL.IDALServiceOrder dalServiceOrder;
        
        public BLLRefund(IDAL.IDALRefund dal, IDAL.IDALServiceOrder dalServiceOrder)
        {
            this.dal = dal;
            this.dalServiceOrder = dalServiceOrder;
          
        }
        
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLLRefund");
        
        public void Save(Refund r)
        {
            dal.Add(r);
        }

        public void Update(Refund r)
        {
            r.LastUpdateTime = DateTime.Now;
            dal.Update(r);
        }

        public Refund GetRefundByPlatformTradeNo(string platformTradeNo)
        {
            return dal.GetRefundByPlatformTradeNo(platformTradeNo);
        }

        public void ReceiveAPICallBack(enum_PaylogType payLogType, IRefundCallBack irefundCallback, string rawRequestString, object callbackParameters)
        {
            BLLRefundLog bllRefundLog = new BLLRefundLog();
            //BLLServiceOrder bllOrder = new BLLServiceOrder();
            //获取回调参数 如果是get , 如果是post?

            //保存记录
            //PaymentLog paymentLog = new PaymentLog();
            //paymentLog.ApiString = rawRequestString + "------" + callbackParameters;
            //paymentLog.PaylogType = payLogType;
            //paymentLog.LogTime = DateTime.Now;
            //paymentLog.PayType = enum_PayType.Online;

            log.Debug("保存支付记录");
            RefundLog refundLog = new RefundLog(rawRequestString, Guid.Empty, 0, enum_PaylogType.ResultNotifyFromAli, enum_PayType.Online);
            bllRefundLog.Save(refundLog);

            //log.Debug("保存支付记录");
            //bllPaymentLog.SaveOrUpdate(paymentLog);

            //处理订单流程
            string platformOrderId, businessOrderId, errMsg;
            decimal amount;
            log.Debug("开始API回调");
            string returnstr = irefundCallback.RefundCallBack(callbackParameters, out businessOrderId, out platformOrderId, out amount, out errMsg);
            log.Debug("回调结果:" + returnstr);

            //todo: they are must be in a single transaction
            //更新支付记录
            log.Debug("TRADE_SUCCESS,更新支付记录,refundLogId为：" + refundLog.Id.ToString());
            refundLog.RefundAmount = amount;
            bllRefundLog.Update(refundLog);
            //paymentLog.PayAmount = amount;
            //paymentLog.PaymentId = new Guid(businessOrderId);
            //bllPaymentLog.SaveOrUpdate(paymentLog);

            //更新退款状态
            Refund refund = GetRefundByPlatformTradeNo(platformOrderId);
            if (refund != null)
            {
                log.Debug("TRADE_SUCCESS,更新支付项,paymentId为：" + refund.Id.ToString());
                refund.RefundStatus = enum_RefundStatus.Success;
                Update(refund);
            }
            else
            {
                log.Error("该退款没有支付记录");
                return;
            }

            //log.Debug("TRADE_SUCCESS,更新支付项,paymentId为：" + businessOrderId);
            //Payment payment = bllPayment.GetOne(new Guid(businessOrderId));
            //if (payment.Status == enum_PaymentStatus.Trade_Success)
            //{
            //    log.Debug("当前支付项状态为：" + payment.Status + "，直接返回");
            //    return;
            //}
            //payment.Status = enum_PaymentStatus.Trade_Success;
            //payment.PayApi = GetPayApi(payLogType);
            //payment.PlatformTradeNo = platformOrderId;
            //bllPayment.SaveOrUpdate(payment);

            ////更新订单状态.
            //log.Debug("TRADE_SUCCESS,订单当前状态为：" + payment.Order.OrderStatus.ToString());
            //log.Debug("TRADE_SUCCESS,更新订单状态");
            //ServiceOrder order = payment.Order;
            //switch (order.OrderStatus)
            //{
            //    case enum_OrderStatus.checkPayWithDeposit:
            //    case enum_OrderStatus.Created:
            //        //支付定金
            //        bllOrder.OrderFlow_ConfirmDeposit(order);
            //        break;
            //    case enum_OrderStatus.checkPayWithNegotiate:
            //    case enum_OrderStatus.Ended:
            //        bllOrder.OrderFlow_OrderFinished(order);
            //        break;
            //    case enum_OrderStatus.checkPayWithRefund:
            //    case enum_OrderStatus.WaitingPayWithRefund:
            //        bllOrder.OrderFlow_RefundSuccess(order);
            //        break;
            //    case enum_OrderStatus.checkPayWithIntervention:
            //    case enum_OrderStatus.NeedPayWithIntervention:
            //        bllOrder.OrderFlow_ConfirmInternention(order);
            //        break;
            //    default:
            //        break;
            //}
            //log.Debug("TRADE_SUCCESS,订单最新状态为：" + order.OrderStatus.ToString());
        }
    }
}
