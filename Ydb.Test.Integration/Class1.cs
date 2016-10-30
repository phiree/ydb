using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ydb.Membership.Application;
using Ydb.InstantMessage.Application;
namespace Ydb.Test.Integration
{
    [TestFixture]
    public class Class1
    {
        [SetUp]
        public void setup()
        {
            Bootstrap.Boot();
        }
        [Test]
        public void testabc()
        {
            IDZMembershipService memberService = Bootstrap.Container.Resolve< IDZMembershipService>();
           IChatService chatService = Bootstrap.Container.Resolve< IChatService>();
            memberService.GetUserByName("abcd");

        }
        [Test]
        public void Testavc()
        {
            Assert.Fail();
        }
    }
}
