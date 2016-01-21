using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections.Specialized;

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
             Assert.AreEqual(nextday-new DateTime(2009, 10, 3),new TimeSpan(0));
         }
         [Test]
         public void test_GetRandon()
         {
            string r= PHCore.GetRandom(10);
            Console.WriteLine(r);
            Assert.LessOrEqual(Convert.ToInt64(r), long.MaxValue);
         }
        [Test]
        public void parseUrlParam()
        {
           Assert.AreEqual("alasdf_$_dlaer", PHSuit.StringHelper.ParseUrlParameter("http://asdfesrawer/getfile.ashx?filename=alasdf_$_dlaer","filename"));
        }
        [Test]
        public void crypttest()
        {
          Console.WriteLine(  PHSuit.Security.Encrypt("123456", false));
        }
        [Test]
        public void TestIOSPush()
        {
            /*毛哥的 
            8b123c1b50b85f67cb2bec37ae2b8c98fda48c8e43e4ce54a4c9291f17cb109a
            */
            Push.pushNotifications("8de76c196a605120db39ab58373edf159c1301b43659bd129fcf72b696e2a26c", "test_push", @"files\aps_development_Mark.p12", 1);
        }
        [Test]
        public void TestCreateHttp()
        {
          string response=  HttpHelper.CreateHttpRequest("http://localhost/","get",null);
            Assert.IsTrue(response.Length > 0);
            Console.WriteLine(response);
            var postData = new NameValueCollection();
            postData.Add("name","aa");
            postData.Add("pWord", "pword");

            response = HttpHelper.CreateHttpRequest("http://localhost",
                "Post",
              postData);

            Console.WriteLine(response);




        }
        [Test]
        public void TestLogger()
        {
            
     
            
        }
    }
}
