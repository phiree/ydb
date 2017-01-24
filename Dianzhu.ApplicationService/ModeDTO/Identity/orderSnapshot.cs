using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using AutoMapper;
using Ydb.Membership.Application;
using Dianzhu.ApplicationService.Order;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
namespace Dianzhu.ApplicationService
{
    public class orderSnapshot
    {
        orderObj _orderobj = new orderObj();
        /// <summary>
        /// 订单信息
        /// </summary>
        public orderObj orderobj
        {
            get
            {
                return _orderobj;
            }
            set
            {
                _orderobj = value;
            }
        }

        workTimeObj _worktimeobj = new workTimeObj();
        /// <summary>
        /// 工作时间
        /// </summary>
        public workTimeObj worktimeobj
        {
            get
            {
                return _worktimeobj;
            }
            set
            {
                _worktimeobj = value;
            }
        }


        public IList<orderSnapshot> Adap(DateTime date,IList<ServiceOrder> orderList, IServiceOrderStateChangeHisService bllstatehis,IOrderService orderService,IDZServiceService dzServiceService)
        {
            IList<orderSnapshot> ordersnapshots = new List<orderSnapshot>();
            foreach (ServiceOrder order in orderList)
            {
                orderSnapshot ordersnapshot = new orderSnapshot();
                orderObj orderobj = Mapper.Map< ServiceOrder, orderObj>(order);
                Order.OrderService.bllstatehis = bllstatehis;
                orderService.changeObj(orderobj, order);
                //todo:refactor: Automapping refactor.
                DZService service = dzServiceService.GetOne2(new Guid( order.ServiceId));
                string errMsg;
                DateTime targetTime = order.Details[0].TargetTime;
              ServiceOpenTimeForDay forday = service.GetWorkTime(targetTime);
                workTimeObj worktimeobj = new workTimeObj();
                worktimeobj.id = forday.Id.ToString();
                worktimeobj.startTime =forday.TimePeriod.StartTime.ToString();//.ToString();
                worktimeobj.endTime = forday.TimePeriod.EndTime.ToString();//.ToString();
                worktimeobj.week = ((int)targetTime.DayOfWeek).ToString();
                worktimeobj.bOpen = true;
                worktimeobj.maxCountOrder = forday.MaxOrderForOpenTime.ToString();
                ordersnapshot.orderobj = orderobj;
                ordersnapshot.worktimeobj = worktimeobj;
                ordersnapshots.Add(ordersnapshot);
            }
            return ordersnapshots;
        }

    }
}
