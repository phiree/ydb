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
namespace Dianzhu.BLL
{
    //支付接口实现支付.
    public class BLLPay
    {
        public IPay CreatePayAPI(enum_PayAPI payApi, ServiceOrder order, enum_PayTarget payTarget)
        {


            decimal payAmount = order.GetAmount(payTarget);
            string paySubject = order.ServiceName;
            string paySubjectPre = GetPreSubject(payTarget, order);

            BLLPayment bllPayment = new BLLPayment();
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
                //payment_type,  notify_url,  return_url, show_url

                case enum_PayAPI.Wechat:
                default:
                    throw new NotImplementedException("尚未实现该接口");

            }
        }
        public void ReceiveAPICallBack()
        {

        }
        public void SavePaymentLog(Payment payment, enum_PayType payType, enum_PaylogType paylogType, enum_PayTarget payTarget, enum_PayAPI payApi, string apiString)
        {
            BLLPaymentLog bllPaymentLog = new BLLPaymentLog();

            PaymentLog paymentLog = new PaymentLog
            {
                LogTime = DateTime.Now,
                ApiString = apiString,
                PayAmount = payment.Amount,
                PayApi = payApi,
                PaylogType = paylogType,
                PayTarget = payTarget,
                PayType = payType,
                PaymentId = payment.Id
            };

            bllPaymentLog.SaveOrUpdate(paymentLog);
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
    public class BLLPaymentLog
    {

        public DALPaymentLog DALPaymentLog = DALFactory.DALPaymentLog;

        public void SaveOrUpdate(PaymentLog PaymentLog)
        {
            DALPaymentLog.SaveOrUpdate(PaymentLog);
        }
    }
}
