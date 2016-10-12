using NUnit.Framework;
using Ydb.InstantMessage.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.Application.Tests
{
    [TestFixture()]
    public class ReceptionServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Boot();
        }

        [Test()]
        public void AssignCustomerLoginTest()
        {
            IReceptionService receptionService = Bootstrapper.Container.Resolve<IReceptionService>();

            receptionService.AssignCustomerLogin("10d304fa-cfe9-4417-beb7-9a8062bd2ba2");
        }

        [Test()]
        public void AssignCSLoginTest()
        {
            IReceptionService receptionService = Bootstrapper.Container.Resolve<IReceptionService>();

            receptionService.AssignCSLogin(Guid.NewGuid().ToString());
        }

        [Test()]
        public void AssignCSLogoffTest()
        {
            IReceptionService receptionService = Bootstrapper.Container.Resolve<IReceptionService>();

            receptionService.AssignCSLogoff("369dbe11-75a8-4b78-90f0-f85403b2c81f");
        }
    }
}