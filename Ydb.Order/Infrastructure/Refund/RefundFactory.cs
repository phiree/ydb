using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;
using Ydb.Order.DomainModel;
using Ydb.Order.Infrastructure;

namespace Ydb.Order.Infrasturcture
{


    public class RefundFactory
    {
        public static IRefundApi CreateRefund(Refund refund,  string operatorId)
        {
            string refund_no = DateTime.Now.ToString("yyyyMMdd") + refund.Id.ToString().Substring(0, 10);
            
            switch (refund.Payment.PayApi)
            {
                case enum_PayAPI.AlipayApp:
                    return new RefundAliApp(Dianzhu.Config.Config.GetAppSetting("PaySite") + "RefundCallBack/Alipay/notify_url.aspx",
                        refund_no, refund.RefundAmount, refund.PlatformTradeNo, operatorId);
                    
                case enum_PayAPI.Wechat:
                    return new RefundWePay(refund_no, refund.RefundAmount, refund.PlatformTradeNo, refund.TotalAmount, operatorId);

                default:throw new NotImplementedException("未实现的退款接口");
            }
        }

    }
}
    
