using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;

namespace Dianzhu.IDAL
{
    public interface IDALServiceOrderStateChangeHis : IRepository<ServiceOrderStateChangeHis, Guid>
    {



        ServiceOrderStateChangeHis GetMaxNumberOrderHis(ServiceOrder order);

        IList<ServiceOrderStateChangeHis> GetOrderHisList(ServiceOrder order);

        DateTime GetChangeTime(ServiceOrder order, enum_OrderStatus status);

        ServiceOrderStateChangeHis GetOrderHis(ServiceOrder order);

        enum_OrderStatus GetOrderStatusPrevious(ServiceOrder order, enum_OrderStatus status);

        /// <summary>
        /// 更新为该状态时的时间
        /// </summary>
        /// <returns></returns>
        //public DateTime GetOrderStatusTime(ServiceOrder order, enum_OrderStatus status)
        //{
        //    return null;
        //}
    }
}
