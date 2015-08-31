using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.DAL;
using NUnit.Framework;
using FizzWare.NBuilder;
namespace Dianzhu.Test.BLLTest
{
    [TestFixture]
   public  class DZServiceTest
    {
       [Test]
        public void GetOne()
        {
            
            var dal = MockRepository.GenerateStub<DALDZService>(string.Empty);
            dal.Stub(x => x.GetOne(Guid.Empty)).Return(
                new DZService { Name="service1"}
                );
            
            BLLDZService service = new BLLDZService(dal);
       
           DZService result= service.GetOne(Guid.Empty);
           Assert.AreEqual("service1", result.Name);

       }
    }
}
