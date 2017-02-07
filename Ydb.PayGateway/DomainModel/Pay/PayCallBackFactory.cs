using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;

namespace Ydb.PayGateway.DomainModel.Pay
{
   public  class PayCallBackFactory
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("Ydb.PayGateway.Pay.PayCallback.PayCallBackFactory");
        public static IPayCallBack CreateCallBack(string invokeUrl,string httpMethod,object requestParameters,out enum_PayAPI payApi)
        {
            IPayCallBack payCallBack = null;
            payApi = enum_PayAPI.None;
            
            if (invokeUrl.ToLower().StartsWith("/paycallback/wepay"))
            {
                payApi = enum_PayAPI.Wechat;
                
                payCallBack = new PayCallBackWePay();
                
            }
            else if (invokeUrl.ToLower().StartsWith("/paycallback/alipay"))
            {

                payApi = enum_PayAPI.AlipayApp;
                payCallBack = new PayCallBackAli();
            }

            

            if (requestParameters.ToString().Contains("PayType=PayBatch"))
            {
                payApi = enum_PayAPI.AlipayWeb;
                requestParameters = requestParameters.ToString().Replace("PayType=PayBatch&", "");
                payCallBack = new PayCallBackAliBatch();
               
            }


            return payCallBack ;
        }
    }
}
