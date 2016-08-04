using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.DAL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using System.Web;
using System.IO;
using Dianzhu.Pay;
using System.Collections.Specialized;
using Dianzhu.Pay.RefundRequest;
using Dianzhu.IDAL;

namespace Dianzhu.BLL
{
    //支付接口实现支付. 
    public class BLLPay
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLLPay");
        
        BLLPayment bllPayment;
        //BLLServiceOrder bllOrder =Bootstrap.Container.Resolve<BLLServiceOrder>();
        IBLLServiceOrder bllOrder;
        IDALPaymentLog bllPaymentLog;

        public BLLPay(IBLLServiceOrder bllOrder, IDALPaymentLog bllPaymentLog,BLLPayment bllPayment)
        {
            this.bllOrder = bllOrder;
            this.bllPaymentLog = bllPaymentLog;
            this.bllPayment = bllPayment;
        }

        public IPayRequest CreatePayAPI(enum_PayAPI payApi, ServiceOrder order, enum_PayTarget payTarget)
        {


            decimal payAmount = bllPayment.GetPayAmount(order, payTarget);
            string paySubject = order.Title;
            string paySubjectPre = GetPreSubject(payTarget, order);
            Payment payment = bllPayment.ApplyPay(order, payTarget);
            string paymentId = payment.Id.ToString();

            switch (payApi)
            {
                case enum_PayAPI.Alipay:
                    return new PayAli(payAmount, paymentId, paySubject,
                            "1",
                            Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/notify_url.aspx",
                            Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/return_url.aspx",
                            "http://www.ydban.cn");
                    ;
                //payment_type,  notify_url,  return_url, show_url

                case enum_PayAPI.Wechat:
                default:
                    throw new NotImplementedException("尚未实现该接口");

            }
        }
        /// <summary>
        /// 支付平台回调,通知支付结果.
        /// </summary>
        /// <param name="payLogType">支付类型</param>
        /// <param name="ipayCallback">支付平台回调方法</param>
        /// <param name="querystring">支付平台的完整请求</param>
        /// <param name="callBackQuery">请求参数列表</param>
        public void ReceiveAPICallBack(enum_PaylogType payLogType, IPayCallBack ipayCallback, string rawRequestString, object callbackParameters)
        {
            //获取回调参数 如果是get , 如果是post?
           
            //保存记录
            PaymentLog paymentLog = new PaymentLog();
            paymentLog.ApiString = rawRequestString+"------"+callbackParameters;
            paymentLog.PaylogType = payLogType;
            paymentLog.LogTime = DateTime.Now;
            paymentLog.PayType = enum_PayType.Online;
            
            log.Debug("保存支付记录");
            bllPaymentLog.Add(paymentLog);

            //处理订单流程
            string platformOrderId, businessOrderId, errMsg;
            decimal amount;
            log.Debug("开始API回调");
            string returnstr= ipayCallback.PayCallBack(callbackParameters, out businessOrderId,out platformOrderId,out amount,out errMsg);
            log.Debug("回调结果:" + returnstr);
            if (returnstr == "ERROR")
            {
                log.Error(errMsg);
                throw new Exception(errMsg);
            }
            else if (returnstr == "FAIL")
            {
                log.Debug("FAIL,更新支付项,paymentId为：" + businessOrderId);
                Payment payment = bllPayment.GetOne(new Guid(businessOrderId));
                payment.Status = enum_PaymentStatus.Fail;
                payment.PayType = enum_PayType.Online;
                payment.PayApi = GetPayApi(payLogType);
                payment.PlatformTradeNo = platformOrderId;
                bllPayment.Update(payment);
            }
            else if (returnstr == "TRADE_CLOSED")
            {
                log.Debug("TRADE_CLOSED,更新支付项,paymentId为：" + businessOrderId);
                Payment payment = bllPayment.GetOne(new Guid(businessOrderId));
                payment.Status = enum_PaymentStatus.Trade_Closed;
                payment.PayType = enum_PayType.Online;
                payment.PayApi = GetPayApi(payLogType);
                payment.PlatformTradeNo = platformOrderId;
                bllPayment.Update(payment);
            }
            else if(returnstr == "TRADE_FINISHED")
            {
                log.Debug("TRADE_FINISHED,更新支付项,paymentId为：" + businessOrderId);
                Payment payment = bllPayment.GetOne(new Guid(businessOrderId));
                payment.Status = enum_PaymentStatus.Trade_Finished;
                payment.PayType = enum_PayType.Online;
                payment.PayApi = GetPayApi(payLogType);
                payment.PlatformTradeNo = platformOrderId;
                bllPayment.Update(payment);
            }
            else if(returnstr == "WAIT_BUYER_PAY")
            {
                log.Debug("回调结果为WAIT_BUYER_PAY，目前不做任何处理");
            }
            else if(returnstr == "TRADE_SUCCESS")
            {
                //todo: they are must be in a single transaction
                //更新支付记录
                log.Debug("TRADE_SUCCESS,更新支付记录,paymentLogId为：" + paymentLog.Id.ToString());
                paymentLog.PayAmount = amount;
                paymentLog.PaymentId = new Guid(businessOrderId);
                bllPaymentLog.Update(paymentLog);

                log.Debug("TRADE_SUCCESS,更新支付项,paymentId为：" + businessOrderId);
                Payment payment= bllPayment.GetOne(new Guid(businessOrderId));
                if(payment.Status == enum_PaymentStatus.Trade_Success)
                {
                    log.Debug("当前支付项状态为：" + payment.Status + "，直接返回");
                    return;
                }
                payment.Status = enum_PaymentStatus.Trade_Success;
                payment.PayApi = GetPayApi(payLogType);
                payment.PayType = enum_PayType.Online;
                payment.PlatformTradeNo = platformOrderId;
                bllPayment.Update(payment);

                //更新订单状态.
                log.Debug("TRADE_SUCCESS,订单当前状态为：" + payment.Order.OrderStatus.ToString());
                log.Debug("TRADE_SUCCESS,更新订单状态");
                ServiceOrder order = payment.Order;
                switch (order.OrderStatus)
                {
                    case enum_OrderStatus.checkPayWithDeposit:
                    case enum_OrderStatus.Created:
                        //支付定金
                        bllOrder.OrderFlow_ConfirmDeposit(order);
                        break;
                    case enum_OrderStatus.WaitingDepositWithCanceled:
                        log.Debug("系统确认到帐，订单当前在等待退款，直接退款");
                        if (bllOrder.ApplyRefund(payment, payment.Amount, "取消订单退还订金"))
                        {
                            log.Debug("更新订单状态");

                            bllOrder.OrderFlow_EndCancel(order);
                        }
                        else
                        {
                            log.Error("退款失败，需联系系统管理员");
                        }
                        break;
                    case enum_OrderStatus.checkPayWithNegotiate:
                    case enum_OrderStatus.Ended:
                        bllOrder.OrderFlow_OrderFinished(order);
                        break;
                    case enum_OrderStatus.checkPayWithRefund:
                    case enum_OrderStatus.WaitingPayWithRefund:
                        bllOrder.OrderFlow_RefundSuccess(order);
                        break;
                    case enum_OrderStatus.checkPayWithIntervention:
                    case enum_OrderStatus.NeedPayWithIntervention:
                        bllOrder.OrderFlow_ConfirmInternention(order);
                        break;
                    default:
                        break;
                }
                log.Debug("TRADE_SUCCESS,订单最新状态为：" + order.OrderStatus.ToString());
            }
            

        }

        protected enum_PayAPI GetPayApi(enum_PaylogType payLogType)
        {
            enum_PayAPI payApi;
            switch (payLogType)
            {
                case enum_PaylogType.ResultReturnFromAli:
                case enum_PaylogType.ResultNotifyFromAli:
                    payApi = enum_PayAPI.Alipay;
                    break;
                case enum_PaylogType.ReturnNotifyFromWePay:
                    payApi = enum_PayAPI.Wechat;
                    break;
                default:
                    payApi = enum_PayAPI.None;
                    break;
            }
            return payApi;
        }
        public void SavePaymentLog(Payment payment, enum_PayType payType, enum_PaylogType paylogType, enum_PayTarget payTarget, enum_PayAPI payApi, string apiString)
        {
            PaymentLog paymentLog = new PaymentLog
            {
                LogTime = DateTime.Now,
                ApiString = apiString,
                PayAmount = payment.Amount,
                PaylogType = paylogType,
                PayType = payType,
                PaymentId = payment.Id
            };

            bllPaymentLog.Add(paymentLog);
        }

        #region helper
        /// <summary>
        /// 获取订单描述的前缀
        /// </summary>
        /// <param name="payTarget"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private string GetPreSubject(enum_PayTarget payTarget, ServiceOrder order)
        {
            string preSubject = string.Empty;
            switch (payTarget)
            {
                case enum_PayTarget.Deposit:

                    break;
                case enum_PayTarget.FinalPayment: break;
                case enum_PayTarget.Compensation: break;
            }
            return preSubject;
        }
        #endregion
    }
}
