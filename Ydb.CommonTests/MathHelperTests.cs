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
    }
}