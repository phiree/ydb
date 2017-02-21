using NUnit.Framework;
using Ydb.ApiClient.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.ApiClient.Application.Tests
{
    [TestFixture()]
    public class ClientServiceTests
    {
        Application.IClientService clientService;
        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();
            clientService = Bootstrap.Container.Resolve<IClientService>();
        }
        [Test()]
        public void RegisterClientTest()
        {
            clientService.RegisterClient(FizzWare.NBuilder.Builder<DomainModel.Client>.CreateNew().Build());
        }
    }
}