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
            Ydb.Infrastructure.EncryptService en = new Ydb.Infrastructure.EncryptService();
            OpenfireDbService dbservice = new OpenfireDbService(en);
            dbservice.AddUsersToGroup("u1,u2,u4", "gg1");
        }
    }
}