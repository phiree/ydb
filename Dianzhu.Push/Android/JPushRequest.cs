using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Push.Android
{
    public class JPushRequest
    {
        public string platform { get; set; }
        public Audience audience { get; set; }
   
        public Notification notification { get; set; }
       
        public Message message { get; set; }
      
        public SMSMessage sms_message { get; set; }
        public Options options { get; set; }


        public class Audience
        {
          public  IList<string> tag { get; set; }
        }
        public class SMSMessage {
            string content { get; set; }
            int delay_time { get; set; }
        }
        public class Options {
            public int time_to_live { get; set; }
            public bool apns_production { get; set; }
        }
        public class Message
        {
            public string msg_countent { get; set; }
            public string content_type { get; set; }
            public string title { get; set; }
            Extra extra { get; set; }
            class Extra
            {
                string key { get; set; }
            }

        }
        public class Notification
        {

            public Android android { get; set; }
            public IOS ios;


            public class IOS
            {
                public string alert { get; set; }
                public string sound { get; set; }
                public string badge { get; set; }
               public Extras extras { get; set; }
               public class Extras
                {
                 public   int newsid { get; set; }
                }
            }
            public class Android
            {
                string alert { get; set; }
                string title { get; set; }
                string builder_id { get; set; }
                Extras extras { get; set; }
                class Extras
                {
                    int newsid { get; set; }
                }
            }
        }

    }

}
