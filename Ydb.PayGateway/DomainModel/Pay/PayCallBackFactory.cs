using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;

namespace Ydb.PayGateway.DomainModel.Pay
{
    public interface IPayCallBackFactory {

        IPayCallBack Create(string invokeUrl, string httpMethod, object requestParameters, out enum_PayAPI payApi);
    }
   public  class PayCallBackFactory:IPayCallBackFactory
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("Ydb.PayGateway.Pay.PayCallback.PayCallBackFactory");
        ICallBackVerify callBackNotify;
        public PayCallBackFactory(ICallBackVerify callBackNotify)
        {
            this.callBackNotify = callBackNotify;
        }
        public  IPayCallBack Create(string invokeUrl,string httpMethod,object requestParameters,out enum_PayAPI payApi)
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
                payCallBack = new PayCallBackAli(callBackNotify);
            }

            

            if (requestParameters.ToString().Contains("PayType=PayBatch"))
            {
                payApi = enum_PayAPI.AlipayWeb;
                requestParameters = requestParameters.ToString().Replace("PayType=PayBatch&", "");
                payCallBack = new PayCallBackAliBatch(callBackNotify);
               
            }


            return payCallBack ;
        }
    }
}
