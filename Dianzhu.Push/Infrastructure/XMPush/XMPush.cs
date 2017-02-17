using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Push.XMPush
{

    public class XMPush : IPushApi
    {
        string apiUrl = "https://api.xmpush.xiaomi.com/v2/message/alias";
        string secretUser = "6TRMaje2tzRjzuQzO0Oq8Q==";
        string secretBusiness = "ZKLdQ2Sdo0SfS84pxa3HTw==";


        PushType pushType;
        PushMessage pushMessage;
        string secret { get { return pushType == PushType.PushToUser ? secretUser : secretBusiness; } }

        public XMPush(PushType pushType,PushMessage message)
        {
            this.pushType = pushType;
            this.pushMessage = message;
        }

        

        public string Push(   string target, int amount)
        {
            string result = string.Empty;
            XMRequestAndoird msg = new XMRequestAndoird(pushType);
            msg.alias =target;
            msg.description = pushMessage.DisplayContent;
         
                msg.payload = pushMessage.OrderId;
         
            

            string formData = msg.ToFormData();
           
            result= PHSuit.HttpHelper.CreateHttpRequest(apiUrl, "post", formData, "key=" + secret);
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
