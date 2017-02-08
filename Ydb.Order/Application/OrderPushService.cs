using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Order.DomainModel;
using Ydb.Common;
using Ydb.Order.DomainModel.Repository;
namespace Ydb.Order.Application
{
    /// <summary>
    /// 推送给用户的服务.
    /// </summary>
   public  class OrderPushService : IOrderPushService
    {
        IRepositoryServiceOrderPushedService repoPushedService;
       
        public OrderPushService(IRepositoryServiceOrderPushedService repoPushedService)
        {
            this.repoPushedService = repoPushedService;
        }
        public void Push(ServiceOrder order, ServiceOrderPushedService service, string targetAddress, DateTime targetTime)
        {
            IList<ServiceOrderPushedService> serviceOrderPushedServices = new List<ServiceOrderPushedService>();
            serviceOrderPushedServices.Add(service);

            Push(order, serviceOrderPushedServices, targetAddress, targetTime);
        }
        /// <summary>
        /// 为某个订单推送服务.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="services"></param>
        /// <param name="targetAddress"></param>
        /// <param name="targetTime"></param>
        public void Push(ServiceOrder order, IList<ServiceOrderPushedService> services, string targetAddress, DateTime targetTime)
        {
            //order.OrderStatus = enum_OrderStatus.DraftPushed;
           

            foreach (ServiceOrderPushedService service in services)
            {
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
