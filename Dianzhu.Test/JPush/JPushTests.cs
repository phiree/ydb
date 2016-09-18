using NUnit.Framework;
using Dianzhu.Push.JPush;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.Push.JPush.Tests
{
    [TestFixture()]
    public class JPushTests
    {
        [Test()]
        public void PushTest()
        {
            Dianzhu.Push.JPush.JPush jpush = new JPush(Guid.NewGuid().ToString());
            jpush.Push("推送测试"+DateTime.Now, "050d30e925f", 1);
        }
    }
}