using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;

using Dianzhu.DAL;
using Dianzhu.IDAL;
using Ydb.Common;
using Dianzhu.Pay;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 订单状态历史记录
    /// </summary>
    public class BLLServiceOrderStateChangeHis
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLLServiceOrder");
        
        //20160622_longphui_modify
        //DALServiceOrderStateChangeHis dalServiceOrderStateChangeHis = null;
        IDALServiceOrderStateChangeHis dalServiceOrderStateChangeHis;
        public BLLServiceOrderStateChangeHis(IDALServiceOrderStateChangeHis dalServiceOrderStateChangeHis)
        {
            this.dalServiceOrderStateChangeHis = dalServiceOrderStateChangeHis;
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
            dalServiceOrderStateChangeHis.Add(orderHis);
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

        public enum_OrderStatus GetOrderStatusPrevious(ServiceOrder order, enum_OrderStatus status)
        {
            return dalServiceOrderStateChangeHis.GetOrderStatusPrevious(order, status);
        }
    }
}
