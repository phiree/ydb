using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Dianzhu.ApplicationService;

namespace Dianzhu.Test.DianzhuRestfulApi
{
    [TestFixture]
    public class ClientApiTest: ApiTestBase
    {
        public override string GetBaseAddress()
        {
            return "http://localhost:52553/";
        }

        [Test]
        public void Should_get_authorization_successfully()
        {
            var newOrder = new Customer() { loginName = "issumao@126.com", password = "123456" };
            var result = InvokePostRequest<UserTokentDTO, Customer>("api/vi/authorization", newOrder);
        }
    }
}
