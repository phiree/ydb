using NUnit.Framework;
using OpenfireExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenfireExtension.Tests
{
    [TestFixture()]
    public class OpenfireDbServiceTests
    {
        [Test()]
        public void AddUsersToGroupTest()
        {
            OpenfireDbService dbservice = new OpenfireDbService();
            dbservice.AddUsersToGroup("u1,u2,u3,u4", "gg1");
        }
    }
}