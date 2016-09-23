using NUnit.Framework;
using Dianzhu.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL;
namespace Dianzhu.BLL.Tests
{
    [TestFixture()]
    public class IMSessionsOpenfireTests
    {
        [Test()]
        public void IsUserOnlineTest()
        {
            IMSessionsOpenfire imo = new IMSessionsOpenfire("http://localhost:9090/plugins/restapi/v1/", "an4P0ja6v3rykV4H");
           var result= imo.IsUserOnline("05ad9bf7-6a9b-11e6-b78a-001a7dda7106");
        }
    }
}