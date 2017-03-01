using NUnit.Framework;
using Ydb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.CommonTests
{
    [TestFixture()]
    public class MathHelperTests
    {
        [Test()]
        public void MathHelper_GetCalculatedRatio_Test()
        {
            string strRatio = "";
            strRatio = MathHelper.GetCalculatedRatio(123, 0);
            Assert.AreEqual("0", strRatio);
            strRatio = MathHelper.GetCalculatedRatio(1, 2);
            Assert.AreEqual("50.00%", strRatio);
            strRatio = MathHelper.GetCalculatedRatio(1, 3);
            Assert.AreEqual("33.33%", strRatio);
            strRatio = MathHelper.GetCalculatedRatio(2, 3);
            Assert.AreEqual("66.67%", strRatio);
        }

        [Test()]
        public void MathHelper_DateDiffTicks_Test()
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = dt1.AddSeconds(1);
            long l1 = MathHelper.DateDiffTicks(dt1, dt2);
            long l2 = MathHelper.DateDiffTicks(dt2, dt1);
            Assert.AreEqual(10000000, l1);
            Assert.AreEqual(10000000, l2);
        }

        [Test()]
        public void MathHelper_ChangeDateDiff_Test()
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = dt1.AddDays(40).AddHours(2).AddMinutes(3).AddSeconds(4);
            long l1 = MathHelper.DateDiffTicks(dt1, dt2);
            string str1 = MathHelper.ChangeDateDiff(l1);
            Assert.AreEqual("40天2小时3分钟4秒", str1);

            DateTime dt3 = dt1.AddHours(2).AddMinutes(3).AddSeconds(4);
            long l2 = MathHelper.DateDiffTicks(dt1, dt3);
            string str2 = MathHelper.ChangeDateDiff(l2);
            Assert.AreEqual("2小时3分钟4秒", str2);

            DateTime dt4 = dt1.AddDays(40).AddMinutes(3).AddSeconds(4);
            long l3 = MathHelper.DateDiffTicks(dt1, dt4);
            string str3 = MathHelper.ChangeDateDiff(l3);
            Assert.AreEqual("40天0小时3分钟4秒", str3);

            DateTime dt5 = dt1.AddMinutes(3).AddSeconds(4);
            long l4 = MathHelper.DateDiffTicks(dt1, dt5);
            string str4 = MathHelper.ChangeDateDiff(l4);
            Assert.AreEqual("3分钟4秒", str4);

            DateTime dt6 = dt1.AddDays(40).AddHours(2).AddSeconds(4);
            long l5 = MathHelper.DateDiffTicks(dt1, dt6);
            string str5 = MathHelper.ChangeDateDiff(l5);
            Assert.AreEqual("40天2小时0分钟4秒", str5);


            DateTime dt7 = dt1.AddSeconds(4);
            long l6 = MathHelper.DateDiffTicks(dt1, dt7);
            string str6 = MathHelper.ChangeDateDiff(l6);
            Assert.AreEqual("4秒", str6);

            DateTime dt8 = dt1.AddDays(40).AddHours(2).AddMinutes(3);
            long l7 = MathHelper.DateDiffTicks(dt1, dt8);
            string str7 = MathHelper.ChangeDateDiff(l7);
            Assert.AreEqual("40天2小时3分钟", str7);

            DateTime dt9 = dt1;
            long l8 = MathHelper.DateDiffTicks(dt1, dt9);
            string str8 = MathHelper.ChangeDateDiff(l8);
            Assert.AreEqual("0秒", str8);

        }
    }
}