using NUnit.Framework;
using Ydb.InstantMessage.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.Application.Reception;

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
        public void AssignToCustomerServiceTest()
        {
            IReceptionService receptionService = Bootstrapper.Container.Resolve<IReceptionService>();


            receptionService.AssignToCustomerService(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
           
        }
    }
}