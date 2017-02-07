using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;
using Ydb.Common.Infrastructure;

namespace Ydb.PayGateway
{


    public class RefundFactory : IRefundFactory
    {
        IHttpRequest httpRequest;
        public RefundFactory(IHttpRequest httpRequest)
        {
            this.httpRequest = httpRequest;
        }
        public  IRefundApi CreateRefund(enum_PayAPI payApi,Guid orderRefundId, decimal refundAmount,decimal paymentTotalAmount, string platformTradeNo, string operatorId)
        {
            string refund_no = DateTime.Now.ToString("yyyyMMdd") + orderRefundId.ToString().Substring(0, 10);
            
            switch (payApi)
            {
                case enum_PayAPI.AlipayApp:
                    return new RefundAlipayApp(Dianzhu.Config.Config.GetAppSetting("PaySite") + "RefundCallBack/Alipay/notify_url.aspx",
                        refund_no, refundAmount, platformTradeNo, operatorId,httpRequest);
                    
                case enum_PayAPI.Wechat:
                    return new RefundWePay(refund_no, refundAmount, platformTradeNo, paymentTotalAmount, operatorId,httpRequest);

                default:throw new NotImplementedException("未实现的退款接口");
            }
        }

    }
}
    
