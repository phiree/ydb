using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FizzWare.NBuilder;
using Rhino.Mocks;
using Dianzhu.BLL;
using Dianzhu.DAL;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Test.BLLTest
{
    [TestFixture]
    public  class TestPayment
    {
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ApplyPay()
        {
            var dal = MockRepository.GenerateStub<DALPayment>(string.Empty);
            ServiceOrder order = Builder<ServiceOrder>.CreateNew()
                .With(x => x.DepositAmount = 1)
                .With(x => x.OrderAmount = 10)
                .With(x => x.NegotiateAmount = 12)
                .With(x => x.OrderStatus = Model.Enums.enum_OrderStatus.Created)
                .Build();
            Guid payId = Guid.NewGuid();
            dal.Stub(x => x.GetPaymentsForOrder(order)).Return(new List<Payment>());
            BLLPayment bll = new BLLPayment(dal);
             Payment payment= bll.ApplyPay(order, Model.Enums.enum_PayTarget.Deposit);
            string payLink = bll.BuildPayLink(payment.Id);
            Console.WriteLine(payLink);
          Assert.True(payLink.StartsWith( Config.Config.GetAppSetting("PayServerUrl")));
            
        }
         
        
    }
}
