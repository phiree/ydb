using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;

using Dianzhu.DAL;
using Dianzhu.Model.Enums;
using Dianzhu.Pay;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 订单状态历史记录
    /// </summary>
    public class BLLServiceOrderStateChangeHis
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLLServiceOrder");

        DALServiceOrderStateChangeHis dalServiceOrderStateChangeHis = null;
        public BLLServiceOrderStateChangeHis()
        {
            dalServiceOrderStateChangeHis = DALFactory.DALServiceOrderStateChangeHis;
        }

        public void SaveOrUpdate(ServiceOrder oldOrder, enum_OrderStatus newStatus)
        {
            int num = 1;
            ServiceOrderStateChangeHis oldOrderHis = GetMaxNumberOrderHis(oldOrder);
            if (oldOrderHis != null)
            {
                num = oldOrderHis.Number + 1;
            }
            ServiceOrderStateChangeHis orderHis = new ServiceOrderStateChangeHis(oldOrder, newStatus, num);
            dalServiceOrderStateChangeHis.SaveOrUpdate(orderHis);
        }

        public ServiceOrderStateChangeHis GetOrderHis(ServiceOrder order)
        {
            return dalServiceOrderStateChangeHis.GetOrderHis(order);
        }

        public ServiceOrderStateChangeHis GetMaxNumberOrderHis(ServiceOrder order)
        {
            return dalServiceOrderStateChangeHis.GetMaxNumberOrderHis(order);
        }

        public IList<ServiceOrderStateChangeHis> GetOrderHisList(ServiceOrder order)
        {
            return dalServiceOrderStateChangeHis.GetOrderHisList(order);
        }
        public DateTime GetChangeTime(ServiceOrder order, enum_OrderStatus status)
        {
            return dalServiceOrderStateChangeHis.GetChangeTime(order, status);
        }
    }
}
