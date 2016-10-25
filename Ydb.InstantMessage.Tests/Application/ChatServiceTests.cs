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
        IChatService chatService;

        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();

            chatService = Bootstrap.Container.Resolve<IChatService>();
        }

        [Test()]
        public void GetChatByOrderTest()
        {
            var list = chatService.GetChatByOrder("770f4636-fa29-4e82-b17b-a6a700abd164");

            Console.WriteLine("list.Count:" + list.Count);
        }

        [Test()]
        public void GetReceptionChatListByCustomerIdTest()
        {
            var list = chatService.GetReceptionChatListByCustomerId(Guid.Parse("0e3e9327-7b82-407c-a92f-a64300fd03db"), 10);

            Console.WriteLine("list.Count:" + list.Count);
        }

        [Test()]
        public void GetReceptionChatListByTargetIdTest()
        {
            var list = chatService.GetReceptionChatListByTargetId(
                Guid.Parse("0e3e9327-7b82-407c-a92f-a64300fd03db"),
                5,
                Guid.Parse("620fbd69-c234-4d54-ae65-1f32e464eb5a"),
                "Y"
                );

            Console.WriteLine("list.Count: " + list.Count);
            Console.WriteLine(list[0].MessageBody);
            Console.WriteLine(list[1].MessageBody);
            Console.WriteLine(list[2].MessageBody);
            Console.WriteLine(list[3].MessageBody);
            Console.WriteLine(list[4].MessageBody);
        }
    }
}