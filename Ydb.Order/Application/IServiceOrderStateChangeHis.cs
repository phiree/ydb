using System;
using System.Collections.Generic;
using Ydb.Common;
using Ydb.Order.DomainModel;

namespace Ydb.Order.Application
{
    public interface IServiceOrderStateChangeHisService
    {
        DateTime GetChangeTime(ServiceOrder order, enum_OrderStatus status);
        ServiceOrderStateChangeHis GetMaxNumberOrderHis(ServiceOrder order);
        ServiceOrderStateChangeHis GetOrderHis(ServiceOrder order);
        IList<ServiceOrderStateChangeHis> GetOrderHisList(ServiceOrder order);
        enum_OrderStatus GetOrderStatusPrevious(ServiceOrder order, enum_OrderStatus status);
        void Save(ServiceOrder order, enum_OrderStatus oldStatus);
    }
}