using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FizzWare.NBuilder;
using Dianzhu.BLL;
using Dianzhu.Model;
namespace Dianzhu.Test.BLLTest
{
    [TestFixture]
    public class TestBLLServiceOrder
    {
        [Test]
       
        public void CreateOrderFromDraftWithoutEssentialInfo()
        {
            //var order = new ServiceOrder();
             
        }
        [Test]
        public void CreateOrderFromDraftWithEssentialInfo()
        {
            
        }
        private void Bll_OrderCreated(ServiceOrder order)
        {
            throw new NotImplementedException();
        }
        [Test]
        public void ServiceOrderFlow()
        {
            var order = Builder<ServiceOrder>.CreateNew().Build();
            

        }
    }
    
}
