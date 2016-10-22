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
    public class ReceptionServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
        }

        [Test()]
        public void AssignCustomerLoginTest()
        {
            IReceptionService receptionService = Bootstrap.Container.Resolve<IReceptionService>();
            string errorMessage = string.Empty;
            receptionService.AssignCustomerLogin("10d304fa-cfe9-4417-beb7-9a8062bd2ba2",out errorMessage);
        }

        [Test()]
        public void AssignCSLoginTest()
        {
            IReceptionService receptionService = Bootstrap.Container.Resolve<IReceptionService>();

            receptionService.AssignCSLogin(Guid.NewGuid().ToString(),3);
        }

        [Test()]
        public void AssignCSLogoffTest()
        {
            IReceptionService receptionService = Bootstrap.Container.Resolve<IReceptionService>();

            receptionService.AssignCSLogoff("369dbe11-75a8-4b78-90f0-f85403b2c81f");
        }
    }
}