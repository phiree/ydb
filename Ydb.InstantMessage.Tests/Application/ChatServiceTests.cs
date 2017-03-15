using NUnit.Framework;
using Ydb.InstantMessage.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var list = chatService.GetReceptionChatListByCustomerId("0e3e9327-7b82-407c-a92f-a64300fd03db", 10);

            Console.WriteLine("list.Count:" + list.Count);
        }

        [Test()]
        public void GetReceptionChatListByTargetIdTest()
        {
            var list = chatService.GetReceptionChatListByTargetId(
                "0e3e9327-7b82-407c-a92f-a64300fd03db",
                5,
                "3174c0fd-96f3-4add-a224-6c088873977a",
                "Y"
                );

            Console.WriteLine("list.Count: " + list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].MessageBody);
            }
        }

        [Test()]
        public void GetChatByIdTest()
        {
            var chat = chatService.GetChatById("54f24bed-0aa5-4406-aa57-838bff39f3ce");

            Console.WriteLine(chat.Id);
        }

        [Test()]
        public void GetInitChatListTest()
        {
            var chat = chatService.GetInitChatList("54f24bed-0aa5-4406-aa57-838bff39f3ce",10);

            Console.WriteLine(chat.Count);
        }
    }
}