using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Infrastructure;
namespace Ydb.Push.XMPush
{

    public class XMPush : IPushApi
    {
        string apiUrl = "https://api.xmpush.xiaomi.com/v2/message/alias";
        string secretUser = "6TRMaje2tzRjzuQzO0Oq8Q==";
        string secretBusiness = "ZKLdQ2Sdo0SfS84pxa3HTw==";


       
        
       
        IHttpRequest httpRequest;
        public XMPush(IHttpRequest httpRequest)
        {
            this.httpRequest = httpRequest;
        }

        public string Push(PushTargetClient pushType, PushMessage pushMessage,   string target, int amount)
        {
            string secret= pushType == PushTargetClient.PushToUser ? secretUser : secretBusiness;
            string result = string.Empty;
            XMRequestAndoird msg = new XMRequestAndoird(pushType);
            msg.alias =target;
            msg.description = pushMessage.DisplayContent;
         
                msg.payload = pushMessage.OrderId;
         
            

            string formData = msg.ToFormData();
           
            result= httpRequest.CreateHttpRequest(apiUrl, "post", formData, "key=" + secret);
            return result;

            /*
             Request URL: https://api.xmpush.xiaomi.com/v2/message/regid
Request Method: POST
Form Data:
description=notification description
&payload=this+is+xiaomi+push
&restricted_package_name=com.xiaomi.mipushdemo
&registration_id=123
&title=notification title
&notify_type=2
&time_to_live=1000
&notify_id=0
             */
        }
    }
 
}
