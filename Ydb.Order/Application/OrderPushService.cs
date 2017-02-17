using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Order.DomainModel;
using Ydb.Common;
using Ydb.Order.DomainModel.Repository;
using Ydb.Order.Infrastructure;

namespace Ydb.Order.Application
{
    /// <summary>
    /// 推送给用户的服务.
    /// </summary>
   public  class OrderPushService : IOrderPushService
    {
        IRepositoryServiceOrderPushedService repoPushedService;
        IServiceOrderService orderService;


        public OrderPushService(IRepositoryServiceOrderPushedService repoPushedService,IServiceOrderService orderService)
        {
            this.repoPushedService = repoPushedService;
            this.orderService = orderService;
        }
        
        /// <summary>
        /// 为某个订单推送服务.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="services"></param>
        /// <param name="targetAddress"></param>
        /// <param name="targetTime"></param>
       [UnitOfWork]
        public void Push(Guid orderId, IList<ServiceOrderPushedService> services, string targetAddress, DateTime targetTime)
        {
            ServiceOrder order = orderService.GetOne(orderId);
            order.OrderStatus = enum_OrderStatus.DraftPushed;
           
            foreach (ServiceOrderPushedService service in services)
            {
                service.ServiceOrder = order;
                repoPushedService.Add(service);
            }
        }
        public IList<ServiceOrderPushedService> GetPushedServicesForOrder(ServiceOrder order)
        {
            return repoPushedService.FindByOrder(order);
        }
        [Obsolete("已经移到 ServiceOrderService.ConfirmOrder",true)]
        /// <summary>
        /// 用户选择某项服务之后创建订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="selectedService"></param>
        public void SelectServiceAndCreate(ServiceOrder order,ServiceSnapShot serviceSnapshot,WorkTimeSnapshot worktimeSnapshot,  string selectedServiceId)
        {

            throw new NotImplementedException("已经移到 ServiceOrderService.ConfirmOrder");
           
        }
    }
}
