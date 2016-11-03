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
            var memberService = MockRepository.GenerateStub<Ydb.Membership.Application.IDZMembershipService>();

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
            var customerService = fix.Create<Ydb.Membership.Application.Dto.MemberDto>();
            order.CustomerServiceId = customerService.Id.ToString();
            //agent
            var agent = fix.Create<Ydb.Membership.Application.Dto.MemberDto>();
            //该类别分成0.3
            IBLLServiceTypePoint.Stub(x => x.GetPoint(serviceType)).Return(0.3m);
            //该区域的代理是agent
            IAgentService.Stub(x => x.GetAreaAgent(area)).Return(agent);
            //该代理的分成比是0.4
            IBLLSharePoint.Stub(x => x.GetSharePoint(agent)).Return(0.4m);
            //该客服的分成比是0.3
            IBLLSharePoint.Stub(x => x.GetSharePoint(customerService)).Return(0.3m);
            



            OrderShare ordershare = new OrderShare(IBLLServiceTypePoint,IBLLSharePoint,IAgentService,IBalanceFlowService, memberService);
          IList<BalanceFlow> flows= ordershare.Share(order);
            

            
            
        }
    }
}
