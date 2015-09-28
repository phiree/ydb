using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using NUnit.Framework;
using FizzWare.NBuilder;
namespace Dianzhu.Test.PayTest
{
    [TestFixture]
   public class payTest
    {
        [Test]
        public void CreatePayLinkTest() {
            ServiceOrder order = Builder<ServiceOrder>
                   .CreateNew().Build();
           // string url
        }
    }
}
