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
            var dalTag = MockRepository.GenerateStub<DALDZTag>(string.Empty);
            dal.Stub(x => x.FindById(Guid.Empty)).Return(
                new DZService { Name="service1"}
                );
            
            BLLDZService service = new BLLDZService(dal,dalTag);
       
           DZService result= service.GetOne(Guid.Empty);
           Assert.AreEqual("service1", result.Name);

       }
        [Test]
        public void GetAll()
        {
            DALDZService dal = new DALDZService();
            int totalRecord;
           //IList<DZService> serviceList= dal.SearchService(string.Empty, 1, 10, out totalRecord);
           // foreach (DZService service in serviceList)
           // {
           //     Console.WriteLine(service.Business.Name);
           // }
        }
    }
}
