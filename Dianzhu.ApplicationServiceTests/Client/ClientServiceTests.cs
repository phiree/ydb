using NUnit.Framework;
using Dianzhu.ApplicationService.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.ApplicationService;
using Ydb.Membership.Application;

namespace Dianzhu.ApplicationServiceTests
{
    [TestFixture()]
    public class ClientServiceTests
    {
        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();
        }
        [Test()]
        public void CreateTokenTest()
        {
            IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
            
        }
    }
}