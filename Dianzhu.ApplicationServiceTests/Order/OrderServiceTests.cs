using NUnit.Framework;
using Dianzhu.ApplicationService.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OR = Ydb.Order.Application;
using Ydb.Order.DomainModel;
using Ydb.Membership.Application;
using Ydb.Membership.DomainModel;
using FizzWare.NBuilder;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
namespace Dianzhu.ApplicationService.Order.Tests
{
    [TestFixture()]
    public class OrderServiceTests
    {
       
        [Test()]
        public void PatchCurrentStatusTest_WithDeposit()
        {
            Assert.AreEqual(1, 1);
            IOrderService orderService = Bootstrap.Container.Resolve<IOrderService>();
            OR.IServiceOrderService serviceOrderService = Bootstrap.Container.Resolve<OR.IServiceOrderService>();
            IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
            OR.IOrderPushService pushService = Bootstrap.Container.Resolve<OR.IOrderPushService>();
            IDZServiceService dzserviceService = Bootstrap.Container.Resolve<IDZServiceService>();
            IBusinessService businessService= Bootstrap.Container.Resolve<IBusinessService>();
            IServiceTypeService serviceTypeService = Bootstrap.Container.Resolve<IServiceTypeService>();
            var businessResult=    businessService.Add("name", "phone", "email", Guid.NewGuid(), "latitude", "longtitude", "address", "contact", 1, 1);
            ServiceType serviceType = Builder<ServiceType>.CreateNew().Build();
            serviceTypeService.Save(serviceType);

            DZService service = Builder<DZService>.CreateNew().
              With(x => x.Business = businessResult.ResultObject)
              .With(x=>x.ServiceType=serviceType)
              .With(x=>x.DepositAmount=0.1m)
                .Build();
            
         
            Ydb.Membership.Application.Dto.RegisterResult customerRegistered= memberService.RegisterMember("customer1", "123456", "123456", "customer", "");
            Ydb.Membership.Application.Dto.RegisterResult csRegistered = memberService.RegisterMember("cs1", "123456", "123456", "customerservice", "");

            dzserviceService.Save(service);
            ServiceOrderPushedService pushedService = Builder<ServiceOrderPushedService>.CreateNew()
             .With(x => x.OriginalServiceId = service.Id.ToString())
            .With(x=>x.ServiceSnapShot=new ServiceSnapShot { Description=service.Description, BusinessId=service.Business.Id.ToString(), BusinessName=service.Business.Name
            , BusinessOwnerId=service.Business.OwnerId.ToString(),
                BusinessPhone =service.Business.Phone, OverTimeForCancel=service.OverTimeForCancel, ServiceTypeName= service.ServiceType.Name
                ,DepositAmount=service.DepositAmount,
            Name=service.Name})
             .With(x => x.Id = Guid.NewGuid())
             .Build();

            ServiceOrder order = serviceOrderService.CreateDraftOrder( csRegistered.ResultObject.Id.ToString(),customerRegistered.ResultObject.Id.ToString());

            pushService.Push(order.Id, new List<ServiceOrderPushedService> { pushedService },"targetAddress",DateTime.Now);
           // order.OrderStatus = Ydb.Common.enum_OrderStatus.Payed;
            serviceOrderService.OrderFlow_ConfirmOrder(order.Id, service.Id.ToString());

            
            serviceOrderService.OrderFlow_ConfirmDeposit(order.Id);
                      

            orderObj orderObj = orderService.PatchCurrentStatus(order.Id.ToString(),Ydb.Common.enum_OrderStatus.Negotiate.ToString(), new Customer {  UserID=customerRegistered.ResultObject.Id.ToString() });
            Assert.AreEqual("Negotiate", orderObj.currentStatusObj.status);

        }
    }
}