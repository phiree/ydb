using NUnit.Framework;
using DianzhuService.Diandian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DianzhuService.Diandian.Tests
{
    [TestFixture()]
    public class ServiceDiandianTests
    {
        [Test()]
        public void ServiceDiandian_CheckMessage_Test()
        {
            ServiceDiandian serviceDiandian = new ServiceDiandian();
            Console.WriteLine(serviceDiandian.CheckMessage(""));
            //Assert.Fail();
        }
    }
}