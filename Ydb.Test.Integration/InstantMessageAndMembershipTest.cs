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
    public class InstantMessageAndMembershipTest
    {

        [SetUp]
        public void setup()
        {
            Bootstrap.Boot();
        }
        [Test]
        public void GetUserNameAndGetChatByOrderTest()
        {
            IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
            IChatService chatService= Bootstrap.Container.Resolve<IChatService>();

            string username = "18889387677";
            memberService.RegisterMember(username, "123456","123456","business","localhost");
            Assert.AreEqual(username, memberService.GetUserByName(username).UserName);


             chatService.GetChatByOrder("orderid");
            Assert.AreEqual(0, chatService.GetChatByOrder("orderid").Count);

        }
         
    }
}
