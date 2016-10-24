using NUnit.Framework;
using Ydb.InstantMessage.Infrastructure.Repository.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.Tests;
using Ydb.InstantMessage.DomainModel.Chat;

namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate.Tests
{
    [TestFixture()]
    public class RepositoryChatTests
    {
        IRepositoryChat repositoryChat;

        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();

            repositoryChat = Bootstrap.Container.Resolve<IRepositoryChat>();
        }
        [Test()]
        public void GetChatByOrderTest()
        {
            var list = repositoryChat.GetChatByOrder("770f4636-fa29-4e82-b17b-a6a700abd164");

            Console.WriteLine(list.Count);
        }

        [Test()]
        public void GetReceptionChatListTest()
        {
            int rowCount;
            var list = repositoryChat.GetReceptionChatList(
                Guid.Parse("0e3e9327-7b82-407c-a92f-a64300fd03db"),
                Guid.Empty,
                Guid.Empty,
                DateTime.Now.AddMonths(-1),
                DateTime.Now,
                0, 999, DomainModel.Chat.Enums.ChatTarget.cer, out rowCount);

            Console.WriteLine(list.Count);
        }

        [Test()]
        public void GetReceptionChatListByTargetIdTest_Y()
        {
            var list = repositoryChat.GetReceptionChatListByTargetId(
                Guid.Parse("0e3e9327-7b82-407c-a92f-a64300fd03db"),
                Guid.Empty,
                Guid.Empty,
                DateTime.Now.AddMonths(-1),
                DateTime.Now,
                5,
                1477302450333.9114,
                "Y",
                DomainModel.Chat.Enums.ChatTarget.cer);

            Console.WriteLine("list.Count: " + list.Count);
            Console.WriteLine(list[0].MessageBody);
            Console.WriteLine(list[1].MessageBody);
            Console.WriteLine(list[2].MessageBody);
            Console.WriteLine(list[3].MessageBody);
            Console.WriteLine(list[4].MessageBody);
        }

        [Test()]
        public void GetReceptionChatListByTargetIdTest_N()
        {
            var list = repositoryChat.GetReceptionChatListByTargetId(
                Guid.Parse("0e3e9327-7b82-407c-a92f-a64300fd03db"),
                Guid.Empty,
                Guid.Empty,
                DateTime.Now.AddMonths(-1),
                DateTime.Now,
                5,
                1477302450333.9114,
                "N",
                DomainModel.Chat.Enums.ChatTarget.cer);

            Console.WriteLine("list.Count: " + list.Count);
            Console.WriteLine(list[0].MessageBody);
            Console.WriteLine(list[1].MessageBody);
            Console.WriteLine(list[2].MessageBody);
            Console.WriteLine(list[3].MessageBody);
            Console.WriteLine(list[4].MessageBody);
        }
    }
}