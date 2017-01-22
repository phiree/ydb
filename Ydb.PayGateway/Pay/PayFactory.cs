using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;
using Ydb.PayGateway;

namespace Ydb.PayGateway
{ 
   public class PayFactory
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="payApi"></param>
        /// <param name="amount"></param>
        /// <param name="subject"></param>
        /// <param name="payTarget"></param>
        /// <returns></returns>
        public static IPayRequest CreatePayAPI(enum_PayAPI payApi,decimal amount,string paymentId,string subject)
        {
            
            
         
            switch (payApi)
            {
                case enum_PayAPI.AlipayWeb:
                    return new PayAli(amount, paymentId, subject,
                            "1",
                            Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/notify_url.aspx",
                            Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/return_url.aspx",
                            "http://www.ydban.cn");
                    ;
                case enum_PayAPI.AlipayBatch:
                    return new PayBatch(amount, paymentId, subject,
                  Dianzhu.Config.Config.GetAppSetting("PaySite") + "PayCallBack/alipay/notify_url.aspx?PayType=PayBatch",
                  "http://www.ydban.cn");
                    ;
                default:
                    throw new NotImplementedException("尚未实现该接口");

            }
        }

        

    }
}
