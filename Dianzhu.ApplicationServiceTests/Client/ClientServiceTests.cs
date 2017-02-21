using NUnit.Framework;
using Dianzhu.ApplicationService.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.ApplicationService.Tests;
using Ydb.Membership.Application;

namespace Dianzhu.ApplicationService.Client.Tests
{
    [TestFixture()]
    public class ClientServiceTests
    {
        [Test()]
        public void CreateTokenTest()
        {
            IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
            
        }
    }
}