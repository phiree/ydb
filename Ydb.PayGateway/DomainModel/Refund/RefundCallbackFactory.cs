using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;

namespace Ydb.PayGateway
{
    public    class RefundCallbackFactory
    {
       static log4net.ILog log = log4net.LogManager.GetLogger("Ydb.PayGateway.RefundCallbackFactory");
        public static IRefundCallBack CreateCallBack(string rawUrl,out enum_PayAPI payApi)
        {
            ////微信退款回调没有异步通知 删除
            IRefundCallBack refundCallBack = null;
            
                
                log.Debug("支付宝回调开始");
            payApi = enum_PayAPI.AlipayApp;
                //保存支付接口返回的原始数据
                
                //payCallBack = new PayCallBackAli();
                refundCallBack = new RefundCallBackAli();
           
             
            return refundCallBack;
        }
    }
}
