using System;
using System.Collections.Generic;
using Ydb.Order.DomainModel;

namespace Ydb.Order.Application
{
    public interface IOrderPushService
    {
        IList<ServiceOrderPushedService> GetPushedServicesForOrder(ServiceOrder order);
        void Push(ServiceOrder order, ServiceOrderPushedService service, string targetAddress, DateTime targetTime);
        void Push(ServiceOrder order, IList<ServiceOrderPushedService> services, string targetAddress, DateTime targetTime);
        [Obsolete("已经移到 ServiceOrderService.ConfirmOrder", true)]
        void SelectServiceAndCreate(ServiceOrder order, ServiceSnapShot serviceSnapshot, WorkTimeSnapshot worktimeSnapshot, string selectedServiceId);
    }
}