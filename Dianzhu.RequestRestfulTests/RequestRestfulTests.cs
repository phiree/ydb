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
            RequestResponse res = req.RequestRestfulApiForAuthenticated("https://dev.ydban.cn:6041/", "18608956891", "123456");

            Console.WriteLine(res.data);
            Assert.IsTrue(res.code);
        }

        [Test()]
        public void RequestRestful_RequestRestfulApiForUserCity_Test()
        {
            RequestRestful.RequestRestful req = new RequestRestful.RequestRestful();
            string strToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJsb2dpbk5hbWUiOiIxODYwODk1Njg5MSIsInBhc3N3b3JkIjoiZHZkdi1sQVN2a0RZaFQwNzNKMGpoN205S0pKeWE5bXJOS2hkVlIzVEpMNCIsIlVzZXJUeXBlIjoiY3VzdG9tZXIiLCJVc2VySUQiOiIxN2YxM2RlMC0wOWJjLTRhYTQtYWMxYi1hNjkwMDExZjU3NTgifQ.k_HrrVCZ4ioUUV0TYTaWqD75JoYVprP-xOT8UWwPjD8";
            RequestResponse res = req.RequestRestfulApiForUserCity("https://dev.ydban.cn:6041/", "0404c4f9-05bd-4a27-9203-a674010cfc36", strToken);

            Console.WriteLine(res.data);
            Assert.IsTrue(res.code);
        }
    }
}