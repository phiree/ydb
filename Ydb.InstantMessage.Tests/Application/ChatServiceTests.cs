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

            var list = chatService.GetListByCustomerId("0e3e9327-7b82-407c-a92f-a64300fd03db");

            Assert.AreNotEqual(0, list.Count);
        }
    }
}