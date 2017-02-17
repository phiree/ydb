using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Push.JPush
{
    /// <summary>
    /// 极光推送的json格式,同一个属性的类型不一样
    /// platform: 可能是string (all) 可能是 liststring(["android","ios"])
    /// so:
    /// 现在只支持:
    /// 1) platform只能是 string[]
    /// 2) audience 只能是  alias.(不支持"all")
    /// 3) 只支持android
    /// </summary>
    public class JPushRequest
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.JPushrequest");
        static string errMsg;

        public static JPushRequest Create(string[] platForm,string[] targetlist, string alert,string title,string newsid)
        {
            JPushRequest req = new JPushRequest();

            req.platform = platForm;
            req.audience = new Audience(targetlist);

            req.notification = new Notification(alert, title, 1, newsid);
            return req;
        }


        public IList<string> platform { get; set; }
        public Audience audience { get;   set; }

        public Notification notification { get;   set; }
 
        public class Audience
        {
            public Audience() { }
            public Audience(IList<string> registration_id)
            {
                this.registration_id = registration_id;
            }
            public IList<string> registration_id { get; set; }

        }
 
        public class Notification
        {
            public Notification() { }
            public Notification(string alert,string title,int builder_id,string newsid)
            {
                this.android = new Android(alert, title, builder_id, newsid);
            }
            public Android android { get; set; }

            public class Android
            {
                public Android() { }
                public Android(string alert, string title, int builder_id, string newsid)
                {
                    this.alert = alert;
                    this.title = title;
                    this.builder_id = builder_id;
                    this.extras = new Extras(newsid);
                }
             public   string alert { get; set; }
                public string title { get; set; }
                public int builder_id { get; set; }
             public   Extras extras { get; set; }
               public class Extras
                {
                    public Extras() { }
                    internal Extras(string newsid)
                    {
                        this.newsid = newsid;
                    }
                  public  string newsid { get; set; }
                }
            }
        }

        public string ToJson()
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, settings);
        }
    }
 

}
