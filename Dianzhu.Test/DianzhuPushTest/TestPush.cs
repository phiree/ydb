﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Dianzhu.Push;
namespace Dianzhu.Test.DianzhuPushTest
{
    [TestFixture]
    public class TestPush
    {
        [Test]
        public void TestPushIOS()
        {
            //         Push.pushNotifications("8de76c196a605120db39ab58373edf159c1301b43659bd129fcf72b696e2a26c", "test_push", @"files\aps_development_Mark.p12", 1);

            IPush ipush = PushFactory.Create("ios", string.Empty);// new PushIOS(1,"default" );
            ipush.Push("test_push" + DateTime.Now.ToString(), "75645d9263bb15257718f7af39d20cd6245b049970a763680ce8da6ea7fcb7b7");
        }
        [Test]
        public void TestPushIOSFromWeb()
        {
            PHSuit.HttpHelper.CreateHttpRequest(
                string.Format("http://localhost:8040/push.ashx?client={0}&pushNum={1}&notificaitonSound={2}&message={3}&deviceToken={4}",
                "ios", 1, string.Empty, "i am pusing from web api, " + DateTime.Now.ToString(), "9192b3cc5112899606a2dbc5968ad948213d76ee73bf6ab3b3fa7c13ce0a58dd")
                , "get", null);

        }
        [Test]
        public void TestPushJsonConvertForJPush()
        {
            string request = @"
  {
    ""platform"": [""android"",""ios""],
    ""audience"": {
                ""alias"": [
                    ""深圳"",
                    ""北京""
        ]
    },
    ""notification"": {
        ""android"": {
            ""alert"": ""Hi, JPush!"",
            ""title"": ""Send to Android"",
            ""builder_id"": 1,
            ""extras"": {
                ""newsid"": ""321""
            }
        },
        
    } 
}
                ";

            Push.JPush.JPushRequest jpushreq = 
                Newtonsoft.Json.JsonConvert
                .DeserializeObject<Push.JPush.JPushRequest>(request);

            Assert.AreEqual("深圳", jpushreq.audience.alias[0]);
            Assert.AreEqual("321", jpushreq .notification.android.extras.newsid);

        }
        [Test]
        public void TestJPushDeserial()
        {

            Push.JPush.JPushRequest req = Push.JPush.JPushRequest.Create
                (new string[] { "android" },new string[] { "aaa" },"alert","title","newsid"
                );

            Console.WriteLine(req.ToJson());
        }
    }


}
