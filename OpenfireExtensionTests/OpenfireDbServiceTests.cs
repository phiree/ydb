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
            IList<string> useridList = new List<string> { "002c0c77-c2a0-4dba-930c-a6b000f80ceb","00c02067-4900-41b6-b3d4-a68100c47cb9", "015351d4-ba0a-41b2-bc5e-a6b400c11c26",
                "0404c4f9-05bd-4a27-9203-a674010cfc36","044bf6a7-6d7a-42bc-a76a-a69000363cbd","076cbb88-594f-462c-ba9b-a6bb010a94d0" };
            string userids = string.Join(",", useridList);
            string groupname = "13bedbf7-c561-4bc5-ae64-a73800b40ba9";
            dbservice.AddUsersToGroup(userids, groupname);

            IList<string> useridsFromDb = dbservice.GetUsersInGroup(groupname);

            Console.WriteLine(useridsFromDb.Count);
            Assert.AreEqual(useridsFromDb.Count, userids.Split(',').Length);
        }
    }
}