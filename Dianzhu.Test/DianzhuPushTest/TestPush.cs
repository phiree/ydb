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

            IPushApi ipush = PushFactory.Create(PushType.PushToUser, "ios",new PushMessage { DisplayContent="test"});// new PushIOS(1,"default" );
            ipush.Push(  "bbcefba2dcaa2fbc2b644fbb7d78d7bfe63d0730f7ea1c9f745cd6769d515a1a",2);
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

           Dianzhu.Push.JPush. JPushRequest jpushreq = 
                Newtonsoft.Json.JsonConvert
                .DeserializeObject < Dianzhu.Push.JPush.JPushRequest>(request);

            Assert.AreEqual("深圳", jpushreq.audience.registration_id[0]);
            Assert.AreEqual("321", jpushreq .notification.android.extras.newsid);

        }
        [Test]
        public void TestJPushDeserial()
        {

            Dianzhu.Push.JPush.JPushRequest req = Dianzhu.Push.JPush.JPushRequest.Create
                (new string[] { "android" },new string[] { "aaa" },"alert","title","newsid"
                );

            Console.WriteLine(req.ToJson());
        }
    }


}
