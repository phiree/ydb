using NUnit.Framework;
using Ydb.InstantMessage.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.Tests;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Ydb.InstantMessage.Application.Tests
{
    [TestFixture()]
    public class ReceptionServiceTests
    {
        IReceptionService receptionService;

        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();

            receptionService = Bootstrap.Container.Resolve<IReceptionService>();
        }

        [Test()]
        public void AssignCustomerLoginTest()
        {
            string errorMessage = string.Empty;
            receptionService.AssignCustomerLogin("10d304fa-cfe9-4417-beb7-9a8062bd2ba2", out errorMessage);
        }

        [Test()]
        public void AssignCSLoginTest()
        {
            receptionService.AssignCSLogin("10d304fa-cfe9-4417-beb7-9a8062bd2ba2", 3);
        }

        [Test()]
        public void AssignCSLogoffTest()
        {
            receptionService.AssignCSLogoff("369dbe11-75a8-4b78-90f0-f85403b2c81f");
        }

        [Test()]
        public void AssignCSLogTest()
        {
            string csId = "369dbe11-75a8-4b78-90f0-f85403b2c81f";

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                receptionService.AssignCSLogin(csId, 3);

                IRepositoryReception repositoryReception = Bootstrap.Container.Resolve<IRepositoryReception>();
                var listOn = repositoryReception.FindByCustomerServiceId(csId);
                Assert.AreEqual(3, listOn.Count);

                receptionService.AssignCSLogoff(csId);
                var listOff = repositoryReception.FindByCustomerServiceId(csId);
                Assert.AreEqual(0, listOff.Count); 
            }
        }
    }
}