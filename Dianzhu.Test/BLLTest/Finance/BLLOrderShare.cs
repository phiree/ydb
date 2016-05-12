using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Dianzhu.BLL.Finance;
using Dianzhu.Model.Finance;
using Rhino.Mocks;
using FizzWare.NBuilder;
using Ploeh.AutoFixture;
using Dianzhu.Model;
namespace Dianzhu.Test.BLLTest.Finance
{
    [TestFixture]
  public  class BLLOrderShare
    {
        [Test]
        public void ShareTest()
        {
            var IBLLServiceTypePoint = MockRepository.GenerateStub<IBLLServiceTypePoint>();
          
            var IBalanceFlowService = MockRepository.GenerateStub<IBalanceFlowService>();
            var IBLLSharePoint = MockRepository.GenerateStub<IBLLSharePoint>();
            var IAgentService = MockRepository.GenerateStub<BLL.Agent.IAgentService>();

            Fixture fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());



            var order =
                fix.Create<Dianzhu.Model.ServiceOrder>()
                //Builder<ServiceOrder>.CreateNew().Build()
                ;
            var originalService = fix.Build<DZService>().Without(x=>x.OpenTimes). Create();

            //detail
            var orderDetail = //new  Dianzhu.Model.ServiceOrderDetail();
                 fix.Build<ServiceOrderDetail>()
                .Without(x=>x.OriginalService)
                .Create<ServiceOrderDetail>();
            orderDetail.OriginalService = originalService;

            var serviceType = fix.Create<ServiceType>();
            orderDetail.OriginalService.ServiceType = serviceType;
            var area = fix.Create<Area>();
            orderDetail.OriginalService.Business.AreaBelongTo = area;
            order.Details.Clear();
            order.Details.Add(orderDetail);
            //amount
            order.NegotiateAmount = 100;
            //customerservice
            var customerService = fix.Create<DZMembership>();
            order.CustomerService = customerService;
            //agent
            var agent = fix.Create<DZMembership>();
            //
            IBLLServiceTypePoint.Stub(x => x.GetPoint(serviceType)).Return(0.3m);
            IAgentService.Stub(x => x.GetAreaAgent(area)).Return(agent);
            IBLLSharePoint.Stub(x => x.GetSharePoint(agent)).Return(0.4m);
            IBLLSharePoint.Stub(x => x.GetSharePoint(customerService)).Return(0.3m);
            



            OrderShare ordershare = new OrderShare(IBLLServiceTypePoint,IBLLSharePoint,IAgentService,IBalanceFlowService);
            ordershare.Share(order);

            
            
        }
    }
}
