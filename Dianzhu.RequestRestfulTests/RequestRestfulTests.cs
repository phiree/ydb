using NUnit.Framework;
using Dianzhu.RequestRestful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.RequestRestfulTests
{
    [TestFixture()]
    public class RequestRestfulTests
    {
        [Test()]
        public void RequestRestful_RequestRestfulApiForAuthenticated_Test()
        {
            RequestRestful.RequestRestful req = new RequestRestful.RequestRestful();
            RequestResponse res = req.RequestRestfulApiForAuthenticated("http://localhost:8041/", "diandian@ydban.cn", "123456");

            Console.WriteLine(res.data);
            Assert.IsTrue(res.code);
        }

        [Test()]
        public void RequestRestful_RequestRestfulApiForUserCity_Test()
        {
            RequestRestful.RequestRestful req = new RequestRestful.RequestRestful();
            string strToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJsb2dpbk5hbWUiOiJkaWFuZGlhbkB5ZGJhbi5jbiIsInBhc3N3b3JkIjoiQ2xTYXBpRTc3RlNabnNZU0Y5UEt1QUlCeWU4VVdYdHVVQm9qczVJNGdfWSIsIlVzZXJUeXBlIjoiZGlhbmRpYW4iLCJVc2VySUQiOiJjNjRkOWRkYS00ZjZlLTQzN2ItODlkMi1hNTkxMDEyZDhjNjUifQ.R5Vp_xJIkGAmc86hPD7b-A0qnOu1LE0JKRjxzAOhICM";
            RequestResponse res = req.RequestRestfulApiForUserCity("http://localhost:8041/", "0404c4f9-05bd-4a27-9203-a674010cfc36", strToken);

            Console.WriteLine(res.data);
            Assert.IsTrue(res.code);
        }
    }
}