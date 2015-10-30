using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

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
    }
}
