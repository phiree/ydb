using NUnit.Framework;
using Ydb.InstantMessage.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.DependencyInstaller;
using Ydb.InstantMessage.Tests;

namespace Ydb.InstantMessage.Application.Tests
{
    [TestFixture()]
    public class ChatServiceTests
    {
        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();
        }
        [Test()]
        public void GetListByCustomerIdTest()
        {
            IChatService chatService = Bootstrap.Container.Resolve<IChatService>();

            var list = chatService.GetListByCustomerId("fa7ef456-0978-4ccd-b664-a594014cbfe7");

            Assert.AreNotEqual(0, list.Count);
        }
    }
}