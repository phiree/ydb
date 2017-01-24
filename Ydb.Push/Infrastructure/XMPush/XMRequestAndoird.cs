using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ydb.Push.XMPush
{
    /// <summary>
    /// 单条消息请求参数
    /// </summary>
    public class XMRequestAndoird
    {
        PushTargetClient pushType;
        long time_to_live = 1000;
        int notify_id = 0;
        int notify_type = 2;
        string restricted_package_name { get { return pushType == PushTargetClient.PushToBusiness ? "ihelperseller.development.jsyk" : "ihelper.development.jsyk"; } }
        public string title { get { return pushType == PushTargetClient.PushToBusiness ? "一点办商户版" : "一点办"; } }
        public XMRequestAndoird(PushTargetClient pushtype)
        {
            this.pushType = pushtype;
        }
        
        public string payload { get; set; }                                    //消息内容

              //在通知栏的标题，长度小于16
        public string description { get; set; }
        public string alias { get; set; }


        //可选，自定义通知数字角标

        /*
       
         description=notification description11
&restricted_package_name=ihelperseller.development.jsyk
&alias=18608956891
&title=notification title11
&notify_type=2
&time_to_live=1000
&notify_id=0 
&payload=payload111
         */

        public string ToFormData()
        {
            StringBuilder formdata = new StringBuilder();
            formdata.Append("alias=" + alias+"&");
            formdata.Append("payload=" + payload + "&");
            formdata.Append("title=" + title + "&");
            formdata.Append("description=" + description + "&");
            formdata.Append("restricted_package_name=" + restricted_package_name + "&");
            formdata.Append("notify_type=" + notify_type + "&");
            formdata.Append("notify_id=" + notify_id + "&");
            return formdata.ToString().TrimEnd('&') ;
        }
        
    }
 

}
