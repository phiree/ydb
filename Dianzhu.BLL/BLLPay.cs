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
        public IPay CreatePayAPI(enum_PayAPI payApi, ServiceOrder order)
        {
            switch (payApi)
            {
                case enum_PayAPI.Alipay:
                    return new PayAli(order,
                            "1",
                            Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/notify_url.aspx",
                            Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/return_url.aspx",
                            "http://www.ydban.cn"
             //payment_type,  notify_url,  return_url, show_url
             );

                case enum_PayAPI.Wechat:
                default:
                    throw new NotImplementedException("尚未实现该接口");

            }
        }
        public void ReceiveAPICallBack()
        {

        }
        public void SavePaymentLog(ServiceOrder order, enum_PayType payType, enum_PaylogType paylogType, enum_PayTarget payTarget, enum_PayAPI payApi, string apiString)
        {
            BLLPaymentLog bllPaymentLog = new BLLPaymentLog();

            PaymentLog paymentLog = new PaymentLog
            {
                LogTime = DateTime.Now,
                ApiString = apiString,
                PayAmount = order.GetAmount(payTarget),
                PayApi = payApi,
                PaylogType = paylogType,
                PayTarget = payTarget,
                PayType = payType,
                ServiceOrder = order
            };

            bllPaymentLog.SaveOrUpdate(paymentLog);
        }
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
