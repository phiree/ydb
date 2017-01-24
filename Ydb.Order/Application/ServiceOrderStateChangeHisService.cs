using System;
using System.Collections.Generic;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
using Ydb.Common;

namespace Ydb.Order.Application
{
    /// <summary>
    /// 订单状态历史记录
    /// </summary>
    public class ServiceOrderStateChangeHisService : IServiceOrderStateChangeHisService
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLLServiceOrder");
        
        //20160622_longphui_modify
        //DALServiceOrderStateChangeHis dalServiceOrderStateChangeHis = null;
        IRepositoryServiceOrderStateChangeHis repoStateChangeHis;

        public ServiceOrderStateChangeHisService(IRepositoryServiceOrderStateChangeHis repoStateChangeHis)
        {
            this.repoStateChangeHis = repoStateChangeHis;
        }
        

        public void Save(ServiceOrder order, enum_OrderStatus oldStatus)
        {
            int num = 1;
            ServiceOrderStateChangeHis oldOrderHis = GetMaxNumberOrderHis(order);
            if (oldOrderHis != null)
            {
                num = oldOrderHis.Number + 1;
            }
            ServiceOrderStateChangeHis orderHis = new ServiceOrderStateChangeHis(order, oldStatus, num);
            repoStateChangeHis.Add(orderHis);
        }

        public ServiceOrderStateChangeHis GetOrderHis(ServiceOrder order)
        {
            return repoStateChangeHis.GetOrderHis(order);
        }

        public ServiceOrderStateChangeHis GetMaxNumberOrderHis(ServiceOrder order)
        {
            return repoStateChangeHis.GetMaxNumberOrderHis(order);
        }

        public IList<ServiceOrderStateChangeHis> GetOrderHisList(ServiceOrder order)
        {
            return repoStateChangeHis.GetOrderHisList(order);
        }
        public DateTime GetChangeTime(ServiceOrder order, enum_OrderStatus status)
        {
            return repoStateChangeHis.GetChangeTime(order, status);
        }

        public enum_OrderStatus GetOrderStatusPrevious(ServiceOrder order, enum_OrderStatus status)
        {
            return repoStateChangeHis.GetOrderStatusPrevious(order, status);
        }
    }
}
