using NUnit.Framework;
using Ydb.InstantMessage.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.Tests;
using Ydb.InstantMessage.Application;

namespace Ydb.InstantMessage.Infrastructure.Tests
{
    [TestFixture()]
    public class OpenfireXMPPTests
    {
        IInstantMessage im;
        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();

            im = Bootstrap.Container.Resolve<IInstantMessage>();
        }

        [Test()]
        public void SendDidichuxingTest()
        {
            var id = Guid.NewGuid();
            im.SendDidichuxing(
                "1", "1", "haikou", "haiya",
                "2", "2", "sanya", "yalongwan", "13912345678",
                id, "0e3e9327-7b82-407c-a92f-a64300fd03db", "YDBan_User", "770f4636-fa29-4e82-b17b-a6a700abd164");

            Console.WriteLine(id);
        }
    }
}