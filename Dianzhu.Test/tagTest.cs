using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
 
using Dianzhu.Model;
using NUnit.Framework;
using FizzWare.NBuilder;
using Rhino.Mocks;
namespace Dianzhu.Test
{
    [TestFixture]
  public  class tagTest
    {
        [Test]
        public void test_create()
        {
            var dal = MockRepository.GenerateStub<DAL.DALDZTag>();

            var cc = Builder<BLLDZTag>.CreateNew().WithConstructor(() => new BLLDZTag());

        }
    }
}
