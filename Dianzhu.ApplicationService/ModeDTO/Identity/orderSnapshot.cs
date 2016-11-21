using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using AutoMapper;
using Ydb.Membership.Application;
using Dianzhu.ApplicationService.Order;
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


        public IList<orderSnapshot> Adap(DateTime date,IList<ServiceOrder> orderList, BLL.BLLServiceOrderStateChangeHis bllstatehis,IOrderService orderService)
        {
            IList<orderSnapshot> ordersnapshots = new List<orderSnapshot>();
            foreach (ServiceOrder order in orderList)
            {
                orderSnapshot ordersnapshot = new orderSnapshot();
                orderObj orderobj = Mapper.Map<Model.ServiceOrder, orderObj>(order);
                Order.OrderService.bllstatehis = bllstatehis;
                orderService.changeObj(orderobj, order);
                //todo:refactor: Automapping refactor.
                
                ServiceOpenTimeForDaySnapShotForOrder forday = order.Service.GetOpenTimeSnapShot(order.Details[0].TargetTime);
                workTimeObj worktimeobj = new workTimeObj();
                worktimeobj.id = forday.Id.ToString();
                worktimeobj.startTime = PHSuit.StringHelper.ConvertPeriodToTimeString(forday.PeriodBegin);//.ToString();
                worktimeobj.endTime = PHSuit.StringHelper.ConvertPeriodToTimeString(forday.PeriodEnd);//.ToString();
                worktimeobj.week = ((int)forday.Date.DayOfWeek).ToString();
                worktimeobj.bOpen = true;
                worktimeobj.maxCountOrder = forday.MaxOrder.ToString();
                ordersnapshot.orderobj = orderobj;
                ordersnapshot.worktimeobj = worktimeobj;
                ordersnapshots.Add(ordersnapshot);
            }
            return ordersnapshots;
        }

    }
}
