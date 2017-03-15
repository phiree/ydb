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
            var rs = receptionService.AssignCustomerLogin("10d304fa-cfe9-4417-beb7-9a8062bd2ba2", out errorMessage);
            Console.WriteLine("CustomerService:" + rs.CustomerServiceId);
        }

        [Test()]
        public void AssignCSLoginTest()
        {
            var list = receptionService.AssignCSLogin("10d304fa-cfe9-4417-beb7-9a8062bd2ba2", 3);
            for(int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("Customer " + i + ":" + list[i].CustomerId);
            }
        }

        [Test()]
        public void AssignCSLogoffTest()
        {
            receptionService.AssignCSLogoff("10d304fa-cfe9-4417-beb7-9a8062bd2ba2");
        }

        [Test()]
        public void AssignCSLogTest()
        {
            //todo:数据依赖,需要修改.
            string csId = "369dbe11-75a8-4b78-90f0-f85403b2c81f";

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                receptionService.AssignCSLogin(csId, 3);

                IRepositoryReception repositoryReception = Bootstrap.Container.Resolve<IRepositoryReception>();
                var listOn = repositoryReception.FindByCustomerServiceId(csId);
               // Assert.AreEqual(3, listOn.Count);

                receptionService.AssignCSLogoff(csId);
                var listOff = repositoryReception.FindByCustomerServiceId(csId);
              //  Assert.AreEqual(0, listOff.Count); 
            }
        }
    }
}