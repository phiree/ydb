﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace PHSuit
{
    [TestFixture]
    public class test
    {
        [Test]
        public void test_GetNextDay()
        {
            DateTime dt = new DateTime(2009, 10, 2, 3, 10, 12);
            DateTime nextday = PHCore.GetNextDay(dt);
            Console.WriteLine(nextday.ToString());
            Assert.AreEqual(nextday - new DateTime(2009, 10, 3), new TimeSpan(0));
        }
        [Test]
        public void test_GetRandon()
        {
            string r = PHCore.GetRandom(10);
            Console.WriteLine(r);
            Assert.LessOrEqual(Convert.ToInt64(r), long.MaxValue);
        }
        [Test]
        public void parseUrlParam()
        {
            Assert.AreEqual("alasdf_$_dlaer", PHSuit.StringHelper.ParseUrlParameter("http://asdfesrawer/getfile.ashx?filename=alasdf_$_dlaer", "filename"));
        }
        [Test]
        public void crypttest()
        {

            Console.WriteLine("172_dianzhu_publish_test:" + PHSuit.Security.Encrypt("172_dianzhu___data source=192.168.1.172;uid=root;pwd=root;database=dianzhu_publish_test", false));
            Console.WriteLine("172_ydb_membership:" + PHSuit.Security.Encrypt("172_membership___data source=192.168.1.172;uid=root;pwd=root;database=ydb_membership", false));
            Console.WriteLine("172_ydb_instantmessage:" + PHSuit.Security.Encrypt("172_instantmessage___data source=192.168.1.172;uid=root;pwd=root;database=ydb_instantmessage", false));
            Console.WriteLine("172_ydb_finance:" + PHSuit.Security.Encrypt("172_finance___data source=192.168.1.172;uid=root;pwd=root;database=ydb_finance", false));
            Console.WriteLine("172_ydb_common:" + PHSuit.Security.Encrypt("172_common___data source=192.168.1.172;uid=root;pwd=root;database=ydb_common", false));
            Console.WriteLine("172_ydb_businessresource:" + PHSuit.Security.Encrypt("172_businessresource___data source=192.168.1.172;uid=root;pwd=root;database=ydb_businessresource", false));

            Console.WriteLine("localhost_Dianzhu" + ":" + PHSuit.Security.Encrypt("localhost_dianzhu___data source=localhost;uid=root;pwd=root;database=dianzhu", false));
            Console.WriteLine("localhost_ydb_membership" + ":" + PHSuit.Security.Encrypt("localhost_membership___data source=localhost;uid=root;pwd=root;database=ydb_membership", false));
            Console.WriteLine("localhost_ydb_instantmessage" + ":" + PHSuit.Security.Encrypt("localhost_instantmessage___data source=localhost;uid=root;pwd=root;database=ydb_instantmessage", false));
            Console.WriteLine("localhost_ydb_finance" + ":" + PHSuit.Security.Encrypt("localhost_finance___data source=localhost;uid=root;pwd=root;database=ydb_finance", false));
            Console.WriteLine("localhost_ydb_common" + ":" + PHSuit.Security.Encrypt("localhost_common___data source=localhost;uid=root;pwd=root;database=ydb_common", false));
            Console.WriteLine("localhost_ydb_businessresource" + ":" + PHSuit.Security.Encrypt("localhost_businessresource___data source=localhost;uid=root;pwd=root;database=ydb_businessresource", false));

            Console.WriteLine("138_dianzhu:" + PHSuit.Security.Encrypt("138_dianzhu___data source=192.168.1.138;uid=root;pwd=root;database=dianzhu", false));
            Console.WriteLine("138_ydb_membership:" + PHSuit.Security.Encrypt("138_membership___data source=192.168.1.138;uid=root;pwd=root;database=ydb_membership", false));
            Console.WriteLine("138_ydb_instantmessage:" + PHSuit.Security.Encrypt("138_instantmessage___data source=192.168.1.138;uid=root;pwd=root;database=ydb_instantmessage", false));
            Console.WriteLine("138_ydb_finance:" + PHSuit.Security.Encrypt("138_finance___data source=192.168.1.138;uid=root;pwd=root;database=ydb_finance", false));
            Console.WriteLine("138_ydb_common:" + PHSuit.Security.Encrypt("138_common___data source=192.168.1.138;uid=root;pwd=root;database=ydb_common", false));
            Console.WriteLine("138_ydb_businessresource:" + PHSuit.Security.Encrypt("138_businessresource___data source=192.168.1.138;uid=root;pwd=root;database=ydb_businessresource", false));


            Console.WriteLine("150_dianzhu_test:" + PHSuit.Security.Encrypt("data source=192.168.1.150;uid=ydb;pwd=jsyk2016;database=dianzhu_test", false));
            Console.WriteLine("150_ydb_instantmessage:" + PHSuit.Security.Encrypt("data source=192.168.1.150;uid=ydb;pwd=jsyk2016;database=ydb_instantmessage", false));

            Console.WriteLine("Aliyun_Official_dianzhu_publish:" + PHSuit.Security.Encrypt("data source=business.ydban.cn;uid=jsyk2016;pwd=X*G7czoy6twAYIz7;port=4407;database=dianzhu", false));
            Console.WriteLine("Aliyun_Official_ydb_instantmessage:" + PHSuit.Security.Encrypt("data source=business.ydban.cn;uid=jsyk2016;pwd=X*G7czoy6twAYIz7;port=4407;database=ydb_instantmessage", false));
            Console.WriteLine("Aliyun_Official_ydb_membership:" + PHSuit.Security.Encrypt("data source=business.ydban.cn;uid=jsyk2016;pwd=X*G7czoy6twAYIz7;port=4407;database=ydb_membership", false));
            Console.WriteLine("Aliyun_Official_ydb_finance:" + PHSuit.Security.Encrypt("data source=business.ydban.cn;uid=jsyk2016;pwd=X*G7czoy6twAYIz7;port=4407;database=ydb_finance", false));
            Console.WriteLine("Aliyun_Official_ydb_common:" + PHSuit.Security.Encrypt("data source=business.ydban.cn;uid=jsyk2016;pwd=X*G7czoy6twAYIz7;port=4407;database=ydb_common", false));

            Console.WriteLine("Aliyun_Test_dianzhu_publish:" + PHSuit.Security.Encrypt("data source=dev.ydban.cn;uid=jsyk2016;pwd=IQDv$qefeqFWuq_L;port=4407;database=dianzhu_publish", false));
            Console.WriteLine("Aliyun_Test_ydb_instantmessage:" + PHSuit.Security.Encrypt("data source=dev.ydban.cn;uid=jsyk2016;pwd=IQDv$qefeqFWuq_L;port=4407;database=ydb_instantmessage", false));
            Console.WriteLine("Aliyun_Test_ydb_membership:" + PHSuit.Security.Encrypt("data source=dev.ydban.cn;uid=jsyk2016;pwd=IQDv$qefeqFWuq_L;port=4407;database=ydb_membership", false));
            Console.WriteLine("Aliyun_Test_ydb_finance:" + PHSuit.Security.Encrypt("data source=dev.ydban.cn;uid=jsyk2016;pwd=IQDv$qefeqFWuq_L;port=4407;database=ydb_finance", false));
            Console.WriteLine("Aliyun_Test_ydb_common:" + PHSuit.Security.Encrypt("data source=dev.ydban.cn;uid=jsyk2016;pwd=IQDv$qefeqFWuq_L;port=4407;database=ydb_common", false));
        }
        [Test]
        public void TestIOSPush()
        {
            /*毛哥的 
            8b123c1b50b85f67cb2bec37ae2b8c98fda48c8e43e4ce54a4c9291f17cb109a
            */
            //Push.pushNotifications("8de76c196a605120db39ab58373edf159c1301b43659bd129fcf72b696e2a26c", "test_push", @"files\aps_development_Mark.p12", 1);
        }
        [Test]
        public void TestCreateHttp()
        {
            string response = HttpHelper.CreateHttpRequest("http://localhost/", "get", null);
            Assert.IsTrue(response.Length > 0);
            Console.WriteLine(response);
            var postData = new NameValueCollection();
            postData.Add("name", "aa");
            postData.Add("pWord", "pword");

            response = HttpHelper.CreateHttpRequest("http://localhost",
                "Post",
              postData);

            Console.WriteLine(response);




        }
        [Test]
        public void TestLoggerWithNewline()
        {



        }
        [Test]
        public void TestXml2Json()
        {
            string xml = @"<xml><return_code><![CDATA[SUCCESS]]></return_code>
                    <return_msg><![CDATA[OK]]></return_msg>
            <appid><![CDATA[wxd928d1f351b77449]]></appid> 
   <mch_id><![CDATA[1304996701]]></mch_id>
<nonce_str><![CDATA[RnTyNTtoDpMC335q]]></nonce_str>
<sign><![CDATA[8440115DC99103B7B242042239395967]]></sign>
<result_code><![CDATA[SUCCESS]]></result_code>
<prepay_id><![CDATA[wx201602031137215b5350a5300317902456]]></prepay_id>
<trade_type><![CDATA[APP]]></trade_type>
</xml> ";

            string json = JsonHelper.Xml2Json(xml, true);
            Console.Write(json);
        }

        [Test]
        public void GetTimeString()
        {
            Assert.AreEqual("00:00",StringHelper.ConvertPeriodToTimeString(0) );
            Assert.AreEqual("00:01", StringHelper.ConvertPeriodToTimeString(1));
            Assert.AreEqual("00:11", StringHelper.ConvertPeriodToTimeString(11));
            Assert.AreEqual("01:00", StringHelper.ConvertPeriodToTimeString(60));
            Assert.AreEqual("12:00", StringHelper.ConvertPeriodToTimeString(720));
            Assert.AreEqual("12:02", StringHelper.ConvertPeriodToTimeString(722));
            Assert.AreEqual("12:59", StringHelper.ConvertPeriodToTimeString(779));
        }
        [Test]
        public void RegexSplitTest()
        {
            string patter = "-{3,}";
            string input = @"--abcde---
                         sdafa----awer--";
           string[] result= StringHelper.RegexSpliter(patter, input);
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("--abcde", result[0]);
            Assert.AreEqual(@"
                         sdafa", result[1]);
            string input2 = "abc";
            string[] result2 = StringHelper.RegexSpliter(patter, input2);
            Assert.AreEqual(1, result2.Length);
            Assert.AreEqual("abc", result2[0]);
        }
    }
 
    
}
