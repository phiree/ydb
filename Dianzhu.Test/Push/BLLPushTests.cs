using NUnit.Framework;
using Dianzhu.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL;
namespace Dianzhu.BLL.Tests
{
    [TestFixture()]
    public class BLLPushTests
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
        }
        [Test()]
        public void PushTest()
        {
            BLLPush push = Bootstrap.Container.Resolve<BLLPush>();
            push.Push("withcustomer",Guid.NewGuid(), "dasdfds");
        }
    }
}